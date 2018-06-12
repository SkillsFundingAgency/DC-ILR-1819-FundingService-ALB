using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using ESFA.DC.ILR.FundingService.ALB.FundingOutput.Model.Attribute;
using ESFA.DC.ILR.FundingService.ALB.FundingOutput.Model.Interface;
using ESFA.DC.ILR.FundingService.ALB.FundingOutput.Model.Interface.Attribute;
using ESFA.DC.ILR.FundingService.ALB.Service.Interface;
using ESFA.DC.ILR.Model;
using ESFA.DC.ILR.Model.Interface;
using ESFA.DC.OPA.Model;
using ESFA.DC.OPA.Model.Interface;
using FluentAssertions;
using Moq;
using Xunit;

namespace ESFA.DC.ILR.FundingService.ALB.FundingOutput.Service.Tests
{
    public class FundingOutputServiceTests
    {
        /// <summary>
        /// Return FundingOutputs from the FundingOutput
        /// </summary>
        [Fact(DisplayName = "ProcessFundingOutputs - FundingOutput - Exists"), Trait("FundingOutput Service", "Unit")]
        public void ProcessFundingOutputs_FundingOutput_Exists()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var fundingOutputService = new FundingOutputService();

            // ASSERT
            fundingOutputService.Should().NotBeNull();
        }

        /// <summary>
        /// Return FundingOutputs from the FundingOutput
        /// </summary>
        [Fact(DisplayName = "ProcessFundingOutputs - FundingOutput - Global Exists"), Trait("FundingOutput Service", "Unit")]
        public void ProcessFundingOutputs_FundingOutput_GlobalExists()
        {
            // ARRANGE
            var fundingOutputService = new FundingOutputService();

            // ACT
            var fundingOutput = fundingOutputService.ProcessFundingOutputs(TestEntities());

            // ASSERT
            fundingOutput.Global.Should().NotBeNull();
        }

        /// <summary>
        /// Return FundingOutputs from the FundingOutput
        /// </summary>
        [Fact(DisplayName = "ProcessFundingOutputs - FundingOutput - Global Correct"), Trait("FundingOutput Service", "Unit")]
        public void ProcessFundingOutputs_FundingOutput_GlobalCorrect()
        {
            // ARRANGE
            IGlobalAttribute expectedGlobal = new GlobalAttribute
            {
                UKPRN = 12345678,
                LARSVersion = "Version_005",
                PostcodeAreaCostVersion = "Version_002",
                RulebaseVersion = "1718.5.10",
            };

            var fundingOutputService = new FundingOutputService();

            // ACT
            var fundingOutput = fundingOutputService.ProcessFundingOutputs(TestEntities());

            // ASSERT
            fundingOutput.Global.Should().BeEquivalentTo(expectedGlobal);
        }

        /// <summary>
        /// Return FundingOutputs from the FundingOutput
        /// </summary>
        [Fact(DisplayName = "ProcessFundingOutputs - FundingOutput - Learners Exist"), Trait("FundingOutput Service", "Unit")]
        public void ProcessFundingOutputs_FundingOutput_LearnersExist()
        {
            // ARRANGE
            var fundingOutputService = new FundingOutputService();

            // ACT
            var fundingOutput = fundingOutputService.ProcessFundingOutputs(TestEntities());

            // ASSERT
            fundingOutput.Learners.Should().NotBeNull();
        }

        /// <summary>
        /// Return FundingOutputs from the FundingOutput
        /// </summary>
        [Fact(DisplayName = "ProcessFundingOutputs - FundingOutput - Learners Correct"), Trait("FundingOutput Service", "Unit")]
        public void ProcessFundingOutputs_FundingOutput_LearnersCorrect()
        {
            // ARRANGE
            ILearnerAttribute[] expectedLearners = new LearnerAttribute[]
            {
                 new LearnerAttribute
                 {
                     LearnRefNumber = "TestLearner1",
                     LearnerPeriodisedAttributes = TestLearnerPeriodisedValuesArray(0),
                     LearningDeliveryAttributes = TestLearningDeliveryAttributeArray(1),
                 },
                 new LearnerAttribute
                 {
                     LearnRefNumber = "TestLearner2",
                     LearnerPeriodisedAttributes = TestLearnerPeriodisedValuesArray(1m),
                     LearningDeliveryAttributes = TestLearningDeliveryAttributeArray(1),
                 }
            };

            var fundingOutputService = new FundingOutputService();

            // ACT
            var fundingOutput = fundingOutputService.ProcessFundingOutputs(TestEntities());

            // ASSERT
            fundingOutput.Learners.Should().BeEquivalentTo(expectedLearners);
        }

