using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESFA.DC.ILR.FundingService.ALB.Contexts.Interface;
using ESFA.DC.ILR.FundingService.Dto.Interfaces;
using ESFA.DC.ILR.Model;
using ESFA.DC.ILR.Model.Interface;
using ESFA.DC.IO.Interfaces;
using ESFA.DC.JobContext.Interface;
using ESFA.DC.Mapping.Interface;
using ESFA.DC.Serialization.Interfaces;

namespace ESFA.DC.ILR.FundingService.ALB.Contexts
{
    public class FundingContextManager : IFundingContextManager, IMapper<IJobContextMessage, IList<ILearner>>
    {
        private const string ValidLearnRefNumberKey = "ValidLearnRefNumbers";
        private const string UKPRNKey = "UkPrn";

        private readonly IJobContextMessage _jobContextMessage;
        private readonly IFundingServiceDto _fundingServiceDto;

        public FundingContextManager(IJobContextMessage jobContextMessage, IFundingServiceDto fundingServiceDto)
        {
            _jobContextMessage = jobContextMessage;
            _fundingServiceDto = fundingServiceDto;
        }

        public int MapUKPRN()
        {
            return int.Parse(_jobContextMessage.KeyValuePairs.Where(k => k.Key.ToString() == UKPRNKey).Select(v => v.Value.ToString()).Single());
        }

        public IList<ILearner> MapValidLearners()
        {
           return MapTo(_jobContextMessage);
        }

        public IList<ILearner> MapTo(IJobContextMessage jobContextMessage)
        {
            return _fundingServiceDto.Message.Learners.Where(learner =>
                _fundingServiceDto.ValidLearners.Contains(learner.LearnRefNumber)).ToList();
        }

        public IJobContextMessage MapFrom(IList<ILearner> learners)
        {
            throw new NotImplementedException();
        }
    }
}
