using System.Collections.Generic;
using ESFA.DC.ILR.FundingService.ALB.ExternalData.Postcodes.Model;

namespace ESFA.DC.ILR.FundingService.ALB.ExternalData.Postcodes.Interface
{
    public interface IPostcodesReferenceDataService
    {
        string PostcodesCurrentVersion();

        IEnumerable<SfaAreaCost> SFAAreaCostsForPostcode(string postcode);
    }
}
