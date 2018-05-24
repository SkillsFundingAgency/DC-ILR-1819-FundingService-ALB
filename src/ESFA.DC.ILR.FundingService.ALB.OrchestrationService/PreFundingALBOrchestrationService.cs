using System;
using System.Collections.Generic;
using System.Text;
using ESFA.DC.ILR.FundingService.ALB.Contexts.Interface;
using ESFA.DC.ILR.FundingService.ALB.ExternalData.Interface;
using ESFA.DC.ILR.FundingService.ALB.OrchestrationService.Interface;
using ESFA.DC.ILR.FundingService.ALB.Service.Interface;
using ESFA.DC.ILR.Model.Interface;

namespace ESFA.DC.ILR.FundingService.ALB.OrchestrationService
{
    public class PreFundingALBOrchestrationService : IPreFundingALBOrchestrationService
    {
        private readonly IPreFundingALBPopulationService _preFundingALBPopulationService;
        private readonly ILearnerPerActorService<ILearner, IList<ILearner>> _learnerPerActorService;
        private readonly IFundingContext _fundingContext;

        public PreFundingALBOrchestrationService(
            IPreFundingALBPopulationService preFundingALBPopulationService,
            ILearnerPerActorService<ILearner, IList<ILearner>> learnerPerActorService,
            IFundingContext fundingContext)
        {
            _preFundingALBPopulationService = preFundingALBPopulationService;
            _learnerPerActorService = learnerPerActorService;
            _fundingContext = fundingContext;
        }

        public IEnumerable<IList<ILearner>> Execute()
        {
            // populate reference data and valid ALB learners
            _preFundingALBPopulationService.Populate(_fundingContext.ValidLearners);

            // calculate learners per actor
            return _learnerPerActorService.Process();
        }
    }
}
