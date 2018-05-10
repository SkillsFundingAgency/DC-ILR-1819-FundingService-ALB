using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using ESFA.DC.ILR.FundingService.ALB.ExternalData.Interface;
using ESFA.DC.ILR.FundingService.ALB.Service.Builders.Interface;
using ESFA.DC.ILR.FundingService.ALB.Service.Interface;
using ESFA.DC.ILR.FundingService.ALB.Service.Interface.Contexts;
using ESFA.DC.ILR.Model.Interface;
using ESFA.DC.OPA.Model.Interface;
using ESFA.DC.OPA.Service.Interface;

namespace ESFA.DC.ILR.FundingService.ALB.Service
{
    public class FundingService : IFundingSevice
    {
        private readonly IReferenceDataCachePopulationService _referenceDataCachePopulationService;
        private readonly IFundingContext _fundingContext;
        private readonly IDataEntityBuilder _dataEntityBuilder;
        private readonly IOPAService _opaService;
        private readonly IList<ILearner> learnerList = new List<ILearner>();
        private readonly HashSet<string> postcodesList = new HashSet<string>();
        private readonly HashSet<string> learnAimRefsList = new HashSet<string>();
        private bool added = false;

        public FundingService(IReferenceDataCachePopulationService referenceDataCachePopulationService, IFundingContext fundingContext, IDataEntityBuilder dataEntityBuilder, IOPAService opaService)
        {
            _referenceDataCachePopulationService = referenceDataCachePopulationService;
            _fundingContext = fundingContext;
            _dataEntityBuilder = dataEntityBuilder;
            _opaService = opaService;
        }

        public IEnumerable<IDataEntity> ProcessFunding(IMessage message)
        {
            // Get the UKPRN
            int ukprn = message.LearningProviderEntity.UKPRN;

            // Populate Learner and Reference Data
            PopulateData(message);

            // Generate Funding Inputs
            var inputDataEntities = _dataEntityBuilder.EntityBuilder(ukprn, learnerList);

            // Execute OPA
            var outputDataEntities = new ConcurrentBag<IDataEntity>();

            foreach (var globalEntity in inputDataEntities)
            {
                IDataEntity sessionEntity = _opaService.ExecuteSession(globalEntity);

                outputDataEntities.Add(sessionEntity);
            }

            return outputDataEntities;
        }

        protected internal void PopulateData(IMessage message)
        {
            foreach (var learner in message.Learners)
            {
                foreach (var learningDelivery in learner.LearningDeliveries.Where(ld => ld.FundModel == 99).ToList())
                {
                    if (!added && _fundingContext.ValidLearnRefNumbers.Contains(learner.LearnRefNumber))
                    {
                        if (!added)
                        {
                            learnerList.Add(learner);
                            added = true;
                        }
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
        }
    }
}
