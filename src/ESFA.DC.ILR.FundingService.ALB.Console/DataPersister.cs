using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;
using ESFA.DC.ILR.FundingService.ALB.Console.Model.Attribute;
using ESFA.DC.ILR.FundingService.ALB.Console.Model.Output;
using ESFA.DC.OPA.Model.Interface;

namespace ESFA.DC.ILR.FundingService.ALB.Console
{
    public class DataPersister
    {
        private static readonly CultureInfo Culture = CultureInfo.CreateSpecificCulture("en-GB");

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
           { 12, new DateTime(2018, 07, 01) }
        };

        public void PersistData(IEnumerable<IDataEntity> dataEntities)
        {
            var output = new ConcurrentBag<FundingOutput>();

            foreach (var de in dataEntities)
            {
                var learnRefNumber = de.Children.Select(l => l.LearnRefNumber).Single();

                var outputToadd =
                   new FundingOutput()
                   {
                       LearnRefNumber = learnRefNumber,
                       Instance = de
                   };

                output.Add(outputToadd);
            }

            PersistToXml(output);
        }

        public void PersistToXml(IEnumerable<FundingOutput> outputs)
        {
            IEnumerable<IDataEntity> entities = outputs.Select(x => x.Instance);

            // Create Global Output
            var globalOutput = TransformGlobalOutput(outputs.Select(x => x.Instance).FirstOrDefault());

            // Create LearningDelivery outputs
            var learningDeliveryOutput = TransformLearningDeliveryOutput(entities, globalOutput.UKPRN);
            var learningDeliveryPeriodOutputsForPivots = TransformLearningDeliveryPeriodOutput(entities, globalOutput.UKPRN);
            var learningDeliveryPeriodOutput = GetLearningDeliveryPeriodOutput(learningDeliveryPeriodOutputsForPivots);
            var learningDeliveryPeriodisedValuesOutput = GetLearningDeliveryPeriodisedValuesOutput(learningDeliveryPeriodOutputsForPivots);

            // Create Learner outputs
            var learnerOutput = TransformLearnerOutput(entities, globalOutput.UKPRN);
            var learnerPeriodOutput = GetLearnerPeriodOutput(learnerOutput);
            var learnerPeriodisedValues = GetLearnerPeriodisedValuesOutput(learnerOutput);

            // Persist reusults
            XmlWriter(globalOutput, "Global");
            XmlWriter(learningDeliveryOutput, "LearningDelivery");
            XmlWriter(learningDeliveryPeriodOutput, "LearningDeliveryPeriod");
            XmlWriter(learningDeliveryPeriodisedValuesOutput, "LearningDeliveryPeriodisedValues");
            XmlWriter(learnerPeriodOutput, "LearnerPeriod");
            XmlWriter(learnerPeriodisedValues, "LearnerPeriodisedValues");
        }

        public GlobalOutput TransformGlobalOutput(IDataEntity output)
        {
            List<GlobalAttribute> globalStaged = new List<GlobalAttribute>();

            List<string> attributeNameList = new List<string>() { "UKPRN", "LARSVersion", "PostcodeAreaCostVersion", "RulebaseVersion" };

            foreach (var name in attributeNameList)
            {
                var globaltoStage = new GlobalAttribute()
                {
                    Name = name,
                    Data = output.Attributes[name].Value.ToString()
                };

                globalStaged.Add(globaltoStage);
            }

            GlobalOutput global = new GlobalOutput()
            {
                UKPRN = int.Parse(globalStaged.Where(g => g.Name == "UKPRN").Select(d => d.Data.Substring(0, 8)).Single(), Culture),
                LARSVersion = globalStaged.Where(g => g.Name == "LARSVersion").Select(d => d.Data).Single(),
                PostcodeAreaCostVersion = globalStaged.Where(g => g.Name == "PostcodeAreaCostVersion").Select(d => d.Data).Single(),
                RulebaseVersion = globalStaged.Where(g => g.Name == "RulebaseVersion").Select(d => d.Data).Single(),
            };

            return global;
        }

