using System;
using System.Collections.Generic;
using System.Text;
using ESFA.DC.ILR.FundingService.ALB.ExternalData.Interface;

namespace ESFA.DC.ILR.FundingService.ALB.ExternalData.Cache
{
    public class Cache<T> : ICache<T>
    {
        public virtual T Item { get; set; }
    }
}
