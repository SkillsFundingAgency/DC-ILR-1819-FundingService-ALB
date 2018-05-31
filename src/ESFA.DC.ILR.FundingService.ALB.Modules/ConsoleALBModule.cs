using System;
using System.Collections.Generic;
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
using ESFA.DC.ILR.FundingService.ALB.InternalData;
using ESFA.DC.ILR.FundingService.ALB.InternalData.Interface;
using ESFA.DC.ILR.FundingService.ALB.OrchestrationService;
using ESFA.DC.ILR.FundingService.ALB.OrchestrationService.Interface;
using ESFA.DC.ILR.FundingService.ALB.Service.Builders;
using ESFA.DC.ILR.FundingService.ALB.Service.Builders.Interface;
using ESFA.DC.ILR.FundingService.ALB.Service.Interface;
using ESFA.DC.ILR.FundingService.ALB.Service.Rulebase;
using ESFA.DC.ILR.FundingService.ALB.Stubs;
using ESFA.DC.ILR.FundingService.ALB.TaskProvider.Interface;
using ESFA.DC.ILR.FundingService.ALB.TaskProvider.Service;
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

namespace ESFA.DC.ILR.FundingService.ALB.Modules
{
    public class ConsoleALBModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<LARS>().As<ILARS>().InstancePerLifetimeScope();
            builder.RegisterType<Postcodes>().As<IPostcodes>().InstancePerLifetimeScope();
            builder.RegisterType<SessionBuilder>().As<ISessionBuilder>().InstancePerLifetimeScope();
            builder.RegisterType<OPADataEntityBuilder>().As<IOPADataEntityBuilder>().WithParameter("yearStartDate", new DateTime(2017, 8, 1)).InstancePerLifetimeScope();
            builder.RegisterType<RulebaseProviderFactory>().As<IRulebaseProviderFactory>().InstancePerLifetimeScope();
            builder.RegisterType<OPAService>().As<IOPAService>().InstancePerLifetimeScope();
            builder.RegisterType<AttributeBuilder>().As<IAttributeBuilder<IAttributeData>>().InstancePerLifetimeScope();
            builder.RegisterType<DataEntityBuilder>().As<IDataEntityBuilder>().InstancePerLifetimeScope();
            builder.RegisterType<FundingOutputService>().As<IFundingOutputService>().InstancePerLifetimeScope();
            builder.RegisterType<Service.FundingService>().As<IFundingService>().InstancePerLifetimeScope();
            builder.RegisterType<InternalDataCache>().As<IInternalDataCache>().InstancePerLifetimeScope();
            builder.RegisterType<LearnerPerActorServiceStub<ILearner, IList<ILearner>>>().As<ILearnerPerActorService<ILearner, IList<ILearner>>>().InstancePerLifetimeScope();
            builder.RegisterType<ALBOrchestrationService>().As<IALBOrchestrationService>().InstancePerLifetimeScope();
            builder.RegisterType<ReferenceDataCache>().As<IReferenceDataCache>().InstancePerLifetimeScope();
            builder.RegisterType<ReferenceDataCachePopulationService>().As<IReferenceDataCachePopulationService>().InstancePerLifetimeScope();
            builder.RegisterType<PreFundingALBOrchestrationService>().As<IPreFundingALBOrchestrationService>().InstancePerLifetimeScope();
            builder.RegisterType<PreFundingALBPopulationService>().As<IPreFundingALBPopulationService>().InstancePerLifetimeScope();
            builder.RegisterType<XmlSerializationService>().As<ISerializationService>().InstancePerLifetimeScope();
            builder.RegisterType<FundingContext>().As<IFundingContext>().InstancePerLifetimeScope();
            builder.RegisterType<FundingContextManager>().As<IFundingContextManager>().InstancePerLifetimeScope();
            builder.RegisterType<DictionaryKeyValuePersistenceService>().As<IKeyValuePersistenceService>().InstancePerLifetimeScope();
            builder.RegisterType<TaskProviderService>().As<ITaskProviderService>().InstancePerLifetimeScope();
            builder.Register(ctx => BuildJobContext()).As<IJobContextMessage>().InstancePerLifetimeScope();
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
                    { JobContextMessageKey.Filename, "fileName" },
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
    }
}