        public LearningDeliveryOutput[] TransformLearningDeliveryOutput(IEnumerable<IDataEntity> output, int ukprn)
        {
            List<LearningDeliveryAttribute> outputAtt = new List<LearningDeliveryAttribute>();

            const string aim = "AimSeqNumber";

            List<string> learningDeliveryAttrList = new List<string>() { "Achieved", "ActualNumInstalm", "AdvLoan", "ApplicFactDate", "ApplicProgWeightFact", "AreaCostFactAdj", "AreaCostInstalment", "FundLine", "FundStart", "LiabilityDate", "LoanBursAreaUplift", "LoanBursSupp", "OutstndNumOnProgInstalm", "PlannedNumOnProgInstalm", "WeightedRate" };

            var ldToProcess = output.SelectMany(i => i.Children.SelectMany(l => l.Children)).ToArray();

            foreach (var ld in ldToProcess)
            {
                foreach (var lda in learningDeliveryAttrList)
                {
                    var ldToStage = new LearningDeliveryAttribute()
                    {
                        Ukprn = ukprn.ToString(Culture),
                        LearnRefNumber = ld.Parent.LearnRefNumber,
                        AimSeqNumber = ld.Attributes[aim].Value.ToString(),
                        Name = lda,
                        Data = ld.Attributes[lda].Value.ToString()
                    };

                    outputAtt.Add(ldToStage);
                }
            }

            var pivot = outputAtt
                .GroupBy(l => new { l.Ukprn, l.LearnRefNumber, l.AimSeqNumber })
                .Select(ld =>
                new LearningDeliveryOutput()
                {
                    UKPRN = int.Parse(ld.Select(v => v.Ukprn).First(), Culture),
                    LearnRefNumber = ld.Select(v => v.LearnRefNumber).First(),
                    AimSeqNumber = DecimalStrToInt(ld.Select(v => v.AimSeqNumber).First()),
                    Achieved = ConvertToBit(ld.Where(n => n.Name == "Achieved").Select(v => v.Data).First()),
                    ActualNumInstalm = DecimalStrToInt(ld.Where(n => n.Name == "ActualNumInstalm").Select(v => v.Data).First()),
                    AdvLoan = ConvertToBit(ld.Where(n => n.Name == "AdvLoan").Select(v => v.Data).First()),
                    ApplicFactDate = DateTime.Parse(ld.Where(n => n.Name == "ApplicFactDate").Select(v => v.Data).First(), Culture),
                    ApplicProgWeightFact = ld.Where(n => n.Name == "ApplicProgWeightFact").Select(v => v.Data).First(),
                    AreaCostFactAdj = decimal.Parse(ld.Where(n => n.Name == "AreaCostFactAdj").Select(v => v.Data).First(), Culture),
                    AreaCostInstalment = decimal.Parse(ld.Where(n => n.Name == "AreaCostInstalment").Select(v => v.Data).First(), Culture),
                    FundLine = ld.Where(n => n.Name == "FundLine").Select(v => v.Data).First(),
                    FundStart = ConvertToBit(ld.Where(n => n.Name == "FundStart").Select(v => v.Data).First()),
                    LiabilityDate = DateTime.Parse(ld.Where(n => n.Name == "LiabilityDate").Select(v => v.Data).First(), Culture),
                    LoanBursAreaUplift = ConvertToBit(ld.Where(n => n.Name == "LoanBursAreaUplift").Select(v => v.Data).First()),
                    LoanBursSupp = ConvertToBit(ld.Where(n => n.Name == "LoanBursSupp").Select(v => v.Data).First()),
                    OutstndNumOnProgInstalm = DecimalStrToInt(ld.Where(n => n.Name == "OutstndNumOnProgInstalm").Select(v => v.Data).First()),
                    PlannedNumOnProgInstalm = DecimalStrToInt(ld.Where(n => n.Name == "PlannedNumOnProgInstalm").Select(v => v.Data).First()),
                    WeightedRate = decimal.Parse(ld.Where(n => n.Name == "WeightedRate").Select(v => v.Data).First(), Culture)
                }).ToArray();

            return pivot;
        }

