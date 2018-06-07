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
    public class FundingContextTests
    {
        /// <summary>
        /// Return FundingContext
        /// </summary>
        [Fact(DisplayName = "FundingContext - Instance Exists"), Trait("Funding Context", "Unit")]
        public void FundingContext_Exists()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var fundingContext = new FundingContext(TestFundingContextManager);

            // ASSERT
            fundingContext.Should().NotBeNull();
        }

        /// <summary>
        /// Return FundingContext
        /// </summary>
        [Fact(DisplayName = "FundingContext - UKPRN Exists"), Trait("Funding Context", "Unit")]
        public void FundingContext_UKPRN_Exists()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var ukprn = fundngContext.UKPRN;

            // ASSERT
            ukprn.Should().NotBe(null);
        }

        /// <summary>
        /// Return FundingContext
        /// </summary>
        [Fact(DisplayName = "FundingContext - UKPRN - Correct"), Trait("Funding Context Manager", "Unit")]
        public void FundingContextMapper_UKPRN_Correct()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var ukprn = fundngContext.UKPRN;

            // ASSERT
            ukprn.Should().Be(10006341);
        }

        /// <summary>
        /// Return FundingContext
        /// </summary>
        [Fact(DisplayName = "FundingContext - UKPRN - CorrectType"), Trait("Funding Context Manager", "Unit")]
        public void FundingContextMapper_UKPRN_CorrectType()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var ukprn = fundngContext.UKPRN;

            // ASSERT
            ukprn.Should().BeOfType(typeof(int));
            ukprn.Should().NotBeOfType(typeof(string));
        }

        /// <summary>
        /// Return FundingContext
        /// </summary>
        [Fact(DisplayName = "FundingContext - ValidLearners Exists"), Trait("Funding Context", "Unit")]
        public void FundingContext_ValidLearners_Exists()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var validLearners = fundngContext.ValidLearners;

            // ASSERT
            validLearners.Should().NotBeNull();
        }

        /// <summary>
        /// Return FundingContext
        /// </summary>
        [Fact(DisplayName = "FundingContext - ValidLearners Count Correct"), Trait("Funding Context", "Unit")]
        public void FundingContext_ValidLearners_CountCorrect()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var validLearners = fundngContext.ValidLearners;

            // ASSERT
            validLearners.Count.Should().Be(2);
        }

        /// <summary>
        /// Return FundingContext
        /// </summary>
        [Fact(DisplayName = "FundingContext - ValidLearners Correct"), Trait("Funding Context", "Unit")]
        public void FundingContext_ValidLearners_Correct()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var validLearners = fundngContext.ValidLearners;

            // ASSERT
            validLearners.Should().BeEquivalentTo(TestLearners());
        }

        #region Test Helpers

        private static readonly IFundingContextManager TestFundingContextManager = new FundingContextManager(JobContextMessage, FundingServiceDto);

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

        private static ISerializationService SerializationService => new JsonSerializationService();

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

        private static DictionaryKeyValuePersistenceService BuildKeyValueDictionary()
        {
            var list = new DictionaryKeyValuePersistenceService();

            IList<ILearner> learners = TestLearners();
            list.SaveAsync("ValidLearnRefNumbers", SerializationService.Serialize(learners)).Wait();

            return list;
        }

        private IFundingContext fundngContext = new FundingContext(TestFundingContextManager);

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