        /// <summary>
        /// Return FundingOutputs from the FundingOutput
        /// </summary>
        [Fact(DisplayName = "ProcessFundingOutputs - FundingOutput - LearnerAttributes Exist"), Trait("FundingOutput Service", "Unit")]
        public void ProcessFundingOutputs_FundingOutput_LearnerAttributesExist()
        {
            // ARRANGE
            var fundingOutputService = new FundingOutputService();

            // ACT
            var fundingOutput = fundingOutputService.ProcessFundingOutputs(TestEntities());

            // ASSERT
            fundingOutput.Learners.Select(l => l.LearnRefNumber).Should().NotBeNull();
            fundingOutput.Learners.Select(l => l.LearnerPeriodisedAttributes).Should().NotBeNull();
            fundingOutput.Learners.Select(l => l.LearningDeliveryAttributes).Should().NotBeNull();
        }

        /// <summary>
        /// Return FundingOutputs from the FundingOutput
        /// </summary>
        [Fact(DisplayName = "ProcessFundingOutputs - FundingOutput - LearnerAttributes LearnRefNumber"), Trait("FundingOutput Service", "Unit")]
        public void ProcessFundingOutputs_FundingOutput_LearnerAttributes_LearnRefNumber()
        {
            // ARRANGE
            var expectedLearnRefNumbers = new List<string> { "TestLearner1", "TestLearner2" };

            var fundingOutputService = new FundingOutputService();

            // ACT
            var fundingOutput = fundingOutputService.ProcessFundingOutputs(TestEntities());

            // ASSERT
            var learnRefNmbers = fundingOutput.Learners.Select(l => l.LearnRefNumber).ToList();

            expectedLearnRefNumbers.Should().BeEquivalentTo(learnRefNmbers);
        }

        /// <summary>
        /// Return FundingOutputs from the FundingOutput
        /// </summary>
        [Fact(DisplayName = "ProcessFundingOutputs - FundingOutput - LearnerAttributes LearnerPeriodAttributes"), Trait("FundingOutput Service", "Unit")]
        public void ProcessFundingOutputs_FundingOutput_LearnerAttributes_LearnerPeriodAttributes()
        {
            // ARRANGE
            IList<ILearnerPeriodisedAttribute[]> expectedLearnerPeriodisedAttributes = new List<ILearnerPeriodisedAttribute[]>
            {
                TestLearnerPeriodisedValuesArray(0.0m),
                TestLearnerPeriodisedValuesArray(1.0m),
            };

            var fundingOutputService = new FundingOutputService();

            // ACT
            var fundingOutput = fundingOutputService.ProcessFundingOutputs(TestEntities());

            // ASSERT
            var learnerPeriodisedAttributes = fundingOutput.Learners.Select(l => l.LearnerPeriodisedAttributes).ToList();

            expectedLearnerPeriodisedAttributes.Should().BeEquivalentTo(learnerPeriodisedAttributes);
        }