        public LearningDeliveryPeriodAttribute[] TransformLearningDeliveryPeriodOutput(IEnumerable<IDataEntity> output, int ukprn)
        {
            List<LearningDeliveryPeriodAttribute> outputAtt = new List<LearningDeliveryPeriodAttribute>();

            const string aim = "AimSeqNumber";

            List<string> learningDeliveryPeriodAttrList = new List<string>() { "ALBCode", "ALBSupportPayment", "AreaUpliftBalPayment", "AreaUpliftOnProgPayment" };

            var ldToProcess = output.SelectMany(i => i.Children.SelectMany(l => l.Children)).ToArray();

            foreach (var ld in ldToProcess)
            {
                foreach (var lda in learningDeliveryPeriodAttrList)
                {
                    var changePoints = ld.Attributes[lda].Changepoints;

                    if (!changePoints.Any())
                    {
                        foreach (var p in Periods)
                        {
                            var ldToStage = new LearningDeliveryPeriodAttribute()
                            {
                                Ukprn = ukprn.ToString(Culture),
                                LearnRefNumber = ld.Parent.LearnRefNumber,
                                AimSeqNumber = ld.Attributes[aim].Value.ToString(),
                                Period = GetPeriodNumber(p.Value),
                                Name = lda,
                                Data = ld.Attributes[lda].Value.ToString()
                            };
                            outputAtt.Add(ldToStage);
                        }
                    }

                    foreach (var cp in changePoints)
                    {
                        var ldToStage = new LearningDeliveryPeriodAttribute()
                        {
                            Ukprn = ukprn.ToString(Culture),
                            LearnRefNumber = ld.Parent.LearnRefNumber,
                            AimSeqNumber = ld.Attributes[aim].Value.ToString(),
                            Period = GetPeriodNumber(cp.ChangePoint),
                            Name = lda,
                            Data = cp.Value.ToString(),
                        };
                        outputAtt.Add(ldToStage);
                    }
                }
            }

            var ldPeriod = outputAtt.OrderBy(l => l.LearnRefNumber).ToArray();

            return ldPeriod;
        }

        public LearningDeliveryPeriodOutput[] GetLearningDeliveryPeriodOutput(LearningDeliveryPeriodAttribute[] learningDeliveryPeriodAttr)
        {
            LearningDeliveryPeriodOutput[] output = learningDeliveryPeriodAttr
                .GroupBy(l => new { l.Ukprn, l.LearnRefNumber, l.AimSeqNumber, l.Period })
                .Select(ld =>
                     new LearningDeliveryPeriodOutput()
                     {
                         UKPRN = int.Parse(ld.Select(v => v.Ukprn).First(), Culture),
                         LearnRefNumber = ld.Select(v => v.LearnRefNumber).First(),
                         AimSeqNumber = DecimalStrToInt(ld.Select(v => v.AimSeqNumber).First()),
                         Period = ld.Select(v => v.Period).First(),
                         ALBCode = DecimalStrToInt(ld.Where(n => n.Name == "ALBCode").Select(v => v.Data).DefaultIfEmpty("0").First()),
                         ALBSupportPayment = decimal.Parse(ld.Where(n => n.Name == "ALBSupportPayment").Select(v => v.Data).First(), Culture),
                         AreaUpliftBalPayment = decimal.Parse(ld.Where(n => n.Name == "AreaUpliftBalPayment").Select(v => v.Data).First(), Culture),
                         AreaUpliftOnProgPayment = decimal.Parse(ld.Where(n => n.Name == "AreaUpliftOnProgPayment").Select(v => v.Data).First(), Culture)
                     }).ToArray();

            return output;
        }

