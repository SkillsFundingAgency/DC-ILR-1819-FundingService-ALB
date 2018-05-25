using System;
using System.Globalization;
using System.Linq;
using ESFA.DC.ILR.FundingService.ALB.FundingOutput.Model.Attribute;
using ESFA.DC.ILR.FundingService.ALB.FundingOutput.Model.Interface;
using ESFA.DC.ILR.FundingService.ALB.FundingOutput.Model.Interface.Attribute;
using FluentAssertions;
using Xunit;

namespace ESFA.DC.ILR.FundingService.ALB.FundingOutput.Model.Tests
{
    public class FundingOutputModelTests
    {
        /// <summary>
        /// Return FundingOutput Model
        /// </summary>
        [Fact(DisplayName = "FundingOutputModel - FundingOutputs - Exists"), Trait("FundingOutputModel", "Unit")]
        public void FundingOutputModel_FundingOutputs_Exists()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var fundingOutputs = TestFundingOutputs();

            // ASSERT
            fundingOutputs.Should().NotBeNull();
        }

        /// <summary>
        /// Return FundingOutput Model
        /// </summary>
        [Fact(DisplayName = "FundingOutputModel - FundingOutputs - GlobalAttribute Exists"), Trait("FundingOutputModel", "Unit")]
        public void FundingOutputModel_FundingOutputs_GlobalAttributeExists()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var fundingOutputs = TestFundingOutputs();

