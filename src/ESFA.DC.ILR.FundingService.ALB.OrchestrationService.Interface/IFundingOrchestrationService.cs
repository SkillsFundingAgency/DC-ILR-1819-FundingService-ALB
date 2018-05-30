using System.Collections.Generic;
using ESFA.DC.ILR.FundingService.ALB.FundingOutput.Model.Interface;

namespace ESFA.DC.ILR.FundingService.ALB.OrchestrationService.Interface
{
    public interface IFundingOrchestrationService
    {
        IEnumerable<IFundingOutputs> FundingServiceInitilise();
    }
}
