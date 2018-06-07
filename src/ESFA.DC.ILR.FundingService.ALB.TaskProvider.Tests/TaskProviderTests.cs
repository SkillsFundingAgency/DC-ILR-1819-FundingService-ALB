using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using ESFA.DC.Data.LARS.Model;
using ESFA.DC.Data.LARS.Model.Interfaces;
using ESFA.DC.Data.Postcodes.Model;
using ESFA.DC.Data.Postcodes.Model.Interfaces;
using ESFA.DC.ILR.FundingService.ALB.Contexts;
using ESFA.DC.ILR.FundingService.ALB.Contexts.Interface;
using ESFA.DC.ILR.FundingService.ALB.ExternalData;
using ESFA.DC.ILR.FundingService.ALB.ExternalData.Interface;
using ESFA.DC.ILR.FundingService.ALB.FundingOutput.Service;
using ESFA.DC.ILR.FundingService.ALB.FundingOutput.Service.Interface;
using ESFA.DC.ILR.FundingService.ALB.InternalData;
using ESFA.DC.ILR.FundingService.ALB.InternalData.Interface;
using ESFA.DC.ILR.FundingService.ALB.OrchestrationService;
using ESFA.DC.ILR.FundingService.ALB.OrchestrationService.Interface;
using ESFA.DC.ILR.FundingService.ALB.Service.Builders;
using ESFA.DC.ILR.FundingService.ALB.Service.Builders.Interface;
using ESFA.DC.ILR.FundingService.ALB.Service.Interface;
using ESFA.DC.ILR.FundingService.ALB.Stubs;
using ESFA.DC.ILR.FundingService.ALB.TaskProvider.Service;
using ESFA.DC.ILR.FundingService.Dto;
using ESFA.DC.ILR.FundingService.Dto.Interfaces;
using ESFA.DC.ILR.Model;
using ESFA.DC.ILR.Model.Interface;
using ESFA.DC.IO.Dictionary;
using ESFA.DC.IO.Interfaces;
using ESFA.DC.JobContext;
using ESFA.DC.JobContext.Interface;
using ESFA.DC.OPA.Model.Interface;
using ESFA.DC.OPA.Service;
using ESFA.DC.OPA.Service.Builders;
using ESFA.DC.OPA.Service.Interface;
using ESFA.DC.OPA.Service.Interface.Builders;
using ESFA.DC.OPA.Service.Interface.Rulebase;
using ESFA.DC.OPA.Service.Rulebase;
using ESFA.DC.Serialization.Interfaces;
using ESFA.DC.Serialization.Json;
using ESFA.DC.TestHelpers.Mocks;
using FluentAssertions;
using Moq;
using Xunit;

namespace ESFA.DC.ILR.FundingService.ALB.TaskProvider.Tests
{
    public class TaskProviderTests
    {
        // TESTS INCOMPLETE.

        /// <summary>
        /// Run TaskProvider
        /// </summary>
        [Fact(DisplayName = "TaskProvider - Exists"), Trait("Funding Service", "Unit")]
        public void TaskProvider_Exists()
        {
            // ARRANGE
            IMessage message = ILRFile(@"Files\ILR-10006341-1819-20180118-023456-02.xml");
            IFundingContext fundingContext = SetupFundingContext(message);
            IKeyValuePersistenceService keyValuePersistenceService = new DictionaryKeyValuePersistenceService();
            IALBOrchestrationService aLlbOrchestrationService = new ALBOrchestrationService(TestFundingService());
            IReferenceDataCache referenceDataCache = new ReferenceDataCache();
            IReferenceDataCachePopulationService referenceDataCachePopulationService = new ReferenceDataCachePopulationService(referenceDataCache, LARSMock().Object, PostcodesMock().Object);

            IPreFundingALBPopulationService preFundingPopulationService = new PreFundingALBPopulationService(referenceDataCachePopulationService, fundingContext, TestInternalDataCache());
            ILearnerPerActorService<ILearner, IList<ILearner>> learnerPerActorService = new LearnerPerActorServiceStub<ILearner, IList<ILearner>>(TestInternalDataCache());

            IPreFundingALBOrchestrationService preFundingALBOrchestrationService = new PreFundingALBOrchestrationService(preFundingPopulationService, learnerPerActorService);
            var taskProvider = new TaskProviderService(keyValuePersistenceService, TestInternalDataCache(), preFundingALBOrchestrationService, aLlbOrchestrationService);

            // ACT

            // ASSERT
            taskProvider.Should().NotBeNull();
        }