        public LearningDeliveryPeriodisedValuesOutput[] GetLearningDeliveryPeriodisedValuesOutput(LearningDeliveryPeriodAttribute[] learningDeliveryPeriodAttr)
        {
            LearningDeliveryPeriodisedValuesOutput[] output = learningDeliveryPeriodAttr
                .GroupBy(l => new { l.Ukprn, l.LearnRefNumber, l.AimSeqNumber, l.Name })
                .Select(ld =>
                     new LearningDeliveryPeriodisedValuesOutput()
                     {
                         UKPRN = int.Parse(ld.Select(v => v.Ukprn).First(), Culture),
                         LearnRefNumber = ld.Select(v => v.LearnRefNumber).First(),
                         AimSeqNumber = DecimalStrToInt(ld.Select(v => v.AimSeqNumber).First()),
                         AttributeName = ld.Select(v => v.Name).First(),
                         Period1 = decimal.Parse(ld.Where(p => p.Period == 1).Select(v => v.Data).First(), Culture),
                         Period2 = decimal.Parse(ld.Where(p => p.Period == 2).Select(v => v.Data).First(), Culture),
                         Period3 = decimal.Parse(ld.Where(p => p.Period == 3).Select(v => v.Data).First(), Culture),
                         Period4 = decimal.Parse(ld.Where(p => p.Period == 4).Select(v => v.Data).First(), Culture),
                         Period5 = decimal.Parse(ld.Where(p => p.Period == 5).Select(v => v.Data).First(), Culture),
                         Period6 = decimal.Parse(ld.Where(p => p.Period == 6).Select(v => v.Data).First(), Culture),
                         Period7 = decimal.Parse(ld.Where(p => p.Period == 7).Select(v => v.Data).First(), Culture),
                         Period8 = decimal.Parse(ld.Where(p => p.Period == 8).Select(v => v.Data).First(), Culture),
                         Period9 = decimal.Parse(ld.Where(p => p.Period == 9).Select(v => v.Data).First(), Culture),
                         Period10 = decimal.Parse(ld.Where(p => p.Period == 10).Select(v => v.Data).First(), Culture),
                         Period11 = decimal.Parse(ld.Where(p => p.Period == 11).Select(v => v.Data).First(), Culture),
                         Period12 = decimal.Parse(ld.Where(p => p.Period == 12).Select(v => v.Data).First(), Culture),
                     }).ToArray();

            return output;
        }

        public LearnerPeriodAttribute[] TransformLearnerOutput(IEnumerable<IDataEntity> output, int ukprn)
        {
            List<LearnerPeriodAttribute> outputAtt = new List<LearnerPeriodAttribute>();

            List<string> learnerPeriodAttrList = new List<string>() { "ALBSeqNum" };

            var learnerToProcess = output.SelectMany(i => i.Children).ToArray();

            foreach (var l in learnerToProcess)
            {
                foreach (var la in learnerPeriodAttrList)
                {
                    var changePoints = l.Attributes[la].Changepoints;

                    if (!changePoints.Any())
                    {
                        foreach (var p in Periods)
                        {
                            var learnerToStage = new LearnerPeriodAttribute()
                            {
                                Ukprn = ukprn.ToString(Culture),
                                LearnRefNumber = l.LearnRefNumber,
                                Period = GetPeriodNumber(p.Value),
                                Name = la,
                                Data = l.Attributes[la].Value.ToString()
                            };

                            outputAtt.Add(learnerToStage);
                        }
                    }

                    foreach (var cp in changePoints)
                    {
                        var learnerToStage = new LearnerPeriodAttribute()
                        {
                            Ukprn = ukprn.ToString(Culture),
                            LearnRefNumber = l.LearnRefNumber,
                            Period = GetPeriodNumber(cp.ChangePoint),
                            Name = la,
                            Data = cp.Value.ToString(),
                        };
                        outputAtt.Add(learnerToStage);
                    }
                }
            }

            return outputAtt.OrderBy(l => l.LearnRefNumber).ToArray();
        }

