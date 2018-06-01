using System;
using System.Collections.Generic;
using System.Linq;
using ESFA.DC.ILR.FundingService.ALB.InternalData.Interface;
using ESFA.DC.ILR.Model;
using ESFA.DC.ILR.Model.Interface;
using FluentAssertions;
using Xunit;

namespace ESFA.DC.ILR.FundingService.ALB.InternalData.Tests
{
    public class InternalDataCacheTests
    {
        /// <summary>
        /// Return InternalDataCache
        /// </summary>
        [Fact(DisplayName = "InternalDataCache - Instance Exists"), Trait("InternalDataCache", "Unit")]
        public void InternalDataCache_Exists()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            IInternalDataCache internalDataCache = TestInternalDataCache();

            // ASSERT
            internalDataCache.Should().NotBeNull();
        }

        /// <summary>
        /// Return InternalDataCache
        /// </summary>
        [Fact(DisplayName = "InternalDataCache - UKPRN Exists"), Trait("InternalDataCache", "Unit")]
        public void InternalDataCache_UKPRN_Exists()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            IInternalDataCache internalDataCache = TestInternalDataCache();

            // ASSERT
            internalDataCache.UKPRN.Should().NotBe(null);
        }

        /// <summary>
        /// Return InternalDataCache
        /// </summary>
        [Fact(DisplayName = "InternalDataCache - UKPRN Correct"), Trait("InternalDataCache", "Unit")]
        public void InternalDataCache_UKPRN_Correct()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            IInternalDataCache internalDataCache = TestInternalDataCache();

            // ASSERT
            internalDataCache.UKPRN.Should().Be(12345678);
        }

        /// <summary>
        /// Return InternalDataCache
        /// </summary>
        [Fact(DisplayName = "InternalDataCache - ValidLearners Exists"), Trait("InternalDataCache", "Unit")]
        public void InternalDataCache_ValidLearners_Exists()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            IInternalDataCache internalDataCache = TestInternalDataCache();

            // ASSERT
            internalDataCache.ValidLearners.Should().NotBeNull();
        }

        /// <summary>
        /// Return InternalDataCache
        /// </summary>
        [Fact(DisplayName = "InternalDataCache - ValidLearners Correct"), Trait("InternalDataCache", "Unit")]
        public void InternalDataCache_ValidLearners_Correct()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            IInternalDataCache internalDataCache = TestInternalDataCache();

            // ASSERT
            internalDataCache.ValidLearners.Should().BeEquivalentTo(TestLearners());
        }

        /// <summary>
        /// Return InternalDataCache
        /// </summary>
        [Fact(DisplayName = "InternalDataCache - ValidLearners Count"), Trait("InternalDataCache", "Unit")]
        public void InternalDataCache_ValidLearners_Count()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            IInternalDataCache internalDataCache = TestInternalDataCache();

            // ASSERT
            internalDataCache.ValidLearners.Count().Should().Be(2);
        }

        #region Test Helpers

        private IInternalDataCache TestInternalDataCache()
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

        #endregion
    }
}
