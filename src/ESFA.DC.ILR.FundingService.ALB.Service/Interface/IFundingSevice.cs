using System.Collections.Generic;
using ESFA.DC.ILR.Model;
using ESFA.DC.ILR.Model.Interface;
using ESFA.DC.OPA.Model.Interface;

namespace ESFA.DC.ILR.FundingService.ALB.Service.Interface
{
    public interface IFundingSevice
    {
        IEnumerable<IDataEntity> ProcessFunding(IMessage message);
    }
}
