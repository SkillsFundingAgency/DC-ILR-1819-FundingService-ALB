using System;
using System.Collections.Generic;
using ESFA.DC.ILR.FundingService.ALB.ExternalData.Interface;
using ESFA.DC.ILR.FundingService.ALB.ExternalData.Postcodes;
using ESFA.DC.ILR.FundingService.ALB.ExternalData.Postcodes.Interface;
using ESFA.DC.ILR.FundingService.ALB.ExternalData.Postcodes.Model;
using FluentAssertions;
using Moq;
using Xunit;

namespace ESFA.DC.ILR.FundingService.ALB.ExternalData.Tests.Postcodes
{
    public class PostcodeFactorsReferenceDataServiceTests
    {
        /// <summary>
        /// Return PostcodeFactors Version
        /// </summary>
        [Fact(DisplayName ="PostcodeFactorsVersion - Does exist"), Trait("PostcodeFactors", "Unit")]
        public void PostcodeFactorsCurrentVersion_Exists()
        {
            // ARRANGE
            var postcodeFactorsVersionExistsVersion = postcodeFactorsVersionTestValue;
            var postcodeFactorsServiceMock = PostcodeFactorsCurrentVersionTestRun(postcodeFactorsVersionExistsVersion);

            // ACT
            var postcodeFactorsVersionExists = postcodeFactorsServiceMock.PostcodesCurrentVersion();

            // ASSERT
            postcodeFactorsVersionExists.Should().NotBeNull();
        }

        /// <summary>
        /// Return PostcodeFactors Version and check value
        /// </summary>
        [Fact(DisplayName = "PostcodeFactorsVersion - Correct values"), Trait("PostcodeFactors", "Unit")]
        public void PostcodeFactorsCurrentVersion_Correct()
        {
            // ARRANGE
            var postcodeFactorsVersionCorrectVersion = postcodeFactorsVersionTestValue;
            var postcodeFactorsServiceMock = PostcodeFactorsCurrentVersionTestRun(postcodeFactorsVersionCorrectVersion);

            // ACT
            var postcodeFactorsVersionCorrect = postcodeFactorsServiceMock.PostcodesCurrentVersion();

            // ASSERT
            postcodeFactorsVersionCorrect.Should().BeEquivalentTo(postcodeFactorsVersionTestValue);
        }

        /// <summary>
        /// Return PostcodeFactors Version and check value
        /// </summary>
        [Fact(DisplayName = "PostcodeFactorsVersion - Incorrect values"), Trait("PostcodeFactors", "Unit")]
        public void PostcodeFactorsCurrentVersion_NotCorrect()
        {
            // ARRANGE
            var postcodeFactorsVersionNotCorrectVersion = "Version_001";
            var postcodeFactorsServiceMock = PostcodeFactorsCurrentVersionTestRun(postcodeFactorsVersionNotCorrectVersion);

            // ACT
            var postcodeFactorsVersionNotCorrect = postcodeFactorsServiceMock.PostcodesCurrentVersion();

            // ASSERT
            postcodeFactorsVersionNotCorrect.Should().NotBeSameAs(postcodeFactorsVersionTestValue);
        }

        /// <summary>
        /// Return list of PostcodeFactors SFA Area Cost Entries
        /// </summary>
        [Fact(DisplayName = "SFA AreaCost - Does exist"), Trait("PostcodeFactors", "Unit")]
        public void SFA_AreaCost_Exists()
        {
            // ARRANGE
            string sfaAreaCostExistsPostcode = postcodeTestValue;
            IList<SfaAreaCost> sfaAreaCostExistsList = new List<SfaAreaCost>()
            {
                sfaAreaCostTestValue
            };
            var postcodeFactorsServiceMock = PostcodesSFAAreaCostTestRun(sfaAreaCostExistsList);

            // ACT
            var sfaAreaCostExists = postcodeFactorsServiceMock.SFAAreaCostsForPostcode(sfaAreaCostExistsPostcode);

            // ASSERT
            sfaAreaCostExists.Should().NotBeNull();
        }

        /// <summary>
        /// Return list of PostcodeFactors SFA Area Cost Entries
        /// </summary>
        [Fact(DisplayName = "SFA AreaCost - Does not exist"), Trait("PostcodeFactors", "Unit")]
        public void SFA_AreaCost_NotExists()
        {
            // ARRANGE
            string sfaAreaCostNotExistsPostcode = "NW1 1AB";
            IList<SfaAreaCost> sfaAreaCostNotExistsList = new List<SfaAreaCost>()
            {
                sfaAreaCostTestValue
            };
            var postcodeFactorsServiceMock = PostcodesSFAAreaCostTestRun(sfaAreaCostNotExistsList);

            // ACT
            Action sfaAreaCostNotExists = () => { postcodeFactorsServiceMock.SFAAreaCostsForPostcode(sfaAreaCostNotExistsPostcode); };

            // ASSERT
            sfaAreaCostNotExists.Should().Throw<KeyNotFoundException>();
        }

