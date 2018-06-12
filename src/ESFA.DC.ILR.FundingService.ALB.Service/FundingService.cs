using System.Collections.Concurrent;
using System.Collections.Generic;
using ESFA.DC.ILR.FundingService.ALB.FundingOutput.Model.Interface;
using ESFA.DC.ILR.FundingService.ALB.FundingOutput.Service.Interface;
using ESFA.DC.ILR.FundingService.ALB.Service.Builders.Interface;
using ESFA.DC.ILR.FundingService.ALB.Service.Interface;
using ESFA.DC.ILR.Model.Interface;
using ESFA.DC.OPA.Model.Interface;
using ESFA.DC.OPA.Service.Interface;

namespace ESFA.DC.ILR.FundingService.ALB.Service
{
    public class FundingService : IFundingService
    {
        private readonly IDataEntityBuilder _dataEntityBuilder;
        private readonly IOPAService _opaService;
        private readonly IFundingOutputService _fundingOutputService;

        public FundingService(IDataEntityBuilder dataEntityBuilder, IOPAService opaService, IFundingOutputService fundingOutputService)
        {
            _dataEntityBuilder = dataEntityBuilder;
            _opaService = opaService;
            _fundingOutputService = fundingOutputService;
        }

        public IFundingOutputs ProcessFunding(int ukprn, IList<ILearner> learnerList)
        {
            // Generate Funding Inputs
            var inputDataEntities = BuildInputEntities(ukprn, learnerList);

            // Execute OPA
            var outputDataEntities = ExecuteSessions(inputDataEntities);

            // Transform to FundingOutput Model and return
            return DataEntitytoFundingOutput(outputDataEntities);
        }

        protected internal IEnumerable<IDataEntity> BuildInputEntities(int ukprn, IList<ILearner> learnerList)
        {
            return _dataEntityBuilder.EntityBuilder(ukprn, learnerList);
        }

        protected internal ConcurrentBag<IDataEntity> ExecuteSessions(IEnumerable<IDataEntity> inputDataEntities)
        {
            var outputDataEntities = new ConcurrentBag<IDataEntity>();

            foreach (var globalEntity in inputDataEntities)
            {
                IDataEntity sessionEntity = _opaService.ExecuteSession(globalEntity);

                outputDataEntities.Add(sessionEntity);
            }

            return outputDataEntities;
        }

        protected internal IFundingOutputs DataEntitytoFundingOutput(ConcurrentBag<IDataEntity> dataEntities)
        {
            return _fundingOutputService.ProcessFundingOutputs(dataEntities);
        }
    }
}
