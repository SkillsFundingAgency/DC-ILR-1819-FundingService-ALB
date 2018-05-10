using System.Collections.Generic;

namespace ESFA.DC.ILR.FundingService.ALB.Service.Interface.Contexts
{
    public interface IFundingContext
    {
       IList<string> ValidLearnRefNumbers { get; }
    }
}
