using System.Collections.Generic;
using ESFA.DC.ILR.FundingService.ALB.FundingOutput.Model.Interface;
using ESFA.DC.ILR.FundingService.ALB.OrchestrationService.Interface;
using ESFA.DC.ILR.FundingService.ALB.Service.Interface;
using ESFA.DC.ILR.Model.Interface;

namespace ESFA.DC.ILR.FundingService.ALB.OrchestrationService
{
    public class ALBOrchestrationService : IALBOrchestrationService
    {
        private readonly IFundingService _fundingService;

        public ALBOrchestrationService(IFundingService fundingService)
        {
            _fundingService = fundingService;
        }

        public IFundingOutputs Execute(int ukprn, IList<ILearner> albValidLearners)
        {
            return _fundingService.ProcessFunding(ukprn, albValidLearners);
        }
    }
}