        /// <summary>
        /// Return FundingOutputs from the FundingOutput
        /// </summary>
        [Fact(DisplayName = "ProcessFundingOutputs - FundingOutput - LearnerAttributes LearnerDeliveryAttributes"), Trait("FundingOutput Service", "Unit")]
        public void ProcessFundingOutputs_FundingOutput_LearnerAttributes_LearnerDeliveryAttributes()
        {
            // ARRANGE
            IList<ILearningDeliveryAttribute[]> expectedLearningDeliveryAttributes = new List<ILearningDeliveryAttribute[]>
            {
                TestLearningDeliveryAttributeArray(1),
                TestLearningDeliveryAttributeArray(1),
            };

            var fundingOutputService = new FundingOutputService();

            // ACT
            var fundingOutput = fundingOutputService.ProcessFundingOutputs(TestEntities());

            // ASSERT
            var learningDelAttributes = fundingOutput.Learners.Select(l => l.LearningDeliveryAttributes).ToList();

            expectedLearningDeliveryAttributes.Should().BeEquivalentTo(learningDelAttributes);
        }

        /// <summary>
        /// Return FundingOutputs GlobalAttribute
        /// </summary>
        [Fact(DisplayName = "FundingOutput - GlobalOutput Exists"), Trait("FundingOutput Service", "Unit")]
        public void FundingOutput_GlobalOutput_Exists()
        {
            // ARRANGE
            var fundingOutputService = new FundingOutputService();

            // ACT
            var globalOutput = fundingOutputService.GlobalOutput(GlobalAttributes());

            // ASSERT
            globalOutput.Should().NotBeNull();
        }

        /// <summary>
        /// Return FundingOutputs GlobalAttribute
        /// </summary>
        [Fact(DisplayName = "FundingOutput - GlobalOutput Correct"), Trait("FundingOutput Service", "Unit")]
        public void FundingOutput_GlobalOutput_Correct()
        {
            // ARRANGE
            var fundingOutputService = new FundingOutputService();

            // ACT
            var globalOutput = fundingOutputService.GlobalOutput(GlobalAttributes());

            // ASSERT
            var expectedGlobalOutput = new GlobalAttribute
            {
                UKPRN = 12345678,
                LARSVersion = "Version_005",
                PostcodeAreaCostVersion = "Version_002",
                RulebaseVersion = "1718.5.10",
            };

            expectedGlobalOutput.Should().BeEquivalentTo(globalOutput);
        }

        /// <summary>
        /// Return FundingOutputs LearnerOutput
        /// </summary>
        [Fact(DisplayName = "FundingOutput - LearnerOutput Exists"), Trait("FundingOutput Service", "Unit")]
        public void FundingOutput_LearnerOutput_Exists()
        {
            // ARRANGE
            var fundingOutputService = new FundingOutputService();

            // ACT
            var learnerOutput = fundingOutputService.LearnerOutput(TestLearnerEntity(null, "TestLearner", true));

            // ASSERT
            learnerOutput.Should().NotBeNull();
        }

        /// <summary>
        /// Return FundingOutputs LearnerOutput
        /// </summary>
        [Fact(DisplayName = "FundingOutput - LearnerOutput Correct"), Trait("FundingOutput Service", "Unit")]
        public void FundingOutput_LearnerOutput_Correct()
        {
            // ARRANGE
            var fundingOutputService = new FundingOutputService();

            // ACT
            var learnerOutput = fundingOutputService.LearnerOutput(TestLearnerEntity(null, "TestLearner", true));

            // ASSERT
            var expectedLearnerOutput = new LearnerAttribute[]
            {
                new LearnerAttribute
                {
                    LearnRefNumber = "TestLearner",
                    LearnerPeriodisedAttributes = new LearnerPeriodisedAttribute[]
                    {
                       TestLearnerPeriodisedValues(1.00m),
                    },
                    LearningDeliveryAttributes = new LearningDeliveryAttribute[]
                    {
                        TestLearningDeliveryAttributeValues(1),
                    },
                },
            };
            expectedLearnerOutput.Should().BeEquivalentTo(learnerOutput);
        }

        /// <summary>
        /// Return FundingOutputs LearnerPeriodisedAttribute
        /// </summary>
        [Fact(DisplayName = "FundingOutput - LearnerPeriodisedAttribute Exists"), Trait("FundingOutput Service", "Unit")]
        public void FundingOutput_LearnerPeriodisedAttribute_Exists()
        {
            // ARRANGE
            var fundingOutputService = new FundingOutputService();

            // ACT
            var learnerPeriodisedAttribute = fundingOutputService.LearnerPeriodisedAttributes(TestLearnerEntity(null, "TestLearner", true).SingleOrDefault());

            // ASSERT
            learnerPeriodisedAttribute.Should().NotBeNull();
        }

