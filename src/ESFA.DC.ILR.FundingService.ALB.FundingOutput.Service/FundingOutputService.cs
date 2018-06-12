using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using ESFA.DC.ILR.FundingService.ALB.FundingOutput.Model;
using ESFA.DC.ILR.FundingService.ALB.FundingOutput.Model.Attribute;
using ESFA.DC.ILR.FundingService.ALB.FundingOutput.Model.Interface;
using ESFA.DC.ILR.FundingService.ALB.FundingOutput.Model.Interface.Attribute;
using ESFA.DC.ILR.FundingService.ALB.FundingOutput.Service.Interface;
using ESFA.DC.OPA.Model;
using ESFA.DC.OPA.Model.Interface;

namespace ESFA.DC.ILR.FundingService.ALB.FundingOutput.Service
{
    public class FundingOutputService : IFundingOutputService
    {
        private static readonly IFormatProvider culture = new CultureInfo("en-GB", true);

        public FundingOutputService()
        {
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

        public IFundingOutputs ProcessFundingOutputs(IEnumerable<IDataEntity> dataEntities)
        {
            return new FundingOutputs
            {
                Global = GlobalOutput(dataEntities.Select(g => g.Attributes).First()),
                Learners = LearnerOutput(dataEntities.SelectMany(g => g.Children))
            };
        }

        protected internal IGlobalAttribute GlobalOutput(IDictionary<string, IAttributeData> attributes)
        {
           return new GlobalAttribute
           {
               UKPRN = DecimalStrToInt(GetAttributeValue(attributes, "UKPRN")),
               LARSVersion = GetAttributeValue(attributes, "LARSVersion"),
               PostcodeAreaCostVersion = GetAttributeValue(attributes, "PostcodeAreaCostVersion"),
               RulebaseVersion = GetAttributeValue(attributes, "RulebaseVersion"),
           };
        }

        protected internal ILearnerAttribute[] LearnerOutput(IEnumerable<IDataEntity> learnerEntities)
        {
            var learners = new List<ILearnerAttribute>();

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

        protected internal ILearnerPeriodisedAttribute[] LearnerPeriodisedAttributes(IDataEntity learner)
        {
            List<string> attributeList = new List<string> { "ALBSeqNum" };
            List<ILearnerPeriodisedAttribute> learnerPeriodisedAttributesList = new List<ILearnerPeriodisedAttribute>();

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

        protected internal ILearningDeliveryAttribute[] LearningDeliveryAttributes(IDataEntity learner)
        {
            List<ILearningDeliveryAttribute> list = new List<ILearningDeliveryAttribute>();
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

        protected internal ILearningDeliveryAttributeData LearningDeliveryAttributeData(IDataEntity learningDelivery)
        {
            var attributes = learningDelivery.Attributes;

            return new LearningDeliveryAttributeData
            {
                Achieved = ConvertToBit(GetAttributeValue(attributes, "Achieved")),
                ActualNumInstalm = DecimalStrToInt(GetAttributeValue(attributes, "ActualNumInstalm")),
                AdvLoan = ConvertToBit(GetAttributeValue(attributes, "AdvLoan")),
                ApplicFactDate = GetAttributeValueDate(attributes, "ApplicFactDate"),
                ApplicProgWeightFact = GetAttributeValue(attributes, "ApplicProgWeightFact"),
                AreaCostFactAdj = decimal.Parse(GetAttributeValue(attributes, "AreaCostFactAdj")),
                AreaCostInstalment = decimal.Parse(GetAttributeValue(attributes, "AreaCostInstalment")),
                FundLine = GetAttributeValue(attributes, "FundLine"),
                FundStart = ConvertToBit(GetAttributeValue(attributes, "FundStart")),
                LiabilityDate = GetAttributeValueDate(attributes, "LiabilityDate"),
                LoanBursAreaUplift = ConvertToBit(GetAttributeValue(attributes, "LoanBursAreaUplift")),
                LoanBursSupp = ConvertToBit(GetAttributeValue(attributes, "LoanBursSupp")),
                OutstndNumOnProgInstalm = DecimalStrToInt(GetAttributeValue(attributes, "OutstndNumOnProgInstalm")),
                PlannedNumOnProgInstalm = DecimalStrToInt(GetAttributeValue(attributes, "PlannedNumOnProgInstalm")),
                WeightedRate = decimal.Parse(GetAttributeValue(attributes, "WeightedRate")),
            };
        }

        protected internal ILearningDeliveryPeriodisedAttribute[] LearningDeliveryPeriodisedAttributeData(IDataEntity learningDelivery)
        {
            List<string> attributeList = new List<string>() { "ALBCode", "ALBSupportPayment", "AreaUpliftBalPayment", "AreaUpliftOnProgPayment" };
            List<ILearningDeliveryPeriodisedAttribute> learningDeliveryPeriodisedAttributesList = new List<ILearningDeliveryPeriodisedAttribute>();

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

        private DateTime? GetAttributeValueDate(IDictionary<string, IAttributeData> attributes, string attributeName)
        {
            var attributeValue = attributes.Where(k => k.Key == attributeName).Select(v => v.Value.Value).Single();

            if (attributeValue != null)
            {
                DateTime attributeDateValue = Convert.ToDateTime(attributeValue, culture);

                return attributeDateValue;
            }

            return null;
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
