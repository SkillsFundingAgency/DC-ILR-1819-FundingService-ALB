using System;
using System.Collections.Generic;
using System.Text;

namespace ESFA.DC.ILR.FundingService.ALB.Service.Interface
{
    public interface ILearnerPerActorService<T, out U>
        where T : class
    {
        IEnumerable<U> Process();
    }
}