        /// <summary>
        /// Return FundingOutputs LearnerPeriodisedAttribute
        /// </summary>
        [Fact(DisplayName = "FundingOutput - LearnerPeriodisedAttribute Correct"), Trait("FundingOutput Service", "Unit")]
        public void FundingOutput_LearnerPeriodisedAttribute_Correct()
        {
            // ARRANGE
            var fundingOutputService = new FundingOutputService();

            // ACT
            var learnerPeriodisedAttribute = fundingOutputService.LearnerPeriodisedAttributes(TestLearnerEntity(null, "TestLearner", true).SingleOrDefault());

            // ASSERT
            var expectedLearnerPeriodisedAttribute = new LearnerPeriodisedAttribute[]
            {
                new LearnerPeriodisedAttribute
                {
                    AttributeName = "ALBSeqNum",
                    Period1 = 1.00m,
                    Period2 = 1.00m,
                    Period3 = 1.00m,
                    Period4 = 1.00m,
                    Period5 = 1.00m,
                    Period6 = 1.00m,
                    Period7 = 1.00m,
                    Period8 = 1.00m,
                    Period9 = 1.00m,
                    Period10 = 1.00m,
                    Period11 = 1.00m,
                    Period12 = 1.00m,
                },
            };

            expectedLearnerPeriodisedAttribute.Should().BeEquivalentTo(learnerPeriodisedAttribute);
        }

        /// <summary>
        /// Return FundingOutputs LearningDeliveryAttributes
        /// </summary>
        [Fact(DisplayName = "FundingOutput - LearningDeliveryAttributes Exists"), Trait("FundingOutput Service", "Unit")]
        public void FundingOutput_LearningDeliveryAttributes_Exists()
        {
            // ARRANGE
            var fundingOutputService = new FundingOutputService();

            // ACT
            var learningDeliveryAttributes = fundingOutputService.LearningDeliveryAttributes(TestLearnerEntity(null, "TestLearner", true).SingleOrDefault());

            // ASSERT
            learningDeliveryAttributes.Should().NotBeNull();
        }

        /// <summary>
        /// Return FundingOutputs LearningDeliveryAttributes
        /// </summary>
        [Fact(DisplayName = "FundingOutput - LearningDeliveryAttributes Correct"), Trait("FundingOutput Service", "Unit")]
        public void FundingOutput_LearningDeliveryAttributes_Correct()
        {
            // ARRANGE
            var fundingOutputService = new FundingOutputService();

            // ACT
            var learningDeliveryAttributes = fundingOutputService.LearningDeliveryAttributes(TestLearnerEntity(null, "TestLearner", true).SingleOrDefault());

            // ASSERT
            var expectedLearningDeliveryAttributes = new LearningDeliveryAttribute[]
            {
                new LearningDeliveryAttribute
                {
                    AimSeqNumber = 1,
                    LearningDeliveryAttributeDatas = TestLearningDeliveryAttributeValues(1).LearningDeliveryAttributeDatas,
                    LearningDeliveryPeriodisedAttributes = TestLearningDeliveryAttributeValues(1).LearningDeliveryPeriodisedAttributes,
                },
            };

            expectedLearningDeliveryAttributes.Should().BeEquivalentTo(learningDeliveryAttributes);
        }

        /// <summary>
        /// Return FundingOutputs LearningDeliveryAttributeDatas
        /// </summary>
        [Fact(DisplayName = "FundingOutput - LearningDeliveryAttributeDatas Exists"), Trait("FundingOutput Service", "Unit")]
        public void FundingOutput_LearningDeliveryAttributeDatas_Exists()
        {
            // ARRANGE
            var fundingOutputService = new FundingOutputService();

            // ACT
            var learningDeliveryAttributeDatas =
                fundingOutputService.LearningDeliveryAttributeData(TestLearnerEntity(null, "TestLearner", true).SingleOrDefault().Children.SingleOrDefault());

            // ASSERT
            learningDeliveryAttributeDatas.Should().NotBeNull();
        }

