using System.Collections.Generic;
using System.Linq;
using ESFA.DC.ILR.FundingService.ALB.Contexts.Interface;
using ESFA.DC.ILR.FundingService.ALB.ExternalData.Interface;
using ESFA.DC.ILR.FundingService.ALB.InternalData;
using ESFA.DC.ILR.FundingService.ALB.InternalData.Interface;
using ESFA.DC.ILR.FundingService.ALB.OrchestrationService.Interface;
using ESFA.DC.ILR.Model.Interface;

namespace ESFA.DC.ILR.FundingService.ALB.OrchestrationService
{
    public class PreFundingALBPopulationService : IPreFundingALBPopulationService
    {
        private readonly IReferenceDataCachePopulationService _referenceDataCachePopulationService;
        private readonly IFundingContext _fundingContext;
        private readonly IInternalDataCache _internalDataCache;

        public PreFundingALBPopulationService(IReferenceDataCachePopulationService referenceDataCachePopulationService, IFundingContext fundingContext, IInternalDataCache internalDataCache)
        {
            _referenceDataCachePopulationService = referenceDataCachePopulationService;
            _fundingContext = fundingContext;
            _internalDataCache = internalDataCache;
        }

        public void PopulateData()
        {
            var learners = _fundingContext.ValidLearners;
            IList<ILearner> learnerList = new List<ILearner>();
            HashSet<string> postcodesList = new HashSet<string>();
            HashSet<string> learnAimRefsList = new HashSet<string>();
            bool added = false;

            foreach (var learner in learners)
            {
                foreach (var learningDelivery in learner.LearningDeliveries.Where(ld => ld.FundModel == 99).ToList())
                {
                    if (!added)
                    {
                        learnerList.Add(learner);
                        added = true;
                    }
                    else
                    {
                        break;
                    }

                    if (added)
                    {
                        postcodesList.Add(learningDelivery.DelLocPostCode);
                        learnAimRefsList.Add(learningDelivery.LearnAimRef);
                    }
                }

                added = false;
            }

            _referenceDataCachePopulationService.Populate(learnAimRefsList.ToList(), postcodesList.ToList());

            var internalDataCache = (InternalDataCache)_internalDataCache;
            internalDataCache.UKPRN = _fundingContext.UKPRN;
            internalDataCache.ValidLearners = learnerList;
        }
    }
}
