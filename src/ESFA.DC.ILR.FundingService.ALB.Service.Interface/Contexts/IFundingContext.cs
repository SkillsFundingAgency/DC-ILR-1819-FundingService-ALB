using System.Collections.Generic;
using ESFA.DC.ILR.Model.Interface;

namespace ESFA.DC.ILR.FundingService.ALB.Service.Interface.Contexts
{
    public interface IFundingContext
    {
        int UKPRN { get; }

        IList<ILearner> ValidLearners { get; }
    }
}