        /// <summary>
        /// Return FundingOutputs LearningDeliveryAttributeDatas
        /// </summary>
        [Fact(DisplayName = "FundingOutput - LearningDeliveryAttributeDatas Correct"), Trait("FundingOutput Service", "Unit")]
        public void FundingOutput_LearningDeliveryAttributeDatas_Correct()
        {
            // ARRANGE
            var fundingOutputService = new FundingOutputService();

            // ACT
            var learningDeliveryAttributeDatas =
                fundingOutputService.LearningDeliveryAttributeData(TestLearnerEntity(null, "TestLearner", true).SingleOrDefault().Children.SingleOrDefault());

            // ASSERT
            var expectedlearningDeliveryAttributeDatas = TestLearningDeliveryAttributeData();

            expectedlearningDeliveryAttributeDatas.Should().BeEquivalentTo(learningDeliveryAttributeDatas);
        }

        /// <summary>
        /// Return FundingOutputs LearningDeliveryPeriodisedAttributeData
        /// </summary>
        [Fact(DisplayName = "FundingOutput - LearningDeliveryPeriodisedAttributeData Exists"), Trait("FundingOutput Service", "Unit")]
        public void FundingOutput_LearningDeliveryPeriodisedAttributeData_Exists()
        {
            // ARRANGE
            var fundingOutputService = new FundingOutputService();

            // ACT
            var learningDeliveryPeriodisedAttributeData =
                fundingOutputService.LearningDeliveryPeriodisedAttributeData(TestLearnerEntity(null, "TestLearner", true).SingleOrDefault().Children.SingleOrDefault());

            // ASSERT
            learningDeliveryPeriodisedAttributeData.Should().NotBeNull();
        }

        /// <summary>
        /// Return FundingOutputs LearningDeliveryPeriodisedAttributeData
        /// </summary>
        [Fact(DisplayName = "FundingOutput - LearningDeliveryPeriodisedAttributeData Correct"), Trait("FundingOutput Service", "Unit")]
        public void FundingOutput_LearningDeliveryPeriodisedAttributeData_Correct()
        {
            // ARRANGE
            var fundingOutputService = new FundingOutputService();

            // ACT
            var learningDeliveryPeriodisedAttributeData =
                fundingOutputService.LearningDeliveryPeriodisedAttributeData(TestLearnerEntity(null, "TestLearner", true).SingleOrDefault().Children.SingleOrDefault());

            // ASSERT
            var expectedLearningDeliveryPeriodisedAttributeData = TestLearningDeliveryPeriodisedAttributesDataArray();

            expectedLearningDeliveryPeriodisedAttributeData.Should().BeEquivalentTo(learningDeliveryPeriodisedAttributeData);
        }

        #region Test Helpers

        private static readonly IFormatProvider culture = new CultureInfo("en-GB", true);

        private static readonly Mock<IFundingService> FundingServiceContextMock = new Mock<IFundingService>();

        private IEnumerable<IDataEntity> TestEntities()
        {
            var entities = new List<DataEntity>();

            var entity1 =
                new DataEntity("global")
                {
                    EntityName = "global",
                    Attributes = GlobalAttributes(),
                    Parent = null,
                };

            entity1.AddChildren(TestLearnerEntity(entity1, "TestLearner1", false));

            entities.Add(entity1);

            var entity2 =
            new DataEntity("global")
            {
                EntityName = "global",
                Attributes = GlobalAttributes(),
                Parent = null,
            };

            entity1.AddChildren(TestLearnerEntity(entity2, "TestLearner2", true));

            entities.Add(entity2);

            return entities;
        }

