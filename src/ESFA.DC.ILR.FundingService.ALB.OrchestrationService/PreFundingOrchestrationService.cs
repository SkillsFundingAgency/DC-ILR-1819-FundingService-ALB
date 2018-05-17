using System;
using System.Collections.Generic;
using System.Linq;
using ESFA.DC.ILR.FundingService.ALB.ExternalData.Interface;
using ESFA.DC.ILR.FundingService.ALB.OrchestrationService.Interface;
using ESFA.DC.ILR.FundingService.ALB.Service.Interface;
using ESFA.DC.ILR.FundingService.ALB.Service.Interface.Contexts;
using ESFA.DC.ILR.Model.Interface;
using ESFA.DC.OPA.Model.Interface;

namespace ESFA.DC.ILR.FundingService.ALB.OrchestrationService
{
    public class PreFundingOrchestrationService : IPreFundingOrchestrationService
    {
        private readonly IReferenceDataCachePopulationService _referenceDataCachePopulationService;
        private readonly IFundingContext _fundingContext;
        private readonly IFundingService _fundingService;

        public PreFundingOrchestrationService(IReferenceDataCachePopulationService referenceDataCachePopulationService, IFundingContext fundingContext, IFundingService fundingService)
        {
            _referenceDataCachePopulationService = referenceDataCachePopulationService;
            _fundingContext = fundingContext;
            _fundingService = fundingService;
        }

        public IEnumerable<IDataEntity> FundingServiceInitilise()
        {
            var ukprn = _fundingContext.UKPRN;

            var learners = PopulateData(_fundingContext.ValidLearners);

            return _fundingService.ProcessFunding(ukprn, learners);
        }

        protected internal IList<ILearner> PopulateData(IList<ILearner> learners)
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

            return learnerList;
        }
    }
}
