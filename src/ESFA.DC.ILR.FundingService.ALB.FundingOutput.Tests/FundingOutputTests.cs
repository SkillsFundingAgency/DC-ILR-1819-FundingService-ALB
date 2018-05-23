using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using ESFA.DC.ILR.FundingService.ALB.FundingOutput.Model;
using ESFA.DC.ILR.FundingService.ALB.FundingOutput.Model.Attribute;
using ESFA.DC.ILR.FundingService.ALB.FundingOutput.Service;
using ESFA.DC.ILR.FundingService.ALB.Service.Interface;
using ESFA.DC.ILR.Model;
using ESFA.DC.ILR.Model.Interface;
using ESFA.DC.IO.Dictionary;
using ESFA.DC.IO.Interfaces;
using ESFA.DC.JobContext;
using ESFA.DC.JobContext.Interface;
using ESFA.DC.OPA.Model;
using ESFA.DC.OPA.Model.Interface;
using ESFA.DC.Serialization.Interfaces;
using ESFA.DC.Serialization.Json;
using FluentAssertions;
using Moq;
using Xunit;

namespace ESFA.DC.ILR.FundingService.ALB.FundingOutput.Tests
{
    public class FundingOutputTests
    {
        /// <summary>
        /// Return DataEntities from the Funding Service
        /// </summary>
        [Fact(DisplayName = "DataEntity - Data Entity Exists"), Trait("Funding Output", "Unit")]
        public void ProcessFunding_Entity_Exists()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var dataEntity = FundingServiceMock().Object.ProcessFunding(12345678, testLearners);

            // ASSERT
            dataEntity.Should().NotBeNull();
        }

        /// <summary>
        /// Return DataEntities from the Funding Service
        /// </summary>
        [Fact(DisplayName = "DataEntity - Data Entity Count"), Trait("Funding Output", "Unit")]
        public void ProcessFunding_Entity_Count()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var dataEntity = FundingServiceMock().Object.ProcessFunding(12345678, testLearners);