        private IEnumerable<IDataEntity> TestLearnerEntity(DataEntity parent, string learnRefNumber, bool includeALBChangePoint)
        {
            var entities = new List<DataEntity>();
            if (includeALBChangePoint)
            {
                var entity1 = new DataEntity("Learner")
                {
                    EntityName = "Learner",
                    Attributes = new Dictionary<string, IAttributeData>
                    {
                        { "LearnRefNumber", new AttributeData("LearnRefNumber", learnRefNumber) },
                        { "ALBSeqNum", Attribute("ALBSeqNum", true, 1.0m) },
                    },
                    Parent = parent
                };

                entity1.AddChildren(TestLearningDeliveryEntity(entity1));

                entities.Add(entity1);

                return entities;
            }

            var entity2 = new DataEntity("Learner")
            {
                EntityName = "Learner",
                Attributes = new Dictionary<string, IAttributeData>
                {
                    { "LearnRefNumber", new AttributeData("LearnRefNumber", learnRefNumber) },
                    { "ALBSeqNum", Attribute("ALBSeqNum", false, 0m) },
                },
                Parent = parent
            };

            entity2.AddChildren(TestLearningDeliveryEntity(entity2));

            entities.Add(entity2);

            return entities;
        }

        private IEnumerable<IDataEntity> TestLearningDeliveryEntity(DataEntity parent)
        {
            var entities = new List<DataEntity>();

            var entity = new DataEntity("LearningDelivery")
            {
                EntityName = "LearningDelivery",
                Attributes = new Dictionary<string, IAttributeData>
                {
                    { "AimSeqNumber", Attribute("AimSeqNumber", false, "1.0") },
                    { "Achieved", Attribute("Achieved", false, "false") },
                    { "ActualNumInstalm", Attribute("ActualNumInstalm", false, "21.0") },
                    { "AdvLoan", Attribute("AdvLoan", false, "true") },
                    { "ApplicFactDate", Attribute("ApplicFactDate", false, "30/04/2017 00:00:00") },
                    { "ApplicProgWeightFact", Attribute("ApplicProgWeightFact", false, "A") },
                    { "AreaCostFactAdj", Attribute("AreaCostFactAdj", false, "0.1") },
                    { "AreaCostInstalment", Attribute("AreaCostInstalment", false, "21.525") },
                    { "FundLine", Attribute("FundLine", false, "Advanced Learner Loans Bursary") },
                    { "FundStart", Attribute("FundStart", false, "true") },
                    { "LiabilityDate", Attribute("LiabilityDate", false, "14/05/2017 00:00:00") },
                    { "LoanBursAreaUplift", Attribute("LoanBursAreaUplift", false, "true") },
                    { "LoanBursSupp", Attribute("LoanBursSupp", false, "true") },
                    { "OutstndNumOnProgInstalm", Attribute("OutstndNumOnProgInstalm", false, "0.0") },
                    { "PlannedNumOnProgInstalm", Attribute("PlannedNumOnProgInstalm", false, "12.0") },
                    { "WeightedRate", Attribute("WeightedRate", false, "2583") },
                    { "ALBCode", Attribute("ALBCode", true, 100.0m) },
                    { "ALBSupportPayment", Attribute("ALBSupportPayment", true, 100.0m) },
                    { "AreaUpliftBalPayment", Attribute("AreaUpliftBalPayment", true, 100.0m) },
                    { "AreaUpliftOnProgPayment", Attribute("AreaUpliftOnProgPayment", true, 100.0m) },
                },
                Parent = parent,
            };

            entities.Add(entity);

            return entities;
        }

        private new Dictionary<string, IAttributeData> GlobalAttributes()
        {
            return new Dictionary<string, IAttributeData>
            {
                { "UKPRN", new AttributeData("UKPRN", "12345678.0") },
                { "LARSVersion", new AttributeData("LARSVersion", "Version_005") },
                { "PostcodeAreaCostVersion", new AttributeData("PostcodeAreaCostVersion", "Version_002") },
                { "RulebaseVersion", new AttributeData("RulebaseVersion", "1718.5.10") },
            };
        }

