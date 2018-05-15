using System.Collections.Generic;
using ESFA.DC.ILR.Model.Interface;
using ESFA.DC.JobContext.Interface;

namespace ESFA.DC.ILR.FundingService.ALB.Service.Interface.Contexts
{
    public interface IFundingContextManager
    {
        int MapUKPRN();

        IList<ILearner> MapValidLearners();
    }
}
