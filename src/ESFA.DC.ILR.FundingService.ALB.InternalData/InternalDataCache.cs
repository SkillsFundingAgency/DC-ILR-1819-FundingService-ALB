using System.Collections.Generic;
using ESFA.DC.ILR.FundingService.ALB.InternalData.Interface;
using ESFA.DC.ILR.Model.Interface;

namespace ESFA.DC.ILR.FundingService.ALB.InternalData
{
    public class InternalDataCache : IInternalDataCache
    {
        public int UKPRN { get; set; }

        public IList<ILearner> ValidLearners { get; set; }
    }
}