        #region Test Helpers

        private IFundingService TestFundingService()
        {
            IMessage message = ILRFile(@"Files\ILR-10006341-1819-20180118-023456-02.xml");
            IFundingContext fundingContext = SetupFundingContext(message);
            ISessionBuilder sessionBuilder = new SessionBuilder();
            IOPADataEntityBuilder opaDataEntityBuilder = new OPADataEntityBuilder(new DateTime(2017, 8, 1));

            IOPAService opaService = new OPAService(sessionBuilder, opaDataEntityBuilder, MockRulebaseProviderFactory());
            IReferenceDataCache referenceDataCache = new ReferenceDataCache();
            IAttributeBuilder<IAttributeData> attributeBuilder = new AttributeBuilder();
            IReferenceDataCachePopulationService referenceDataCachePopulationService = new ReferenceDataCachePopulationService(referenceDataCache, LARSMock().Object, PostcodesMock().Object);
            IDataEntityBuilder dataEntityBuilder = new DataEntityBuilder(referenceDataCache, attributeBuilder);
            IFundingOutputService fundingOutputService = new FundingOutputService();
            IInternalDataCache validALBLearnersCache = new InternalDataCache();

            var fundingService = new ALB.Service.FundingService(dataEntityBuilder, opaService, fundingOutputService);

            var preFundingOrchestrationService = new PreFundingALBPopulationService(referenceDataCachePopulationService, fundingContext, validALBLearnersCache);
            preFundingOrchestrationService.PopulateData();

            return fundingService;
        }

        private Message ILRFile(string filePath)
        {
            Message message;
            Stream stream = new FileStream(filePath, FileMode.Open);

            using (var reader = XmlReader.Create(stream))
            {
                var serializer = new XmlSerializer(typeof(Message));
                message = serializer.Deserialize(reader) as Message;
            }

            stream.Close();

            return message;
        }

        private IFundingContext SetupFundingContext(IMessage message)
        {
            IKeyValuePersistenceService keyValuePersistenceService = BuildKeyValueDictionary(message);
            ISerializationService serializationService = new JsonSerializationService();
            IFundingServiceDto fundingServiceDto = BuildFundingServiceDto(message);

            IFundingContextManager fundingContextManager = new FundingContextManager(JobContextMessage, fundingServiceDto);

            return new FundingContext(fundingContextManager);
        }

        private static IFundingServiceDto BuildFundingServiceDto(IMessage message)
        {
            return new FundingServiceDto()
            {
                Message = message,
                ValidLearners = message.Learners.Select(learner => learner.LearnRefNumber).ToArray()
            };
        }

        private static IJobContextMessage JobContextMessage => new JobContextMessage
        {
            JobId = 1,
            SubmissionDateTimeUtc = DateTime.Parse("2018-08-01").ToUniversalTime(),
            Topics = Topics,
            TopicPointer = 1,
            KeyValuePairs = KeyValuePairsDictionary,
        };

        private static IReadOnlyList<ITopicItem> Topics => new List<TopicItem>();

        private static IDictionary<JobContextMessageKey, object> KeyValuePairsDictionary => new Dictionary<JobContextMessageKey, object>()
            {
                { JobContextMessageKey.Filename, "FileName" },
                { JobContextMessageKey.UkPrn, 10006341 },
                { JobContextMessageKey.ValidLearnRefNumbers, "ValidLearnRefNumbers" },
            };

        private static DictionaryKeyValuePersistenceService BuildKeyValueDictionary(IMessage message)
        {
            var messageNew = (Message)message;

            var learners = messageNew.Learner.ToList();

            // var learnRefNumbers = new List<string> { "16v224" };
            var list = new DictionaryKeyValuePersistenceService();
            var serializer = new JsonSerializationService();

            list.SaveAsync("ValidLearnRefNumbers", serializer.Serialize(learners)).Wait();

            return list;
        }

