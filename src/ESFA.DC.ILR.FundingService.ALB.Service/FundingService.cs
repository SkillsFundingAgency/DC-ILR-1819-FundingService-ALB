using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using ESFA.DC.ILR.FundingService.ALB.ExternalData.Interface;
using ESFA.DC.ILR.FundingService.ALB.Service.Builders.Interface;
using ESFA.DC.ILR.FundingService.ALB.Service.Interface;
using ESFA.DC.ILR.FundingService.ALB.Service.Interface.Contexts;
using ESFA.DC.ILR.Model.Interface;
using ESFA.DC.IO.Interfaces;
using ESFA.DC.OPA.Model.Interface;
using ESFA.DC.OPA.Service.Interface;
using ESFA.DC.Serialization.Interfaces;

namespace ESFA.DC.ILR.FundingService.ALB.Service
{
    public class FundingService : IFundingSevice
    {
        private readonly IReferenceDataCachePopulationService _referenceDataCachePopulationService;
        private readonly IFundingContext _fundingContext;
        private readonly IDataEntityBuilder _dataEntityBuilder;
        private readonly IOPAService _opaService;

        public FundingService(IReferenceDataCachePopulationService referenceDataCachePopulationService, IFundingContext fundingContext, IDataEntityBuilder dataEntityBuilder, IOPAService opaService)
        {
            _referenceDataCachePopulationService = referenceDataCachePopulationService;
            _fundingContext = fundingContext;
            _dataEntityBuilder = dataEntityBuilder;
            _opaService = opaService;
        }

        public IEnumerable<IDataEntity> ProcessFunding(IMessage message)
        {
            int ukprn = message.LearningProviderEntity.UKPRN;

            var learners = message.Learners.Where(l =>
                l.LearningDeliveries.Any(fm => fm.FundModel == 99 && _fundingContext.ValidLearnRefNumbers.Contains(l.LearnRefNumber)));

            PopulateReferenceData(learners);

            // Generate Funding Inputs
            var inputDataEntities = _dataEntityBuilder.EntityBuilder(ukprn, learners);

            // Execute OPA
            var outputDataEntities = new ConcurrentBag<IDataEntity>();

            foreach (var globalEntity in inputDataEntities)
            {
                IDataEntity sessionEntity = _opaService.ExecuteSession(globalEntity);

                outputDataEntities.Add(sessionEntity);
            }

            return outputDataEntities;
        }

        protected internal void PopulateReferenceData(IEnumerable<ILearner> learners)
        {
            var postcodesList = learners.SelectMany(l => l.LearningDeliveries.Select(ld => ld.DelLocPostCode)).Distinct().ToList();
            var learnAimRefs = learners.SelectMany(l => l.LearningDeliveries.Select(ld => ld.LearnAimRef)).Distinct().ToList();

            _referenceDataCachePopulationService.Populate(learnAimRefs, postcodesList);
        }
    }
}
