﻿using System;
using System.Collections.Generic;
using System.Linq;
using ESFA.DC.ILR.FundingService.ALB.FundingOutput.Model;
using ESFA.DC.ILR.FundingService.ALB.FundingOutput.Model.Attribute;
using ESFA.DC.OPA.Model;
using ESFA.DC.OPA.Model.Interface;

namespace ESFA.DC.ILR.FundingService.ALB.FundingOutput.Service
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
            FundingOutputs fundingOutputs = new FundingOutputs
            {
                Global = GlobalOutput(_dataEntities.Select(g => g.Attributes).First()),
                Learners = LearnerOutput(_dataEntities.SelectMany(g => g.Children)),
            };

            return fundingOutputs;
        }

        private GlobalAttribute GlobalOutput(IDictionary<string, IAttributeData> attributes)
        {
           return new GlobalAttribute
           {
               UKPRN = DecimalStrToInt(GetAttributeValue(attributes, "UKPRN")),
               LARSVersion = GetAttributeValue(attributes, "LARSVersion"),
               PostcodeAreaCostVersion = GetAttributeValue(attributes, "PostcodeAreaCostVersion"),
               RulebaseVersion = GetAttributeValue(attributes, "RulebaseVersion"),
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
                    LearningDeliveryAttributes = LearningDeliveryAttributes(learner),
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
                        Period1 = GetPeriodAttributeValue(attributeValue, 1),
                        Period2 = GetPeriodAttributeValue(attributeValue, 2),
                        Period3 = GetPeriodAttributeValue(attributeValue, 3),
                        Period4 = GetPeriodAttributeValue(attributeValue, 4),
                        Period5 = GetPeriodAttributeValue(attributeValue, 5),
                        Period6 = GetPeriodAttributeValue(attributeValue, 6),
                        Period7 = GetPeriodAttributeValue(attributeValue, 7),
                        Period8 = GetPeriodAttributeValue(attributeValue, 8),
                        Period9 = GetPeriodAttributeValue(attributeValue, 9),
                        Period10 = GetPeriodAttributeValue(attributeValue, 10),
                        Period11 = GetPeriodAttributeValue(attributeValue, 11),
                        Period12 = GetPeriodAttributeValue(attributeValue, 12),
                    });
                }
            }

            return learnerPeriodisedAttributesList.ToArray();
        }

        private LearningDeliveryAttribute[] LearningDeliveryAttributes(IDataEntity learner)
        {
            List<LearningDeliveryAttribute> list = new List<LearningDeliveryAttribute>();
            string aimSeqNumber = "AimSeqNumber";

            var learningdeliveries = learner.Children.ToList();

            foreach (var delivery in learningdeliveries)
            {
                list.Add(new LearningDeliveryAttribute
                {
                    AimSeqNumber = DecimalStrToInt(delivery.Attributes[aimSeqNumber].Value.ToString()),
                    LearningDeliveryAttributeDatas = LearningDeliveryAttributeData(delivery),
                    LearningDeliveryPeriodisedAttributes = LearningDeliveryPeriodisedAttributeData(delivery),
                });
            }

            return list.ToArray();
        }

        private LearningDeliveryAttributeData LearningDeliveryAttributeData(IDataEntity learningDelivery)
        {
            var attributes = learningDelivery.Attributes;

            return new LearningDeliveryAttributeData
            {
                Achieved = ConvertToBit(GetAttributeValue(attributes, "Achieved")),
                ActualNumInstalm = DecimalStrToInt(GetAttributeValue(attributes, "ActualNumInstalm")),
                AdvLoan = ConvertToBit(GetAttributeValue(attributes, "AdvLoan")),
                ApplicFactDate = DateTime.Parse(GetAttributeValue(attributes, "ApplicFactDate")),
                ApplicProgWeightFact = GetAttributeValue(attributes, "ApplicProgWeightFact"),
                AreaCostFactAdj = decimal.Parse(GetAttributeValue(attributes, "AreaCostFactAdj")),
                AreaCostInstalment = decimal.Parse(GetAttributeValue(attributes, "AreaCostInstalment")),
                FundLine = GetAttributeValue(attributes, "FundLine"),
                FundStart = ConvertToBit(GetAttributeValue(attributes, "FundStart")),
                LiabilityDate = DateTime.Parse(GetAttributeValue(attributes, "LiabilityDate")),
                LoanBursAreaUplift = ConvertToBit(GetAttributeValue(attributes, "LoanBursAreaUplift")),
                LoanBursSupp = ConvertToBit(GetAttributeValue(attributes, "LoanBursSupp")),
                OutstndNumOnProgInstalm = DecimalStrToInt(GetAttributeValue(attributes, "OutstndNumOnProgInstalm")),
                PlannedNumOnProgInstalm = DecimalStrToInt(GetAttributeValue(attributes, "PlannedNumOnProgInstalm")),
                WeightedRate = decimal.Parse(GetAttributeValue(attributes, "WeightedRate")),
            };
        }

        private LearningDeliveryPeriodisedAttribute[] LearningDeliveryPeriodisedAttributeData(IDataEntity learningDelivery)
        {
            List<string> attributeList = new List<string>() { "ALBCode", "ALBSupportPayment", "AreaUpliftBalPayment", "AreaUpliftOnProgPayment" };
            List<LearningDeliveryPeriodisedAttribute> learningDeliveryPeriodisedAttributesList = new List<LearningDeliveryPeriodisedAttribute>();

            foreach (var attribute in attributeList)
            {
                var attributeValue = (AttributeData)learningDelivery.Attributes[attribute];

                var changePoints = attributeValue.Changepoints;

                if (!changePoints.Any())
                {
                    var value = decimal.Parse(attributeValue.Value.ToString());

                    learningDeliveryPeriodisedAttributesList.Add(new LearningDeliveryPeriodisedAttribute
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
                    learningDeliveryPeriodisedAttributesList.Add(new LearningDeliveryPeriodisedAttribute
                    {
                        AttributeName = attributeValue.Name,
                        Period1 = GetPeriodAttributeValue(attributeValue, 1),
                        Period2 = GetPeriodAttributeValue(attributeValue, 2),
                        Period3 = GetPeriodAttributeValue(attributeValue, 3),
                        Period4 = GetPeriodAttributeValue(attributeValue, 4),
                        Period5 = GetPeriodAttributeValue(attributeValue, 5),
                        Period6 = GetPeriodAttributeValue(attributeValue, 6),
                        Period7 = GetPeriodAttributeValue(attributeValue, 7),
                        Period8 = GetPeriodAttributeValue(attributeValue, 8),
                        Period9 = GetPeriodAttributeValue(attributeValue, 9),
                        Period10 = GetPeriodAttributeValue(attributeValue, 10),
                        Period11 = GetPeriodAttributeValue(attributeValue, 11),
                        Period12 = GetPeriodAttributeValue(attributeValue, 12),
                    });
                }
            }

            return learningDeliveryPeriodisedAttributesList.ToArray();
        }

        private decimal GetPeriodAttributeValue(AttributeData attributes, int period)
        {
            return decimal.Parse(attributes.Changepoints.Where(cp => cp.ChangePoint == GetPeriodDate(period)).Select(v => v.Value).SingleOrDefault().ToString());
        }

        private string GetAttributeValue(IDictionary<string, IAttributeData> attributes, string attributeName)
        {
            return attributes.Where(k => k.Key == attributeName).Select(v => v.Value.Value).Single().ToString();
        }

        private int DecimalStrToInt(string value)
        {
            var valueInt = value.Substring(0, value.IndexOf('.', 0));
            return int.Parse(valueInt);
        }

        private bool ConvertToBit(string value)
        {
            bool newValue;
            newValue = value == "true" ? true : false;

            return newValue;
        }

        private int GetPeriodNumber(DateTime date)
        {
            return Periods.Where(p => p.Value == date).Select(k => k.Key).First();
        }

        private DateTime GetPeriodDate(int periodNumber)
        {
            return Periods.Where(p => p.Key == periodNumber).Select(v => v.Value).First();
        }
    }
}
