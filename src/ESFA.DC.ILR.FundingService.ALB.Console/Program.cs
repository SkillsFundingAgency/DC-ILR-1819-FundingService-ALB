using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Autofac;
using ESFA.DC.ILR.FundingService.ALB.Modules;
using ESFA.DC.ILR.FundingService.ALB.TaskProvider.Interface;
using ESFA.DC.ILR.Model;
using ESFA.DC.IO.Dictionary;
using ESFA.DC.JobContext;
using ESFA.DC.JobContext.Interface;
using ESFA.DC.Serialization.Interfaces;
using ESFA.DC.Serialization.Xml;

namespace ESFA.DC.ILR.FundingService.ALB.Console
{
    public static class Program
    {
        // private static string fileName = "ILR-10006341-1819-20180118-023456-01.xml";
        private const string FileName = "ILR-10006341-1819-20180118-023456-02.xml";

        private static Stream stream;

        private static Message message;

        public static void Main(string[] args)
        {
            var stopwatch = new Stopwatch();

            GetILRFile();

            System.Console.WriteLine("Executing Funding Service...");

            var container = BuildContainer();

            using (var scope = container.BeginLifetimeScope())
            {
                var fundingTasks = container.Resolve<ITaskProviderService>();

                stopwatch.Start();

                fundingTasks.ExecuteTasks(message);

                var fundingCreateTime = stopwatch.Elapsed;
                System.Console.WriteLine("Process completed in " + fundingCreateTime.ToString());
            }
        }

        public static void GetILRFile()
        {
            try
            {
                System.Console.WriteLine("Loading file..");

                stream = new FileStream(@"Files\" + FileName, FileMode.Open);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("File Load Error: Problem loading file... {0}", ex);
            }

            ISerializationService serializationService = new XmlSerializationService();
            message = serializationService.Deserialize<Message>(stream);

            stream.Close();
        }

        private static IContainer BuildContainer()
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterModule<ConsoleALBModule>();

            return containerBuilder.Build();
        }
    }
}
