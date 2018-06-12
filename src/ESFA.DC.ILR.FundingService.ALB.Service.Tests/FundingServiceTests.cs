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
using ESFA.DC.ILR.FundingService.ALB.FundingOutput.Model;
using ESFA.DC.ILR.FundingService.ALB.FundingOutput.Model.Interface;
using ESFA.DC.ILR.FundingService.ALB.FundingOutput.Service;
using ESFA.DC.ILR.FundingService.ALB.FundingOutput.Service.Interface;
using ESFA.DC.ILR.FundingService.ALB.InternalData;
using ESFA.DC.ILR.FundingService.ALB.InternalData.Interface;
using ESFA.DC.ILR.FundingService.ALB.OrchestrationService;
using ESFA.DC.ILR.FundingService.ALB.Service.Builders;
using ESFA.DC.ILR.FundingService.ALB.Service.Builders.Interface;
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

namespace ESFA.DC.ILR.FundingService.ALB.Service.Tests
{
    public class FundingServiceTests
    {
        #region ProcessFunding Tests

        /// <summary>
        /// Return FundingOutputs from the Funding Service
        /// </summary>
        [Fact(DisplayName = "ProcessFunding - FundingOutput Exists"), Trait("Funding Service", "Unit")]
        public void ProcessFunding_Exists()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var fundingOutput = RunFundingService();

            // ASSERT
            fundingOutput.Should().NotBeNull();
        }

        /// <summary>
        /// Return FundingOutputs from the Funding Service
        /// </summary>
        [Fact(DisplayName = "ProcessFunding - FundingOutput - FundingOutput Correct"), Trait("Funding Service", "Unit")]
        public void ProcessFunding_FundingOutput_Correct()
        {
            // ARRANGE
            JsonSerializationService jsonSerializationService = new JsonSerializationService();

            // ACT
            var actualFundingOutput = RunFundingService();

            // ASSERT
            var expectedFundingOutputModel = jsonSerializationService.Deserialize<FundingOutputs>(LoadJsonToString());

            expectedFundingOutputModel.Should().BeEquivalentTo(actualFundingOutput);
        }

        #endregion

        #region BuildInputEntities Tests

        /// <summary>
        /// Return FundingOutputs from the Funding Service
        /// </summary>
        [Fact(DisplayName = "BuildInputEntities - DataEntity Exists"), Trait("Funding Service", "Unit")]
        public void BuildInputEntities_DataEntity_Exists()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var dataEntity = TestBuildInputEntities();

