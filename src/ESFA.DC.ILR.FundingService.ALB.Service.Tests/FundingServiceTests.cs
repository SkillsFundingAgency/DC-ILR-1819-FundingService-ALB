using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using ESFA.DC.Data.LARS.Model.Interfaces;
using ESFA.DC.Data.Postcodes.Model.Interfaces;
using ESFA.DC.ILR.FundingService.ALB.ExternalData;
using ESFA.DC.ILR.FundingService.ALB.ExternalData.Interface;
using ESFA.DC.ILR.FundingService.ALB.ExternalData.LARS.Model;
using ESFA.DC.ILR.FundingService.ALB.ExternalData.Postcodes.Model;
using ESFA.DC.ILR.FundingService.ALB.Service.Builders;
using ESFA.DC.ILR.FundingService.ALB.Service.Builders.Interface;
using ESFA.DC.ILR.FundingService.ALB.Service.Interface;
using ESFA.DC.ILR.Model;
using ESFA.DC.OPA.Model.Interface;
using FluentAssertions;
using Xunit;
using Moq;
using ESFA.DC.OPA.Service;
using ESFA.DC.OPA.Service.Builders;
using ESFA.DC.OPA.Service.Interface;
using ESFA.DC.OPA.Service.Interface.Builders;
using ESFA.DC.TestHelpers.Mocks;
using ESFA.DC.Data.LARS.Model;
using ESFA.DC.Data.Postcodes.Model;
using ESFA.DC.ILR.Model.Interface;
using ESFA.DC.OPA.Service.Interface.Rulebase;
using ESFA.DC.OPA.Service.Rulebase;
using ESFA.DC.ILR.FundingService.ALB.Stubs;
using ESFA.DC.Serialization.Interfaces;
using ESFA.DC.Serialization.Xml;
using ESFA.DC.IO.Interfaces;
using ESFA.DC.IO.Dictionary;

namespace ESFA.DC.ILR.FundingService.ALB.Service.Tests
{
    public class FundingServiceTests
    {
        #region ProcessFunding Tests

        /// <summary>
        /// Return DataEntities from the Funding Service
        /// </summary>
        [Fact(DisplayName = "ProcessFunding - Data Entity Exists"), Trait("Funding Service", "Unit")]
        public void ProcessFunding_Entity_Exists()
        {
            //ARRANGE
            //Use Test Helpers

            //ACT
            var dataEntity = RunFundingService(@"Files\ILR-10006341-1819-20180118-023456-02.xml");

            //ASSERT
            dataEntity.Should().NotBeNull();
        }

        /// <summary>
        /// Return DataEntities from the Funding Service
        /// </summary>
        [Fact(DisplayName = "ProcessFunding - Data Entity Count"), Trait("Funding Service", "Unit")]
        public void ProcessFunding_Entity_EntityCount()
        {
            //ARRANGE
            //Use Test Helpers

            //ACT
            var dataEntity = RunFundingService(@"Files\ILR-10006341-1819-20180118-023456-02.xml");

            //ASSERT
            dataEntity.Count().Should().Be(2);
        }

        /// <summary>
        /// Return DataEntities from the Funding Service
        /// </summary>
        [Fact(DisplayName = "ProcessFunding - Learners Correct"), Trait("Funding Service", "Unit")]
        public void ProcessFunding_Entity_LearnerCorrect()
        {
            //ARRANGE
            //Use Test Helpers

            //ACT
            var dataEntity = RunFundingService(@"Files\ILR-10006341-1819-20180118-023456-02.xml");

            //ASSERT
            var learnersActual = dataEntity.SelectMany(g => g.Children.Select(l => l.LearnRefNumber)).ToList();

            var learnersExpected = new List<string>()
            {
                "22v237",
                "16v224"
            };

            learnersExpected.Should().BeEquivalentTo(learnersActual);
        }

        /// <summary>
        /// Return DataEntities from the Funding Service
        /// </summary>
        [Fact(DisplayName = "ProcessFunding - LearningDelivery Count"), Trait("Funding Service", "Unit")]
        public void ProcessFunding_Entity_LearningDeliveryCount()
        {
            //ARRANGE
            //Use Test Helpers

            //ACT
            var dataEntity = RunFundingService(@"Files\ILR-10006341-1819-20180118-023456-02.xml");
            var learningDeliveries = LearningDeliveries(dataEntity);

            //ASSERT
            learningDeliveries.Count.Should().Be(2);
        }

