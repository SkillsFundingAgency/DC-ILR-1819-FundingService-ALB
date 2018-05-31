using System.Collections.Generic;
using ESFA.DC.ILR.FundingService.ALB.Contexts.Interface;
using ESFA.DC.ILR.FundingService.ALB.OrchestrationService.Interface;
using ESFA.DC.ILR.FundingService.ALB.Service.Interface;
using ESFA.DC.ILR.Model.Interface;

namespace ESFA.DC.ILR.FundingService.ALB.OrchestrationService
{
    public class PreFundingALBOrchestrationService : IPreFundingALBOrchestrationService
    {
        private readonly IPreFundingALBPopulationService _preFundingOrchestrationService;
        private readonly ILearnerPerActorService<ILearner, IList<ILearner>> _learnerPerActorService;

        public PreFundingALBOrchestrationService(IPreFundingALBPopulationService preFundingOrchestrationService, ILearnerPerActorService<ILearner, IList<ILearner>> learnerPerActorService)
        {
            _preFundingOrchestrationService = preFundingOrchestrationService;
            _learnerPerActorService = learnerPerActorService;
        }

        public IEnumerable<IList<ILearner>> Execute()
        {
            // var ukprn = _fundingContext.UKPRN;

            _preFundingOrchestrationService.PopulateData();

            // return _fundingService.ProcessFunding(ukprn, _validALBLearnersCache.ValidLearners);
            return _learnerPerActorService.Process();
        }
    }
}
