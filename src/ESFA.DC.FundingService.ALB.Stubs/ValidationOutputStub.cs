using System;
using System.Collections.Generic;
using System.Text;
using ESFA.DC.ILR.FundingService.ALB.Service.Interface;
using ESFA.DC.IO.Interfaces;
using ESFA.DC.Serialization.Interfaces;

namespace ESFA.DC.ILR.FundingService.ALB.Stubs
{
    public class ValidationOutputStub
    {
        private IFundingContext _fundingContext;
        private IKeyValuePersistenceService _keyValuePersistenceService;
        private ISerializationService _serializationService;

        public ValidationOutputStub(IFundingContext fundingContext, IKeyValuePersistenceService keyValuePersistenceService, ISerializationService serializationService)
        {
            _fundingContext = fundingContext;
            _keyValuePersistenceService = keyValuePersistenceService;
            _serializationService = serializationService;
        }

        public void ValidLearners(IList<string> learnRefNumbers)
        {
            if (learnRefNumbers != null)
            {
                _keyValuePersistenceService.SaveAsync(_fundingContext.ValidLearnRefNumbersKey, _serializationService.Serialize(learnRefNumbers)).Wait();
            }
        }
    }
}
