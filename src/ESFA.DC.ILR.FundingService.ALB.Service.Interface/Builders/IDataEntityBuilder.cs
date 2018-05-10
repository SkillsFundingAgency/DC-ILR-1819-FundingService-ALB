using System.Collections.Generic;
using ESFA.DC.ILR.Model.Interface;
using ESFA.DC.OPA.Model.Interface;

namespace ESFA.DC.ILR.FundingService.ALB.Service.Builders.Interface
{
    public interface IDataEntityBuilder
    {
        IEnumerable<IDataEntity> EntityBuilder(int ukprn, IEnumerable<ILearner> learners);
    }
}
