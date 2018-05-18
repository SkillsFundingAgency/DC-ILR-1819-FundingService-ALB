using System;
using System.Collections.Generic;
using System.Linq;
using ESFA.DC.ILR.FundingService.ALB.ExternalData.Interface;
using ESFA.DC.ILR.FundingService.ALB.ExternalData.LARS.Model;
using ESFA.DC.ILR.FundingService.ALB.ExternalData.Postcodes.Model;
using FluentAssertions;
using Xunit;

namespace ESFA.DC.ILR.FundingService.ALB.ExternalData.Tests
{
    public class ReferenceDataCacheTests
    {
        /// <summary>
        /// Return Data from Reference Data Cache
        /// </summary>
        [Fact(DisplayName = "LARSFunding - Does exist"), Trait("LARS", "Unit")]
        public void LARSFunding_Exists()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var referenceDataCache = SetupReferenceDataCache();

            // ASSERT
            referenceDataCache.LARSFunding.Should().NotBeNull();
        }

        /// <summary>
        /// Return Data from Reference Data Cache
        /// </summary>
        [Fact(DisplayName = "LARSFunding - Value is Correct"), Trait("LARS", "Unit")]
        public void LARSFunding_Correct()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var referenceDataCache = SetupReferenceDataCache();

            // ASSERT
            referenceDataCache.LARSFunding.Values.Single().Should().BeEquivalentTo(LarsFundingTestValue);
        }

        /// <summary>
        /// Return Data from Reference Data Cache
        /// </summary>
        [Fact(DisplayName = "LARSLearningDelivery - Does exist"), Trait("LARS", "Unit")]
        public void LARSLearningDelivery_Exists()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var referenceDataCache = SetupReferenceDataCache();

            // ASSERT
            referenceDataCache.LARSLearningDelivery.Should().NotBeNull();
        }

        /// <summary>
        /// Return Data from Reference Data Cache
        /// </summary>
        [Fact(DisplayName = "LARSLearningDelivery - Value is Correct"), Trait("LARS", "Unit")]
        public void LARSLearningDelivery_Correct()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var referenceDataCache = SetupReferenceDataCache();

            // ASSERT
            referenceDataCache.LARSLearningDelivery.Values.Single().Should().BeEquivalentTo(LarsLearningDeliveryTestValue);
        }

        /// <summary>
        /// Return Data from Reference Data Cache
        /// </summary>
        [Fact(DisplayName = "LARSCurrentVersion - Does exist"), Trait("LARS", "Unit")]
        public void LARSCurrentVersion_Exists()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var referenceDataCache = SetupReferenceDataCache();

            // ASSERT
            referenceDataCache.LARSCurrentVersion.Should().NotBeNull();
        }

        /// <summary>
        /// Return Data from Reference Data Cache
        /// </summary>
        [Fact(DisplayName = "LARSCurrentVersion - Value is Correct"), Trait("LARS", "Unit")]
        public void LARSCurrentVersion_Correct()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var referenceDataCache = SetupReferenceDataCache();

            // ASSERT
            referenceDataCache.LARSCurrentVersion.Should().BeEquivalentTo(LARSCurrentVersion);
        }

        /// <summary>
        /// Return Data from Reference Data Cache
        /// </summary>
        [Fact(DisplayName = "PostcodesCurrentVersion - Does exist"), Trait("LARS", "Unit")]
        public void PostcodesCurrentVersion_Exists()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var referenceDataCache = SetupReferenceDataCache();

            // ASSERT
            referenceDataCache.PostcodeCurrentVersion.Should().NotBeNull();
        }

        /// <summary>
        /// Return Data from Reference Data Cache
        /// </summary>
        [Fact(DisplayName = "PostcodesCurrentVersion - Value is Correct"), Trait("LARS", "Unit")]
        public void PostcodesCurrentVersion_Correct()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var referenceDataCache = SetupReferenceDataCache();

            // ASSERT
            referenceDataCache.PostcodeCurrentVersion.Should().BeEquivalentTo(PostcodesCurrentVersion);
        }

        /// <summary>
        /// Return Data from Reference Data Cache
        /// </summary>
        [Fact(DisplayName = "SfaAreaCost - Does exist"), Trait("LARS", "Unit")]
        public void SfaAreaCost_Exists()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var referenceDataCache = SetupReferenceDataCache();

            // ASSERT
            referenceDataCache.SfaAreaCost.Should().NotBeNull();
        }

        /// <summary>
        /// Return Data from Reference Data Cache
        /// </summary>
        [Fact(DisplayName = "SfaAreaCost - Value is Correct"), Trait("LARS", "Unit")]
        public void SfaAreaCost_Correct()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var referenceDataCache = SetupReferenceDataCache();

            // ASSERT
            referenceDataCache.SfaAreaCost.Values.Single().Should().BeEquivalentTo(SfaAreaCostTestValue);
        }

        #region Test Helpers

        private IReferenceDataCache SetupReferenceDataCache()
        {
            IReferenceDataCache referenceDataCache;
            ReferenceDataCache referenceData = new ReferenceDataCache
            {
                LARSFunding = new Dictionary<string, IEnumerable<LARSFunding>>
                {
                    { "123456",  LARSFundingList(LarsFundingTestValue) }
                },
                LARSCurrentVersion = LARSCurrentVersion,
                LARSLearningDelivery = new Dictionary<string, LARSLearningDelivery>
                {
                    { "123456", LarsLearningDeliveryTestValue }
                },
                PostcodeCurrentVersion = PostcodesCurrentVersion,
                SfaAreaCost = new Dictionary<string, IEnumerable<SfaAreaCost>>
                {
                    { "CV1 2WT", SFAAreaCostList(SfaAreaCostTestValue) }
                }
            };

            referenceDataCache = referenceData;

            return referenceDataCache;
        }

        private static readonly string LARSCurrentVersion = "Version_005";
        private static readonly string PostcodesCurrentVersion = "Version_002";

        private IList<LARSFunding> LARSFundingList(LARSFunding larsFundingData)
        {
            return new List<LARSFunding>
            {
                larsFundingData
            };
        }

        private static readonly LARSFunding LarsFundingTestValue =
           new LARSFunding()
           {
               EffectiveFrom = DateTime.Parse("2000-01-01"),
               EffectiveTo = null,
               FundingCategory = "Matrix",
               LearnAimRef = "123456",
               RateWeighted = 1.5m,
               WeightingFactor = "W-Factor"
           };

        private static readonly LARSLearningDelivery LarsLearningDeliveryTestValue =
             new LARSLearningDelivery()
             {
                 LearnAimRef = "123456",
                 LearnAimRefType = "006",
                 NotionalNVQLevelv2 = "2",
                 RegulatedCreditValue = 180
             };

        private IList<SfaAreaCost> SFAAreaCostList(SfaAreaCost sfaAreaCostData)
        {
            return new List<SfaAreaCost>
            {
                sfaAreaCostData
            };
        }

        private static readonly SfaAreaCost SfaAreaCostTestValue =
           new SfaAreaCost()
           {
               Postcode = "CV1 2WT",
               AreaCostFactor = 1.2m,
               EffectiveFrom = DateTime.Parse("2000-01-01"),
               EffectiveTo = null
           };

        #endregion
    }
}
