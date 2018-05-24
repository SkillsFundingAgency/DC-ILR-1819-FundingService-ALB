using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESFA.DC.ILR.FundingService.ALB.ExternalData.Cache;
using ESFA.DC.ILR.FundingService.ALB.ExternalData.Interface;
using ESFA.DC.ILR.FundingService.ALB.OrchestrationService.Interface;
using ESFA.DC.ILR.Model.Interface;

namespace ESFA.DC.ILR.FundingService.ALB.OrchestrationService
{
    public class PreFundingALBPopulationService : IPreFundingALBPopulationService
    {
        private readonly IReferenceDataCachePopulationService _referenceDataCachePopulationService;
        private readonly ICache<IList<ILearner>> _validALBLearnersCache;

        public PreFundingALBPopulationService(IReferenceDataCachePopulationService referenceDataCachePopulationService, ICache<IList<ILearner>> validALBLearnersCache)
        {
            _referenceDataCachePopulationService = referenceDataCachePopulationService;
            _validALBLearnersCache = validALBLearnersCache;
        }

        public void Populate(IList<ILearner> learners)
        {
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
            var validALBLearnersCache = (Cache<IList<ILearner>>)_validALBLearnersCache;
            validALBLearnersCache.Item = learnerList;
        }
    }
}