            // ASSERT
            dataEntity.Should().NotBeNull();
        }

        /// <summary>
        /// Return FundingOutputs from the Funding Service
        /// </summary>
        [Fact(DisplayName = "BuildInputEntities - DataEntity - Count"), Trait("Funding Service", "Unit")]
        public void BuildInputEntities_DataEntity_Count()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var dataEntity = TestBuildInputEntities();

            // ASSERT
            dataEntity.Count().Should().Be(2);
        }

        /// <summary>
        /// Return FundingOutputs from the Funding Service
        /// </summary>
        [Fact(DisplayName = "BuildInputEntities - DataEntity - LearnRefNumbers Correct"), Trait("Funding Service", "Unit")]
        public void BuildInputEntities_DataEntity_LearnRefNumbersCorrect()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var dataEntity = TestBuildInputEntities();

            // ASSERT
            var actualLearners = dataEntity.SelectMany(g => g.Children.Select(l => l.LearnRefNumber)).ToList();

            var expectedLearners = new List<string> { "16v224", "22v237" };

            expectedLearners.Should().BeEquivalentTo(actualLearners);
        }

        #endregion

        #region ExecuteSessions Tests

        /// <summary>
        /// Return FundingOutputs from the Funding Service
        /// </summary>
        [Fact(DisplayName = "ExecuteSessions - Data Entity Exists"), Trait("Funding Service", "Unit")]
        public void ExecuteSessions_Entity_Exists()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var dataEntity = TestExecuteSessions();

            // ASSERT
            dataEntity.Should().NotBeNull();
        }

        /// <summary>
        /// Return FundingOutputs from the Funding Service
        /// </summary>
        [Fact(DisplayName = "ExecuteSessions - Data Entity Count"), Trait("Funding Service", "Unit")]
        public void ExecuteSessions_Entity_EntityCount()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var dataEntity = TestExecuteSessions();

            // ASSERT
            dataEntity.Count().Should().Be(2);
        }

        /// <summary>
        /// Return FundingOutputs from the Funding Service
        /// </summary>
        [Fact(DisplayName = "ExecuteSessions - Learners Correct"), Trait("Funding Service", "Unit")]
        public void ExecuteSessions_Entity_LearnerCorrect()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var dataEntity = TestExecuteSessions();

            // ASSERT
            var learnersActual = dataEntity.SelectMany(g => g.Children.Select(l => l.LearnRefNumber)).ToList();

            var learnersExpected = new List<string>()
            {
                "22v237",
                "16v224"
            };

            learnersExpected.Should().BeEquivalentTo(learnersActual);
        }

        /// <summary>
        /// Return FundingOutputs from the Funding Service
        /// </summary>
        [Fact(DisplayName = "ExecuteSessions - LearningDelivery Count"), Trait("Funding Service", "Unit")]
        public void ExecuteSessions_Entity_LearningDeliveryCount()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var dataEntity = TestExecuteSessions();
            var learningDeliveries = LearningDeliveries(dataEntity);

            // ASSERT
            learningDeliveries.Count.Should().Be(2);
        }

        /// <summary>
        /// Return FundingOutputs from the Funding Service
        /// </summary>
        [Fact(DisplayName = "ExecuteSessions - LearningDelivery Entity Name Correct"), Trait("Funding Service", "Unit")]
        public void ExecuteSessions_Entity_LearningDeliveryNameCorrect()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var dataEntity = TestExecuteSessions();
            var learningDeliveries = LearningDeliveries(dataEntity);

            // ASSERT
            learningDeliveries[0].EntityName.Should().Be("LearningDelivery");
        }

        /// <summary>
        /// Return FundingOutputs from the Funding Service
        /// </summary>
        [Fact(DisplayName = "ExecuteSessions - LearningDelivery Attributes Correct"), Trait("Funding Service", "Unit")]
        public void ExecuteSessions_Entity_LearningDeliveryAttributesCorrect()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var dataEntity = TestExecuteSessions();
            var learningDeliveries = LearningDeliveries(dataEntity);

            // ASSERT
            var learnAimRefActual = DecimalStrToInt(Attribute(learningDeliveries[0], "LrnDelFAM_ADL").ToString());

            learnAimRefActual.Should().Be(1);
        }

        /// <summary>
        /// Return FundingOutputs from the Funding Service
        /// </summary>
        [Fact(DisplayName = "ExecuteSessions - LearningDelivery ChangePoints Exist"), Trait("Funding Service", "Unit")]
        public void ExecuteSessions_Entity_LearningDeliveryChangePointsExist()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var dataEntity = TestExecuteSessions();
            var learningDeliveries = LearningDeliveries(dataEntity);

            // ASSERT
            var changePointsActual = ChangePoints(learningDeliveries[0], "AreaUpliftOnProgPayment");

            changePointsActual.Should().NotBeNull();
        }

        /// <summary>
        /// Return FundingOutputs from the Funding Service
        /// </summary>
        [Fact(DisplayName = "ExecuteSessions - LearningDelivery ChangePoints Correct"), Trait("Funding Service", "Unit")]
        public void ExecuteSessions_Entity_LearningDeliveryChangePointsCorrect()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var dataEntity = TestExecuteSessions();
            var learningDeliveries = LearningDeliveries(dataEntity);

            // ASSERT
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
        /// Return FundingOutputs from the Funding Service
        /// </summary>
        [Fact(DisplayName = "ExecuteSessions - LearningDeliveryChildren Count"), Trait("Funding Service", "Unit")]
        public void ExecuteSessions_Entity_LearningDeliveryChildrenCount()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var dataEntity = TestExecuteSessions();
            var learningDeliveryChildren = LearningDeliveryChildren(dataEntity);

            // ASSERT
            learningDeliveryChildren.Count.Should().Be(11);
        }

        /// <summary>
        /// Return FundingOutputs from the Funding Service
        /// </summary>
        [Fact(DisplayName = "ExecuteSessions - LearningDeliveryChildren Count"), Trait("Funding Service", "Unit")]
        public void ExecuteSessions_Entity_LearningDeliveryChildrenCorrect()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var dataEntity = TestExecuteSessions();
            var learningDeliveryChildren = LearningDeliveryChildren(dataEntity).ToList();

            // ASSERT
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
        /// Return FundingOutputs from the Funding Service
        /// </summary>
        [Fact(DisplayName = "ExecuteSessions - LearningDeliveryFAM Attributes Correct"), Trait("Funding Service", "Unit")]
        public void ExecuteSessions_Entity_LearningDeliveryFAM_AttributesCorrect()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var dataEntity = TestExecuteSessions();
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

        #region DataEntitytoFundingOutput Tests

        /// <summary>
        /// Return FundingOutputs from the Funding Service
        /// </summary>
        [Fact(DisplayName = "DataEntitytoFundingOutput - FundingOutput Exists"), Trait("Funding Service", "Unit")]
        public void DataEntitytoFundingOutput_FundingOutput_Exists()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var fundingOutput = DataEntitytoFundingOutput();

            // ASSERT
            fundingOutput.Should().NotBeNull();
        }

        /// <summary>
        /// Return FundingOutputs from the Funding Service
        /// </summary>
        [Fact(DisplayName = "DataEntitytoFundingOutput - FundingOutput Count"), Trait("Funding Service", "Unit")]
        public void DataEntitytoFundingOutput_FundingOutput_Count()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var fundingOutput = DataEntitytoFundingOutput();

            // ASSERT
            fundingOutput.Should().NotBeNull();
        }

        /// <summary>
        /// Return FundingOutputs from the Funding Service
        /// </summary>
        [Fact(DisplayName = "DataEntitytoFundingOutput - FundingOutput - Learner Count"), Trait("Funding Service", "Unit")]
        public void DataEntitytoFundingOutput_FundingOutput_LearnerCount()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var fundingOutput = DataEntitytoFundingOutput();

            // ASSERT
            fundingOutput.Learners.Count().Should().Be(2);
        }

        /// <summary>
        /// Return FundingOutputs from the Funding Service
        /// </summary>
        [Fact(DisplayName = "DataEntitytoFundingOutput - FundingOutput - Global Correct"), Trait("Funding Service", "Unit")]
        public void DataEntitytoFundingOutput_FundingOutput_GlobalCorrect()
        {
            // ARRANGE
            JsonSerializationService jsonSerializationService = new JsonSerializationService();

            // ACT
            var actualFundingOutput = DataEntitytoFundingOutput();

            // ASSERT
            var expectedFundingOutputModel = jsonSerializationService.Deserialize<FundingOutputs>(LoadJsonToString());

            expectedFundingOutputModel.Global.Should().BeEquivalentTo(actualFundingOutput.Global);
        }

        /// <summary>
        /// Return FundingOutputs from the Funding Service
        /// </summary>
        [Fact(DisplayName = "DataEntitytoFundingOutput - FundingOutput - Learners Correct"), Trait("Funding Service", "Unit")]
        public void DataEntitytoFundingOutput_FundingOutput_LearnersCorrect()
        {
            // ARRANGE
            JsonSerializationService jsonSerializationService = new JsonSerializationService();

            // ACT
            var actualFundingOutput = DataEntitytoFundingOutput();

            // ASSERT
            var expectedFundingOutputModel = jsonSerializationService.Deserialize<FundingOutputs>(LoadJsonToString());

            expectedFundingOutputModel.Learners.Should().BeEquivalentTo(actualFundingOutput.Learners);
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

        private IFundingOutputs RunFundingService()
        {
            IMessage message = ILRFile(@"Files\ILR-10006341-1819-20180118-023456-02.xml");
            IFundingContext fundingContext = SetupFundingContext(message);

            IReferenceDataCache referenceDataCache = new ReferenceDataCache();
            IAttributeBuilder<IAttributeData> attributeBuilder = new AttributeBuilder();
            IReferenceDataCachePopulationService referenceDataCachePopulationService = new ReferenceDataCachePopulationService(referenceDataCache, LARSMock().Object, PostcodesMock().Object);
            IDataEntityBuilder dataEntityBuilder = new DataEntityBuilder(referenceDataCache, attributeBuilder);
            IFundingOutputService fundingOutputService = new FundingOutputService();
            IInternalDataCache validALBLearnersCache = new InternalDataCache();

            var fundingService = new FundingService(dataEntityBuilder, opaService, fundingOutputService);

            var preFundingOrchestrationService = new PreFundingALBPopulationService(referenceDataCachePopulationService, fundingContext, validALBLearnersCache);
            preFundingOrchestrationService.PopulateData();

            return fundingService.ProcessFunding(fundingContext.UKPRN, validALBLearnersCache.ValidLearners);
        }

        private IEnumerable<IDataEntity> TestBuildInputEntities()
        {
            IMessage message = ILRFile(@"Files\ILR-10006341-1819-20180118-023456-02.xml");
            IFundingContext fundingContext = SetupFundingContext(message);

            IReferenceDataCache referenceDataCache = new ReferenceDataCache();
            IAttributeBuilder<IAttributeData> attributeBuilder = new AttributeBuilder();
            IReferenceDataCachePopulationService referenceDataCachePopulationService = new ReferenceDataCachePopulationService(referenceDataCache, LARSMock().Object, PostcodesMock().Object);
            IDataEntityBuilder dataEntityBuilder = new DataEntityBuilder(referenceDataCache, attributeBuilder);
            IFundingOutputService fundingOutputService = new FundingOutputService();
            IInternalDataCache validALBLearnersCache = new InternalDataCache();

            var fundingService = new FundingService(dataEntityBuilder, opaService, fundingOutputService);

            var preFundingOrchestrationService = new PreFundingALBPopulationService(referenceDataCachePopulationService, fundingContext, validALBLearnersCache);
            preFundingOrchestrationService.PopulateData();

            return fundingService.BuildInputEntities(fundingContext.UKPRN, validALBLearnersCache.ValidLearners);
        }

        private IEnumerable<IDataEntity> TestExecuteSessions()
        {
            IMessage message = ILRFile(@"Files\ILR-10006341-1819-20180118-023456-02.xml");
            IFundingContext fundingContext = SetupFundingContext(message);

            IReferenceDataCache referenceDataCache = new ReferenceDataCache();
            IAttributeBuilder<IAttributeData> attributeBuilder = new AttributeBuilder();
            IReferenceDataCachePopulationService referenceDataCachePopulationService = new ReferenceDataCachePopulationService(referenceDataCache, LARSMock().Object, PostcodesMock().Object);
            IDataEntityBuilder dataEntityBuilder = new DataEntityBuilder(referenceDataCache, attributeBuilder);
            IFundingOutputService fundingOutputService = new FundingOutputService();
            IInternalDataCache validALBLearnersCache = new InternalDataCache();

            var fundingService = new FundingService(dataEntityBuilder, opaService, fundingOutputService);

            var preFundingOrchestrationService = new PreFundingALBPopulationService(referenceDataCachePopulationService, fundingContext, validALBLearnersCache);
            preFundingOrchestrationService.PopulateData();

            var inputs = fundingService.BuildInputEntities(fundingContext.UKPRN, validALBLearnersCache.ValidLearners);

            return fundingService.ExecuteSessions(inputs);
        }

        private IFundingOutputs DataEntitytoFundingOutput()
        {
            IMessage message = ILRFile(@"Files\ILR-10006341-1819-20180118-023456-02.xml");
            IFundingContext fundingContext = SetupFundingContext(message);

            IReferenceDataCache referenceDataCache = new ReferenceDataCache();
            IAttributeBuilder<IAttributeData> attributeBuilder = new AttributeBuilder();
            IReferenceDataCachePopulationService referenceDataCachePopulationService = new ReferenceDataCachePopulationService(referenceDataCache, LARSMock().Object, PostcodesMock().Object);
            IDataEntityBuilder dataEntityBuilder = new DataEntityBuilder(referenceDataCache, attributeBuilder);
            IFundingOutputService fundingOutputService = new FundingOutputService();
            IInternalDataCache validALBLearnersCache = new InternalDataCache();

            var fundingService = new FundingService(dataEntityBuilder, opaService, fundingOutputService);

            var preFundingOrchestrationService = new PreFundingALBPopulationService(referenceDataCachePopulationService, fundingContext, validALBLearnersCache);
            preFundingOrchestrationService.PopulateData();

            var inputs = fundingService.BuildInputEntities(fundingContext.UKPRN, validALBLearnersCache.ValidLearners);

            var dataEntities = fundingService.ExecuteSessions(inputs);

            return fundingService.DataEntitytoFundingOutput(dataEntities);
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

        private int DecimalStrToInt(string value)
        {
            var valueInt = value.Substring(0, value.IndexOf('.', 0));
            return Int32.Parse(valueInt);
        }

        private string LoadJsonToString()
        {
            string json;

            using (StreamReader r = new StreamReader("Files\\ExpectedJsonString.json"))
            {
                json = r.ReadToEnd();
            }

            return json;
        }

        #endregion
    }
}
