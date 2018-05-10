using System.Collections.Generic;
using ESFA.DC.IO.Interfaces;
using ESFA.DC.Serialization.Interfaces;

namespace ESFA.DC.ILR.FundingService.ALB.Stubs
{
    public class ValidationOutputStub
    {
        private IKeyValuePersistenceService _keyValuePersistenceService;
        private ISerializationService _serializationService;

        public ValidationOutputStub(IKeyValuePersistenceService keyValuePersistenceService, ISerializationService serializationService)
        {
            _keyValuePersistenceService = keyValuePersistenceService;
            _serializationService = serializationService;
        }

        public void ValidLearners(IList<string> learnRefNumbers)
        {
            if (learnRefNumbers != null)
            {
                _keyValuePersistenceService.SaveAsync("ValidLearnRefNumbers", _serializationService.Serialize(learnRefNumbers)).Wait();
            }
        }
    }
}