        private static IInternalDataCache TestInternalDataCache()
        {
            return new InternalDataCache
            {
                UKPRN = 12345678,
                ValidLearners = TestLearners(),
            };
        }

        private static IList<ILearner> TestLearners()
        {
            return new[]
            {
                new MessageLearner
                {
                    LearnRefNumber = "Learner1",
                    LearningDelivery = new[]
                    {
                        new MessageLearnerLearningDelivery
                        {
                            LearnAimRef = "123456",
                            AimSeqNumber = 1,
                            CompStatus = 1,
                            DelLocPostCode = "CV1 2WT",
                            LearnActEndDateSpecified = true,
                            LearnActEndDate = DateTime.Parse("2018-06-30"),
                            LearnStartDate = DateTime.Parse("2017-08-30"),
                            LearnPlanEndDate = DateTime.Parse("2018-07-30"),
                            OrigLearnStartDateSpecified = true,
                            OrigLearnStartDate = DateTime.Parse("2017-08-30"),
                            OtherFundAdjSpecified = false,
                            OutcomeSpecified = false,
                            PriorLearnFundAdjSpecified = false,
                            LearningDeliveryFAM = new[]
                            {
                                new MessageLearnerLearningDeliveryLearningDeliveryFAM
                                {
                                    LearnDelFAMCode = "1",
                                    LearnDelFAMType = "ADL",
                                    LearnDelFAMDateFromSpecified = true,
                                    LearnDelFAMDateFrom = DateTime.Parse("2017-08-30"),
                                    LearnDelFAMDateToSpecified = true,
                                    LearnDelFAMDateTo = DateTime.Parse("2017-10-31")
                                },
                                new MessageLearnerLearningDeliveryLearningDeliveryFAM
                                {
                                    LearnDelFAMCode = "100",
                                    LearnDelFAMType = "SOF",
                                    LearnDelFAMDateFromSpecified = true,
                                    LearnDelFAMDateFrom = DateTime.Parse("2017-10-31"),
                                    LearnDelFAMDateToSpecified = true,
                                    LearnDelFAMDateTo = DateTime.Parse("2017-11-30")
                                },
                                new MessageLearnerLearningDeliveryLearningDeliveryFAM
                                {
                                    LearnDelFAMCode = "1",
                                    LearnDelFAMType = "RES",
                                    LearnDelFAMDateFromSpecified = true,
                                    LearnDelFAMDateFrom = DateTime.Parse("2017-12-01"),
                                    LearnDelFAMDateToSpecified = false
                                }
                            }
                        }
                    }
                },
                new MessageLearner
                {
                    LearnRefNumber = "Learner2",
                    LearningDelivery = new[]
                    {
                        new MessageLearnerLearningDelivery
                        {
                            LearnAimRef = "123456",
                            AimSeqNumber = 1,
                            CompStatus = 1,
                            DelLocPostCode = "CV1 2WT",
                            LearnActEndDateSpecified = true,
                            LearnActEndDate = DateTime.Parse("2018-06-30"),
                            LearnStartDate = DateTime.Parse("2017-08-30"),
                            LearnPlanEndDate = DateTime.Parse("2018-07-30"),
                            OrigLearnStartDateSpecified = true,
                            OrigLearnStartDate = DateTime.Parse("2017-08-30"),
                            OtherFundAdjSpecified = false,
                            OutcomeSpecified = false,
                            PriorLearnFundAdjSpecified = false,
                            LearningDeliveryFAM = new[]
                            {
                                new MessageLearnerLearningDeliveryLearningDeliveryFAM
                                {
                                    LearnDelFAMCode = "1",
                                    LearnDelFAMType = "ADL",
                                    LearnDelFAMDateFromSpecified = true,
                                    LearnDelFAMDateFrom = DateTime.Parse("2017-08-30"),
                                    LearnDelFAMDateToSpecified = true,
                                    LearnDelFAMDateTo = DateTime.Parse("2017-10-31")
                                },
                                new MessageLearnerLearningDeliveryLearningDeliveryFAM
                                {
                                    LearnDelFAMCode = "100",
                                    LearnDelFAMType = "SOF",
                                    LearnDelFAMDateFromSpecified = true,
                                    LearnDelFAMDateFrom = DateTime.Parse("2017-10-31"),
                                    LearnDelFAMDateToSpecified = true,
                                    LearnDelFAMDateTo = DateTime.Parse("2017-11-30")
                                },
                                new MessageLearnerLearningDeliveryLearningDeliveryFAM
                                {
                                    LearnDelFAMCode = "1",
                                    LearnDelFAMType = "RES",
                                    LearnDelFAMDateFromSpecified = true,
                                    LearnDelFAMDateFrom = DateTime.Parse("2017-12-01"),
                                    LearnDelFAMDateToSpecified = false
                                }
                            }
                        }
                    }
                }
            };
        }

