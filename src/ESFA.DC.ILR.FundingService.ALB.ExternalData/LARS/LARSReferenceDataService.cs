using System;
using System.Collections.Generic;
using ESFA.DC.ILR.FundingService.ALB.ExternalData.Interface;
using ESFA.DC.ILR.FundingService.ALB.ExternalData.LARS.Interface;
using ESFA.DC.ILR.FundingService.ALB.ExternalData.LARS.Model;

namespace ESFA.DC.ILR.FundingService.ALB.ExternalData.LARS
{
    public class LARSReferenceDataService : ILARSReferenceDataService
    {
        private readonly IReferenceDataCache _referenceDataCache;

        public LARSReferenceDataService(IReferenceDataCache referenceDataCache)
        {
            _referenceDataCache = referenceDataCache;
        }

        public string LARSCurrentVersion()
        {
            return _referenceDataCache.LARSCurrentVersion;
        }

        public IEnumerable<LARSFunding> LARSFundingsForLearnAimRef(string learnAimRef)
        {
            try
            {
                return _referenceDataCache.LARSFunding[learnAimRef];
            }
            catch (Exception ex)
            {
                throw new KeyNotFoundException(string.Format("Cannot find LARS Funding data for LearnAimRef: " + learnAimRef + " in the Dictionary. Exception details: " + ex));
            }
        }

        public LARSLearningDelivery LARSLearningDeliveriesForLearnAimRef(string learnAimRef)
        {
            try
            {
                return _referenceDataCache.LARSLearningDelivery[learnAimRef];
            }
            catch (Exception ex)
            {
                throw new KeyNotFoundException(string.Format("Cannot find LARS Learning Delivery data for LearnAimRef: " + learnAimRef + " in the Dictionary. Exception details: " + ex));
            }
        }
    }
}