        /// <summary>
        /// Return DataEntities from the Funding Service
        /// </summary>
        [Fact(DisplayName = "ProcessFunding - LearningDelivery Entity Name Correct"), Trait("Funding Service", "Unit")]
        public void ProcessFunding_Entity_LearningDeliveryNameCorrect()
        {
            //ARRANGE
            //Use Test Helpers

            //ACT
            var dataEntity = RunFundingService(@"Files\ILR-10006341-1819-20180118-023456-02.xml");
            var learningDeliveries = LearningDeliveries(dataEntity);

            //ASSERT
            learningDeliveries[0].EntityName.Should().Be("LearningDelivery");
        }

        /// <summary>
        /// Return DataEntities from the Funding Service
        /// </summary>
        [Fact(DisplayName = "ProcessFunding - LearningDelivery Attributes Correct"), Trait("Funding Service", "Unit")]
        public void ProcessFunding_Entity_LearningDeliveryAttributesCorrect()
        {
            //ARRANGE
            //Use Test Helpers

            //ACT
            var dataEntity = RunFundingService(@"Files\ILR-10006341-1819-20180118-023456-02.xml");
            var learningDeliveries = LearningDeliveries(dataEntity);

            //ASSERT
            var learnAimRefActual = DecimalStrToInt(Attribute(learningDeliveries[0], "LrnDelFAM_ADL").ToString());

            learnAimRefActual.Should().Be(1);
        }

        /// <summary>
        /// Return DataEntities from the Funding Service
        /// </summary>
        [Fact(DisplayName = "ProcessFunding - LearningDelivery ChangePoints Exist"), Trait("Funding Service", "Unit")]
        public void ProcessFunding_Entity_LearningDeliveryChangePointsExist()
        {
            //ARRANGE
            //Use Test Helpers

            //ACT
            var dataEntity = RunFundingService(@"Files\ILR-10006341-1819-20180118-023456-02.xml");
            var learningDeliveries = LearningDeliveries(dataEntity);

            //ASSERT
            var changePointsActual = ChangePoints(learningDeliveries[0], "AreaUpliftOnProgPayment");

            changePointsActual.Should().NotBeNull();
        }

        /// <summary>
        /// Return DataEntities from the Funding Service
        /// </summary>
        [Fact(DisplayName = "ProcessFunding - LearningDelivery ChangePoints Correct"), Trait("Funding Service", "Unit")]
        public void ProcessFunding_Entity_LearningDeliveryChangePointsCorrect()
        {
            //ARRANGE
            //Use Test Helpers

            //ACT
            var dataEntity = RunFundingService(@"Files\ILR-10006341-1819-20180118-023456-02.xml");
            var learningDeliveries = LearningDeliveries(dataEntity);

            //ASSERT
            var changePointsActual = ChangePoints(learningDeliveries[0], "AreaUpliftOnProgPayment");

            var changePointsExpected = new List<string>
            {
                "43.05",
                "43.05",
                "43.05",
                "43.05",
                "43.05",
                "43.05",
                "43.05",
                "43.05",
                "43.05",
                "0.0",
                "0.0",
                "0.0"
            };

            changePointsActual.Should().BeEquivalentTo(changePointsExpected);
        }

        /// <summary>
        /// Return DataEntities from the Funding Service
        /// </summary>
        [Fact(DisplayName = "ProcessFunding - LearningDeliveryChildren Count"), Trait("Funding Service", "Unit")]
        public void ProcessFunding_Entity_LearningDeliveryChildrenCount()
        {
            //ARRANGE
            //Use Test Helpers

            //ACT
            var dataEntity = RunFundingService(@"Files\ILR-10006341-1819-20180118-023456-02.xml");
            var learningDeliveryChildren = LearningDeliveryChildren(dataEntity);

            //ASSERT
            learningDeliveryChildren.Count.Should().Be(11);
        }

        /// <summary>
        /// Return DataEntities from the Funding Service
        /// </summary>
        [Fact(DisplayName = "ProcessFunding - LearningDeliveryChildren Count"), Trait("Funding Service", "Unit")]
        public void ProcessFunding_Entity_LearningDeliveryChildrenCorrect()
        {
            //ARRANGE
            //Use Test Helpers

            //ACT
            var dataEntity = RunFundingService(@"Files\ILR-10006341-1819-20180118-023456-02.xml");
            var learningDeliveryChildren = LearningDeliveryChildren(dataEntity).ToList();

            //ASSERT
            var actualChildren = learningDeliveryChildren.Select(e => e.EntityName).ToList();

            var expectedChildren = new List<string>
            {
                "LearningDeliveryFAM",
                "LearningDeliveryFAM",
                "LearningDeliveryFAM",
                "LearningDeliveryFAM",
                "SFA_PostcodeAreaCost",
                "LearningDeliveryLARS_Funding",
                "LearningDeliveryFAM",
                "LearningDeliveryFAM",
                "LearningDeliveryFAM",
                "SFA_PostcodeAreaCost",
                "LearningDeliveryLARS_Funding"
            };

            expectedChildren.Should().BeEquivalentTo(actualChildren);
        }

