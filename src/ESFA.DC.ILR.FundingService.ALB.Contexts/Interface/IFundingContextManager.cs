using System.Collections.Generic;
using ESFA.DC.ILR.Model.Interface;

namespace ESFA.DC.ILR.FundingService.ALB.Contexts.Interface
{
    public interface IFundingContextManager
    {
        int MapUKPRN();

        IList<ILearner> MapValidLearners();
    }
}
