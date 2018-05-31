using ESFA.DC.ILR.Model.Interface;
using System.Collections.Generic;

namespace ESFA.DC.ILR.FundingService.ALB.InternalData.Interface
{
    public interface IValidALBLearnersCache
    {
        IList<ILearner> ValidLearners { get; }
    }
}
