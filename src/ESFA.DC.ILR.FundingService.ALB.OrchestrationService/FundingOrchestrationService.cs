using System;
using System.Collections.Generic;
using System.Text;
using ESFA.DC.ILR.FundingService.ALB.Contexts.Interface;
using ESFA.DC.ILR.FundingService.ALB.FundingOutput.Model.Interface;
using ESFA.DC.ILR.FundingService.ALB.InternalData.Interface;
using ESFA.DC.ILR.FundingService.ALB.OrchestrationService.Interface;
using ESFA.DC.ILR.FundingService.ALB.Service.Interface;
using ESFA.DC.OPA.Model.Interface;

namespace ESFA.DC.ILR.FundingService.ALB.OrchestrationService
{
    public class FundingOrchestrationService : IFundingOrchestrationService
    {
        private readonly IPreFundingOrchestrationService _preFundingOrchestrationService;
        private readonly IFundingContext _fundingContext;
        private readonly IFundingService _fundingService;
        private readonly IValidALBLearnersCache _validALBLearnersCache;

        public FundingOrchestrationService(IPreFundingOrchestrationService preFundingOrchestrationService, IFundingContext fundingContext, IFundingService fundingService, IValidALBLearnersCache validALBLearnersCache)
        {
            _preFundingOrchestrationService = preFundingOrchestrationService;
            _fundingContext = fundingContext;
            _fundingService = fundingService;
            _validALBLearnersCache = validALBLearnersCache;
        }

        public IEnumerable<IFundingOutputs> FundingServiceInitilise()
        {
            var ukprn = _fundingContext.UKPRN;

            _preFundingOrchestrationService.PopulateData(_fundingContext.ValidLearners);

            return _fundingService.ProcessFunding(ukprn, _validALBLearnersCache.ValidLearners);
        }
    }
}