            // ASSERT
            fundingOutputs.Global.Should().NotBeNull();
        }

        /// <summary>
        /// Return FundingOutput Model
        /// </summary>
        [Fact(DisplayName = "FundingOutputModel - FundingOutputs - GlobalAttribute Correct"), Trait("FundingOutputModel", "Unit")]
        public void FundingOutputModel_FundingOutputs_GlobalAttributeCorrect()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var fundingOutputs = TestFundingOutputs();

            // ASSERT
            fundingOutputs.Global.Should().BeEquivalentTo(TestGlobalAttribute());
        }

        /// <summary>
        /// Return FundingOutput Model
        /// </summary>
        [Fact(DisplayName = "FundingOutputModel - FundingOutputs - LearnerAttribute Exists"), Trait("FundingOutputModel", "Unit")]
        public void FundingOutputModel_FundingOutputs_LearnerAttributeExists()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var fundingOutputs = TestFundingOutputs();

            // ASSERT
            fundingOutputs.Learners.Should().NotBeNull();
        }

        /// <summary>
        /// Return FundingOutput Model
        /// </summary>
        [Fact(DisplayName = "FundingOutputModel - FundingOutputs - LearnerAttribute Correct"), Trait("FundingOutputModel", "Unit")]
        public void FundingOutputModel_FundingOutputs_LearnerAttributeCorrect()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var fundingOutputs = TestFundingOutputs();

            // ASSERT
            fundingOutputs.Learners.Should().BeEquivalentTo(TestLearnerAttribute());
        }

        /// <summary>
        /// Return FundingOutput Model
        /// </summary>
        [Fact(DisplayName = "FundingOutputModel - FundingOutputs - LearnerAttribute Count"), Trait("FundingOutputModel", "Unit")]
        public void FundingOutputModel_FundingOutputs_LearnerAttributeCount()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var fundingOutputs = TestFundingOutputs();

            // ASSERT
            fundingOutputs.Learners.Count().Should().Be(1);
        }

        /// <summary>
        /// Return FundingOutput Model
        /// </summary>
        [Fact(DisplayName = "FundingOutputModel - Global - UKPRN Exists"), Trait("FundingOutputModel", "Unit")]
        public void FundingOutputModel_Global_UKPRNExists()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var global = TestFundingOutputs().Global;

            // ASSERT
            global.UKPRN.Should().NotBe(null);
        }

        /// <summary>
        /// Return FundingOutput Model
        /// </summary>
        [Fact(DisplayName = "FundingOutputModel - Global - UKPRN Correct"), Trait("FundingOutputModel", "Unit")]
        public void FundingOutputModel_Global_UKPRNCorrect()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var global = TestFundingOutputs().Global;

            // ASSERT
            global.UKPRN.Should().Be(12345678);
        }

        /// <summary>
        /// Return FundingOutput Model
        /// </summary>
        [Fact(DisplayName = "FundingOutputModel - Global - LARSVersion Exists"), Trait("FundingOutputModel", "Unit")]
        public void FundingOutputModel_Global_LARSVersionExists()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var global = TestFundingOutputs().Global;

            // ASSERT
            global.LARSVersion.Should().NotBeNull();
        }

        /// <summary>
        /// Return FundingOutput Model
        /// </summary>
        [Fact(DisplayName = "FundingOutputModel - Global - LARSVersion Correct"), Trait("FundingOutputModel", "Unit")]
        public void FundingOutputModel_Global_LARSVersionCorrect()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var global = TestFundingOutputs().Global;

            // ASSERT
            global.LARSVersion.Should().Be("Version_005");
        }

        /// <summary>
        /// Return FundingOutput Model
        /// </summary>
        [Fact(DisplayName = "FundingOutputModel - Global - PostcodeAreaCostVersion Exists"), Trait("FundingOutputModel", "Unit")]
        public void FundingOutputModel_Global_PostcodeAreaCostVersionExists()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var global = TestFundingOutputs().Global;

            // ASSERT
            global.PostcodeAreaCostVersion.Should().NotBeNull();
        }

        /// <summary>
        /// Return FundingOutput Model
        /// </summary>
        [Fact(DisplayName = "FundingOutputModel - Global - PostcodeAreaCostVersion Correct"), Trait("FundingOutputModel", "Unit")]
        public void FundingOutputModel_Global_PostcodeAreaCostVersionCorrect()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var global = TestFundingOutputs().Global;

            // ASSERT
            global.PostcodeAreaCostVersion.Should().Be("Version_002");
        }

        /// <summary>
        /// Return FundingOutput Model
        /// </summary>
        [Fact(DisplayName = "FundingOutputModel - Global - RulebaseVersion Exists"), Trait("FundingOutputModel", "Unit")]
        public void FundingOutputModel_Global_RulebaseVersionExists()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var global = TestFundingOutputs().Global;

            // ASSERT
            global.RulebaseVersion.Should().NotBeNull();
        }

        /// <summary>
        /// Return FundingOutput Model
        /// </summary>
        [Fact(DisplayName = "FundingOutputModel - Global - RulebaseVersion Correct"), Trait("FundingOutputModel", "Unit")]
        public void FundingOutputModel_Global_RulebaseVersionCorrect()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var global = TestFundingOutputs().Global;

            // ASSERT
            global.RulebaseVersion.Should().Be("1819.01.01");
        }

        /// <summary>
        /// Return FundingOutput Model
        /// </summary>
        [Fact(DisplayName = "FundingOutputModel - Learners - Learners Count"), Trait("FundingOutputModel", "Unit")]
        public void FundingOutputModel_Learners_LearnersCount()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var learners = TestFundingOutputs().Learners;

            // ASSERT
            learners.Count().Should().Be(1);
        }

        /// <summary>
        /// Return FundingOutput Model
        /// </summary>
        [Fact(DisplayName = "FundingOutputModel - Learners - LearnRefNumber Exists"), Trait("FundingOutputModel", "Unit")]
        public void FundingOutputModel_Learners_LearnRefNumberExists()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var learners = TestFundingOutputs().Learners.Select(l => l).FirstOrDefault();

            // ASSERT
            learners.LearnRefNumber.Should().NotBeNull();
        }

        /// <summary>
        /// Return FundingOutput Model
        /// </summary>
        [Fact(DisplayName = "FundingOutputModel - Learners - LearnRefNumber Correct"), Trait("FundingOutputModel", "Unit")]
        public void FundingOutputModel_Learners_LearnRefNumberCorrect()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var learners = TestFundingOutputs().Learners.Select(l => l).FirstOrDefault();

            // ASSERT
            learners.LearnRefNumber.Should().Be("TestLearner1");
        }

        /// <summary>
        /// Return FundingOutput Model
        /// </summary>
        [Fact(DisplayName = "FundingOutputModel - LearnerPeriodisedAttributes - LearnerPeriodisedAttributes Count"), Trait("FundingOutputModel", "Unit")]
        public void FundingOutputModel_LearnerPeriodisedAttributes_LearnersCount()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var learnerPeriodisedAttributes = TestFundingOutputs().Learners.Select(l => l.LearnerPeriodisedAttributes);

            // ASSERT
            learnerPeriodisedAttributes.Count().Should().Be(1);
        }

        /// <summary>
        /// Return FundingOutput Model
        /// </summary>
        [Fact(DisplayName = "FundingOutputModel - LearnerPeriodisedAttributes - AttributeName Exists"), Trait("FundingOutputModel", "Unit")]
        public void FundingOutputModel_LearnerPeriodisedAttributes_AttributeNameExists()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var learnerPeriodisedAttributes = TestFundingOutputs().Learners.SelectMany(l => l.LearnerPeriodisedAttributes).FirstOrDefault();

            // ASSERT
            learnerPeriodisedAttributes.AttributeName.Should().NotBeNull();
        }

        /// <summary>
        /// Return FundingOutput Model
        /// </summary>
        [Fact(DisplayName = "FundingOutputModel - LearnerPeriodisedAttributes - AttributeName Correct"), Trait("FundingOutputModel", "Unit")]
        public void FundingOutputModel_LearnerPeriodisedAttributes_AttributeNameCorrect()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var learnerPeriodisedAttributes = TestFundingOutputs().Learners.SelectMany(l => l.LearnerPeriodisedAttributes).FirstOrDefault();

            // ASSERT
            learnerPeriodisedAttributes.AttributeName.Should().Be("ALBSeqNum");
        }

        /// <summary>
        /// Return FundingOutput Model
        /// </summary>
        [Fact(DisplayName = "FundingOutputModel - LearnerPeriodisedAttributes - Periods Exists"), Trait("FundingOutputModel", "Unit")]
        public void FundingOutputModel_LearnerPeriodisedAttributes_PeriodsExists()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var learnerPeriodisedAttributes = TestFundingOutputs().Learners.SelectMany(l => l.LearnerPeriodisedAttributes).FirstOrDefault();

            // ASSERT
            learnerPeriodisedAttributes.Period1.Should().NotBe(null);
            learnerPeriodisedAttributes.Period2.Should().NotBe(null);
            learnerPeriodisedAttributes.Period3.Should().NotBe(null);
            learnerPeriodisedAttributes.Period4.Should().NotBe(null);
            learnerPeriodisedAttributes.Period5.Should().NotBe(null);
            learnerPeriodisedAttributes.Period6.Should().NotBe(null);
            learnerPeriodisedAttributes.Period7.Should().NotBe(null);
            learnerPeriodisedAttributes.Period8.Should().NotBe(null);
            learnerPeriodisedAttributes.Period9.Should().NotBe(null);
            learnerPeriodisedAttributes.Period10.Should().NotBe(null);
            learnerPeriodisedAttributes.Period11.Should().NotBe(null);
            learnerPeriodisedAttributes.Period12.Should().NotBe(null);
        }

        /// <summary>
        /// Return FundingOutput Model
        /// </summary>
        [Fact(DisplayName = "FundingOutputModel - LearnerPeriodisedAttributes - Periods Correct"), Trait("FundingOutputModel", "Unit")]
        public void FundingOutputModel_LearnerPeriodisedAttributes_PeriodsCorrect()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var learnerPeriodisedAttributes = TestFundingOutputs().Learners.SelectMany(l => l.LearnerPeriodisedAttributes).FirstOrDefault();

            // ASSERT
            learnerPeriodisedAttributes.Period1.Should().Be(1.00m);
            learnerPeriodisedAttributes.Period2.Should().Be(1.00m);
            learnerPeriodisedAttributes.Period3.Should().Be(1.00m);
            learnerPeriodisedAttributes.Period4.Should().Be(1.00m);
            learnerPeriodisedAttributes.Period5.Should().Be(1.00m);
            learnerPeriodisedAttributes.Period6.Should().Be(1.00m);
            learnerPeriodisedAttributes.Period7.Should().Be(1.00m);
            learnerPeriodisedAttributes.Period8.Should().Be(1.00m);
            learnerPeriodisedAttributes.Period9.Should().Be(1.00m);
            learnerPeriodisedAttributes.Period10.Should().Be(1.00m);
            learnerPeriodisedAttributes.Period11.Should().Be(1.00m);
            learnerPeriodisedAttributes.Period12.Should().Be(1.00m);
        }

        /// <summary>
        /// Return FundingOutput Model
        /// </summary>
        [Fact(DisplayName = "FundingOutputModel - LearningDeliveryAttributes - LearningDeliveryAttributes Count"), Trait("FundingOutputModel", "Unit")]
        public void FundingOutputModel_LearningDeliveryAttributes_LearningDeliveryAttributesCount()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var learningDeliveryAttributes = TestFundingOutputs().Learners.SelectMany(l => l.LearningDeliveryAttributes);

            // ASSERT
            learningDeliveryAttributes.Count().Should().Be(1);
        }

        /// <summary>
        /// Return FundingOutput Model
        /// </summary>
        [Fact(DisplayName = "FundingOutputModel - LearningDeliveryAttributes - AimSeqNumber Exists"), Trait("FundingOutputModel", "Unit")]
        public void FundingOutputModel_LearningDeliveryAttributes_AimSeqNumberExists()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var learningDeliveryAttributes = TestFundingOutputs().Learners.SelectMany(l => l.LearningDeliveryAttributes).FirstOrDefault();

            // ASSERT
            learningDeliveryAttributes.AimSeqNumber.Should().NotBe(null);
        }

        /// <summary>
        /// Return FundingOutput Model
        /// </summary>
        [Fact(DisplayName = "FundingOutputModel - LearningDeliveryAttributes - AimSeqNumber Correct"), Trait("FundingOutputModel", "Unit")]
        public void FundingOutputModel_LearningDeliveryAttributes_AimSeqNumberCorrect()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var learningDeliveryAttributes = TestFundingOutputs().Learners.SelectMany(l => l.LearningDeliveryAttributes).FirstOrDefault();

            // ASSERT
            learningDeliveryAttributes.AimSeqNumber.Should().Be(1);
        }

        /// <summary>
        /// Return FundingOutput Model
        /// </summary>
        [Fact(DisplayName = "FundingOutputModel - LearningDeliveryAttributes - LearningDeliveryAttributeDatas Exists"), Trait("FundingOutputModel", "Unit")]
        public void FundingOutputModel_LearningDeliveryAttributes_LearningDeliveryAttributeDatasExists()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var learningDeliveryAttributes = TestFundingOutputs().Learners.SelectMany(l => l.LearningDeliveryAttributes).FirstOrDefault();

            // ASSERT
            learningDeliveryAttributes.LearningDeliveryAttributeDatas.Should().NotBeNull();
        }

        /// <summary>
        /// Return FundingOutput Model
        /// </summary>
        [Fact(DisplayName = "FundingOutputModel - LearningDeliveryAttributes - LearningDeliveryPeriodisedAttributes Exist"), Trait("FundingOutputModel", "Unit")]
        public void FundingOutputModel_LearningDeliveryAttributes_LearningDeliveryPeriodisedAttributesExist()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var learningDeliveryAttributes = TestFundingOutputs().Learners.SelectMany(l => l.LearningDeliveryAttributes).FirstOrDefault();

            // ASSERT
            learningDeliveryAttributes.LearningDeliveryPeriodisedAttributes.Should().NotBeNull();
        }

        /// <summary>
        /// Return FundingOutput Model
        /// </summary>
        [Fact(DisplayName = "FundingOutputModel - LearningDeliveryAttributes - LearningDeliveryPeriodisedAttributes Count"), Trait("FundingOutputModel", "Unit")]
        public void FundingOutputModel_LearningDeliveryAttributes_LearningDeliveryPeriodisedAttributesCount()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var learningDeliveryAttributes = TestFundingOutputs().Learners.SelectMany(l => l.LearningDeliveryAttributes).FirstOrDefault();

            // ASSERT
            learningDeliveryAttributes.LearningDeliveryPeriodisedAttributes.Count().Should().Be(2);
        }

        /// <summary>
        /// Return FundingOutput Model
        /// </summary>
        [Fact(DisplayName = "FundingOutputModel - LearningDeliveryAttributeDatas - Achieved Correct"), Trait("FundingOutputModel", "Unit")]
        public void FundingOutputModel_LearningDeliveryAttributeDatas_AchievedCorrect()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var learningDeliveryAttributeDatas =
                TestFundingOutputs().Learners.SelectMany(l =>
                l.LearningDeliveryAttributes.Select(ld => ld.LearningDeliveryAttributeDatas)).FirstOrDefault();

            // ASSERT
            learningDeliveryAttributeDatas.Achieved.Should().Be(true);
        }

        /// <summary>
        /// Return FundingOutput Model
        /// </summary>
        [Fact(DisplayName = "FundingOutputModel - LearningDeliveryAttributeDatas - ActualNumInstalm Correct"), Trait("FundingOutputModel", "Unit")]
        public void FundingOutputModel_LearningDeliveryAttributeDatas_ActualNumInstalmCorrect()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var learningDeliveryAttributeDatas =
                TestFundingOutputs().Learners.SelectMany(l =>
                l.LearningDeliveryAttributes.Select(ld => ld.LearningDeliveryAttributeDatas)).FirstOrDefault();

            // ASSERT
            learningDeliveryAttributeDatas.ActualNumInstalm.Should().Be(1);
        }

        /// <summary>
        /// Return FundingOutput Model
        /// </summary>
        [Fact(DisplayName = "FundingOutputModel - LearningDeliveryAttributeDatas - AdvLoan Correct"), Trait("FundingOutputModel", "Unit")]
        public void FundingOutputModel_LearningDeliveryAttributeDatas_AdvLoanCorrect()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var learningDeliveryAttributeDatas =
                TestFundingOutputs().Learners.SelectMany(l =>
                l.LearningDeliveryAttributes.Select(ld => ld.LearningDeliveryAttributeDatas)).FirstOrDefault();

            // ASSERT
            learningDeliveryAttributeDatas.AdvLoan.Should().Be(false);
        }

        /// <summary>
        /// Return FundingOutput Model
        /// </summary>
        [Fact(DisplayName = "FundingOutputModel - LearningDeliveryAttributeDatas - ApplicFactDate Correct"), Trait("FundingOutputModel", "Unit")]
        public void FundingOutputModel_LearningDeliveryAttributeDatas_ApplicFactDateCorrect()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var learningDeliveryAttributeDatas =
                TestFundingOutputs().Learners.SelectMany(l =>
                l.LearningDeliveryAttributes.Select(ld => ld.LearningDeliveryAttributeDatas)).FirstOrDefault();

            // ASSERT
            learningDeliveryAttributeDatas.ApplicFactDate.Should().Be(DateTime.Parse("2018-08-01 00:00:00", culture));
        }

        /// <summary>
        /// Return FundingOutput Model
        /// </summary>
        [Fact(DisplayName = "FundingOutputModel - LearningDeliveryAttributeDatas - ApplicProgWeightFact Correct"), Trait("FundingOutputModel", "Unit")]
        public void FundingOutputModel_LearningDeliveryAttributeDatas_ApplicProgWeightFactCorrect()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var learningDeliveryAttributeDatas =
                TestFundingOutputs().Learners.SelectMany(l =>
                l.LearningDeliveryAttributes.Select(ld => ld.LearningDeliveryAttributeDatas)).FirstOrDefault();

            // ASSERT
            learningDeliveryAttributeDatas.ApplicProgWeightFact.Should().Be("ApplicProgWeightFact");
        }

        /// <summary>
        /// Return FundingOutput Model
        /// </summary>
        [Fact(DisplayName = "FundingOutputModel - LearningDeliveryAttributeDatas - AreaCostFactAdj Correct"), Trait("FundingOutputModel", "Unit")]
        public void FundingOutputModel_LearningDeliveryAttributeDatas_AreaCostFactAdjCorrect()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var learningDeliveryAttributeDatas =
                TestFundingOutputs().Learners.SelectMany(l =>
                l.LearningDeliveryAttributes.Select(ld => ld.LearningDeliveryAttributeDatas)).FirstOrDefault();

            // ASSERT
            learningDeliveryAttributeDatas.AreaCostFactAdj.Should().Be(1.00m);
        }

        /// <summary>
        /// Return FundingOutput Model
        /// </summary>
        [Fact(DisplayName = "FundingOutputModel - LearningDeliveryAttributeDatas - AreaCostInstalment Correct"), Trait("FundingOutputModel", "Unit")]
        public void FundingOutputModel_LearningDeliveryAttributeDatas_AreaCostInstalmentCorrect()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var learningDeliveryAttributeDatas =
                TestFundingOutputs().Learners.SelectMany(l =>
                l.LearningDeliveryAttributes.Select(ld => ld.LearningDeliveryAttributeDatas)).FirstOrDefault();

            // ASSERT
            learningDeliveryAttributeDatas.AreaCostInstalment.Should().Be(2.00m);
        }

        /// <summary>
        /// Return FundingOutput Model
        /// </summary>
        [Fact(DisplayName = "FundingOutputModel - LearningDeliveryAttributeDatas - FundLine Correct"), Trait("FundingOutputModel", "Unit")]
        public void FundingOutputModel_LearningDeliveryAttributeDatas_FundLineCorrect()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var learningDeliveryAttributeDatas =
                TestFundingOutputs().Learners.SelectMany(l =>
                l.LearningDeliveryAttributes.Select(ld => ld.LearningDeliveryAttributeDatas)).FirstOrDefault();

            // ASSERT
            learningDeliveryAttributeDatas.FundLine.Should().Be("FundLine");
        }

        /// <summary>
        /// Return FundingOutput Model
        /// </summary>
        [Fact(DisplayName = "FundingOutputModel - LearningDeliveryAttributeDatas - FundStart Correct"), Trait("FundingOutputModel", "Unit")]
        public void FundingOutputModel_LearningDeliveryAttributeDatas_FundStartCorrect()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var learningDeliveryAttributeDatas =
                TestFundingOutputs().Learners.SelectMany(l =>
                l.LearningDeliveryAttributes.Select(ld => ld.LearningDeliveryAttributeDatas)).FirstOrDefault();

            // ASSERT
            learningDeliveryAttributeDatas.FundStart.Should().Be(true);
        }

        /// <summary>
        /// Return FundingOutput Model
        /// </summary>
        [Fact(DisplayName = "FundingOutputModel - LearningDeliveryAttributeDatas - LiabilityDate Correct"), Trait("FundingOutputModel", "Unit")]
        public void FundingOutputModel_LearningDeliveryAttributeDatas_LiabilityDateCorrect()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var learningDeliveryAttributeDatas =
                TestFundingOutputs().Learners.SelectMany(l =>
                l.LearningDeliveryAttributes.Select(ld => ld.LearningDeliveryAttributeDatas)).FirstOrDefault();

            // ASSERT
            learningDeliveryAttributeDatas.LiabilityDate.Should().Be(DateTime.Parse("2018-10-01 00:00:00", culture));
        }

        /// <summary>
        /// Return FundingOutput Model
        /// </summary>
        [Fact(DisplayName = "FundingOutputModel - LearningDeliveryAttributeDatas - LoanBursAreaUplift Correct"), Trait("FundingOutputModel", "Unit")]
        public void FundingOutputModel_LearningDeliveryAttributeDatas_LoanBursAreaUpliftCorrect()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var learningDeliveryAttributeDatas =
                TestFundingOutputs().Learners.SelectMany(l =>
                l.LearningDeliveryAttributes.Select(ld => ld.LearningDeliveryAttributeDatas)).FirstOrDefault();

            // ASSERT
            learningDeliveryAttributeDatas.LoanBursAreaUplift.Should().Be(false);
        }

        /// <summary>
        /// Return FundingOutput Model
        /// </summary>
        [Fact(DisplayName = "FundingOutputModel - LearningDeliveryAttributeDatas - LoanBursSupp Correct"), Trait("FundingOutputModel", "Unit")]
        public void FundingOutputModel_LearningDeliveryAttributeDatas_LoanBursSuppCorrect()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var learningDeliveryAttributeDatas =
                TestFundingOutputs().Learners.SelectMany(l =>
                l.LearningDeliveryAttributes.Select(ld => ld.LearningDeliveryAttributeDatas)).FirstOrDefault();

            // ASSERT
            learningDeliveryAttributeDatas.LoanBursSupp.Should().Be(true);
        }

        /// <summary>
        /// Return FundingOutput Model
        /// </summary>
        [Fact(DisplayName = "FundingOutputModel - LearningDeliveryAttributeDatas - OutstndNumOnProgInstalm Correct"), Trait("FundingOutputModel", "Unit")]
        public void FundingOutputModel_LearningDeliveryAttributeDatas_OutstndNumOnProgInstalmCorrect()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var learningDeliveryAttributeDatas =
                TestFundingOutputs().Learners.SelectMany(l =>
                l.LearningDeliveryAttributes.Select(ld => ld.LearningDeliveryAttributeDatas)).FirstOrDefault();

            // ASSERT
            learningDeliveryAttributeDatas.OutstndNumOnProgInstalm.Should().Be(10);
        }

        /// <summary>
        /// Return FundingOutput Model
        /// </summary>
        [Fact(DisplayName = "FundingOutputModel - LearningDeliveryAttributeDatas - PlannedNumOnProgInstalm Correct"), Trait("FundingOutputModel", "Unit")]
        public void FundingOutputModel_LearningDeliveryAttributeDatas_PlannedNumOnProgInstalmCorrect()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var learningDeliveryAttributeDatas =
                TestFundingOutputs().Learners.SelectMany(l =>
                l.LearningDeliveryAttributes.Select(ld => ld.LearningDeliveryAttributeDatas)).FirstOrDefault();

            // ASSERT
            learningDeliveryAttributeDatas.PlannedNumOnProgInstalm.Should().Be(20);
        }

        /// <summary>
        /// Return FundingOutput Model
        /// </summary>
        [Fact(DisplayName = "FundingOutputModel - LearningDeliveryAttributeDatas - WeightedRate Correct"), Trait("FundingOutputModel", "Unit")]
        public void FundingOutputModel_LearningDeliveryAttributeDatas_WeightedRateCorrect()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var learningDeliveryAttributeDatas =
                TestFundingOutputs().Learners.SelectMany(l =>
                l.LearningDeliveryAttributes.Select(ld => ld.LearningDeliveryAttributeDatas)).FirstOrDefault();

            // ASSERT
            learningDeliveryAttributeDatas.WeightedRate.Should().Be(3.00m);
        }

        /// <summary>
        /// Return FundingOutput Model
        /// </summary>
        [Fact(DisplayName = "FundingOutputModel - LearningDeliveryPeriodisedAttributes - WeightedRate Correct"), Trait("FundingOutputModel", "Unit")]
        public void FundingOutputModel_LearningDeliveryPeriodisedAttributes_AttributeNameCorrect()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var learningDeliveryPeriodisedAttributes =
                TestFundingOutputs().Learners.SelectMany(l =>
                l.LearningDeliveryAttributes.Select(ld => ld.LearningDeliveryPeriodisedAttributes)).FirstOrDefault();

            // ASSERT
            learningDeliveryPeriodisedAttributes[0].AttributeName.Should().Be("ALBSupportPayment");
            learningDeliveryPeriodisedAttributes[1].AttributeName.Should().Be("AreaUpliftBalPayment");
        }

        /// <summary>
        /// Return FundingOutput Model
        /// </summary>
        [Fact(DisplayName = "FundingOutputModel - LearningDeliveryPeriodisedAttributes - Periods Correct"), Trait("FundingOutputModel", "Unit")]
        public void FundingOutputModel_LearningDeliveryPeriodisedAttributes_PeriodsCorrect()
        {
            // ARRANGE
            // Use Test Helpers

            // ACT
            var learningDeliveryPeriodisedAttributes =
                TestFundingOutputs().Learners.SelectMany(l =>
                l.LearningDeliveryAttributes.Select(ld => ld.LearningDeliveryPeriodisedAttributes)).FirstOrDefault();

            // ASSERT
            learningDeliveryPeriodisedAttributes[0].Period1.Should().Be(100.00m);
            learningDeliveryPeriodisedAttributes[0].Period2.Should().Be(100.00m);
            learningDeliveryPeriodisedAttributes[0].Period3.Should().Be(100.00m);
            learningDeliveryPeriodisedAttributes[0].Period4.Should().Be(100.00m);
            learningDeliveryPeriodisedAttributes[0].Period5.Should().Be(100.00m);
            learningDeliveryPeriodisedAttributes[0].Period6.Should().Be(100.00m);
            learningDeliveryPeriodisedAttributes[0].Period7.Should().Be(100.00m);
            learningDeliveryPeriodisedAttributes[0].Period8.Should().Be(100.00m);
            learningDeliveryPeriodisedAttributes[0].Period9.Should().Be(100.00m);
            learningDeliveryPeriodisedAttributes[0].Period10.Should().Be(100.00m);
            learningDeliveryPeriodisedAttributes[0].Period11.Should().Be(100.00m);
            learningDeliveryPeriodisedAttributes[0].Period12.Should().Be(100.00m);

            learningDeliveryPeriodisedAttributes[1].Period1.Should().Be(200.00m);
            learningDeliveryPeriodisedAttributes[1].Period2.Should().Be(200.00m);
            learningDeliveryPeriodisedAttributes[1].Period3.Should().Be(200.00m);
            learningDeliveryPeriodisedAttributes[1].Period4.Should().Be(200.00m);
            learningDeliveryPeriodisedAttributes[1].Period5.Should().Be(200.00m);
            learningDeliveryPeriodisedAttributes[1].Period6.Should().Be(200.00m);
            learningDeliveryPeriodisedAttributes[1].Period7.Should().Be(200.00m);
            learningDeliveryPeriodisedAttributes[1].Period8.Should().Be(200.00m);
            learningDeliveryPeriodisedAttributes[1].Period9.Should().Be(200.00m);
            learningDeliveryPeriodisedAttributes[1].Period10.Should().Be(200.00m);
            learningDeliveryPeriodisedAttributes[1].Period11.Should().Be(200.00m);
            learningDeliveryPeriodisedAttributes[1].Period12.Should().Be(200.00m);
        }

        #region Test Helpers

        private static readonly IFormatProvider culture = new CultureInfo("en-GB", true);

        private static IFundingOutputs TestFundingOutputs()
        {
            return new FundingOutputs
            {
                Global = TestGlobalAttribute(),
                Learners = TestLearnerAttribute(),
            };
        }

        private static IGlobalAttribute TestGlobalAttribute()
        {
            return new GlobalAttribute
            {
                UKPRN = 12345678,
                LARSVersion = "Version_005",
                PostcodeAreaCostVersion = "Version_002",
                RulebaseVersion = "1819.01.01"
            };
        }

        private static ILearnerAttribute[] TestLearnerAttribute()
        {
            return new LearnerAttribute[]
            {
                new LearnerAttribute
                {
                    LearnRefNumber = "TestLearner1",
                    LearnerPeriodisedAttributes = TestLearnerPeriodisedAttributes(),
                    LearningDeliveryAttributes = TestLearningDeliveryAttributes(),
                }
            };
        }

        private static ILearnerPeriodisedAttribute[] TestLearnerPeriodisedAttributes()
        {
            return new LearnerPeriodisedAttribute[]
            {
                new LearnerPeriodisedAttribute
                {
                    AttributeName = "ALBSeqNum",
                    Period1 = 1.0m,
                    Period2 = 1.0m,
                    Period3 = 1.0m,
                    Period4 = 1.0m,
                    Period5 = 1.0m,
                    Period6 = 1.0m,
                    Period7 = 1.0m,
                    Period8 = 1.0m,
                    Period9 = 1.0m,
                    Period10 = 1.0m,
                    Period11 = 1.0m,
                    Period12 = 1.0m,
                }
            };
        }

        private static ILearningDeliveryAttribute[] TestLearningDeliveryAttributes()
        {
            return new LearningDeliveryAttribute[]
            {
                new LearningDeliveryAttribute
                {
                    AimSeqNumber = 1,
                    LearningDeliveryAttributeDatas = TestLearningDeliveryAttributeDatas(),
                    LearningDeliveryPeriodisedAttributes = TestLearningDeliveryPeriodisedAttributes(),
                }
            };
        }

        private static ILearningDeliveryAttributeData TestLearningDeliveryAttributeDatas()
        {
            return new LearningDeliveryAttributeData
            {
                Achieved = true,
                ActualNumInstalm = 1,
                AdvLoan = false,
                ApplicFactDate = DateTime.Parse("2018-08-01 00:00:00", culture),
                ApplicProgWeightFact = "ApplicProgWeightFact",
                AreaCostFactAdj = 1.00m,
                AreaCostInstalment = 2.00m,
                FundLine = "FundLine",
                FundStart = true,
                LiabilityDate = DateTime.Parse("2018-10-01 00:00:00", culture),
                LoanBursAreaUplift = false,
                LoanBursSupp = true,
                OutstndNumOnProgInstalm = 10,
                PlannedNumOnProgInstalm = 20,
                WeightedRate = 3.00m,
            };
        }

        private static ILearningDeliveryPeriodisedAttribute[] TestLearningDeliveryPeriodisedAttributes()
        {
            return new LearningDeliveryPeriodisedAttribute[]
            {
                new LearningDeliveryPeriodisedAttribute
                {
                    AttributeName = "ALBSupportPayment",
                    Period1 = 100.0m,
                    Period2 = 100.0m,
                    Period3 = 100.0m,
                    Period4 = 100.0m,
                    Period5 = 100.0m,
                    Period6 = 100.0m,
                    Period7 = 100.0m,
                    Period8 = 100.0m,
                    Period9 = 100.0m,
                    Period10 = 100.0m,
                    Period11 = 100.0m,
                    Period12 = 100.0m,
                },
                new LearningDeliveryPeriodisedAttribute
                {
                    AttributeName = "AreaUpliftBalPayment",
                    Period1 = 200.0m,
                    Period2 = 200.0m,
                    Period3 = 200.0m,
                    Period4 = 200.0m,
                    Period5 = 200.0m,
                    Period6 = 200.0m,
                    Period7 = 200.0m,
                    Period8 = 200.0m,
                    Period9 = 200.0m,
                    Period10 = 200.0m,
                    Period11 = 200.0m,
                    Period12 = 200.0m,
                }
            };
        }

        #endregion
    }
}
