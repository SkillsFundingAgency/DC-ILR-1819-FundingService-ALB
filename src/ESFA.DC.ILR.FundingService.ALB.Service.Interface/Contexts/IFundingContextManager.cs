using System.Collections.Generic;
using ESFA.DC.JobContext.Interface;

namespace ESFA.DC.ILR.FundingService.ALB.Service.Interface.Contexts
{
    public interface IFundingContextManager
    {
        IList<string> MapFundingContext(IJobContextMessage value);
    }
}
