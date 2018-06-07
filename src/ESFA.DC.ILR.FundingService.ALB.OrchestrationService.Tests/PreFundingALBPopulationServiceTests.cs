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
using ESFA.DC.ILR.FundingService.ALB.ExternalData.LARS.Model;
using ESFA.DC.ILR.FundingService.ALB.ExternalData.Postcodes.Model;
using ESFA.DC.ILR.FundingService.ALB.FundingOutput.Service;
using ESFA.DC.ILR.FundingService.ALB.FundingOutput.Service.Interface;
using ESFA.DC.ILR.FundingService.ALB.InternalData;
using ESFA.DC.ILR.FundingService.ALB.InternalData.Interface;
using ESFA.DC.ILR.FundingService.ALB.Service.Builders;
using ESFA.DC.ILR.FundingService.ALB.Service.Builders.Interface;
using ESFA.DC.ILR.FundingService.Dto;
using ESFA.DC.ILR.FundingService.Dto.Interfaces;
using ESFA.DC.ILR.Model;
using ESFA.DC.ILR.Model.Interface;
using ESFA.DC.IO.Dictionary;
using ESFA.DC.JobContext;
using ESFA.DC.JobContext.Interface;
using ESFA.DC.OPA.Model.Interface;
using ESFA.DC.OPA.Service;
using ESFA.DC.OPA.Service.Builders;
using ESFA.DC.OPA.Service.Interface;
using ESFA.DC.OPA.Service.Interface.Builders;
using ESFA.DC.OPA.Service.Interface.Rulebase;
using ESFA.DC.OPA.Service.Rulebase;
using ESFA.DC.Serialization.Json;
using ESFA.DC.TestHelpers.Mocks;
using FluentAssertions;
using Moq;
using Xunit;

namespace ESFA.DC.ILR.FundingService.ALB.OrchestrationService.Tests
{
    public class PreFundingALBPopulationServiceTests
    {
        /// <summary>
        /// Return PreFundingALBPopulationService
        /// </summary>
        [Fact(DisplayName = "PreFundingOrchestration - Instance Exists"), Trait("PreFundingOrchestration", "Unit")]
        public void PreFundingALBPopulationService_Exists()
        {
            // ARRANGE
            IMessage message = ILRFile(@"Files\ILR-10006341-1819-20180118-023456-02.xml");
            IReferenceDataCache referenceDataCache = new ReferenceDataCache();

            // ACT
            var preFundingALBPopulationService = SetupPreFundingALBPopulationService(message, referenceDataCache);

            // ASSERT
            preFundingALBPopulationService.Should().NotBeNull();
        }

        /// <summary>
        /// Populate reference data cache and check values
        /// </summary>
        [Fact(DisplayName = "PopulateReferenceData - LARS Version Correct"), Trait("Funding Service", "Unit")]
        public void PopulateReferenceData_LARSVersion_Correct()
        {
            // ARRANGE
            IMessage message = ILRFile(@"Files\ILR-10006341-1819-20180118-023456-02.xml");
            IReferenceDataCache referenceDataCache = new ReferenceDataCache();
            var preFundingALBPopulationService = SetupPreFundingALBPopulationService(message, referenceDataCache);

            // ACT
            preFundingALBPopulationService.PopulateData();

            // ASSERT
            referenceDataCache.LARSCurrentVersion.Should().Be("Version_005");
        }

        /// <summary>
        /// Populate reference data cache and check values
        /// </summary>
        [Fact(DisplayName = "PopulateReferenceData - LARS LearningDelivery Correct"), Trait("Funding Service", "Unit")]
        public void PopulateReferenceData_LARSVLearningDelivery_Correct()
        {
            // ARRANGE
            IMessage message = ILRFile(@"Files\ILR-10006341-1819-20180118-023456-02.xml");
            IReferenceDataCache referenceDataCache = new ReferenceDataCache();
            var preFundingALBPopulationService = SetupPreFundingALBPopulationService(message, referenceDataCache);

            // ACT
            preFundingALBPopulationService.PopulateData();

            // ASSERT
            var expectedOutput1 = new LARSLearningDelivery
            {
                LearnAimRef = "50094488",
                LearnAimRefType = "0006",
                NotionalNVQLevelv2 = "2",
                RegulatedCreditValue = 180
            };

            var expectedOutput2 = new LARSLearningDelivery
            {
                LearnAimRef = "60005415",
                LearnAimRefType = "0006",
                NotionalNVQLevelv2 = "4",
                RegulatedCreditValue = 42
            };

            var output1 = referenceDataCache.LARSLearningDelivery.Where(k => k.Key == "50094488").Select(o => o.Value);
            var output2 = referenceDataCache.LARSLearningDelivery.Where(k => k.Key == "60005415").Select(o => o.Value);

            output1.FirstOrDefault().Should().BeEquivalentTo(expectedOutput1);
            output2.FirstOrDefault().Should().BeEquivalentTo(expectedOutput2);
        }

