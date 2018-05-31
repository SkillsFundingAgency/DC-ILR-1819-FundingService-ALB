//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using ESFA.DC.ILR.FundingService.ALB.InternalData;
//using ESFA.DC.ILR.FundingService.ALB.InternalData.Cache;
//using ESFA.DC.ILR.FundingService.ALB.InternalData.Interface;
//using ESFA.DC.ILR.FundingService.ALB.Service.Interface;
//using ESFA.DC.ILR.Model.Interface;

//namespace ESFA.DC.ILR.FundingService.ALB.Stubs
//{
//    public class LearnerPerActorServiceStub<T, U> : ILearnerPerActorService<T, List<T>>
//        where T : class
//    {
//        private readonly IValidALBLearnersCache _validALBLearnersCache;

//        public LearnerPerActorServiceStub(IValidALBLearnersCache validALBLearnersCache)
//        {
//            _validALBLearnersCache = validALBLearnersCache;
//        }

//        public IEnumerable<List<T>> Process()
//        {
//            var learnersCache = (ValidALBLearnersCache)_validALBLearnersCache;
//            var learnersPerActors = CalculateLearnersPerActor(learnersCache.ValidLearners.Count);

//            return SplitList(learnersCache.ValidLearners.ToList(), learnersPerActors);
//        }

//        private int CalculateLearnersPerActor(int totalMessagesCount)
//        {
//            if (totalMessagesCount <= 500)
//            {
//                return 100;
//            }

//            if (totalMessagesCount <= 1700)
//            {
//                return 500;
//            }

//            if (totalMessagesCount <= 10000)
//            {
//                return 1000;
//            }

//            if (totalMessagesCount <= 30000)
//            {
//                return 5000;
//            }

//            return 10000;
//        }

//        private IEnumerable<List<T>> SplitList(List<T> learners, int nSize = 30)
//        {
//            for (int i = 0; i < learners.Count; i += nSize)
//            {
//                yield return learners.GetRange(i, Math.Min(nSize, learners.Count - i));
//            }
//        }
//    }
//}