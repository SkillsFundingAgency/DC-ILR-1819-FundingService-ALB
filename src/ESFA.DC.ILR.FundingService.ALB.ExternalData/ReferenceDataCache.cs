using System.Collections.Generic;
using ESFA.DC.ILR.FundingService.ALB.ExternalData.Interface;
using ESFA.DC.ILR.FundingService.ALB.ExternalData.LARS.Model;
using ESFA.DC.ILR.FundingService.ALB.ExternalData.Postcodes.Model;

namespace ESFA.DC.ILR.FundingService.ALB.ExternalData
{
    public class ReferenceDataCache : IReferenceDataCache
    {
        public IDictionary<string, IEnumerable<LARSFunding>> LARSFunding { get; set; } = new Dictionary<string, IEnumerable<LARSFunding>>();

        public IDictionary<string, LARSLearningDelivery> LARSLearningDelivery { get; set; } = new Dictionary<string, LARSLearningDelivery>();

        public string LARSCurrentVersion { get; set; }

        public IDictionary<string, IEnumerable<SfaAreaCost>> SfaAreaCost { get; set; } = new Dictionary<string, IEnumerable<SfaAreaCost>>();

        public string PostcodeCurrentVersion { get; set; }
    }
}