        /// <summary>
        /// Populate reference data cache and check values
        /// </summary>
        [Fact(DisplayName = "PopulateReferenceData - LARS Funding Correct"), Trait("Funding Service", "Unit")]
        public void PopulateReferenceData_LARSFunding_Correct()
        {
            // ARRANGE
            IMessage message = ILRFile(@"Files\ILR-10006341-1819-20180118-023456-02.xml");
            IReferenceDataCache referenceDataCache = new ReferenceDataCache();
            var preFundingALBPopulationService = SetupPreFundingALBPopulationService(message, referenceDataCache);

            // ACT
            preFundingALBPopulationService.PopulateData();

            // ASSERT
            var expectedOutput1 = new LARSFunding
            {
                LearnAimRef = "50094488",
                EffectiveFrom = DateTime.Parse("2000-01-01"),
                EffectiveTo = null,
                FundingCategory = "Matrix",
                WeightingFactor = "G",
                RateWeighted = 11356m
            };

            var expectedOutput2 = new LARSFunding
            {
                LearnAimRef = "60005415",
                EffectiveFrom = DateTime.Parse("2000-01-01"),
                EffectiveTo = null,
                FundingCategory = "Matrix",
                WeightingFactor = "C",
                RateWeighted = 2583m
            };

            var output1 = referenceDataCache.LARSFunding.Where(k => k.Key == "50094488").Select(o => o.Value).SingleOrDefault();
            var output2 = referenceDataCache.LARSFunding.Where(k => k.Key == "60005415").SelectMany(o => o.Value).SingleOrDefault();

            output1.Should().BeEquivalentTo(expectedOutput1);
            output2.Should().BeEquivalentTo(expectedOutput2);
        }

        /// <summary>
        /// Populate reference data cache and check values
        /// </summary>
        [Fact(DisplayName = "PopulateReferenceData - Postcodes Version Correct"), Trait("Funding Service", "Unit")]
        public void PopulateReferenceData_Postcodes_Correct()
        {
            // ARRANGE
            IMessage message = ILRFile(@"Files\ILR-10006341-1819-20180118-023456-02.xml");
            IReferenceDataCache referenceDataCache = new ReferenceDataCache();
            var preFundingALBPopulationService = SetupPreFundingALBPopulationService(message, referenceDataCache);

            // ACT
            preFundingALBPopulationService.PopulateData();

            // ASSERT
            referenceDataCache.PostcodeCurrentVersion.Should().Be("Version_002");
        }

        /// <summary>
        /// Populate reference data cache and check values
        /// </summary>
        [Fact(DisplayName = "PopulateReferenceData - Postcodes SFA AreaCost Correct"), Trait("Funding Service", "Unit")]
        public void PopulateReferenceData_PostcodesSFAAreaCost_Correct()
        {
            // ARRANGE
            IMessage message = ILRFile(@"Files\ILR-10006341-1819-20180118-023456-02.xml");
            IReferenceDataCache referenceDataCache = new ReferenceDataCache();
            var preFundingALBPopulationService = SetupPreFundingALBPopulationService(message, referenceDataCache);

            // ACT
            preFundingALBPopulationService.PopulateData();

            // ASSERT
            var expectedOutput1 = new SfaAreaCost
            {
                Postcode = "CV1 2WT",
                AreaCostFactor = 1.2m,
                EffectiveFrom = DateTime.Parse("2000-01-01"),
                EffectiveTo = null,
            };

            var output = referenceDataCache.SfaAreaCost.Where(k => k.Key == "CV1 2WT").SelectMany(o => o.Value).FirstOrDefault();

            output.Should().BeEquivalentTo(expectedOutput1);
        }

        #region Test Helpers

        #region Test Data

        private static LARS_Version[] MockLARSVersionArray()
        {
            return new LARS_Version[]
            {
                LarsVersionTestValue,
            };
        }

