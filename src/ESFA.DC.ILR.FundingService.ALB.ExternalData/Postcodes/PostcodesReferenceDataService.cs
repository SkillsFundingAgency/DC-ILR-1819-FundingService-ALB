using System;
using System.Collections.Generic;
using ESFA.DC.ILR.FundingService.ALB.ExternalData.Interface;
using ESFA.DC.ILR.FundingService.ALB.ExternalData.Postcodes.Interface;
using ESFA.DC.ILR.FundingService.ALB.ExternalData.Postcodes.Model;

namespace ESFA.DC.ILR.FundingService.ALB.ExternalData.Postcodes
{
    public class PostcodesReferenceDataService : IPostcodesReferenceDataService
    {
        private readonly IReferenceDataCache _referenceDataCache;

        public PostcodesReferenceDataService(IReferenceDataCache referenceDataCache)
        {
            _referenceDataCache = referenceDataCache;
        }

        public string PostcodesCurrentVersion()
        {
            return _referenceDataCache.PostcodeCurrentVersion;
        }

        public IEnumerable<SfaAreaCost> SFAAreaCostsForPostcode(string postcode)
        {
            try
            {
                return _referenceDataCache.SfaAreaCost[postcode];
            }
            catch (Exception ex)
            {
                throw new KeyNotFoundException(string.Format("Cannot find Postcode: " + postcode + " in the Dictionary. Exception details: " + ex));
            }
        }
    }
}