        public LearnerPeriodOutput[] GetLearnerPeriodOutput(LearnerPeriodAttribute[] learnerPeriodAttr)
        {
            LearnerPeriodOutput[] output = learnerPeriodAttr
                .GroupBy(l => new { l.Ukprn, l.LearnRefNumber, l.Period })
                .Select(lp =>
                     new LearnerPeriodOutput()
                     {
                         UKPRN = int.Parse(lp.Select(v => v.Ukprn).First(), Culture),
                         LearnRefNumber = lp.Select(v => v.LearnRefNumber).First(),
                         Period = lp.Select(v => v.Period).First(),
                         ALBSeqNum = DecimalStrToInt(lp.Where(n => n.Name == "ALBSeqNum").Select(v => v.Data).DefaultIfEmpty("0").First()),
                     }).ToArray();

            return output;
        }

        public LearnerPeriodisedValuesOutput[] GetLearnerPeriodisedValuesOutput(LearnerPeriodAttribute[] learnerPeriodAttr)
        {
            LearnerPeriodisedValuesOutput[] output = learnerPeriodAttr
                .GroupBy(l => new { l.Ukprn, l.LearnRefNumber, l.Name })
                .Select(lp =>
                     new LearnerPeriodisedValuesOutput()
                     {
                         UKPRN = int.Parse(lp.Select(v => v.Ukprn).First(), Culture),
                         LearnRefNumber = lp.Select(v => v.LearnRefNumber).First(),
                         AttributeName = lp.Select(v => v.Name).First(),
                         Period1 = decimal.Parse(lp.Where(p => p.Period == 1).Select(v => v.Data).First(), Culture),
                         Period2 = decimal.Parse(lp.Where(p => p.Period == 2).Select(v => v.Data).First(), Culture),
                         Period3 = decimal.Parse(lp.Where(p => p.Period == 3).Select(v => v.Data).First(), Culture),
                         Period4 = decimal.Parse(lp.Where(p => p.Period == 4).Select(v => v.Data).First(), Culture),
                         Period5 = decimal.Parse(lp.Where(p => p.Period == 5).Select(v => v.Data).First(), Culture),
                         Period6 = decimal.Parse(lp.Where(p => p.Period == 6).Select(v => v.Data).First(), Culture),
                         Period7 = decimal.Parse(lp.Where(p => p.Period == 7).Select(v => v.Data).First(), Culture),
                         Period8 = decimal.Parse(lp.Where(p => p.Period == 8).Select(v => v.Data).First(), Culture),
                         Period9 = decimal.Parse(lp.Where(p => p.Period == 9).Select(v => v.Data).First(), Culture),
                         Period10 = decimal.Parse(lp.Where(p => p.Period == 10).Select(v => v.Data).First(), Culture),
                         Period11 = decimal.Parse(lp.Where(p => p.Period == 11).Select(v => v.Data).First(), Culture),
                         Period12 = decimal.Parse(lp.Where(p => p.Period == 12).Select(v => v.Data).First(), Culture),
                     }).ToArray();

            return output;
        }

        private static void XmlWriter(object output, string outputName)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(output.GetType());
            using (System.IO.StringWriter textWriter = new System.IO.StringWriter())
            {
                xmlSerializer.Serialize(textWriter, output);
                var xml = XElement.Parse(textWriter.ToString()).ToString();
                System.IO.File.WriteAllText(@"C:\Code\temp\ALBFundingService\" + outputName + "_Output.xml", xml);
            }
        }

        private static int DecimalStrToInt(string value)
        {
            var valueInt = value.Substring(0, value.IndexOf('.', 0));
            return int.Parse(valueInt, Culture);
        }

        private static bool ConvertToBit(string value)
        {
            bool newValue;
            newValue = value == "true" ? true : false;

            return newValue;
        }

        private static int GetPeriodNumber(DateTime date)
        {
            var period = Periods.Where(p => p.Value == date).Select(k => k.Key).First();

            return period;
        }
    }
}