        private IAttributeData Attribute(string attributeName, bool hasChangePoints, object attributeValue)
        {
            if (hasChangePoints)
            {
                var attribute = new AttributeData(attributeName, null);
                attribute.AddChangepoints(ChangePoints(decimal.Parse(attributeValue.ToString())));

                return attribute;
            }

            return new AttributeData(attributeName, attributeValue);
        }

        private IEnumerable<ITemporalValueItem> ChangePoints(decimal value)
        {
            var changePoints = new List<TemporalValueItem>();

            IEnumerable<TemporalValueItem> cps = new List<TemporalValueItem>
            {
                 new TemporalValueItem(new DateTime(2017, 08, 01), value, null),
                 new TemporalValueItem(new DateTime(2017, 09, 01), value, null),
                 new TemporalValueItem(new DateTime(2017, 10, 01), value, null),
                 new TemporalValueItem(new DateTime(2017, 11, 01), value, null),
                 new TemporalValueItem(new DateTime(2017, 12, 01), value, null),
                 new TemporalValueItem(new DateTime(2018, 01, 01), value, null),
                 new TemporalValueItem(new DateTime(2018, 02, 01), value, null),
                 new TemporalValueItem(new DateTime(2018, 03, 01), value, null),
                 new TemporalValueItem(new DateTime(2018, 04, 01), value, null),
                 new TemporalValueItem(new DateTime(2018, 05, 01), value, null),
                 new TemporalValueItem(new DateTime(2018, 06, 01), value, null),
                 new TemporalValueItem(new DateTime(2018, 07, 01), value, null),
            };

            changePoints.AddRange(cps);

            return changePoints;
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
            return int.Parse(valueInt);
        }

