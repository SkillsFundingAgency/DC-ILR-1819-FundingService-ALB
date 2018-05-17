//using System;
//using System.Collections.Generic;
//using ESFA.DC.ILR.FundingService.ALB.Service.Contexts;
//using ESFA.DC.ILR.FundingService.ALB.Service.Interface.Contexts;
//using ESFA.DC.IO.Dictionary;
//using ESFA.DC.IO.Interfaces;
//using ESFA.DC.JobContext;
//using ESFA.DC.JobContext.Interface;
//using ESFA.DC.Mapping.Interface;
//using ESFA.DC.Serialization.Interfaces;
//using ESFA.DC.Serialization.Xml;
//using FluentAssertions;
//using Xunit;

//namespace ESFA.DC.ILR.FundingService.ALB.Service.Tests
//{
//    public class FundingContextManagerTests
//    {
//        /// <summary>
//        /// Return FundingContextManager
//        /// </summary>
//        [Fact(DisplayName = "FundingContextManager - IMapper - Mapper Instance Exists"), Trait("Funding Context Manager", "Unit")]
//        public void FundingContextMapper_IMapper_InstanceExist()
//        {
//            // ARRANGE
//            // Use Test Helpers

//            //ACT
//            var mapper = Mapper;

//            //ASSERT
//            mapper.Should().NotBeNull();
//        }

//        /// <summary>
//        /// Return FundingContextManager
//        /// </summary>
//        [Fact(DisplayName = "FundingContextManager - IMapper - MapTo Exists"), Trait("Funding Context Manager", "Unit")]
//        public void FundingContextMapper_IMapper_MapToExist()
//        {
//            // ARRANGE
//            // Use Test Helpers

//            //ACT
//            var mapTo = Mapper.MapTo(JobContextMessage);

//            //ASSERT
//            mapTo.Should().NotBeNull();
//        }

//        /// <summary>
//        /// Return FundingContextManager
//        /// </summary>
//        [Fact(DisplayName = "FundingContextManager - IMapper - MapTo Correct"), Trait("Funding Context Manager", "Unit")]
//        public void FundingContextMapper_IMapper_MapToCorrect()
//        {
//            // ARRANGE
//            // Use Test Helpers

//            //ACT
//            var mapTo = Mapper.MapTo(JobContextMessage);

//            //ASSERT
//            mapTo.Should().BeEquivalentTo(FundingContext());
//        }

//        /// <summary>
//        /// Return FundingContextManager
//        /// </summary>
//        [Fact(DisplayName = "FundingContextManager - ContextManager - Exists"), Trait("Funding Context Manager", "Unit")]
//        public void FundingContextMapper_ContextManager_Exists()
//        {
//            // ARRANGE
//            // Use Test Helpers

//            //ACT
//            var fundingContextManager = FundingContextManager;

//            //ASSERT
//            fundingContextManager.Should().NotBeNull();
//        }

//        /// <summary>
//        /// Return FundingContextManager
//        /// </summary>
//        [Fact(DisplayName = "FundingContextManager - ContextManager - MapFundingContext Exists"), Trait("Funding Context Manager", "Unit")]
//        public void FundingContextMapper_ContextManager_MapFundingContextExists()
//        {
//            // ARRANGE
//            // Use Test Helpers

//            //ACT
//            var fundingContextManager = FundingContextManager.MapValidLearners(JobContextMessage);

//            //ASSERT
//            fundingContextManager.Should().NotBeNull();
//        }

//        /// <summary>
//        /// Return FundingContextManager
//        /// </summary>
//        [Fact(DisplayName = "FundingContextManager - ContextManager - MapFundingContext Count Correct"), Trait("Funding Context Manager", "Unit")]
//        public void FundingContextMapper_ContextManager_MapFundingContextCountCorrect()
//        {
//            // ARRANGE
//            // Use Test Helpers

//            //ACT
//            var fundingContextManager = FundingContextManager.MapValidLearners(JobContextMessage);

//            //ASSERT
//            fundingContextManager.Count.Should().Be(2);
//        }

//        /// <summary>
//        /// Return FundingContextManager
//        /// </summary>
//        [Fact(DisplayName = "FundingContextManager - ContextManager - MapFundingContext Correct"), Trait("Funding Context Manager", "Unit")]
//        public void FundingContextMapper_ContextManager_MapFundingContextCorrect()
//        {
//            // ARRANGE
//            // Use Test Helpers

//            //ACT
//            var fundingContextManager = FundingContextManager.MapValidLearners(JobContextMessage);

//            //ASSERT
//            fundingContextManager.Should().BeEquivalentTo(LearnRefNumbers);
//        }


//        #region Test Helpers

//        private IFundingContextManager FundingContextManager = new FundingContextManager(keyValuePersistenceService, serializationService);

//        private IMapper<IJobContextMessage, IList<string>> Mapper = new FundingContextManager(keyValuePersistenceService, serializationService);

//        private static IJobContextMessage JobContextMessage => new JobContextMessage
//        {
//            JobId = 1,
//            SubmissionDateTimeUtc = DateTime.Parse("2018-08-01").ToUniversalTime(),
//            Topics = Topics,
//            TopicPointer = 1,
//            KeyValuePairs = KeyValuePairsDictionary,
//        };

//        private static IReadOnlyList<ITopicItem> Topics => new List<TopicItem>();

//        private static IDictionary<JobContextMessageKey, object> KeyValuePairsDictionary => new Dictionary<JobContextMessageKey, object>()
//        {
//            { JobContextMessageKey.ValidLearnRefNumbers, "ValidLearnRefNumbers" }
//        };

//        private IList<string> FundingContext()
//        {
//            return Mapper.MapTo(JobContextMessage);
//        }

//        private static IKeyValuePersistenceService keyValuePersistenceService => BuildKeyValueDictionary();

//        private static ISerializationService serializationService => new XmlSerializationService();

//        private static DictionaryKeyValuePersistenceService BuildKeyValueDictionary()
//        {
//            var learnRefNumbers = LearnRefNumbers;
//            var list = new DictionaryKeyValuePersistenceService();
//            var serializer = new XmlSerializationService();

//            list.SaveAsync("ValidLearnRefNumbers", serializer.Serialize(learnRefNumbers)).Wait();

//            return list;
//        }

//        private static IList<string> LearnRefNumbers => new List<string> { "Learner1", "Learner2" };

//        #endregion
//    }
//}
