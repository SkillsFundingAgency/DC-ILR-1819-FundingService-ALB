using System;
using System.Diagnostics;
using System.IO;
using Autofac;
using ESFA.DC.Data.LARS.Model;
using ESFA.DC.Data.LARS.Model.Interfaces;
using ESFA.DC.Data.Postcodes.Model;
using ESFA.DC.Data.Postcodes.Model.Interfaces;
using ESFA.DC.ILR.FundingService.ALB.ExternalData;
using ESFA.DC.ILR.FundingService.ALB.ExternalData.Interface;
using ESFA.DC.ILR.FundingService.ALB.Service.Builders.Implementation;
using ESFA.DC.ILR.FundingService.ALB.Service.Builders.Interface;
using ESFA.DC.ILR.FundingService.ALB.Service.Interface;
using ESFA.DC.ILR.FundingService.ALB.Service.Rulebase;
using ESFA.DC.ILR.Model;
using ESFA.DC.ILR.Model.Interface;
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

        public static void Main(string[] args)
        {
            var stopwatch = new Stopwatch();

            try
            {
                System.Console.WriteLine("Loading file..");

                // stream = new FileStream(@"Files\ILR-10006341-1819-20180118-023456-01.xml", FileMode.Open);
                stream = new FileStream(@"Files\ILR-10006341-1819-20180118-023456-02.xml", FileMode.Open);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("File Load Error: Problem loading file... {0}", ex);
            }

            ISerializationService serializationService = new XmlSerializationService();

            IMessage message = serializationService.Deserialize<Message>(stream);

            stream.Close();

            System.Console.WriteLine("Executing Funding Service...");

            var builder = ConfigureBuilder();
            var container = builder.Build();

            using (var scope = container.BeginLifetimeScope())
            {
                var fundingService = container.Resolve<IFundingSevice>();

                stopwatch.Reset();
                stopwatch.Start();

                var fundingOutputs = fundingService.ProcessFunding(message);

                var dataPersister = new DataPersister();
                dataPersister.PersistData(fundingOutputs);

                stopwatch.Stop();
                var inputsCreateTime = stopwatch.Elapsed;
                System.Console.WriteLine("Process completed in " + inputsCreateTime.ToString());
                stopwatch.Reset();
            }
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
            builder.RegisterType<Service.Implementation.FundingService>().As<IFundingSevice>();

            return builder;
        }
    }
}
