using System;
using System.Collections.Generic;
using System.Text;

namespace ESFA.DC.ILR.FundingService.ALB.ExternalData.Interface
{
    public interface ICache<out T>
    {
        T Item { get; }
    }
}
