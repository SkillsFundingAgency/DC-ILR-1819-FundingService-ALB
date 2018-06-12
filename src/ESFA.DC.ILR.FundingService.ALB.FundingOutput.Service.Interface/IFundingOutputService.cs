using System.Collections.Generic;
using ESFA.DC.ILR.FundingService.ALB.FundingOutput.Model.Interface;
using ESFA.DC.OPA.Model.Interface;

namespace ESFA.DC.ILR.FundingService.ALB.FundingOutput.Service.Interface
{
    public interface IFundingOutputService
    {
        IFundingOutputs ProcessFundingOutputs(IEnumerable<IDataEntity> dataEntities);
    }
}
