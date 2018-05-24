using System;
using System.Collections.Generic;
using System.Text;
using ESFA.DC.ILR.FundingService.ALB.OrchestrationService.Interface;
using ESFA.DC.ILR.FundingService.ALB.Service.Interface;
using ESFA.DC.ILR.Model.Interface;
using ESFA.DC.OPA.Model.Interface;

namespace ESFA.DC.ILR.FundingService.ALB.OrchestrationService
{
    public class ActorALBOrchestrationService : IActorALBOrchestrationService
    {
        private readonly IFundingService _fundingService;

        public ActorALBOrchestrationService(IFundingService fundingService)
        {
            _fundingService = fundingService;
        }

        public IEnumerable<IDataEntity> Execute(int ukprn, IList<ILearner> albValidLearners)
        {
            return _fundingService.ProcessFunding(ukprn, albValidLearners);
        }
    }
}
