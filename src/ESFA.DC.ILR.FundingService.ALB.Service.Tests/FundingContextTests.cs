//using ESFA.DC.ILR.FundingService.ALB.Service.Contexts;
//using ESFA.DC.ILR.FundingService.ALB.Service.Interface.Contexts;
//using ESFA.DC.IO.Dictionary;
//using ESFA.DC.IO.Interfaces;
//using ESFA.DC.JobContext;
//using ESFA.DC.JobContext.Interface;
//using ESFA.DC.Serialization.Interfaces;
//using ESFA.DC.Serialization.Xml;
//using FluentAssertions;
//using System;
//using System.Collections.Generic;
//using Xunit;

//namespace ESFA.DC.ILR.FundingService.ALB.Service.Tests
//{
//    public class FundingContextTests
//    {
//        /// <summary>
//        /// Return FundingContextManager
//        /// </summary>
//        [Fact(DisplayName = "FundingContext - Instance Exists"), Trait("Funding Context", "Unit")]
//        public void FundingContext_Exists()
//        {
//            // ARRANGE
//            // Use Test Helpers

//            //ACT
//            var fundingContext = FundngContext;

//            //ASSERT
//            fundingContext.Should().NotBeNull();
//        }

//        /// <summary>
//        /// Return FundingContextManager
//        /// </summary>
//        [Fact(DisplayName = "FundingContext - ValidLearnRefNumbers Exists"), Trait("Funding Context", "Unit")]
//        public void FundingContext_ValidLearnRefNumbersExists()
//        {
//            // ARRANGE
//            // Use Test Helpers

//            //ACT
//            var validLearnRefNumbers = FundngContext.ValidLearnRefNumbers;

//            //ASSERT
//            validLearnRefNumbers.Should().NotBeNull();
//        }

//        /// <summary>
//        /// Return FundingContextManager
//        /// </summary>
//        [Fact(DisplayName = "FundingContext - ValidLearnRefNumbers Count Correct"), Trait("Funding Context", "Unit")]
//        public void FundingContext_ValidLearnRefNumbersCountCorrect()
//        {
//            // ARRANGE
//            // Use Test Helpers

//            //ACT
//            var validLearnRefNumbers = FundngContext.ValidLearnRefNumbers;

//            //ASSERT
//            validLearnRefNumbers.Count.Should().Be(2);
//        }

//        /// <summary>
//        /// Return FundingContextManager
//        /// </summary>
//        [Fact(DisplayName = "FundingContext - ValidLearnRefNumbers Correct"), Trait("Funding Context", "Unit")]
//        public void FundingContext_ValidLearnRefNumbersCorrect()
//        {
//            // ARRANGE
//            // Use Test Helpers

//            //ACT
//            var validLearnRefNumbers = FundngContext.ValidLearnRefNumbers;

//            //ASSERT
//            validLearnRefNumbers.Should().BeEquivalentTo(LearnRefNumbers);
//        }

//        #region Test Helpers
                
//        private static readonly IFundingContextManager fundingContextManager = new FundingContextManager(KeyValuePersistenceService, SerializationService);

//        private IFundingContext FundngContext = new FundingContext(JobContextMessage, fundingContextManager);

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
              
//        private static IKeyValuePersistenceService KeyValuePersistenceService => BuildKeyValueDictionary();

//        private static ISerializationService SerializationService => new XmlSerializationService();

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