            // ASSERT
            dataEntity.Count().Should().Be(2);
        }

        /// <summary>
        /// Return DataEntities from the Funding Service
        /// </summary>
        [Fact(DisplayName = "DataEntity - Data Entity - Learners Correct"), Trait("Funding Output", "Unit")]
        public void ProcessFunding_Entity_LearnersCorrect()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var dataEntity = FundingServiceMock().Object.ProcessFunding(12345678, testLearners);

            // ASSERT
            var learners = dataEntity.SelectMany(g => g.Children.Select(l => l.LearnRefNumber)).ToList();

            learners.Should().BeEquivalentTo(new List<string> { "TestLearner1", "TestLearner2" });
        }

        /// <summary>
        /// Return DataEntities from the Funding Service
        /// </summary>
        [Fact(DisplayName = "DataEntity - Data Entity - ChangePoints Correct"), Trait("Funding Output", "Unit")]
        public void ProcessFunding_Entity_ChangePointsCorrect()
        {
            // ARRANGE
            var expectedChangePoints = new List<ITemporalValueItem>();

            expectedChangePoints.AddRange(ChangePoints(100.00m));
            expectedChangePoints.AddRange(ChangePoints(100.00m));
            expectedChangePoints.AddRange(ChangePoints(100.00m));
            expectedChangePoints.AddRange(ChangePoints(100.00m));
            expectedChangePoints.AddRange(ChangePoints(100.00m));
            expectedChangePoints.AddRange(ChangePoints(100.00m));
            expectedChangePoints.AddRange(ChangePoints(100.00m));
            expectedChangePoints.AddRange(ChangePoints(100.00m));

            // ACT
            var dataEntity = FundingServiceMock().Object.ProcessFunding(12345678, testLearners);

            // ASSERT
            var actualChangePoints = new List<ITemporalValueItem>();
            var actualAttributes = dataEntity.SelectMany(g => g.Children.SelectMany(l => l.Children.SelectMany(ld => ld.Attributes.Select(a => a.Value).Where(c => c.Changepoints.Count() > 0))));

            foreach (var attribute in actualAttributes)
            {
                actualChangePoints.AddRange(attribute.Changepoints);
            }

            expectedChangePoints.Should().BeEquivalentTo(actualChangePoints);
        }

        /// <summary>
        /// Return FundingOutputs from the FundingOutput
        /// </summary>
        [Fact(DisplayName = "Transform - FundingOutput - Exists"), Trait("Funding Output", "Unit")]
        public void Transform_FundingOutput_Exists()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var fundingOutput = TestFundingOutputs();

            // ASSERT
            fundingOutput.Should().NotBeNull();
        }

        /// <summary>
        /// Return FundingOutputs from the FundingOutput
        /// </summary>
        [Fact(DisplayName = "Transform - FundingOutput - Global Exists"), Trait("Funding Output", "Unit")]
        public void Transform_FundingOutput_GlobalExists()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var fundingOutput = TestFundingOutputs();

            // ASSERT
            fundingOutput.Global.Should().NotBeNull();
        }

        /// <summary>
        /// Return FundingOutputs from the FundingOutput
        /// </summary>
        [Fact(DisplayName = "Transform - FundingOutput - Global Correct"), Trait("Funding Output", "Unit")]
        public void Transform_FundingOutput_GlobalCorrect()
        {
            // ARRANGE
            var expectedGlobal = new GlobalAttribute
            {
                UKPRN = 12345678,
                LARSVersion = "Version_005",
                PostcodeAreaCostVersion = "Version_002",
                RulebaseVersion = "1718.5.10",
            };

            // ACT
            var fundingOutput = TestFundingOutputs();

            // ASSERT
            fundingOutput.Global.Should().BeEquivalentTo(expectedGlobal);

            ISerializationService serializationService = new JsonSerializationService();

            var str = serializationService.Serialize<FundingOutputs>(fundingOutput);
        }

        /// <summary>
        /// Return FundingOutputs from the FundingOutput
        /// </summary>
        [Fact(DisplayName = "Transform - FundingOutput - Learners Exist"), Trait("Funding Output", "Unit")]
        public void Transform_FundingOutput_LearnersExist()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var fundingOutput = TestFundingOutputs();

            // ASSERT
            fundingOutput.Learners.Should().NotBeNull();
        }

        /// <summary>
        /// Return FundingOutputs from the FundingOutput
        /// </summary>
        [Fact(DisplayName = "Transform - FundingOutput - Learners Correct"), Trait("Funding Output", "Unit")]
        public void Transform_FundingOutput_LearnersCorrect()
        {
            // ARRANGE
            var expectedLearners = new LearnerAttribute[]
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

            // ACT
            var fundingOutput = TestFundingOutputs();

            // ASSERT
            fundingOutput.Learners.Should().BeEquivalentTo(expectedLearners);

            ISerializationService serializationService = new JsonSerializationService();

            var str = serializationService.Serialize<FundingOutputs>(fundingOutput);
        }

        /// <summary>
        /// Return FundingOutputs from the FundingOutput
        /// </summary>
        [Fact(DisplayName = "Transform - FundingOutput - LearnerAttributes Exist"), Trait("Funding Output", "Unit")]
        public void Transform_FundingOutput_LearnerAttributesExist()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var fundingOutput = TestFundingOutputs();

            // ASSERT
            fundingOutput.Learners.Select(l => l.LearnRefNumber).Should().NotBeNull();
            fundingOutput.Learners.Select(l => l.LearnerPeriodisedAttributes).Should().NotBeNull();
            fundingOutput.Learners.Select(l => l.LearningDeliveryAttributes).Should().NotBeNull();
        }

        /// <summary>
        /// Return FundingOutputs from the FundingOutput
        /// </summary>
        [Fact(DisplayName = "Transform - FundingOutput - LearnerAttributes LearnRefNumber"), Trait("Funding Output", "Unit")]
        public void Transform_FundingOutput_LearnerAttributes_LearnRefNumber()
        {
            // ARRANGE
            var expectedLearnRefNumbers = new List<string> { "TestLearner1", "TestLearner2" };

            // ACT
            var fundingOutput = TestFundingOutputs();

            // ASSERT
            var learnRefNmbers = fundingOutput.Learners.Select(l => l.LearnRefNumber).ToList();

            expectedLearnRefNumbers.Should().BeEquivalentTo(learnRefNmbers);
        }

        /// <summary>
        /// Return FundingOutputs from the FundingOutput
        /// </summary>
        [Fact(DisplayName = "Transform - FundingOutput - LearnerAttributes LearnerPeriodAttributes"), Trait("Funding Output", "Unit")]
        public void Transform_FundingOutput_LearnerAttributes_LearnerPeriodAttributes()
        {
            // ARRANGE
            var expectedLearnerPeriodisedAttributes = new List<LearnerPeriodisedAttribute[]>
            {
                TestLearnerPeriodisedValuesArray(0.0m),
                TestLearnerPeriodisedValuesArray(1.0m),
            };

            // ACT
            var fundingOutput = TestFundingOutputs();

            // ASSERT
            var learnerPeriodisedAttributes = fundingOutput.Learners.Select(l => l.LearnerPeriodisedAttributes).ToList();

            expectedLearnerPeriodisedAttributes.Should().BeEquivalentTo(learnerPeriodisedAttributes);
        }

        /// <summary>
        /// Return FundingOutputs from the FundingOutput
        /// </summary>
        [Fact(DisplayName = "Transform - FundingOutput - LearnerAttributes LearnerDeliveryAttributes"), Trait("Funding Output", "Unit")]
        public void Transform_FundingOutput_LearnerAttributes_LearnerDeliveryAttributes()
        {
            // ARRANGE
            var expectedLearningDeliveryAttributes = new List<LearningDeliveryAttribute[]>
            {
                TestLearningDeliveryAttributeArray(1),
                TestLearningDeliveryAttributeArray(1),
            };

            // ACT
            var fundingOutput = TestFundingOutputs();

            // ASSERT
            var learningDelAttributes = fundingOutput.Learners.Select(l => l.LearningDeliveryAttributes).ToList();

            expectedLearningDeliveryAttributes.Should().BeEquivalentTo(learningDelAttributes);
        }

        #region Test Helpers

        private static readonly IFormatProvider culture = new CultureInfo("en-GB", true);

        private static readonly Mock<IFundingService> FundingServiceContextMock = new Mock<IFundingService>();

        private Mock<IFundingService> FundingServiceMock()
        {
            FundingServiceContextMock.Setup(x => x.ProcessFunding(12345678, testLearners)).Returns(ProcessFundingMock());

            return FundingServiceContextMock;
        }

        private IEnumerable<IDataEntity> ProcessFundingMock()
        {
            var entities = new List<DataEntity>();

            var entity1 =
                new DataEntity("global")
                {
                    EntityName = "global",
                    Attributes = new Dictionary<string, IAttributeData>
                    {
                        { "UKPRN", new AttributeData("UKPRN", "12345678.0") },
                        { "LARSVersion", new AttributeData("LARSVersion", "Version_005") },
                        { "PostcodeAreaCostVersion", new AttributeData("PostcodeAreaCostVersion", "Version_002") },
                        { "RulebaseVersion", new AttributeData("RulebaseVersion", "1718.5.10") },
                    },
                    Parent = null,
                };

            entity1.AddChildren(TestLearnerEntity(entity1, "TestLearner1", false));

            entities.Add(entity1);

            var entity2 =
            new DataEntity("global")
            {
                EntityName = "global",
                Attributes = new Dictionary<string, IAttributeData>
                {
                    { "UKPRN", new AttributeData("UKPRN", "12345678.0") },
                    { "LARSVersion", new AttributeData("LARSVersion", "Version_005") },
                    { "PostcodeAreaCostVersion", new AttributeData("PostcodeAreaCostVersion", "Version_002") },
                    { "RulebaseVersion", new AttributeData("RulebaseVersion", "1718.5.10") },
                },
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

        private FundingOutputs TestFundingOutputs()
        {
            var dataEntities = FundingServiceMock().Object.ProcessFunding(12345678, testLearners);

            var output = new FundingOutputTransform(dataEntities);
            return output.Transform();
        }

        private LearnerPeriodisedAttribute[] TestLearnerPeriodisedValuesArray(decimal value)
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

        private LearningDeliveryAttribute[] TestLearningDeliveryAttributeArray(int aimSeq)
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
                LearningDeliveryAttributeDatas = LearningDeliveryAttributeData(),
                LearningDeliveryPeriodisedAttributes = LearningDeliveryPeriodisedAttributesDataArray(),
            };
        }

        private LearningDeliveryAttributeData LearningDeliveryAttributeData()
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

        private LearningDeliveryPeriodisedAttribute[] LearningDeliveryPeriodisedAttributesDataArray()
        {
            return new LearningDeliveryPeriodisedAttribute[]
            {
                LearningDeliveryPeriodisedAttributesData("ALBCode", 100.00m),
                LearningDeliveryPeriodisedAttributesData("ALBSupportPayment", 100.00m),
                LearningDeliveryPeriodisedAttributesData("AreaUpliftBalPayment", 100.00m),
                LearningDeliveryPeriodisedAttributesData("AreaUpliftOnProgPayment", 100.00m),
            };
        }

        private LearningDeliveryPeriodisedAttribute LearningDeliveryPeriodisedAttributesData(string attribute, decimal value)
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
