using System.Collections.Generic;
using ESFA.DC.OPA.Model.Interface;

namespace ESFA.DC.ILR.FundingService.ALB.OrchestrationService.Interface
{
    public interface IPreFundingOrchestrationService
    {
        IEnumerable<IDataEntity> FundingServiceInitilise();
    }
}
