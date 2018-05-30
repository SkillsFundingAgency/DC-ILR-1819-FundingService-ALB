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
using ESFA.DC.ILR.FundingService.ALB.Contexts;
using ESFA.DC.ILR.FundingService.ALB.Contexts.Interface;
using ESFA.DC.ILR.FundingService.ALB.ExternalData;
using ESFA.DC.ILR.FundingService.ALB.ExternalData.Interface;
using ESFA.DC.ILR.FundingService.ALB.FundingOutput.Service;
using ESFA.DC.ILR.FundingService.ALB.FundingOutput.Service.Interface;
using ESFA.DC.ILR.FundingService.ALB.OrchestrationService;
using ESFA.DC.ILR.FundingService.ALB.OrchestrationService.Interface;
using ESFA.DC.ILR.FundingService.ALB.Service.Builders;
using ESFA.DC.ILR.FundingService.ALB.Service.Builders.Interface;
using ESFA.DC.ILR.FundingService.ALB.Service.Interface;
using ESFA.DC.ILR.FundingService.ALB.Service.Rulebase;
using ESFA.DC.ILR.FundingService.ALB.Stubs.Persistance;
using ESFA.DC.ILR.Model;
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
using ESFA.DC.Serialization.Json;
using ESFA.DC.Serialization.Xml;

namespace ESFA.DC.ILR.FundingService.ALB.Console
{
    public static class Program
    {
        // private static string fileName = "ILR-10006341-1819-20180118-023456-01.xml";
        private static string fileName = "ILR-10006341-1819-20180118-023456-02.xml";

        private static Stream stream;

        private static Message message;

        public static void Main(string[] args)
        {
            var stopwatch = new Stopwatch();

            GetILRFile();

            System.Console.WriteLine("Executing Funding Service...");

            var builder = ConfigureBuilder();
            var container = builder.Build();

            using (var scope = container.BeginLifetimeScope())
            {
                var fundingOrchestration = container.Resolve<IFundingOrchestrationService>();

                stopwatch.Start();

                var fundingOutputs = fundingOrchestration.FundingServiceInitilise();

                var fundingCreateTime = stopwatch.Elapsed;
                System.Console.WriteLine("Funding Complete in " + fundingCreateTime.ToString());

                stopwatch.Reset();
                stopwatch.Start();
                var dataPersister = new DataPersister();
                dataPersister.PersistData(fundingOutputs);

                stopwatch.Stop();
                var timetoPersist = stopwatch.Elapsed;
                System.Console.WriteLine("Persistance completed in " + timetoPersist.ToString());
                stopwatch.Reset();
            }
        }

        public static void GetILRFile()
        {
            try
            {
                System.Console.WriteLine("Loading file..");

                stream = new FileStream(@"Files\" + fileName, FileMode.Open);
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

            builder.RegisterType<FundingOutputService>().As<IFundingOutputService>().InstancePerLifetimeScope();
            builder.RegisterType<LARS>().As<ILARS>().InstancePerLifetimeScope();
            builder.RegisterType<Postcodes>().As<IPostcodes>().InstancePerLifetimeScope();
            builder.RegisterType<SessionBuilder>().As<ISessionBuilder>().InstancePerLifetimeScope();
            builder.RegisterType<OPADataEntityBuilder>().As<IOPADataEntityBuilder>().WithParameter("yearStartDate", new DateTime(2017, 8, 1)).InstancePerLifetimeScope();
            builder.RegisterType<RulebaseProviderFactory>().As<IRulebaseProviderFactory>().InstancePerLifetimeScope();
            builder.RegisterType<OPAService>().As<IOPAService>().InstancePerLifetimeScope();
            builder.RegisterType<AttributeBuilder>().As<IAttributeBuilder<IAttributeData>>().InstancePerLifetimeScope();
            builder.RegisterType<DataEntityBuilder>().As<IDataEntityBuilder>().InstancePerLifetimeScope();
            builder.RegisterType<Service.FundingService>().As<IFundingService>().InstancePerLifetimeScope();
            builder.RegisterType<ReferenceDataCache>().As<IReferenceDataCache>().InstancePerLifetimeScope();
            builder.RegisterType<ReferenceDataCachePopulationService>().As<IReferenceDataCachePopulationService>().InstancePerLifetimeScope();
            builder.RegisterType<PreFundingOrchestrationService>().As<IPreFundingOrchestrationService>().InstancePerLifetimeScope();
            builder.RegisterType<FundingOrchestrationService>().As<IFundingOrchestrationService>().InstancePerLifetimeScope();
            builder.RegisterType<XmlSerializationService>().As<ISerializationService>().InstancePerLifetimeScope();
            builder.Register(ctx => BuildKeyValueDictionary()).As<IKeyValuePersistenceService>().InstancePerLifetimeScope();
            builder.RegisterType<FundingContext>().As<IFundingContext>().InstancePerLifetimeScope();
            builder.RegisterType<FundingContextManager>().As<IFundingContextManager>().InstancePerLifetimeScope();
            builder.Register(ctx => BuildJobContext()).As<IJobContextMessage>().InstancePerLifetimeScope();

            return builder;
        }

        private static JobContextMessage BuildJobContext()
        {
            return new JobContextMessage
            {
                JobId = 1,
                SubmissionDateTimeUtc = DateTime.Parse("2018-08-01").ToUniversalTime(),
                Topics = TopicList,
                TopicPointer = 1,
                KeyValuePairs = new Dictionary<JobContextMessageKey, object>
                {
                    { JobContextMessageKey.Filename, fileName },
                    { JobContextMessageKey.UkPrn, 10006341 },
                    { JobContextMessageKey.ValidLearnRefNumbers, "ValidLearnRefNumbers" },
                },
            };
        }

        private static ITaskItem TaskItem => new TaskItem
        {
            Tasks = new List<string>
            {
                "Task A",
            },
            SupportsParallelExecution = true,
        };

        private static IReadOnlyList<ITaskItem> TaskItemList => new List<ITaskItem> { TaskItem };

        private static ITopicItem TopicItem => new TopicItem("Subscription", "SubscriptionFilter", TaskItemList);

        private static IReadOnlyList<ITopicItem> TopicList => new List<ITopicItem> { TopicItem };

        private static DictionaryKeyValuePersistenceService BuildKeyValueDictionary()
        {
            var learners = message.Learner.ToList();

            var list = new DictionaryKeyValuePersistenceService();
            var serializer = new XmlSerializationService();

            list.SaveAsync("ValidLearnRefNumbers", serializer.Serialize(learners)).Wait();

            return list;
        }
    }
}
