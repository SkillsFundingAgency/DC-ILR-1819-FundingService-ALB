using System;
using System.Collections.Generic;
using System.Linq;
using ESFA.DC.ILR.FundingService.ALB.Service.Interface.Contexts;
using ESFA.DC.ILR.Model;
using ESFA.DC.ILR.Model.Interface;
using ESFA.DC.IO.Interfaces;
using ESFA.DC.JobContext.Interface;
using ESFA.DC.Mapping.Interface;
using ESFA.DC.Serialization.Interfaces;

namespace ESFA.DC.ILR.FundingService.ALB.Service.Contexts
{
    public class FundingContextManager : IFundingContextManager, IMapper<IJobContextMessage, IList<ILearner>>
    {
        private const string ValidLearnRefNumberKey = "ValidLearnRefNumbers";
        private const string UKPRNKey = "UkPrn";

        private readonly IJobContextMessage _jobContextMessage;
        private readonly IKeyValuePersistenceService _keyValuePersistenceService;
        private readonly ISerializationService _serializationService;

        public FundingContextManager(IJobContextMessage jobContextMessage, IKeyValuePersistenceService keyValuePersistenceService, ISerializationService serializationService)
        {
            _jobContextMessage = jobContextMessage;
            _keyValuePersistenceService = keyValuePersistenceService;
            _serializationService = serializationService;
        }

        public int MapUKPRN()
        {
            var key = _jobContextMessage.KeyValuePairs.Where(k => k.Key.ToString() == UKPRNKey).Select(v => v.Value.ToString()).FirstOrDefault();

            return int.Parse(_keyValuePersistenceService.GetAsync(key).Result);
        }

        public IList<ILearner> MapValidLearners()
        {
           return MapTo(_jobContextMessage);
        }

        public IList<ILearner> MapTo(IJobContextMessage jobContextMessage)
        {
            var key = jobContextMessage.KeyValuePairs.Where(k => k.Key.ToString() == ValidLearnRefNumberKey).Select(v => v.Value.ToString()).FirstOrDefault();

            return (IList<ILearner>)_serializationService.Deserialize<MessageLearner[]>(_keyValuePersistenceService.GetAsync(key).Result);
        }

        public IJobContextMessage MapFrom(IList<ILearner> learners)
        {
            throw new NotImplementedException();
        }
    }
}
