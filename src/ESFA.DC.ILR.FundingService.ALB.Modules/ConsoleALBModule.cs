using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Autofac.Core;
using ESFA.DC.Data.LARS.Model;
using ESFA.DC.Data.LARS.Model.Interfaces;
using ESFA.DC.Data.Postcodes.Model;
using ESFA.DC.Data.Postcodes.Model.Interfaces;
using ESFA.DC.ILR.FundingService.ALB.Contexts;
using ESFA.DC.ILR.FundingService.ALB.Contexts.Interface;
using ESFA.DC.ILR.FundingService.ALB.ExternalData;
using ESFA.DC.ILR.FundingService.ALB.ExternalData.Interface;
using ESFA.DC.ILR.FundingService.ALB.OrchestrationService;
using ESFA.DC.ILR.FundingService.ALB.OrchestrationService.Interface;
using ESFA.DC.ILR.FundingService.ALB.Service.Builders;
using ESFA.DC.ILR.FundingService.ALB.Service.Builders.Interface;
using ESFA.DC.ILR.FundingService.ALB.Service.Interface;
using ESFA.DC.ILR.FundingService.ALB.Service.Rulebase;
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
            builder.RegisterType<Service.FundingService>().As<IFundingService>().InstancePerLifetimeScope();
            builder.RegisterType<ReferenceDataCache>().As<IReferenceDataCache>().InstancePerLifetimeScope();
            builder.RegisterType<ReferenceDataCachePopulationService>().As<IReferenceDataCachePopulationService>().InstancePerLifetimeScope();
            builder.RegisterType<ConsoleFundingOrchestrationService>().As<IPreFundingOrchestrationService>().InstancePerLifetimeScope();
            builder.RegisterType<XmlSerializationService>().As<ISerializationService>().InstancePerLifetimeScope();
//            builder.Register(ctx => BuildKeyValueDictionary()).As<IKeyValuePersistenceService>().InstancePerLifetimeScope();
            builder.RegisterType<FundingContext>().As<IFundingContext>().InstancePerLifetimeScope();
            builder.RegisterType<FundingContextManager>().As<IFundingContextManager>().InstancePerLifetimeScope();
            builder.Register(ctx => BuildJobContext()).As<IJobContextMessage>().InstancePerLifetimeScope();
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
                    { JobContextMessageKey.Filename, "test" }, // TODO : add correct file name
                    { JobContextMessageKey.UkPrn, "UKPRN" },
                    { JobContextMessageKey.ValidLearnRefNumbers, "ValidLearnRefNumbers" },
                },
            };
        }
    }
}
