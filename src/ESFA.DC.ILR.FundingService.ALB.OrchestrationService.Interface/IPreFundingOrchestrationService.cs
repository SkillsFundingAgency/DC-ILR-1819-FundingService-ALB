using System.Collections.Generic;
using ESFA.DC.ILR.Model.Interface;

namespace ESFA.DC.ILR.FundingService.ALB.OrchestrationService.Interface
{
    public interface IPreFundingOrchestrationService
    {
        IList<ILearner> PopulateData(IList<ILearner> learners);
    }
}
