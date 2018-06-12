using System.Collections.Generic;
using System.Linq;
using ESFA.DC.ILR.FundingService.ALB.FundingOutput.Model;
using ESFA.DC.ILR.FundingService.ALB.FundingOutput.Model.Interface;
using ESFA.DC.ILR.FundingService.ALB.InternalData.Interface;
using ESFA.DC.ILR.FundingService.ALB.OrchestrationService.Interface;
using ESFA.DC.ILR.FundingService.ALB.Stubs.Persistance;
using ESFA.DC.ILR.FundingService.ALB.TaskProvider.Interface;
using ESFA.DC.ILR.Model;
using ESFA.DC.ILR.Model.Interface;
using ESFA.DC.IO.Dictionary;
using ESFA.DC.IO.Interfaces;
using ESFA.DC.Serialization.Xml;

namespace ESFA.DC.ILR.FundingService.ALB.TaskProvider.Service
{
    public class TaskProviderService : ITaskProviderService
    {
        private readonly IKeyValuePersistenceService _keyValuePersistenceService;
        private readonly IInternalDataCache _internalDataCache;
        private readonly IPreFundingALBOrchestrationService _preFundingALBOrchestrationService;
        private readonly IALBOrchestrationService _albOrchestrationService;

        public TaskProviderService(IKeyValuePersistenceService keyValuePersistenceService, IInternalDataCache internalDataCache, IPreFundingALBOrchestrationService preFundingALBOrchestrationService, IALBOrchestrationService albOrchestrationService)
        {
            _keyValuePersistenceService = keyValuePersistenceService;
            _internalDataCache = internalDataCache;
            _preFundingALBOrchestrationService = preFundingALBOrchestrationService;
            _albOrchestrationService = albOrchestrationService;
        }

        public void ExecuteTasks(Message message)
        {
            // Build Persistance Dictionary
            BuildKeyValueDictionary(message);

            // pre funding
            var learnersToProcess = _preFundingALBOrchestrationService.Execute();

            // process funding
            var fundingOutputs = ProcessFunding(learnersToProcess);

            // transform shards in to new object
            var fundingOutputsToPersist = TransformFundingOutput(fundingOutputs);

            // persist
            DataPersister dataPersister = new DataPersister();
            dataPersister.PersistData(fundingOutputsToPersist);
        }

        private void BuildKeyValueDictionary(Message message)
        {
            var learners = message.Learner.ToList();

            var list = new DictionaryKeyValuePersistenceService();
            var serializer = new XmlSerializationService();

            _keyValuePersistenceService.SaveAsync("ValidLearnRefNumbers", serializer.Serialize(learners)).Wait();
        }

        private IList<IFundingOutputs> ProcessFunding(IEnumerable<IList<ILearner>> learnersList)
        {
            int ukprn = _internalDataCache.UKPRN;
            IList<IFundingOutputs> fundingOutputsList = new List<IFundingOutputs>();

            foreach (var list in learnersList)
            {
                fundingOutputsList.Add(_albOrchestrationService.Execute(ukprn, list));
            }

            return fundingOutputsList;
        }

        private IFundingOutputs TransformFundingOutput(IList<IFundingOutputs> fundingOutputsList)
        {
            var global = fundingOutputsList[0].Global;

            var learnerAttributes = fundingOutputsList.SelectMany(l => l.Learners).ToArray();

            return new FundingOutputs
            {
                Global = global,
                Learners = learnerAttributes,
            };
        }
    }
}