        private static readonly Mock<ILARS> larsContextMock = new Mock<ILARS>();
        private static readonly Mock<IPostcodes> postcodesContextMock = new Mock<IPostcodes>();

        private Mock<ILARS> LARSMock()
        {
            var larsVersionMock = MockDBSetHelper.GetQueryableMockDbSet(MockLARSVersionArray());
            var larsLearningDeliveryMock = MockDBSetHelper.GetQueryableMockDbSet(MockLARSLearningDeliveryArray());
            var larsFundingMock = MockDBSetHelper.GetQueryableMockDbSet(MockLARSFundingArray());

            larsContextMock.Setup(x => x.LARS_Version).Returns(larsVersionMock);
            larsContextMock.Setup(x => x.LARS_LearningDelivery).Returns(larsLearningDeliveryMock);
            larsContextMock.Setup(x => x.LARS_Funding).Returns(larsFundingMock);

            return larsContextMock;
        }

        private Mock<IPostcodes> PostcodesMock()
        {
            var postcodesVersionMock = MockDBSetHelper.GetQueryableMockDbSet(MockPostcodesVersionArray());
            var sfaAreaCostMock = MockDBSetHelper.GetQueryableMockDbSet(MockSFAAreaCostArray());

            postcodesContextMock.Setup(x => x.SFA_PostcodeAreaCost).Returns(sfaAreaCostMock);
            postcodesContextMock.Setup(x => x.VersionInfos).Returns(postcodesVersionMock);

            return postcodesContextMock;
        }

        private static LARS_Version[] MockLARSVersionArray()
        {
            return new LARS_Version[]
            {
                larsVersionTestValue,
            };
        }

        readonly static LARS_Version larsVersionTestValue =
            new LARS_Version()
            {
                MajorNumber = 5,
                MinorNumber = 0,
                MaintenanceNumber = 0,
                MainDataSchemaName = "Version_005",
                RefDataSchemaName = "REF_Version_005",
                ActivationDate = DateTime.Parse("2017-07-01"),
                ExpiryDate = null,
                Description = "Fifth Version of LARS",
                Comment = null,
                Created_On = DateTime.Parse("2017-07-01"),
                Created_By = "System",
                Modified_On = DateTime.Parse("2018-07-01"),
                Modified_By = "System"
            };

        private static LARS_LearningDelivery[] MockLARSLearningDeliveryArray()
        {
            return new LARS_LearningDelivery[]
            {
                larsLearningDeliveryTestValue1,
                larsLearningDeliveryTestValue2
            };
        }

        readonly static LARS_LearningDelivery larsLearningDeliveryTestValue1 =
            new LARS_LearningDelivery()
            {
                LearnAimRef = "50094488",
                LearnAimRefTitle = "Test Learning Aim Title 50094488",
                LearnAimRefType = "0006",
                NotionalNVQLevel = "2",
                NotionalNVQLevelv2 = "2",
                CertificationEndDate = DateTime.Parse("2018-01-01"),
                OperationalStartDate = DateTime.Parse("2018-01-01"),
                OperationalEndDate = DateTime.Parse("2018-01-01"),
                RegulatedCreditValue = 180,
                EffectiveFrom = DateTime.Parse("2000-01-01"),
                EffectiveTo = null,
                Created_On = DateTime.Parse("2017-01-01"),
                Created_By = "TestUser",
                Modified_On = DateTime.Parse("2018-01-01"),
                Modified_By = "TestUser"
            };

