using System;
using System.Collections.Generic;
using ESFA.DC.ILR.FundingService.ALB.ExternalData.Interface;
using ESFA.DC.ILR.FundingService.ALB.ExternalData.LARS;
using ESFA.DC.ILR.FundingService.ALB.ExternalData.LARS.Interface;
using ESFA.DC.ILR.FundingService.ALB.ExternalData.LARS.Model;
using FluentAssertions;
using Moq;
using Xunit;

namespace ESFA.DC.ILR.FundingService.ALB.ExternalData.Tests.LARS
{
    public class LARSReferenceDataServiceTests
    {
        /// <summary>
        /// Return LARS Version
        /// </summary>
        [Fact(DisplayName = "LARSVersion - Does exist"), Trait("LARS", "Unit")]
        public void LARSCurrentVersion_Exists()
        {
            // ARRANGE
            var larsCurrentVersionValue = LarsCurrentVersionTestValue;
            var larsServiceMock = LARSCurrentVersionTestRun(larsCurrentVersionValue);

            // ACT
            var larsCurrentVersionExists = larsServiceMock.LARSCurrentVersion();

            // ASSERT
            larsCurrentVersionExists.Should().NotBeNull();
        }

        /// <summary>
        /// Return LARS Version and check value
        /// </summary>
        [Fact(DisplayName = "LARSVersion - Correct values"), Trait("LARS", "Unit")]
        public void LARSCurrentVersion_Correct()
        {
            // ARRANGE
            var larsCurrentVersionValue = LarsCurrentVersionTestValue;
            var larsServiceMock = LARSCurrentVersionTestRun(larsCurrentVersionValue);

            // ACT
            var larsCurrentVersionCorrect = larsServiceMock.LARSCurrentVersion();

            // ASSERT
            larsCurrentVersionCorrect.Should().BeEquivalentTo(LarsCurrentVersionTestValue);
        }

        /// <summary>
        /// Return LARS Version and check value
        /// </summary>
        [Fact(DisplayName = "LARSVersion - Incorrect values"), Trait("LARS", "Unit")]
        public void LARSCurrentVersion_NotCorrect()
        {
            // ARRANGE
            var larsCurrentVersionValue = "Version_002";
            var larsServiceMock = LARSCurrentVersionTestRun(larsCurrentVersionValue);

            // ACT
            var larsCurrentVersionNotCorrect = larsServiceMock.LARSCurrentVersion();

            // ASSERT
            larsCurrentVersionNotCorrect.Should().NotBeSameAs(LarsCurrentVersionTestValue);
        }

        /// <summary>
        /// Return LARS LearningDelivery
        /// </summary>
        [Fact(DisplayName = "LARSLearningDelivery - Does exist"), Trait("LARS", "Unit")]
        public void LARSLearningDelivery_Exists()
        {
            // ARRANGE
            var learnAimRef = LearnAimRefTestValue;
            var larsServiceMock = LARSLearningDeliveryTestRun();

            // ACT
            var larsLearningDeliveryCorrect = larsServiceMock.LARSLearningDeliveriesForLearnAimRef(learnAimRef);

            // ASSERT
            larsLearningDeliveryCorrect.Should().NotBeNull();
        }

        /// <summary>
        /// Return LARS LearningDelivery
        /// </summary>
        [Fact(DisplayName = "LARSLearningDelivery - Does not exist"), Trait("LARS", "Unit")]
        public void LARSLearningDelivery_NotExist()
        {
            // ARRANGE
            var larsLearningDeliveryNotExistsAimRef = "456";
            var larsServiceMock = LARSLearningDeliveryTestRun();

            // ACT
            Action learningDeliveryNotExist = () => { larsServiceMock.LARSLearningDeliveriesForLearnAimRef(larsLearningDeliveryNotExistsAimRef); };

            // ASSERT
            learningDeliveryNotExist.Should().Throw<KeyNotFoundException>();
        }

        /// <summary>
        /// Return LARS LearningDelivery and check value
        /// </summary>
        [Fact(DisplayName = "LARSLearningDelivery - Correct values"), Trait("LARS", "Unit")]
        public void LARSLearningDelivery_Correct()
        {
            // ARRANGE
            var larsServiceMock = LARSLearningDeliveryTestRun();

            // ACT
            var larsLearningDeliveryCorrect = larsServiceMock.LARSLearningDeliveriesForLearnAimRef(LearnAimRefTestValue);

            // ASSERT
            larsLearningDeliveryCorrect.Should().BeEquivalentTo(LarsLearningDeliveryTestValue);
        }

        /// <summary>
        /// Return LARS Funding
        /// </summary>
        [Fact(DisplayName = "LARSFunding - Does exist"), Trait("LARS", "Unit")]
        public void LARSFunding_Exists()
        {
            // ARRANGE
            string larsFundingExistsAimRef = LearnAimRefTestValue;
            List<LARSFunding> larsFundingExistsTestList = new List<LARSFunding>
            {
                LarsFundingTestValue
            };
            var larsServiceMock = LARSFundingTestRun(larsFundingExistsTestList);

            // ACT
            var larsFundingExists = larsServiceMock.LARSFundingsForLearnAimRef(larsFundingExistsAimRef);

            // ASSERT
            larsFundingExists.Should().NotBeNull();
        }

        /// <summary>
        /// Return LARS Funding
        /// </summary>
        [Fact(DisplayName = "LARSFunding - Does not exist"), Trait("LARS", "Unit")]
        public void LARSFunding_NotExists()
        {
            // ARRANGE
            string larsFundingExistsAimRef = "456";
            List<LARSFunding> larsFundingNotExistsTestList = new List<LARSFunding>
            {
                LarsFundingTestValue
            };
            var larsServiceMock = LARSFundingTestRun(larsFundingNotExistsTestList);

            // ACT
            Action larsFundingNotExist = () => { larsServiceMock.LARSFundingsForLearnAimRef(larsFundingExistsAimRef); };

            // ASSERT
            larsFundingNotExist.Should().Throw<KeyNotFoundException>();
        }