        private readonly IList<ILearner> testLearners = new[]
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
                        LearnActEndDate = DateTime.Parse("2018-06-30", culture),
                        LearnStartDate = DateTime.Parse("2017-08-30", culture),
                        LearnPlanEndDate = DateTime.Parse("2018-07-30", culture),
                        OrigLearnStartDateSpecified = true,
                        OrigLearnStartDate = DateTime.Parse("2017-08-30", culture),
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
                                LearnDelFAMDateFrom = DateTime.Parse("2017-08-30", culture),
                                LearnDelFAMDateToSpecified = true,
                                LearnDelFAMDateTo = DateTime.Parse("2017-10-31", culture)
                            },
                            new MessageLearnerLearningDeliveryLearningDeliveryFAM
                            {
                                LearnDelFAMCode = "100",
                                LearnDelFAMType = "SOF",
                                LearnDelFAMDateFromSpecified = true,
                                LearnDelFAMDateFrom = DateTime.Parse("2017-10-31", culture),
                                LearnDelFAMDateToSpecified = true,
                                LearnDelFAMDateTo = DateTime.Parse("2017-11-30", culture)
                            },
                            new MessageLearnerLearningDeliveryLearningDeliveryFAM
                            {
                                LearnDelFAMCode = "1",
                                LearnDelFAMType = "RES",
                                LearnDelFAMDateFromSpecified = true,
                                LearnDelFAMDateFrom = DateTime.Parse("2017-12-01", culture),
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
                        LearnActEndDate = DateTime.Parse("2018-06-30", culture),
                        LearnStartDate = DateTime.Parse("2017-08-30", culture),
                        LearnPlanEndDate = DateTime.Parse("2018-07-30", culture),
                        OrigLearnStartDateSpecified = true,
                        OrigLearnStartDate = DateTime.Parse("2017-08-30", culture),
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
                                LearnDelFAMDateFrom = DateTime.Parse("2017-08-30", culture),
                                LearnDelFAMDateToSpecified = true,
                                LearnDelFAMDateTo = DateTime.Parse("2017-10-31", culture)
                            },
                            new MessageLearnerLearningDeliveryLearningDeliveryFAM
                            {
                                LearnDelFAMCode = "100",
                                LearnDelFAMType = "SOF",
                                LearnDelFAMDateFromSpecified = true,
                                LearnDelFAMDateFrom = DateTime.Parse("2017-10-31", culture),
                                LearnDelFAMDateToSpecified = true,
                                LearnDelFAMDateTo = DateTime.Parse("2017-11-30", culture)
                            },
                            new MessageLearnerLearningDeliveryLearningDeliveryFAM
                            {
                                LearnDelFAMCode = "1",
                                LearnDelFAMType = "RES",
                                LearnDelFAMDateFromSpecified = true,
                                LearnDelFAMDateFrom = DateTime.Parse("2017-12-01", culture),
                                LearnDelFAMDateToSpecified = false
                            }
                        }
                    }
                }
            }
        };

        private ILearnerPeriodisedAttribute[] TestLearnerPeriodisedValuesArray(decimal value)
        {
            return new LearnerPeriodisedAttribute[]
            {
                TestLearnerPeriodisedValues(value)
            };
        }

        private LearnerPeriodisedAttribute TestLearnerPeriodisedValues(decimal value)
        {
            return new LearnerPeriodisedAttribute
            {
                AttributeName = "ALBSeqNum",
                Period1 = value,
                Period2 = value,
                Period3 = value,
                Period4 = value,
                Period5 = value,
                Period6 = value,
                Period7 = value,
                Period8 = value,
                Period9 = value,
                Period10 = value,
                Period11 = value,
                Period12 = value,
            };
        }

        private ILearningDeliveryAttribute[] TestLearningDeliveryAttributeArray(int aimSeq)
        {
            return new LearningDeliveryAttribute[]
            {
                TestLearningDeliveryAttributeValues(1)
            };
        }

        private LearningDeliveryAttribute TestLearningDeliveryAttributeValues(int aimSeq)
        {
            return new LearningDeliveryAttribute
            {
                AimSeqNumber = aimSeq,
                LearningDeliveryAttributeDatas = TestLearningDeliveryAttributeData(),
                LearningDeliveryPeriodisedAttributes = TestLearningDeliveryPeriodisedAttributesDataArray(),
            };
        }

        private ILearningDeliveryAttributeData TestLearningDeliveryAttributeData()
        {
            return new LearningDeliveryAttributeData
            {
                Achieved = false,
                ActualNumInstalm = 21,
                AdvLoan = true,
                ApplicFactDate = DateTime.Parse("30/04/2017 00:00:00", culture),
                ApplicProgWeightFact = "A",
                AreaCostFactAdj = 0.1m,
                AreaCostInstalment = 21.525m,
                FundLine = "Advanced Learner Loans Bursary",
                FundStart = true,
                LiabilityDate = DateTime.Parse("14/05/2017 00:00:00", culture),
                LoanBursAreaUplift = true,
                LoanBursSupp = true,
                OutstndNumOnProgInstalm = 0,
                PlannedNumOnProgInstalm = 12,
                WeightedRate = 2583.0m,
            };
        }

        private ILearningDeliveryPeriodisedAttribute[] TestLearningDeliveryPeriodisedAttributesDataArray()
        {
            return new LearningDeliveryPeriodisedAttribute[]
            {
                TestLearningDeliveryPeriodisedAttributesData("ALBCode", 100.00m),
                TestLearningDeliveryPeriodisedAttributesData("ALBSupportPayment", 100.00m),
                TestLearningDeliveryPeriodisedAttributesData("AreaUpliftBalPayment", 100.00m),
                TestLearningDeliveryPeriodisedAttributesData("AreaUpliftOnProgPayment", 100.00m),
            };
        }

        private LearningDeliveryPeriodisedAttribute TestLearningDeliveryPeriodisedAttributesData(string attribute, decimal value)
        {
            return new LearningDeliveryPeriodisedAttribute
            {
                AttributeName = attribute,
                Period1 = value,
                Period2 = value,
                Period3 = value,
                Period4 = value,
                Period5 = value,
                Period6 = value,
                Period7 = value,
                Period8 = value,
                Period9 = value,
                Period10 = value,
                Period11 = value,
                Period12 = value,
            };
        }

        #endregion
    }
}
