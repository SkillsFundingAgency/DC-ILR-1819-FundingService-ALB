using System.Collections.Generic;
using ESFA.DC.ILR.FundingService.ALB.FundingOutput.Model.Interface;
using ESFA.DC.ILR.Model.Interface;

namespace ESFA.DC.ILR.FundingService.ALB.OrchestrationService.Interface
{
    public interface IALBOrchestrationService
    {
        IFundingOutputs Execute(int ukprn, IList<ILearner> albValidLearners);
    }
}
