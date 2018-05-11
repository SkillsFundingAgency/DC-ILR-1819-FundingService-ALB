using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Autofac;
using ESFA.DC.Data.LARS.Model;
using ESFA.DC.Data.LARS.Model.Interfaces;
using ESFA.DC.Data.Postcodes.Model;
using ESFA.DC.Data.Postcodes.Model.Interfaces;
using ESFA.DC.ILR.FundingService.ALB.ExternalData;
using ESFA.DC.ILR.FundingService.ALB.ExternalData.Interface;
using ESFA.DC.ILR.FundingService.ALB.Service.Builders;
using ESFA.DC.ILR.FundingService.ALB.Service.Builders.Interface;
using ESFA.DC.ILR.FundingService.ALB.Service.Contexts;
using ESFA.DC.ILR.FundingService.ALB.Service.Interface;
using ESFA.DC.ILR.FundingService.ALB.Service.Interface.Contexts;
using ESFA.DC.ILR.FundingService.ALB.Service.Rulebase;
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
using ESFA.DC.Serialization.Interfaces;
using ESFA.DC.Serialization.Xml;

namespace ESFA.DC.ILR.FundingService.ALB.Console
{
    public static class Program
    {
        private static Stream stream;

        private static IMessage message;

        public static void Main(string[] args)
        {
            var stopwatch = new Stopwatch();

            GetILRFile();

            System.Console.WriteLine("Executing Funding Service...");

            var builder = ConfigureBuilder();
            var container = builder.Build();

            using (var scope = container.BeginLifetimeScope())
            {
                var fundingService = container.Resolve<IFundingSevice>();

                stopwatch.Start();

                var fundingOutputs = fundingService.ProcessFunding(message);

                var fundingCreateTime = stopwatch.Elapsed;
                System.Console.WriteLine("Funding Complete in " + fundingCreateTime.ToString());

                stopwatch.Reset();
                stopwatch.Start();
                var dataPersister = new DataPersister();
                dataPersister.PersistData(fundingOutputs);

                stopwatch.Stop();
                var inputsCreateTime = stopwatch.Elapsed;
                System.Console.WriteLine("Persistance completed in " + inputsCreateTime.ToString());
                stopwatch.Reset();
            }
        }

        public static void GetILRFile()
        {
            try
            {
                System.Console.WriteLine("Loading file..");

                stream = new FileStream(@"Files\ILR-10006341-1819-20180118-023456-01.xml", FileMode.Open);

                // stream = new FileStream(@"Files\ILR-10006341-1819-20180118-023456-02.xml", FileMode.Open);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("File Load Error: Problem loading file... {0}", ex);
            }

            ISerializationService serializationService = new XmlSerializationService();
            message = serializationService.Deserialize<Message>(stream);

            stream.Close();
        }

        private static ContainerBuilder ConfigureBuilder()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<LARS>().As<ILARS>().InstancePerLifetimeScope();
            builder.RegisterType<Postcodes>().As<IPostcodes>().InstancePerLifetimeScope();
            builder.RegisterType<ReferenceDataCache>().As<IReferenceDataCache>().InstancePerLifetimeScope();
            builder.RegisterType<ReferenceDataCachePopulationService>().As<IReferenceDataCachePopulationService>().InstancePerLifetimeScope();
            builder.RegisterType<SessionBuilder>().As<ISessionBuilder>().InstancePerLifetimeScope();
            builder.RegisterType<OPADataEntityBuilder>().As<IOPADataEntityBuilder>().WithParameter("yearStartDate", new DateTime(2017, 8, 1)).InstancePerLifetimeScope();
            builder.RegisterType<RulebaseProviderFactory>().As<IRulebaseProviderFactory>().InstancePerLifetimeScope();
            builder.RegisterType<OPAService>().As<IOPAService>().InstancePerLifetimeScope();
            builder.RegisterType<AttributeBuilder>().As<IAttributeBuilder<IAttributeData>>().InstancePerLifetimeScope();
            builder.RegisterType<DataEntityBuilder>().As<IDataEntityBuilder>().InstancePerLifetimeScope();
            builder.RegisterType<FundingContext>().As<IFundingContext>().InstancePerLifetimeScope();
            builder.RegisterType<FundingContextManager>().As<IFundingContextManager>().InstancePerLifetimeScope();
            builder.RegisterType<Service.FundingService>().As<IFundingSevice>().InstancePerLifetimeScope();
            builder.RegisterType<XmlSerializationService>().As<ISerializationService>().InstancePerLifetimeScope();
            builder.Register(ctx => BuildKeyValueDictionary()).As<IKeyValuePersistenceService>().InstancePerLifetimeScope();
            builder.Register(ctx => BuildJobContext()).As<IJobContextMessage>().InstancePerLifetimeScope();

            return builder;
        }

        private static JobContextMessage BuildJobContext()
        {
            return new JobContextMessage
            {
                JobId = 1,
                SubmissionDateTimeUtc = DateTime.Parse("2018-08-01").ToUniversalTime(),
                Topics = new List<ITopicItem>
                {
                    new TopicItem
                    {
                        Tasks = new List<ITaskItem>
                        {
                            new TaskItem
                            {
                                Tasks = new List<string>
                                {
                                    "Task A",
                                },
                                SupportsParallelExecution = true,
                            },
                        },
                        TopicName = "Topic A",
                    },
                },
                TopicPointer = 1,
                KeyValuePairs = new Dictionary<JobContextMessageKey, object>
                {
                    { JobContextMessageKey.ValidLearnRefNumbers, "ValidLearnRefNumbers" },
                },
            };
        }

        private static DictionaryKeyValuePersistenceService BuildKeyValueDictionary()
        {
            var learnRefNumbers = message.Learners.Select(l => l.LearnRefNumber).ToList();

            // var learnRefNumbers = new List<string> { "16v224" };
            var list = new DictionaryKeyValuePersistenceService();
            var serializer = new XmlSerializationService();

            list.SaveAsync("ValidLearnRefNumbers", serializer.Serialize(learnRefNumbers)).Wait();

            return list;
        }
    }
}
