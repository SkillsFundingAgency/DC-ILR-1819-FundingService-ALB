using System.Collections.Generic;
using ESFA.DC.ILR.FundingService.ALB.Service.Interface.Contexts;
using ESFA.DC.JobContext.Interface;

namespace ESFA.DC.ILR.FundingService.ALB.Service.Contexts
{
    public class FundingContext : IFundingContext
    {
        private readonly IJobContextMessage _jobContextMessage;
        private readonly IFundingContextManager _fundingContextManager;

        public FundingContext(IJobContextMessage jobContextMessage, IFundingContextManager fundingContextManager)
        {
            _jobContextMessage = jobContextMessage;
            _fundingContextManager = fundingContextManager;
        }

        public IList<string> ValidLearnRefNumbers => _fundingContextManager.MapFundingContext(_jobContextMessage);
    }
}