        /// <summary>
        /// Return DataEntities from the Funding Service
        /// </summary>
        [Fact(DisplayName = "ProcessFunding - LearningDeliveryFAM Attributes Correct"), Trait("Funding Service", "Unit")]
        public void ProcessFunding_Entity_LearningDeliveryFAM_AttributesCorrect()
        {
            //ARRANGE
            //Use Test Helpers

            //ACT
            var dataEntity = RunFundingService(@"Files\ILR-10006341-1819-20180118-023456-02.xml");
            var learningDeliveryChildren = LearningDeliveryChildren(dataEntity).ToList();

            //ASSERT
            var actualAttributes = learningDeliveryChildren.Where(ldf => ldf.EntityName == "LearningDeliveryFAM").Select(a => a.Attributes.Keys).ToList();

            var expectedAttributes = new List<string>
            {
                "LearnDelFAMTypeUC",
                "LearnDelFAMType",
                "LearnDelFAMDateTo",
                "ValidForALB",
                "ALBRate",
                "LearnDelFAMCode",
                "FAMALBRateLiabilityDatesStage1",
                "FAMALBCodeLiabilityDatesStage1",
                "ALBRateFirst",
                "ALBCodeFirst",
                "ALBRateLiabilityDatesFAM",
                "ALBCodeLiabilityDatesFAM",
                "LearnDelFAMDateFrom",
                "IntTestLearnDelFAM"
            };

            expectedAttributes.Should().BeEquivalentTo(actualAttributes[0]);
        }

        #endregion

        #region Populate Reference Data Tests

        /// <summary>
        /// Populate reference data cache and check values
        /// </summary>
        [Fact(DisplayName = "PopulateReferenceData - LARS Version Correct"), Trait("Funding Service", "Unit")]
        public void PopulateReferenceData_LARSVersion_Correct()
        {
            //ARRANGE
            IMessage message = ILRFile(@"Files\ILR-10006341-1819-20180118-023456-02.xml");
            IReferenceDataCache referenceDataCache = new ReferenceDataCache();
            var fundingService = FundingServicePopulationReferenceDataMock(referenceDataCache);

            //ACT
            fundingService.PopulateReferenceData(message.Learners);

            //ASSERT
            referenceDataCache.LARSCurrentVersion.Should().Be("Version_005");
        }

        /// <summary>
        /// Populate reference data cache and check values
        /// </summary>
        [Fact(DisplayName = "PopulateReferenceData - LARS LearningDelivery Correct"), Trait("Funding Service", "Unit")]
        public void PopulateReferenceData_LARSVLearningDelivery_Correct()
        {
            //ARRANGE
            IMessage message = ILRFile(@"Files\ILR-10006341-1819-20180118-023456-02.xml");
            IReferenceDataCache referenceDataCache = new ReferenceDataCache();
            var fundingService = FundingServicePopulationReferenceDataMock(referenceDataCache);

            //ACT
            fundingService.PopulateReferenceData(message.Learners);

            //ASSERT
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
            //ARRANGE
            IMessage message = ILRFile(@"Files\ILR-10006341-1819-20180118-023456-02.xml");
            IReferenceDataCache referenceDataCache = new ReferenceDataCache();
            var fundingService = FundingServicePopulationReferenceDataMock(referenceDataCache);

            //ACT
            fundingService.PopulateReferenceData(message.Learners);

            //ASSERT
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
            //ARRANGE
            IMessage message = ILRFile(@"Files\ILR-10006341-1819-20180118-023456-02.xml");
            IReferenceDataCache referenceDataCache = new ReferenceDataCache();
            var fundingService = FundingServicePopulationReferenceDataMock(referenceDataCache);

            //ACT
            fundingService.PopulateReferenceData(message.Learners);

            //ASSERT
            referenceDataCache.PostcodeCurrentVersion.Should().Be("Version_002");
        }