        /// <summary>
        /// Return list of PostcodeFactors SFA Area Cost Entries and check value
        /// </summary>
        [Fact(DisplayName = "SFA AreaCost - Correct values (Single)"), Trait("PostcodeFactors", "Unit")]
        public void SFA_AreaCost_Correct_Single()
        {
            // ARRANGE
            string sfaAreaCostCorrectSinglePostcode = postcodeTestValue;
            IList<SfaAreaCost> sfaAreaCostCorrectSingleList = new List<SfaAreaCost>()
            {
                sfaAreaCostTestValue
            };
            var postcodeFactorsServiceMock = PostcodesSFAAreaCostTestRun(sfaAreaCostCorrectSingleList);

            // ACT
            var sfaAreaCostExists = postcodeFactorsServiceMock.SFAAreaCostsForPostcode(postcodeTestValue);

            // ASSERT
            sfaAreaCostExists.Should().BeEquivalentTo(sfaAreaCostTestValue);
        }

        /// <summary>
        /// Return list of PostcodeFactors SFA Area Cost Entries and check value
        /// </summary>
        [Fact(DisplayName = "SFA AreaCost - Correct values (Many)"), Trait("PostcodeFactors", "Unit")]
        public void SFA_AreaCost_Correct_Many()
        {
            // ARRANGE
            string sfaAreaCostCorrectManyPostcode = postcodeTestValue;
            IList<SfaAreaCost> sfaAreaCostCorrectManyList = new List<SfaAreaCost>()
            {
                sfaAreaCostTestValue,
                sfaAreaCostTestValue
            };
            var postcodeFactorsServiceMock = PostcodesSFAAreaCostTestRun(sfaAreaCostCorrectManyList);

            // ACT
            var sfaAreaCostExists = postcodeFactorsServiceMock.SFAAreaCostsForPostcode(postcodeTestValue);

            // ASSERT
            var expectedListCorrect = new List<SfaAreaCost>
            {
                sfaAreaCostTestValue,
                sfaAreaCostTestValue
            };

            sfaAreaCostExists.Should().BeEquivalentTo(expectedListCorrect);
        }

        /// <summary>
        /// Return list of PostcodeFactors SFA Area Cost Entries and check value
        /// </summary>
        [Fact(DisplayName = "SFA AreaCost - Incorrect values (Single)"), Trait("PostcodeFactors", "Unit")]
        public void SFA_AreaCost_NotCorrect_Many()
        {
            // ARRANGE
            string sfaAreaCostNotCorrectManyPostcode = postcodeTestValue;
            IList<SfaAreaCost> sfaAreaCostNotCorrectManyList = new List<SfaAreaCost>()
            {
                sfaAreaCostTestValue
            };
            var postcodeFactorsServiceMock = PostcodesSFAAreaCostTestRun(sfaAreaCostNotCorrectManyList);

            // ACT
            var sfaAreaCostNotExists = postcodeFactorsServiceMock.SFAAreaCostsForPostcode(sfaAreaCostNotCorrectManyPostcode);

            // ASSERT
            IList<SfaAreaCost> expectedListNotCorrect = new List<SfaAreaCost>
            {
                sfaAreaCostTestValue,
                sfaAreaCostTestValue
            };

            sfaAreaCostNotExists.Should().NotBeSameAs(expectedListNotCorrect);
        }

        #region Test Helpers

        private IPostcodesReferenceDataService PostcodeFactorsCurrentVersionTestRun(string postcodeFactorsVersion)
        {
            var postcodeFactorsCurrentVersionMock = referenceDataCacheMock;
            postcodeFactorsCurrentVersionMock.SetupGet(rdc => rdc.PostcodeCurrentVersion).Returns(postcodeFactorsVersion);

            return MockTestObject(postcodeFactorsCurrentVersionMock.Object);
        }

        private IPostcodesReferenceDataService PostcodesSFAAreaCostTestRun(IEnumerable<SfaAreaCost> sfaAreaCostList)
        {
            var sfaAreaCostMock = referenceDataCacheMock;
            sfaAreaCostMock.SetupGet(rdc => rdc.SfaAreaCost).Returns(new Dictionary<string, IEnumerable<SfaAreaCost>>()
            {
                { postcodeTestValue, sfaAreaCostList }
            });

            return MockTestObject(sfaAreaCostMock.Object);
        }

        private IPostcodesReferenceDataService MockTestObject(IReferenceDataCache @object)
        {
            IPostcodesReferenceDataService postcodeFactorsReferenceDataService = new PostcodesReferenceDataService(@object);

            return postcodeFactorsReferenceDataService;
        }

        private readonly Mock<IReferenceDataCache> referenceDataCacheMock = new Mock<IReferenceDataCache>();

        private readonly string postcodeTestValue = "SW3 5DN";
        private readonly string postcodeFactorsVersionTestValue = "Version_002";

        private readonly SfaAreaCost sfaAreaCostTestValue =
            new SfaAreaCost()
            {
                Postcode = "SW3 5DN",
                AreaCostFactor = 1.2m,
                EffectiveFrom = DateTime.Parse("2000-01-01"),
                EffectiveTo = null
            };

        #endregion
    }
}
