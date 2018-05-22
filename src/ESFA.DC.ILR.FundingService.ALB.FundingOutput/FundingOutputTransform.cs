using System;
using System.Collections.Generic;
using System.Linq;
using ESFA.DC.ILR.FundingService.ALB.FundingOutput.Model;
using ESFA.DC.ILR.FundingService.ALB.FundingOutput.Model.Attribute;
using ESFA.DC.OPA.Model;
using ESFA.DC.OPA.Model.Interface;

namespace ESFA.DC.ILR.FundingService.ALB.FundingOutput
{
    public class FundingOutputTransform
    {
        private readonly IEnumerable<IDataEntity> _dataEntities;

        public FundingOutputTransform(IEnumerable<IDataEntity> dataEntities)
        {
            _dataEntities = dataEntities;
        }

        private static Dictionary<int, DateTime> Periods => new Dictionary<int, DateTime>
        {
           { 1, new DateTime(2017, 08, 01) },
           { 2, new DateTime(2017, 09, 01) },
           { 3, new DateTime(2017, 10, 01) },
           { 4, new DateTime(2017, 11, 01) },
           { 5, new DateTime(2017, 12, 01) },
           { 6, new DateTime(2018, 01, 01) },
           { 7, new DateTime(2018, 02, 01) },
           { 8, new DateTime(2018, 03, 01) },
           { 9, new DateTime(2018, 04, 01) },
           { 10, new DateTime(2018, 05, 01) },
           { 11, new DateTime(2018, 06, 01) },
           { 12, new DateTime(2018, 07, 01) },
        };

        public FundingOutputs Transform()
        {
            FundingOutputs fundingOutputs = new FundingOutputs();

            fundingOutputs.global = GlobalOutput(_dataEntities.Select(g => g.Attributes.Select(a => a.Value)).First());

            fundingOutputs.learners = LearnerOutput(_dataEntities.SelectMany(g => g.Children));

            return fundingOutputs;
        }

        private GlobalAttribute GlobalOutput(IEnumerable<IAttributeData> attributes)
        {
           return new GlobalAttribute
           {
               UKPRN = int.Parse(attributes.Where(n => n.Name == "UKPRN").Select(v => v.Value).SingleOrDefault().ToString()),
               LARSVersion = attributes.Where(n => n.Name == "LARSVersion").Select(v => v.Value).SingleOrDefault().ToString(),
               PostcodeAreaCostVersion = attributes.Where(n => n.Name == "PostcodeAreaCostVersion").Select(v => v.Value).SingleOrDefault().ToString(),
               RulebaseVersion = attributes.Where(n => n.Name == "RulebaseVersion").Select(v => v.Value).SingleOrDefault().ToString(),
           };
        }

        private LearnerAttribute[] LearnerOutput(IEnumerable<IDataEntity> learnerEntities)
        {
            var learners = new List<LearnerAttribute>();

            foreach (var learner in learnerEntities)
            {
                learners.Add(new LearnerAttribute
                {
                    LearnRefNumber = learner.LearnRefNumber,
                    LearnerPeriodisedAttributes = LearnerPeriodisedAttributes(learner),
                    LearningDeliveryAttributes = null // LearningDeliveryAttributes(learner),
                });
            }

            return learners.ToArray();
        }

        private LearnerPeriodisedAttribute[] LearnerPeriodisedAttributes(IDataEntity learner)
        {
            List<string> attributeList = new List<string> { "ALBSeqNum" };
            List<LearnerPeriodisedAttribute> learnerPeriodisedAttributesList = new List<LearnerPeriodisedAttribute>();

            foreach (var attribute in attributeList)
            {
                var attributeValue = (AttributeData)learner.Attributes[attribute];

                var changePoints = attributeValue.Changepoints;

                if (!changePoints.Any())
                {
                    var value = decimal.Parse(attributeValue.Value.ToString());

                    learnerPeriodisedAttributesList.Add(new LearnerPeriodisedAttribute
                    {
                        AttributeName = attributeValue.Name,
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
                    });
                }

                if (changePoints.Any())
                {
                    learnerPeriodisedAttributesList.Add(new LearnerPeriodisedAttribute
                    {
                        AttributeName = attributeValue.Name,
                        Period1 = LearnerPeriodAttributeValue(attributeValue, 1),
                        Period2 = LearnerPeriodAttributeValue(attributeValue, 2),
                        Period3 = LearnerPeriodAttributeValue(attributeValue, 3),
                        Period4 = LearnerPeriodAttributeValue(attributeValue, 4),
                        Period5 = LearnerPeriodAttributeValue(attributeValue, 5),
                        Period6 = LearnerPeriodAttributeValue(attributeValue, 6),
                        Period7 = LearnerPeriodAttributeValue(attributeValue, 7),
                        Period8 = LearnerPeriodAttributeValue(attributeValue, 8),
                        Period9 = LearnerPeriodAttributeValue(attributeValue, 9),
                        Period10 = LearnerPeriodAttributeValue(attributeValue, 10),
                        Period11 = LearnerPeriodAttributeValue(attributeValue, 11),
                        Period12 = LearnerPeriodAttributeValue(attributeValue, 12),
                    });
                }
            }

            return learnerPeriodisedAttributesList.ToArray();
        }

        //private LearningDeliveryAttribute[] LearningDeliveryAttributes(IDataEntity learner)
        //{
        //}

        private decimal LearnerPeriodAttributeValue(AttributeData attributes, int period)
        {
            return decimal.Parse(attributes.Changepoints.Where(cp => cp.ChangePoint == GetPeriodDate(period)).Select(v => v.Value).SingleOrDefault().ToString());
        }

        private static int GetPeriodNumber(DateTime date)
        {
            return Periods.Where(p => p.Value == date).Select(k => k.Key).First();
        }

        private static DateTime GetPeriodDate(int periodNumber)
        {
            return Periods.Where(p => p.Key == periodNumber).Select(v => v.Value).First();
        }
    }
}
