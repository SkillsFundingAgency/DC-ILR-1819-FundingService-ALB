using System.Collections.Generic;
using ESFA.DC.ILR.Model.Interface;
using ESFA.DC.OPA.Model.Interface;

namespace ESFA.DC.ILR.FundingService.ALB.OrchestrationService.Interface
{
    public interface IActorALBOrchestrationService
    {
        IEnumerable<IDataEntity> Execute(int ukprn, IList<ILearner> albValidLearners);
    }
}