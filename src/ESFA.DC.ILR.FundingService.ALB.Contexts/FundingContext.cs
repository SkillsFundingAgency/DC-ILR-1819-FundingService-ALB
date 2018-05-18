using System.Collections.Generic;
using ESFA.DC.ILR.FundingService.ALB.Contexts.Interface;
using ESFA.DC.ILR.Model.Interface;

namespace ESFA.DC.ILR.FundingService.ALB.Contexts
{
    public class FundingContext : IFundingContext
    {
        private readonly IFundingContextManager _fundingContextManager;

        public FundingContext(IFundingContextManager fundingContextManager)
        {
            _fundingContextManager = fundingContextManager;
        }

        public int UKPRN => _fundingContextManager.MapUKPRN();

        public IList<ILearner> ValidLearners => _fundingContextManager.MapValidLearners();
    }
}