        private static readonly LARS_Version LarsVersionTestValue =
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
                LarsLearningDeliveryTestValue1,
                LarsLearningDeliveryTestValue2
            };
        }

        private static readonly LARS_LearningDelivery LarsLearningDeliveryTestValue1 =
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

        private static readonly LARS_LearningDelivery LarsLearningDeliveryTestValue2 =
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
                LarsFundingTestValue1,
                LarsFundingTestValue2
            };
        }

        private static readonly LARS_Funding LarsFundingTestValue1 =
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

        private static readonly LARS_Funding LarsFundingTestValue2 =
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

        private static readonly VersionInfo PostcodesVersionTestValue =
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

        private static readonly SFA_PostcodeAreaCost SFAAreaCostTestValue1 =
          new SFA_PostcodeAreaCost()
          {
              MasterPostcode = new MasterPostcode { Postcode = "CV1 2WT" },
              Postcode = "CV1 2WT",
              AreaCostFactor = 1.2m,
              EffectiveFrom = DateTime.Parse("2000-01-01"),
              EffectiveTo = null,
          };

        #endregion

        #region Mocks

        private static readonly Mock<ILARS> LarsContextMock = new Mock<ILARS>();
        private static readonly Mock<IPostcodes> PostcodesContextMock = new Mock<IPostcodes>();

        private static IReadOnlyList<ITopicItem> Topics => new List<TopicItem>();

        private static IDictionary<JobContextMessageKey, object> KeyValuePairsDictionary => new Dictionary<JobContextMessageKey, object>()
        {
            { JobContextMessageKey.Filename, "FileName" },
            { JobContextMessageKey.UkPrn, 10006341 },
            { JobContextMessageKey.ValidLearnRefNumbers, "ValidLearnRefNumbers" },
        };

        private static IJobContextMessage JobContextMessage => new JobContextMessage
        {
            JobId = 1,
            SubmissionDateTimeUtc = DateTime.Parse("2018-08-01").ToUniversalTime(),
            Topics = Topics,
            TopicPointer = 1,
            KeyValuePairs = KeyValuePairsDictionary,
        };

        private PreFundingALBPopulationService SetupPreFundingALBPopulationService(IMessage message, IReferenceDataCache referenceDataCache)
        {
            IFundingContext fundingContext = SetupFundingContext(message);

            IAttributeBuilder<IAttributeData> attributeBuilder = new AttributeBuilder();

            var dataEntityBuilder = new DataEntityBuilder(referenceDataCache, attributeBuilder);

            var referenceDataCachePopulationService = new ReferenceDataCachePopulationService(referenceDataCache, LARSMock().Object, PostcodesMock().Object);
            IFundingOutputService fundingOutputService = new FundingOutputService();
            IInternalDataCache validALBLearnersCache = new InternalDataCache();

            var fundingService = new Service.FundingService(dataEntityBuilder, opaService, fundingOutputService);

            return new PreFundingALBPopulationService(referenceDataCachePopulationService, fundingContext, validALBLearnersCache);
        }

        private Mock<ILARS> LARSMock()
        {
            var larsVersionMock = MockDBSetHelper.GetQueryableMockDbSet(MockLARSVersionArray());
            var larsLearningDeliveryMock = MockDBSetHelper.GetQueryableMockDbSet(MockLARSLearningDeliveryArray());
            var larsFundingMock = MockDBSetHelper.GetQueryableMockDbSet(MockLARSFundingArray());

            LarsContextMock.Setup(x => x.LARS_Version).Returns(larsVersionMock);
            LarsContextMock.Setup(x => x.LARS_LearningDelivery).Returns(larsLearningDeliveryMock);
            LarsContextMock.Setup(x => x.LARS_Funding).Returns(larsFundingMock);

            return LarsContextMock;
        }

        private Mock<IPostcodes> PostcodesMock()
        {
            var postcodesVersionMock = MockDBSetHelper.GetQueryableMockDbSet(MockPostcodesVersionArray());
            var sfaAreaCostMock = MockDBSetHelper.GetQueryableMockDbSet(MockSFAAreaCostArray());

            PostcodesContextMock.Setup(x => x.SFA_PostcodeAreaCost).Returns(sfaAreaCostMock);
            PostcodesContextMock.Setup(x => x.VersionInfos).Returns(postcodesVersionMock);

            return PostcodesContextMock;
        }

        private IFundingContext SetupFundingContext(IMessage message)
        {
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

        private static DictionaryKeyValuePersistenceService BuildKeyValueDictionary(IMessage message)
        {
            var learners = message.Learners.ToList();

            var list = new DictionaryKeyValuePersistenceService();
            var serializer = new JsonSerializationService();

            list.SaveAsync("ValidLearnRefNumbers", serializer.Serialize(learners)).Wait();

            return list;
        }

        #endregion

        private static readonly ISessionBuilder _sessionBuilder = new SessionBuilder();
        private static readonly IOPADataEntityBuilder _dataEntityBuilder = new OPADataEntityBuilder(new DateTime(2017, 8, 1));

        private readonly IOPAService opaService =
            new OPAService(_sessionBuilder, _dataEntityBuilder, MockRulebaseProviderFactory());

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

        private int DecimalStrToInt(string value)
        {
            var valueInt = value.Substring(0, value.IndexOf('.', 0));
            return int.Parse(valueInt);
        }

        #endregion
    }
}
