using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using ESFA.DC.ILR.FundingService.ALB.Service.Interface;
using ESFA.DC.ILR.Model;
using ESFA.DC.ILR.Model.Interface;
using ESFA.DC.IO.Dictionary;
using ESFA.DC.IO.Interfaces;
using ESFA.DC.JobContext;
using ESFA.DC.JobContext.Interface;
using ESFA.DC.OPA.Model;
using ESFA.DC.OPA.Model.Interface;

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

            foreach (var temporal in ChangePoints())
            {
                expectedChangePoints.Add(temporal);
            }

            foreach (var temporal in ChangePoints())
            {
                expectedChangePoints.Add(temporal);
            }

            // ACT
            var dataEntity = FundingServiceMock().Object.ProcessFunding(12345678, testLearners);

            // ASSERT
            var actualChangePoints = new List<ITemporalValueItem>();
            var actualAttributes = dataEntity.SelectMany(g => g.Children.SelectMany(l => l.Children.SelectMany(ld => ld.Attributes.Select(a => a.Value))));

            foreach (var attribute in actualAttributes)
            {
                actualChangePoints.AddRange(attribute.Changepoints);
            }

            expectedChangePoints.Should().BeEquivalentTo(actualChangePoints);
        }

        #region Test Helpers

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
                        { "UKPRN", new AttributeData("UKPRN", 12345678) },
                        { "LARSVersion", new AttributeData("LARSVersion", "Version_005") },
                    },
                    Parent = null,
                };

            entity1.AddChildren(TestLearnerEntity(entity1, "TestLearner1"));

            entities.Add(entity1);

            var entity2 =
            new DataEntity("global")
            {
                EntityName = "global",
                Attributes = new Dictionary<string, IAttributeData>
                {
                                { "UKPRN", new AttributeData("UKPRN", 12345678) },
                                { "LARSVersion", new AttributeData("LARSVersion", "Version_005") },
                },
                Parent = null,
            };

            entity1.AddChildren(TestLearnerEntity(entity2, "TestLearner2"));

            entities.Add(entity2);

            return entities;
        }

        private IEnumerable<IDataEntity> TestLearnerEntity(DataEntity parent, string learnRefNumber)
        {
            var entities = new List<DataEntity>();

            var entity = new DataEntity("Learner")
            {
                EntityName = "Learner",
                Attributes = new Dictionary<string, IAttributeData>
                {
                    { "LearnRefNumber", new AttributeData("LearnRefNumber", learnRefNumber) },
                },
                Parent = parent
            };

            entity.AddChildren(TestLearningDeliveryEntity(entity));

            entities.Add(entity);

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
                        { "Payment", Attribute("Payment", true) },
                        { "Non-Payment",  Attribute("Non-Payment", false) },
                    },
                Parent = parent,
            };

            entities.Add(entity);

            return entities;
        }

        private IAttributeData Attribute(string attributeName, bool hasChangePoints)
        {
            var attribute = new AttributeData(attributeName, null);

            if (hasChangePoints)
            {
                attribute.AddChangepoints(ChangePoints());
            }

            return attribute;
        }

        private IEnumerable<ITemporalValueItem> ChangePoints()
        {
            var changePoints = new List<TemporalValueItem>();

            IEnumerable<TemporalValueItem> cps = new List<TemporalValueItem>
            {
                 new TemporalValueItem(new DateTime(2017, 08, 01), 1.5m, null),
                 new TemporalValueItem(new DateTime(2017, 09, 01), 1.5m, null),
                 new TemporalValueItem(new DateTime(2017, 10, 01), 1.5m, null),
                 new TemporalValueItem(new DateTime(2017, 11, 01), 1.5m, null),
                 new TemporalValueItem(new DateTime(2017, 12, 01), 1.5m, null),
                 new TemporalValueItem(new DateTime(2018, 01, 01), 1.5m, null),
                 new TemporalValueItem(new DateTime(2018, 02, 01), 1.5m, null),
                 new TemporalValueItem(new DateTime(2018, 03, 01), 1.5m, null),
                 new TemporalValueItem(new DateTime(2018, 04, 01), 1.5m, null),
                 new TemporalValueItem(new DateTime(2018, 05, 01), 1.5m, null),
                 new TemporalValueItem(new DateTime(2018, 06, 01), 1.5m, null),
                 new TemporalValueItem(new DateTime(2018, 07, 01), 1.5m, null),
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

        #endregion
    }
}