        /// <summary>
        /// Return LARS Funding and check values
        /// </summary>
        [Fact(DisplayName = "LARSFunding - Correct values (Single)"), Trait("LARS", "Unit")]
        public void LARSFunding_Correct_Single()
        {
            // ARRANGE
            string larsFundingExistsAimRef = LearnAimRefTestValue;
            List<LARSFunding> larsFundingCorrectSingleTestList = new List<LARSFunding>
            {
                LarsFundingTestValue
            };
            var larsServiceMock = LARSFundingTestRun(larsFundingCorrectSingleTestList);

            // ACT
            var larsFundingCorrectSingle = larsServiceMock.LARSFundingsForLearnAimRef(larsFundingExistsAimRef);

            // ASSERT
            larsFundingCorrectSingle.Should().BeEquivalentTo(LarsFundingTestValue);
        }

        /// <summary>
        /// Return LARS Funding and check values
        /// </summary>
        [Fact(DisplayName = "LARSFunding - Correct values (Many)"), Trait("LARS", "Unit")]
        public void LARSFunding_Correct_Many()
        {
            // ARRANGE
            string larsFundingExistsAimRef = LearnAimRefTestValue;
            List<LARSFunding> larsFundingCorrectManyTestList = new List<LARSFunding>
            {
                LarsFundingTestValue,
                LarsFundingTestValue
            };
            var larsServiceMock = LARSFundingTestRun(larsFundingCorrectManyTestList);

            // ACT
            var larsFundingCorrectMany = larsServiceMock.LARSFundingsForLearnAimRef(larsFundingExistsAimRef);

            // ASSERT
            var expectedListCorrect = new List<LARSFunding>
            {
                LarsFundingTestValue,
                LarsFundingTestValue
            };

            larsFundingCorrectMany.Should().BeEquivalentTo(expectedListCorrect);
        }

        /// <summary>
        /// Return LARS Funding and check values
        /// </summary>
        [Fact(DisplayName = "LARSFunding - Incorrect values (Many)"), Trait("LARS", "Unit")]
        public void LARSFunding_NotCorrect_Many()
        {
            // ARRANGE
            string larsFundingExistsAimRef = LearnAimRefTestValue;
            List<LARSFunding> larsFundingNotCorrectManyTestList = new List<LARSFunding>
            {
                LarsFundingTestValue
            };
            var larsServiceMock = LARSFundingTestRun(larsFundingNotCorrectManyTestList);

            // ACT
            var larsFundingNotCorrectMany = larsServiceMock.LARSFundingsForLearnAimRef(larsFundingExistsAimRef);

            // ASSERT
            var expectedListNotCorrect = new List<LARSFunding>
            {
                LarsFundingTestValue,
                LarsFundingTestValue
            };

            larsFundingNotCorrectMany.Should().NotBeSameAs(expectedListNotCorrect);
        }

        #region Test Helpers

        private ILARSReferenceDataService LARSCurrentVersionTestRun(string larsVersion)
        {
            var larsCurrentVersionMock = referenceDataCacheMock;
            larsCurrentVersionMock.SetupGet(rdc => rdc.LARSCurrentVersion).Returns(larsVersion);

            return MockTestObject(larsCurrentVersionMock.Object);
        }

        private ILARSReferenceDataService LARSLearningDeliveryTestRun()
        {
            var larsLearningDeliveryMock = referenceDataCacheMock;
            larsLearningDeliveryMock.SetupGet(rdc => rdc.LARSLearningDelivery).Returns(new Dictionary<string, LARSLearningDelivery>()
             {
                { LearnAimRefTestValue, LarsLearningDeliveryTestValue }
            });

            return MockTestObject(larsLearningDeliveryMock.Object);
        }

        private ILARSReferenceDataService LARSFundingTestRun(List<LARSFunding> larsFundingList)
        {
            var larsFundingMock = referenceDataCacheMock;
            larsFundingMock.SetupGet(rdc => rdc.LARSFunding).Returns(new Dictionary<string, IEnumerable<LARSFunding>>()
            {
                { LearnAimRefTestValue, larsFundingList }
            });

            return MockTestObject(larsFundingMock.Object);
        }

        private ILARSReferenceDataService MockTestObject(IReferenceDataCache @object)
        {
            ILARSReferenceDataService larsReferenceDataService = new LARSReferenceDataService(@object);

            return larsReferenceDataService;
        }

        private readonly Mock<IReferenceDataCache> referenceDataCacheMock = new Mock<IReferenceDataCache>();

        private static readonly string LarsCurrentVersionTestValue = "Version_005";
        private static readonly string LearnAimRefTestValue = "123456";

        private static readonly LARSLearningDelivery LarsLearningDeliveryTestValue =
             new LARSLearningDelivery()
             {
                 LearnAimRef = "123456",
                 LearnAimRefType = "006",
                 NotionalNVQLevelv2 = "2",
                 RegulatedCreditValue = 180
             };

        private static readonly LARSFunding LarsFundingTestValue =
            new LARSFunding()
            {
                LearnAimRef = "123456",
                FundingCategory = "Matrix",
                RateWeighted = 1.5m,
                WeightingFactor = "W-Factor",
                EffectiveFrom = DateTime.Parse("2000-01-01"),
                EffectiveTo = null
            };

        #endregion
    }
}
