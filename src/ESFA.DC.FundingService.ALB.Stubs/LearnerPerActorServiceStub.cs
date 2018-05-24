using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESFA.DC.ILR.FundingService.ALB.ExternalData.Cache;
using ESFA.DC.ILR.FundingService.ALB.ExternalData.Interface;
using ESFA.DC.ILR.FundingService.ALB.Service.Interface;
using ESFA.DC.ILR.Model.Interface;

namespace ESFA.DC.ILR.FundingService.ALB.Stubs
{
    public class LearnerPerActorServiceStub<T, U> : ILearnerPerActorService<T, List<T>>
        where T : class
    {
        private readonly ICache<IList<T>> _validAlbLearnersCache;

        public LearnerPerActorServiceStub(ICache<IList<T>> validALBLearnersCache)
        {
            _validAlbLearnersCache = validALBLearnersCache;
        }

        public IEnumerable<List<T>> Process()
        {
            var learnersCache = (Cache<IList<T>>)_validAlbLearnersCache;
            var learnersPerActors = CalculateLearnersPerActor(learnersCache.Item.Count);
            return SplitList(learnersCache.Item.ToList(), learnersPerActors);
        }

        private int CalculateLearnersPerActor(int totalMessagesCount)
        {
            if (totalMessagesCount <= 500)
            {
                return 100;
            }

            if (totalMessagesCount <= 1700)
            {
                return 500;
            }

            if (totalMessagesCount <= 10000)
            {
                return 1000;
            }

            if (totalMessagesCount <= 30000)
            {
                return 5000;
            }

            return 10000;
        }

        private IEnumerable<List<T>> SplitList(List<T> learners, int nSize = 30)
        {
            for (int i = 0; i < learners.Count; i += nSize)
            {
                yield return learners.GetRange(i, Math.Min(nSize, learners.Count - i));
            }
        }
    }
}
