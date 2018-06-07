using System;
using System.Collections.Generic;
using System.Linq;
using ESFA.DC.ILR.FundingService.ALB.Contexts.Interface;
using ESFA.DC.ILR.FundingService.Dto;
using ESFA.DC.ILR.FundingService.Dto.Interfaces;
using ESFA.DC.ILR.Model;
using ESFA.DC.ILR.Model.Interface;
using ESFA.DC.IO.Dictionary;
using ESFA.DC.IO.Interfaces;
using ESFA.DC.JobContext;
using ESFA.DC.JobContext.Interface;
using ESFA.DC.Serialization.Interfaces;
using ESFA.DC.Serialization.Json;
using FluentAssertions;
using Xunit;

namespace ESFA.DC.ILR.FundingService.ALB.Contexts.Tests
{
    public class FundingContextManagerTests
    {
        /// <summary>
        /// Return FundingContextManager
        /// </summary>
        [Fact(DisplayName = "FundingContextManager - Instance Exists"), Trait("Funding Context Manager", "Unit")]
        public void FundingContextManager_InstanceExist()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var fundingContextManager = new FundingContextManager(JobContextMessage, FundingServiceDto);

            // ASSERT
            fundingContextManager.Should().NotBeNull();
        }

        /// <summary>
        /// Return FundingContextManager
        /// </summary>
        [Fact(DisplayName = "FundingContextManager - MapUKPRN - Exists"), Trait("Funding Context Manager", "Unit")]
        public void FundingContextMapper_MapUKPRN_Exist()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var ukprn = TestFundingContextManager.MapUKPRN();

