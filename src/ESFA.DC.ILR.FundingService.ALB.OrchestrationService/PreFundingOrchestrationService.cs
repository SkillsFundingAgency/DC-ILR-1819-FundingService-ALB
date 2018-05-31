using System.Collections.Generic;
using System.Linq;
using ESFA.DC.ILR.FundingService.ALB.Contexts.Interface;
using ESFA.DC.ILR.FundingService.ALB.ExternalData.Interface;
using ESFA.DC.ILR.FundingService.ALB.InternalData;
using ESFA.DC.ILR.FundingService.ALB.InternalData.Interface;
using ESFA.DC.ILR.FundingService.ALB.OrchestrationService.Interface;
using ESFA.DC.ILR.FundingService.ALB.Service.Interface;
using ESFA.DC.ILR.Model.Interface;
using ESFA.DC.OPA.Model.Interface;

namespace ESFA.DC.ILR.FundingService.ALB.OrchestrationService
{
    public class PreFundingOrchestrationService : IPreFundingOrchestrationService
    {
        private readonly IReferenceDataCachePopulationService _referenceDataCachePopulationService;
        private readonly IValidALBLearnersCache _validALBLearnersCache;

        public PreFundingOrchestrationService(IReferenceDataCachePopulationService referenceDataCachePopulationService, IValidALBLearnersCache validALBLearnersCache)
        {
            _referenceDataCachePopulationService = referenceDataCachePopulationService;
            _validALBLearnersCache = validALBLearnersCache;
        }

        public void PopulateData(IList<ILearner> learners)
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

            var validALBLearnersCache = (ValidALBLearnersCache)_validALBLearnersCache;
            validALBLearnersCache.ValidLearners = learnerList;
        }
    }
}
