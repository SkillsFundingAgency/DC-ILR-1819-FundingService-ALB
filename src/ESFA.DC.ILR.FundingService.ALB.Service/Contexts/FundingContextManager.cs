using System;
using System.Collections.Generic;
using System.Linq;
using ESFA.DC.ILR.FundingService.ALB.Service.Interface.Contexts;
using ESFA.DC.IO.Interfaces;
using ESFA.DC.JobContext.Interface;
using ESFA.DC.Mapping.Interface;
using ESFA.DC.Serialization.Interfaces;

namespace ESFA.DC.ILR.FundingService.ALB.Service.Contexts
{
    public class FundingContextManager : IFundingContextManager, IMapper<IJobContextMessage, IList<string>>
    {
        private const string ValidLearnRefNumberKey = "ValidLearnRefNumbers";

        private readonly IKeyValuePersistenceService _keyValuePersistenceService;
        private readonly ISerializationService _serializationService;

        public FundingContextManager(IKeyValuePersistenceService keyValuePersistenceService, ISerializationService serializationService)
        {
            _keyValuePersistenceService = keyValuePersistenceService;
            _serializationService = serializationService;
        }

        public IList<string> MapFundingContext(IJobContextMessage value)
        {
            return MapTo(value);
        }

        public IList<string> MapTo(IJobContextMessage value)
        {
            var key = value.KeyValuePairs.Where(k => k.Key.ToString() == ValidLearnRefNumberKey).Select(v => v.Value.ToString()).FirstOrDefault();

            return _serializationService.Deserialize<List<string>>(_keyValuePersistenceService.GetAsync(key).Result);
        }

        public IJobContextMessage MapFrom(IList<string> value)
        {
            throw new NotImplementedException();
        }
    }
}