        readonly static LARS_LearningDelivery larsLearningDeliveryTestValue2 =
           new LARS_LearningDelivery()
           {
               LearnAimRef = "60005415",
               LearnAimRefTitle = "Test Learning Aim Title 60005415",
               LearnAimRefType = "0006",
               NotionalNVQLevel = "4",
               NotionalNVQLevelv2 = "4",
               CertificationEndDate = DateTime.Parse("2018-01-01"),
               OperationalStartDate = DateTime.Parse("2018-01-01"),
               OperationalEndDate = DateTime.Parse("2018-01-01"),
               RegulatedCreditValue = 42,
               EffectiveFrom = DateTime.Parse("2000-01-01"),
               EffectiveTo = null,
               Created_On = DateTime.Parse("2017-01-01"),
               Created_By = "TestUser",
               Modified_On = DateTime.Parse("2018-01-01"),
               Modified_By = "TestUser"
           };

        private static LARS_Funding[] MockLARSFundingArray()
        {
            return new LARS_Funding[]
            {
                larsFundingTestValue1,
                larsFundingTestValue2
            };
        }

        readonly static LARS_Funding larsFundingTestValue1 =
            new LARS_Funding()
            {
                LearnAimRef = "50094488",
                FundingCategory = "Matrix",
                RateWeighted = 11356m,
                RateUnWeighted = null,
                WeightingFactor = "G",
                EffectiveFrom = DateTime.Parse("2000-01-01"),
                EffectiveTo = null,
                Created_On = DateTime.Parse("2017-01-01"),
                Created_By = "TestUser",
                Modified_On = DateTime.Parse("2018-01-01"),
                Modified_By = "TestUser"
            };

        readonly static LARS_Funding larsFundingTestValue2 =
          new LARS_Funding()
          {
              LearnAimRef = "60005415",
              FundingCategory = "Matrix",
              RateWeighted = 2583m,
              RateUnWeighted = null,
              WeightingFactor = "C",
              EffectiveFrom = DateTime.Parse("2000-01-01"),
              EffectiveTo = null,
              Created_On = DateTime.Parse("2017-01-01"),
              Created_By = "TestUser",
              Modified_On = DateTime.Parse("2018-01-01"),
              Modified_By = "TestUser"
          };

        private static VersionInfo[] MockPostcodesVersionArray()
        {
            return new VersionInfo[]
            {
                PostcodesVersionTestValue,
            };
        }

        readonly static VersionInfo PostcodesVersionTestValue =
            new VersionInfo
            {
                VersionNumber = "Version_002",
                DataSource = "Source",
                Comments = "Comments",
                ModifiedAt = DateTime.Parse("2018-01-01"),
                ModifiedBy = "System"
            };

        private static SFA_PostcodeAreaCost[] MockSFAAreaCostArray()
        {
            return new SFA_PostcodeAreaCost[]
            {
                SFAAreaCostTestValue1,
            };
        }

        readonly static SFA_PostcodeAreaCost SFAAreaCostTestValue1 =
          new SFA_PostcodeAreaCost()
          {
              MasterPostcode = new MasterPostcode { Postcode = "CV1 2WT" },
              Postcode = "CV1 2WT",
              AreaCostFactor = 1.2m,
              EffectiveFrom = DateTime.Parse("2000-01-01"),
              EffectiveTo = null,
          };

        private static IRulebaseProvider RulebaseProviderMock()
        {
            return new RulebaseProvider(@"ESFA.DC.ILR.FundingService.ALB.Service.Rulebase.Loans Bursary 17_18.zip");
        }

        private static IRulebaseProviderFactory MockRulebaseProviderFactory()
        {
            var mock = new Mock<IRulebaseProviderFactory>();

            mock.Setup(m => m.Build()).Returns(RulebaseProviderMock());

            return mock.Object;
        }

        #endregion
    }
}
