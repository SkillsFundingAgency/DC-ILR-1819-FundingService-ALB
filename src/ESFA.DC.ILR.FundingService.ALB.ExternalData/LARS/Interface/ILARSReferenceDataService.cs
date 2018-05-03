using System.Collections.Generic;
using ESFA.DC.ILR.FundingService.ALB.ExternalData.LARS.Model;

namespace ESFA.DC.ILR.FundingService.ALB.ExternalData.LARS.Interface
{
    public interface ILARSReferenceDataService
    {
        string LARSCurrentVersion();

        IEnumerable<LARSFunding> LARSFundingsForLearnAimRef(string learnAimRef);

        LARSLearningDelivery LARSLearningDeliveriesForLearnAimRef(string learnAimRef);
    }
}