            // ASSERT
            ukprn.Should().NotBe(null);
        }

        /// <summary>
        /// Return FundingContextManager
        /// </summary>
        [Fact(DisplayName = "FundingContextManager - MapUKPRN - Correct"), Trait("Funding Context Manager", "Unit")]
        public void FundingContextMapper_MapUKPRN_Correct()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var ukprn = TestFundingContextManager.MapUKPRN();

            // ASSERT
            ukprn.Should().Be(10006341);
        }

        /// <summary>
        /// Return FundingContextManager
        /// </summary>
        [Fact(DisplayName = "FundingContextManager - MapUKPRN - CorrectType"), Trait("Funding Context Manager", "Unit")]
        public void FundingContextMapper_MapUKPRN_CorrectType()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var ukprn = TestFundingContextManager.MapUKPRN();

            // ASSERT
            ukprn.Should().NotBeOfType(typeof(string));
        }

        /// <summary>
        /// Return FundingContextManager
        /// </summary>
        [Fact(DisplayName = "FundingContextManager - MapValidLearners - Exists"), Trait("Funding Context Manager", "Unit")]
        public void FundingContextMapper_MapValidLearners_Exist()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var learners = TestFundingContextManager.MapValidLearners();

            // ASSERT
            learners.Should().NotBeNullOrEmpty();
        }

        /// <summary>
        /// Return FundingContextManager
        /// </summary>
        [Fact(DisplayName = "FundingContextManager - MapValidLearners - Correct"), Trait("Funding Context Manager", "Unit")]
        public void FundingContextMapper_MapValidLearners_Correct()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var learners = TestFundingContextManager.MapValidLearners();

            // ASSERT
            learners.Should().BeEquivalentTo(TestLearners());
        }

        /// <summary>
        /// Return FundingContextManager
        /// </summary>
        [Fact(DisplayName = "FundingContextManager - MapValidLearners - Correct Count"), Trait("Funding Context Manager", "Unit")]
        public void FundingContextMapper_MapValidLearners_CorrectCount()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var learners = TestFundingContextManager.MapValidLearners();

            // ASSERT
            learners.Count.Should().Be(2);
        }

        /// <summary>
        /// Return FundingContextManager
        /// </summary>
        [Fact(DisplayName = "FundingContextManager - Mapto - Exists"), Trait("Funding Context Manager", "Unit")]
        public void FundingContextMapper_Mapto_Exist()
        {
            // ARRANGE
            var fundingContextManager = new FundingContextManager(JobContextMessage, FundingServiceDto);

            // ACT
            var mapTo = fundingContextManager.MapTo(JobContextMessage);

            // ASSERT
            mapTo.Should().NotBeNullOrEmpty();
        }

        /// <summary>
        /// Return FundingContextManager
        /// </summary>
        [Fact(DisplayName = "FundingContextManager - Mapto - Correct"), Trait("Funding Context Manager", "Unit")]
        public void FundingContextMapper_Mapto_Correct()
        {
            // ARRANGE
            var fundingContextManager = new FundingContextManager(JobContextMessage, FundingServiceDto);

            // ACT
            var mapTo = fundingContextManager.MapTo(JobContextMessage);

            // ASSERT
            mapTo.Should().BeEquivalentTo(TestLearners());
        }

        /// <summary>
        /// Return FundingContextManager
        /// </summary>
        [Fact(DisplayName = "FundingContextManager - MapTo - Correct Count"), Trait("Funding Context Manager", "Unit")]
        public void FundingContextMapper_Mapto_CorrectCount()
        {
            // ARRANGE
            var fundingContextManager = new FundingContextManager(JobContextMessage, FundingServiceDto);

            // ACT
            var mapTo = fundingContextManager.MapTo(JobContextMessage);

            // ASSERT
            mapTo.Count.Should().Be(2);
        }

        /// <summary>
        /// Return FundingContextManager
        /// </summary>
        [Fact(DisplayName = "FundingContextManager - MapFrom - NotImpelemented"), Trait("Funding Context Manager", "Unit")]
        public void FundingContextMapper_MapFrom_Correct()
        {
            // ARRANGE
            var fundingContextManager = new FundingContextManager(JobContextMessage, FundingServiceDto);

            // ACT
            Action mapFrom = () => fundingContextManager.MapFrom(TestLearners());

            // ASSERT
            mapFrom.Should().Throw<NotImplementedException>();
        }

        #region KeyValuePersistanceService Tests

        /// <summary>
        /// Return Valid Learners from KeyValuePersistanceService
        /// </summary>
        [Fact(DisplayName = "KeyValuePersistanceService - Learners Exist"), Trait("Funding Service", "Unit")]
        public void KeyValuePersistanceService_Learners_Exist()
        {
            // ARRANGE
            var testLearnerString = SerializationService.Serialize(TestLearners());

            // ACT
            var learners = KeyValuePersistenceService.GetAsync("ValidLearnRefNumbers").Result;

            // ASSERT
            learners.Should().NotBeNullOrEmpty();
        }

        /// <summary>
        /// Return Valid Learners from KeyValuePersistanceService
        /// </summary>
        [Fact(DisplayName = "KeyValuePersistanceService - Learners Correct"), Trait("Funding Service", "Unit")]
        public void KeyValuePersistanceService_Learners_Correct()
        {
            // ARRANGE
            var testLearnerString = SerializationService.Serialize(TestLearners());

            // ACT
            var learners = KeyValuePersistenceService.GetAsync("ValidLearnRefNumbers").Result;

            // ASSERT
            testLearnerString.Should().BeEquivalentTo(learners);
        }

        #endregion

        #region Test Helpers

        private IFundingContextManager TestFundingContextManager = new FundingContextManager(JobContextMessage, FundingServiceDto);

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

        private static IKeyValuePersistenceService KeyValuePersistenceService => BuildKeyValueDictionary();

        private static IFundingServiceDto FundingServiceDto => BuildFundingServiceDto();

        private static IFundingServiceDto BuildFundingServiceDto()
        {
            return new FundingServiceDto()
            {
                Message = new Message()
                {
                    Learner = TestLearners().Cast<MessageLearner>().ToArray()
                },
                ValidLearners = new[]
                {
                    "Learner1", "Learner2"
                }
            };
        }

        private static ISerializationService SerializationService => new JsonSerializationService();

        private static DictionaryKeyValuePersistenceService BuildKeyValueDictionary()
        {
            var list = new DictionaryKeyValuePersistenceService();
            var serializer = new JsonSerializationService();

            list.SaveAsync("ValidLearnRefNumbers", serializer.Serialize(TestLearners())).Wait();

            return list;
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

        #endregion
    }
}