        /// <summary>
        /// Populate reference data cache and check values
        /// </summary>
        [Fact(DisplayName = "PopulateReferenceData - Postcodes SFA AreaCost Correct"), Trait("Funding Service", "Unit")]
        public void PopulateReferenceData_PostcodesSFAAreaCost_Correct()
        {
            //ARRANGE
            IMessage message = ILRFile(@"Files\ILR-10006341-1819-20180118-023456-02.xml");
            IReferenceDataCache referenceDataCache = new ReferenceDataCache();
            var fundingService = FundingServicePopulationReferenceDataMock(referenceDataCache);

            //ACT
            fundingService.PopulateReferenceData(message.Learners);

            //ASSERT
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

        #endregion

        #region Retrieve Valid Learners Tests

        /// <summary>
        /// Return Valid Learners from KeyValuePersistanceService
        /// </summary>
        [Fact(DisplayName = "Valid Learners - Learners Exist"), Trait("Funding Service", "Unit")]
        public void ValidLearners_LearnersExist()
        {
            // ARRANGE
            var fundingContext = new FundingContextStub { ValidLearnRefNumbersKey = "ValidLearnRefNumbers" };

            IKeyValuePersistenceService keyValuePersistenceService = new DictionaryKeyValuePersistenceService();
            ISerializationService serializationService = new XmlSerializationService();

            var validationOutput = new ValidationOutputStub(fundingContext, keyValuePersistenceService, serializationService);

            var validLearnersList = new List<string> { "22v237", "16v224" };

            //ACT
            validationOutput.ValidLearners(validLearnersList);

            //ASSERT
            var result = keyValuePersistenceService.GetAsync(fundingContext.ValidLearnRefNumbersKey).Result;

            result.Should().NotBeNull();
        }

        /// <summary>
        /// Return Valid Learners from KeyValuePersistanceService
        /// </summary>
        [Fact(DisplayName = "Valid Learners - Learners Correct"), Trait("Funding Service", "Unit")]
        public void ValidLearners_LearnersCorrect()
        {
            // ARRANGE
            var fundingContext = new FundingContextStub { ValidLearnRefNumbersKey = "ValidLearnRefNumbers" };

            IKeyValuePersistenceService keyValuePersistenceService = new DictionaryKeyValuePersistenceService();
            ISerializationService serializationService = new XmlSerializationService();

            var validationOutput = new ValidationOutputStub(fundingContext, keyValuePersistenceService, serializationService);

            var validLearnersList = new List<string> { "22v237", "16v224" };

            //ACT
            validationOutput.ValidLearners(validLearnersList);

            //ASSERT
            var result = keyValuePersistenceService.GetAsync(fundingContext.ValidLearnRefNumbersKey).Result;

            serializationService.Deserialize<List<string>>(result).Should().BeEquivalentTo(validLearnersList);
        }

        /// <summary>
        /// Return Valid Learners from KeyValuePersistanceService
        /// </summary>
        [Fact(DisplayName = "Valid Learners - Run Funding Service - Learner Count"), Trait("Funding Service", "Unit")]
        public void ValidLearners_FundingServiceLearnersCount()
        {
            // ARRANGE        
            var validLearnersList = new List<string> { "16v224" };

            //ACT
            var dataEntity = RunFundingServiceForValidLearners(@"Files\ILR-10006341-1819-20180118-023456-02.xml", validLearnersList);

            //ASSERT
            dataEntity.Count().Should().Be(1);
        }

        /// <summary>
        /// Return Valid Learners from KeyValuePersistanceService
        /// </summary>
        [Fact(DisplayName = "Valid Learners - Run Funding Service - Learner Correct"), Trait("Funding Service", "Unit")]
        public void ValidLearners_FundingServiceLearnersCorrect()
        {
            // ARRANGE        
            var validLearnersList = new List<string> { "16v224" };

            //ACT
            var dataEntity = RunFundingServiceForValidLearners(@"Files\ILR-10006341-1819-20180118-023456-02.xml", validLearnersList);

            //ASSERT
            dataEntity.Select(g => g.Children.Select(l => l.LearnRefNumber)).Single().Should().BeEquivalentTo("16v224");
        }
        
        #endregion

        #region Test Helpers

        #region Test Data

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

        #endregion

        #region Mocks

        private static readonly Mock<ILARS> larsContextMock = new Mock<ILARS>();
        private static readonly Mock<IPostcodes> postcodesContextMock = new Mock<IPostcodes>();

        private FundingService FundingServicePopulationReferenceDataMock(IReferenceDataCache referenceDataCache)
        {
            IFundingContext fundingContext = new FundingContextStub { ValidLearnRefNumbersKey = "ValidLearnRefNumbers" };
            IKeyValuePersistenceService keyValuePersistenceService = new DictionaryKeyValuePersistenceService();
            ISerializationService serializationService = new XmlSerializationService();
            IAttributeBuilder<IAttributeData> attributeBuilder = new AttributeBuilder();
            var dataEntityBuilder = new DataEntityBuilder(referenceDataCache, attributeBuilder);

            var referenceDataCachePopulationService = new ReferenceDataCachePopulationService(referenceDataCache, LARSMock().Object, PostcodesMock().Object);

            return new FundingService(referenceDataCachePopulationService, keyValuePersistenceService, serializationService, fundingContext, dataEntityBuilder, opaService);
        }

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

        private IEnumerable<IDataEntity> RunFundingService(string filePath)
        {
            Message message = ILRFile(filePath);

            IKeyValuePersistenceService keyValuePersistenceService = new DictionaryKeyValuePersistenceService();
            ISerializationService serializationService = new XmlSerializationService();
            IFundingContext fundingContext = new FundingContextStub { ValidLearnRefNumbersKey = "ValidLearnRefNumbers" };
            var validationOutput = new ValidationOutputStub(fundingContext, keyValuePersistenceService, serializationService);
            validationOutput.ValidLearners(message.Learner.Select(l => l.LearnRefNumber).ToList());

            IReferenceDataCache referenceDataCache = new ReferenceDataCache();
            IReferenceDataCachePopulationService referenceDataCachePopulationService = new ReferenceDataCachePopulationService(referenceDataCache, LARSMock().Object, PostcodesMock().Object);
            IAttributeBuilder<IAttributeData> attributeBuilder = new AttributeBuilder();
            var dataEntityBuilder = new DataEntityBuilder(referenceDataCache, attributeBuilder);
            IFundingSevice fundingService = new FundingService(referenceDataCachePopulationService, keyValuePersistenceService, serializationService, fundingContext, dataEntityBuilder, opaService);

            return fundingService.ProcessFunding(message);
        }

        private IEnumerable<IDataEntity> RunFundingServiceForValidLearners(string filePath, IList<string> validLearners)
        {
            Message message = ILRFile(filePath);

            IKeyValuePersistenceService keyValuePersistenceService = new DictionaryKeyValuePersistenceService();
            ISerializationService serializationService = new XmlSerializationService();
            IFundingContext fundingContext = new FundingContextStub { ValidLearnRefNumbersKey = "ValidLearnRefNumbers" };
            var validationOutput = new ValidationOutputStub(fundingContext, keyValuePersistenceService, serializationService);
            validationOutput.ValidLearners(validLearners);

            IReferenceDataCache referenceDataCache = new ReferenceDataCache();
            IReferenceDataCachePopulationService referenceDataCachePopulationService = new ReferenceDataCachePopulationService(referenceDataCache, LARSMock().Object, PostcodesMock().Object);
            IAttributeBuilder<IAttributeData> attributeBuilder = new AttributeBuilder();
            var dataEntityBuilder = new DataEntityBuilder(referenceDataCache, attributeBuilder);
            IFundingSevice fundingService = new FundingService(referenceDataCachePopulationService, keyValuePersistenceService, serializationService, fundingContext, dataEntityBuilder, opaService);

            return fundingService.ProcessFunding(message);
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

        #endregion

        private static readonly ISessionBuilder _sessionBuilder = new SessionBuilder();
        private static readonly IOPADataEntityBuilder _dataEntityBuilder = new OPADataEntityBuilder(new DateTime(2017, 8, 1));

        private readonly IOPAService opaService =
            new OPAService(_sessionBuilder, _dataEntityBuilder, MockRulebaseProviderFactory());

        private IList<IDataEntity> LearningDeliveryChildren(IEnumerable<IDataEntity> entity)
        {
            return entity.SelectMany(g => g.Children
                .SelectMany(l => l.Children.SelectMany(ld => ld.Children))).ToList();
        }

        private IList<IDataEntity> LearningDeliveries(IEnumerable<IDataEntity> entity)
        {
            return entity.SelectMany(g => g.Children
                .SelectMany(l => l.Children)).ToList();
        }

        private object Attribute(IDataEntity entity, string attributeName)
        {
            return entity.Attributes.Where(k => k.Key == attributeName).Select(v => v.Value.Value).Single();
        }

        private IList<string> ChangePoints(IDataEntity entity, string attributeName)
        {
            return entity.Attributes.Where(k => k.Key == attributeName)
                .SelectMany(v => v.Value.Changepoints.Select(c => c.Value.ToString())).ToList();
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

        public int DecimalStrToInt(string value)
        {
            var valueInt = value.Substring(0, value.IndexOf('.', 0));
            return Int32.Parse(valueInt);
        }

        #endregion
    }
}
