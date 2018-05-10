using System;
using System.Collections.Generic;
using System.Linq;
using ESFA.DC.ILR.FundingService.ALB.Service.Builders;
using Xunit;
using FluentAssertions;
using ESFA.DC.ILR.FundingService.ALB.Service.Builders.Interface;
using ESFA.DC.OPA.Model.Interface;

namespace ESFA.DC.ILR.FundingService.ALB.Service.Tests.Builders
{
    public class AttributeBuilderTests
    {
        #region Global Attributes

        /// <summary>
        /// Return Global Attributes from AttributeBuilder and check values
        /// /// </summary>
        [Fact(DisplayName = "Global - Attributes Exist"), Trait("Funding Service Attribute Builders", "Unit")]
        public void AttributeBuilder_Global_Exists()
        {
            //ARRANGE
            //Use Test Helpers

            //ACT
            var globalAttributes = SetupGlobalAttributes(12345678, "Version_005", "Version_003");

            //ASSERT
            globalAttributes.Should().NotBeEmpty();
        }

        /// <summary>
        /// Return Global Attributes from AttributeBuilder and check values
        /// /// </summary>
        [Fact(DisplayName = "Global - CorrectCount"), Trait("Funding Service Attribute Builders", "Unit")]
        public void AttributeBuilder_Global_CorrectCount()
        {
            //ARRANGE
            //Use Test Helpers

            //ACT
            var globalAttributes = SetupGlobalAttributes(12345678, "Version_005", "Version_003");

            //ASSERT
            globalAttributes.Count.Should().Be(3);
        }

        /// <summary>
        /// Return Global Attributes from AttributeBuilder and check values
        /// /// </summary>
        [Fact(DisplayName = "Global - UKPRN Correct"), Trait("Funding Service Attribute Builders", "Unit")]
        public void AttributeBuilder_Global_CorrectUKPRN()
        {
            //ARRANGE
            //Use Test Helpers

            //ACT
            var globalAttributes = SetupGlobalAttributes(12345678, "Version_005", "Version_003");

            //ASSERT
            AttributeValue(globalAttributes, "UKPRN").Should().Be(12345678);
        }

        /// <summary>
        /// Return Global Attributes from AttributeBuilder and check values
        /// /// </summary>
        [Fact(DisplayName = "Global - LARS Version Correct"), Trait("Funding Service Attribute Builders", "Unit")]
        public void AttributeBuilder_Global_CorrectLARSVersion()
        {
            //ARRANGE
            //Use Test Helpers

            //ACT
            var globalAttributes = SetupGlobalAttributes(12345678, "Version_005", "Version_003");

            //ASSERT
            AttributeValue(globalAttributes, "LARSVersion").Should().Be("Version_005");
        }

        /// <summary>
        /// Return Global Attributes from AttributeBuilder and check values
        /// /// </summary>
        [Fact(DisplayName = "Global - PostcodeFactors Version Correct"), Trait("Funding Service Attribute Builders", "Unit")]
        public void AttributeBuilder_Global_CorrectPostcodeFactorsVersion()
        {
            //ARRANGE
            //Use Test Helpers

            //ACT
            var globalAttributes = SetupGlobalAttributes(12345678, "Version_005", "Version_003");

            //ASSERT
            AttributeValue(globalAttributes, "PostcodeAreaCostVersion").Should().Be("Version_003");
        }

        #endregion

        #region Learner Attributes

        /// <summary>
        /// Return Learner Attributes from AttributeBuilder and check values
        /// /// </summary>
        [Fact(DisplayName = "Learner - Attributes Exist"), Trait("Funding Service Attribute Builders", "Unit")]
        public void AttributeBuilder_Learner_Exists()
        {
            //ARRANGE
            //Use Test Helpers

            //ACT
            var learnerAttributes = SetupLearnerAttributes("Learner1");

            //ASSERT
            learnerAttributes.Should().NotBeEmpty();
        }

        /// <summary>
        /// Return Learner Attributes from AttributeBuilder and check values
        /// /// </summary>
        [Fact(DisplayName = "Learner - Correct Count"), Trait("Funding Service Attribute Builders", "Unit")]
        public void AttributeBuilder_Learner_CorrectCount()
        {
            //ARRANGE
            //Use Test Helpers

            //ACT
            var learnerAttributes = SetupLearnerAttributes("Learner1");

            //ASSERT
            learnerAttributes.Count.Should().Be(1);
        }

        /// <summary>
        /// Return Learner Attributes from AttributeBuilder and check values
        /// /// </summary>
        [Fact(DisplayName = "Learner - Correct"), Trait("Funding Service Attribute Builders", "Unit")]
        public void AttributeBuilder_Learner_CorrectLearnRefNumber()
        {
            //ARRANGE
            //Use Test Helpers

            //ACT
            var learnerAttributes = SetupLearnerAttributes("Learner1");

            //ASSERT
            AttributeValue(learnerAttributes, "LearnRefNumber").Should().Be("Learner1");
        }

        #endregion

        #region LearningDelivery Attributes

        /// <summary>
        /// Return LearningDelivery Attributes from AttributeBuilder and check values
        /// /// </summary>
        [Fact(DisplayName = "LearningDelivery - Attributes Exists"), Trait("Funding Service Attribute Builders", "Unit")]
        public void AttributeBuilder_LearningDelivery_Exists()
        {
            //ARRANGE
            //Use Test Helpers

            //ACT
            var learningDeliveryAttributes = SetupLearningDeliveryAttributes
                (1, 1, DateTime.Parse("2018-07-31"), "0032", DateTime.Parse("2018-06-30"), DateTime.Parse("2017-08-31"), "1", "1", "4", DateTime.Parse("2018-07-31"), 100, 2, 200, 180);

            //ASSERT
            learningDeliveryAttributes.Should().NotBeEmpty();
        }

        /// <summary>
        /// Return LearningDelivery Attributes from AttributeBuilder and check values
        /// /// </summary>
        [Fact(DisplayName = "LearningDelivery - Correct Count"), Trait("Funding Service Attribute Builders", "Unit")]
        public void AttributeBuilder_LearningDelivery_CorrectCount()
        {
            //ARRANGE
            //Use Test Helpers
            
            //ACT
            var learningDeliveryAttributes = SetupLearningDeliveryAttributes
                (1, 1, DateTime.Parse("2018-07-31"), "0032", DateTime.Parse("2018-06-30"), DateTime.Parse("2017-08-31"), "1", "1", "4", DateTime.Parse("2018-07-31"), 100, 2, 200, 180);

            //ASSERT
            learningDeliveryAttributes.Count.Should().Be(14);
        }

        /// <summary>
        /// Return LearningDelivery Attributes from AttributeBuilder and check values
        /// /// </summary>
        [Fact(DisplayName = "LearningDelivery - AimSeqNumber Correct"), Trait("Funding Service Attribute Builders", "Unit")]
        public void AttributeBuilder_LearningDelivery_AimSeqNumberCorrect()
        {
            //ARRANGE
            //Use Test Helpers

            //ACT
            var learningDeliveryAttributes = SetupLearningDeliveryAttributes
                (1, 1, DateTime.Parse("2018-07-31"), "0032", DateTime.Parse("2018-06-30"), DateTime.Parse("2017-08-31"), "1", "1", "4", DateTime.Parse("2018-07-31"), 100, 2, 200, 180);

            //ASSERT
            AttributeValue(learningDeliveryAttributes, "AimSeqNumber").Should().Be(1);
        }

        /// <summary>
        /// Return LearningDelivery Attributes from AttributeBuilder and check values
        /// /// </summary>
        [Fact(DisplayName = "LearningDelivery - CompStatus Correct"), Trait("Funding Service Attribute Builders", "Unit")]
        public void AttributeBuilder_LearningDelivery_CompStatusCorrect()
        {
            //ARRANGE
            //Use Test Helpers

            //ACT
            var learningDeliveryAttributes = SetupLearningDeliveryAttributes
                (1, 1, DateTime.Parse("2018-07-31"), "0032", DateTime.Parse("2018-06-30"), DateTime.Parse("2017-08-31"), "1", "1", "4", DateTime.Parse("2018-07-31"), 100, 2, 200, 180);

            //ASSERT
            AttributeValue(learningDeliveryAttributes, "CompStatus").Should().Be(1);
        }

        /// <summary>
        /// Return LearningDelivery Attributes from AttributeBuilder and check values
        /// /// </summary>
        [Fact(DisplayName = "LearningDelivery - LearnActEndDate Correct"), Trait("Funding Service Attribute Builders", "Unit")]
        public void AttributeBuilder_LearningDelivery_LearnActEndDateCorrect()
        {
            //ARRANGE
            //Use Test Helpers

            //ACT
            var learningDeliveryAttributes = SetupLearningDeliveryAttributes
                (1, 1, DateTime.Parse("2018-07-31"), "0032", DateTime.Parse("2018-06-30"), DateTime.Parse("2017-08-31"), "1", "1", "4", DateTime.Parse("2018-07-31"), 100, 2, 200, 180);

            //ASSERT
            AttributeValue(learningDeliveryAttributes, "LearnActEndDate").Should().Be(DateTime.Parse("2018-07-31"));
        }

        /// <summary>
        /// Return LearningDelivery Attributes from AttributeBuilder and check values
        /// /// </summary>
        [Fact(DisplayName = "LearningDelivery - LearnActEndDate NULL Correct"), Trait("Funding Service Attribute Builders", "Unit")]
        public void AttributeBuilder_LearningDelivery_LearnActEndDateNullCorrect()
        {
            //ARRANGE
            //Use Test Helpers

            //ACT
            var learningDeliveryAttributes = SetupLearningDeliveryAttributes
                (1, 1, null, "0032", DateTime.Parse("2018-06-30"), DateTime.Parse("2017-08-31"), "1", "1", "4", DateTime.Parse("2018-07-31"), 100, 2, 200, 180);

            //ASSERT
            AttributeValue(learningDeliveryAttributes, "LearnActEndDate").Should().BeNull();
        }

        /// <summary>
        /// Return LearningDelivery Attributes from AttributeBuilder and check values
        /// /// </summary>
        [Fact(DisplayName = "LearningDelivery - LearnAimRefType Correct"), Trait("Funding Service Attribute Builders", "Unit")]
        public void AttributeBuilder_LearningDelivery_LearnAimRefTypeCorrect()
        {
            //ARRANGE
            //Use Test Helpers

            //ACT
            var learningDeliveryAttributes = SetupLearningDeliveryAttributes
                (1, 1, DateTime.Parse("2018-07-31"), "0032", DateTime.Parse("2018-06-30"), DateTime.Parse("2017-08-31"), "1", "1", "4", DateTime.Parse("2018-07-31"), 100, 2, 200, 180);

            //ASSERT
            AttributeValue(learningDeliveryAttributes, "LearnAimRefType").Should().Be("0032");
        }

        /// <summary>
        /// Return LearningDelivery Attributes from AttributeBuilder and check values
        /// /// </summary>
        [Fact(DisplayName = "LearningDelivery - LearnPlanEndDate Correct"), Trait("Funding Service Attribute Builders", "Unit")]
        public void AttributeBuilder_LearningDelivery_LearnPlanEndDateCorrect()
        {
            //ARRANGE
            //Use Test Helpers

            //ACT
            var learningDeliveryAttributes = SetupLearningDeliveryAttributes
                (1, 1, DateTime.Parse("2018-07-31"), "0032", DateTime.Parse("2018-06-30"), DateTime.Parse("2017-08-31"), "1", "1", "4", DateTime.Parse("2018-07-31"), 100, 2, 200, 180);

            //ASSERT
            AttributeValue(learningDeliveryAttributes, "LearnPlanEndDate").Should().Be(DateTime.Parse("2018-06-30"));
        }

        /// <summary>
        /// Return LearningDelivery Attributes from AttributeBuilder and check values
        /// /// </summary>
        [Fact(DisplayName = "LearningDelivery - LearnPlanEndDate NULL Correct"), Trait("Funding Service Attribute Builders", "Unit")]
        public void AttributeBuilder_LearningDelivery_LearnPlanEndDateNullCorrect()
        {
            //ARRANGE
            //Use Test Helpers

            //ACT
            var learningDeliveryAttributes = SetupLearningDeliveryAttributes
                (1, 1, DateTime.Parse("2018-07-31"), "0032", null, DateTime.Parse("2017-08-31"), "1", "1", "4", DateTime.Parse("2018-07-31"), 100, 2, 200, 180);

            //ASSERT
            AttributeValue(learningDeliveryAttributes, "LearnPlanEndDate").Should().BeNull();
        }

        /// <summary>
        /// Return LearningDelivery Attributes from AttributeBuilder and check values
        /// /// </summary>
        [Fact(DisplayName = "LearningDelivery - LearnStartDate Correct"), Trait("Funding Service Attribute Builders", "Unit")]
        public void AttributeBuilder_LearningDelivery_LearnStartDateCorrect()
        {
            //ARRANGE
            //Use Test Helpers

            //ACT
            var learningDeliveryAttributes = SetupLearningDeliveryAttributes
                (1, 1, DateTime.Parse("2018-07-31"), "0032", DateTime.Parse("2018-06-30"), DateTime.Parse("2017-08-31"), "1", "1", "4", DateTime.Parse("2018-07-31"), 100, 2, 200, 180);

            //ASSERT
            AttributeValue(learningDeliveryAttributes, "LearnStartDate").Should().Be(DateTime.Parse("2017-08-31"));
        }

        /// <summary>
        /// Return LearningDelivery Attributes from AttributeBuilder and check values
        /// /// </summary>
        [Fact(DisplayName = "LearningDelivery - LearnStartDate NULL Correct"), Trait("Funding Service Attribute Builders", "Unit")]
        public void AttributeBuilder_LearningDelivery_LearnStartDateNullCorrect()
        {
            //ARRANGE
            //Use Test Helpers

            //ACT
            var learningDeliveryAttributes = SetupLearningDeliveryAttributes
                (1, 1, DateTime.Parse("2018-07-31"), "0032", DateTime.Parse("2018-06-30"), null, "1", "1", "4", DateTime.Parse("2018-07-31"), 100, 2, 200, 180);

            //ASSERT
            AttributeValue(learningDeliveryAttributes, "LearnStartDate").Should().BeNull();
        }

        /// <summary>
        /// Return LearningDelivery Attributes from AttributeBuilder and check values
        /// /// </summary>
        [Fact(DisplayName = "LearningDelivery - LrnDelFAM_ADL Correct"), Trait("Funding Service Attribute Builders", "Unit")]
        public void AttributeBuilder_LearningDelivery_LrnDelFAM_ADLCorrect()
        {
            //ARRANGE
            //Use Test Helpers

            //ACT
            var learningDeliveryAttributes = SetupLearningDeliveryAttributes
                (1, 1, DateTime.Parse("2018-07-31"), "0032", DateTime.Parse("2018-06-30"), DateTime.Parse("2017-08-31"), "1", "1", "4", DateTime.Parse("2018-07-31"), 100, 2, 200, 180);

            //ASSERT
            AttributeValue(learningDeliveryAttributes, "LrnDelFAM_ADL").Should().Be("1");
        }

        /// <summary>
        /// Return LearningDelivery Attributes from AttributeBuilder and check values
        /// /// </summary>
        [Fact(DisplayName = "LearningDelivery - LrnDelFAM_RES Correct"), Trait("Funding Service Attribute Builders", "Unit")]
        public void AttributeBuilder_LearningDelivery_LrnDelFAM_RESCorrect()
        {
            //ARRANGE
            //Use Test Helpers

            //ACT
            var learningDeliveryAttributes = SetupLearningDeliveryAttributes
                (1, 1, DateTime.Parse("2018-07-31"), "0032", DateTime.Parse("2018-06-30"), DateTime.Parse("2017-08-31"), "1", "1", "4", DateTime.Parse("2018-07-31"), 100, 2, 200, 180);

            //ASSERT
            AttributeValue(learningDeliveryAttributes, "LrnDelFAM_RES").Should().Be("1");
        }

        /// <summary>
        /// Return LearningDelivery Attributes from AttributeBuilder and check values
        /// /// </summary>
        [Fact(DisplayName = "LearningDelivery - NotionalNVQLevelv2 Correct"), Trait("Funding Service Attribute Builders", "Unit")]
        public void AttributeBuilder_LearningDelivery_NotionalNVQLevelv2Correct()
        {
            //ARRANGE
            //Use Test Helpers

            //ACT
            var learningDeliveryAttributes = SetupLearningDeliveryAttributes
                (1, 1, DateTime.Parse("2018-07-31"), "0032", DateTime.Parse("2018-06-30"), DateTime.Parse("2017-08-31"), "1", "1", "4", DateTime.Parse("2018-07-31"), 100, 2, 200, 180);

            //ASSERT
            AttributeValue(learningDeliveryAttributes, "NotionalNVQLevelv2").Should().Be("4");
        }

        /// <summary>
        /// Return LearningDelivery Attributes from AttributeBuilder and check values
        /// /// </summary>
        [Fact(DisplayName = "LearningDelivery - OrigLearnStartDate Correct"), Trait("Funding Service Attribute Builders", "Unit")]
        public void AttributeBuilder_LearningDelivery_OrigLearnStartDateCorrect()
        {
            //ARRANGE
            //Use Test Helpers

            //ACT
            var learningDeliveryAttributes = SetupLearningDeliveryAttributes
                (1, 1, DateTime.Parse("2018-07-31"), "0032", DateTime.Parse("2018-06-30"), DateTime.Parse("2017-08-31"), "1", "1", "4", DateTime.Parse("2018-07-31"), 100, 2, 200, 180);

            //ASSERT
            AttributeValue(learningDeliveryAttributes, "OrigLearnStartDate").Should().Be(DateTime.Parse("2018-07-31"));
        }

        /// <summary>
        /// Return LearningDelivery Attributes from AttributeBuilder and check values
        /// /// </summary>
        [Fact(DisplayName = "LearningDelivery - OrigLearnStartDate NULL Correct"), Trait("Funding Service Attribute Builders", "Unit")]
        public void AttributeBuilder_LearningDelivery_OrigLearnStartDateNullCorrect()
        {
            //ARRANGE
            //Use Test Helpers

            //ACT
            var learningDeliveryAttributes = SetupLearningDeliveryAttributes
                (1, 1, DateTime.Parse("2018-07-31"), "0032", DateTime.Parse("2018-06-30"), DateTime.Parse("2017-08-31"), "1", "1", "4", null, 100, 2, 200, 180);

            //ASSERT
            AttributeValue(learningDeliveryAttributes, "OrigLearnStartDate").Should().BeNull();
        }

        /// <summary>
        /// Return LearningDelivery Attributes from AttributeBuilder and check values
        /// /// </summary>
        [Fact(DisplayName = "LearningDelivery - OtherFundAdj Correct"), Trait("Funding Service Attribute Builders", "Unit")]
        public void AttributeBuilder_LearningDelivery_OtherFundAdjCorrect()
        {
            //ARRANGE
            //Use Test Helpers

            //ACT
            var learningDeliveryAttributes = SetupLearningDeliveryAttributes
                (1, 1, DateTime.Parse("2018-07-31"), "0032", DateTime.Parse("2018-06-30"), DateTime.Parse("2017-08-31"), "1", "1", "4", DateTime.Parse("2018-07-31"), 100, 2, 200, 180);

            //ASSERT
            AttributeValue(learningDeliveryAttributes, "OtherFundAdj").Should().Be(100);
        }

        /// <summary>
        /// Return LearningDelivery Attributes from AttributeBuilder and check values
        /// /// </summary>
        [Fact(DisplayName = "LearningDelivery - Outcome Correct"), Trait("Funding Service Attribute Builders", "Unit")]
        public void AttributeBuilder_LearningDelivery_OutcomeCorrect()
        {
            //ARRANGE
            //Use Test Helpers

            //ACT
            var learningDeliveryAttributes = SetupLearningDeliveryAttributes
                (1, 1, DateTime.Parse("2018-07-31"), "0032", DateTime.Parse("2018-06-30"), DateTime.Parse("2017-08-31"), "1", "1", "4", DateTime.Parse("2018-07-31"), 100, 2, 200, 180);

            //ASSERT
            AttributeValue(learningDeliveryAttributes, "Outcome").Should().Be(2);
        }

        /// <summary>
        /// Return LearningDelivery Attributes from AttributeBuilder and check values
        /// /// </summary>
        [Fact(DisplayName = "LearningDelivery - PriorLearnFundAdj Correct"), Trait("Funding Service Attribute Builders", "Unit")]
        public void AttributeBuilder_LearningDelivery_PriorLearnFundAdjCorrect()
        {
            //ARRANGE
            //Use Test Helpers

            //ACT
            var learningDeliveryAttributes = SetupLearningDeliveryAttributes
                (1, 1, DateTime.Parse("2018-07-31"), "0032", DateTime.Parse("2018-06-30"), DateTime.Parse("2017-08-31"), "1", "1", "4", DateTime.Parse("2018-07-31"), 100, 2, 200, 180);

            //ASSERT
            AttributeValue(learningDeliveryAttributes, "PriorLearnFundAdj").Should().Be(200);
        }

        /// <summary>
        /// Return LearningDelivery Attributes from AttributeBuilder and check values
        /// /// </summary>
        [Fact(DisplayName = "LearningDelivery - RegulatedCreditValue Correct"), Trait("Funding Service Attribute Builders", "Unit")]
        public void AttributeBuilder_LearningDelivery_RegulatedCreditValueCorrect()
        {
            //ARRANGE
            //Use Test Helpers

            //ACT
            var learningDeliveryAttributes = SetupLearningDeliveryAttributes
                (1, 1, DateTime.Parse("2018-07-31"), "0032", DateTime.Parse("2018-06-30"), DateTime.Parse("2017-08-31"), "1", "1", "4", DateTime.Parse("2018-07-31"), 100, 2, 200, 180);

            //ASSERT
            AttributeValue(learningDeliveryAttributes, "RegulatedCreditValue").Should().Be(180);
        }

        #endregion

        #region LearningDeliveryFAM Attributes

        /// <summary>
        /// Return LearningDeliveryFAM Attributes from AttributeBuilder and check values
        /// /// </summary>
        [Fact(DisplayName = "LearningDeliveryFAM - Attributes Exist"), Trait("Funding Service Attribute Builders", "Unit")]
        public void AttributeBuilder_LearningDeliveryFAM_Exists()
        {
            //ARRANGE
            //Use Test Helpers

            //ACT
            var learningDeliveryFAMAttributes = SetupLearningDeliveryFAMAttributes
                ("1", DateTime.Parse("2018-07-31"), DateTime.Parse("2018-06-30"), "ADL");

            //ASSERT
            learningDeliveryFAMAttributes.Should().NotBeEmpty();
        }

        /// <summary>
        /// Return LearningDeliveryFAM Attributes from AttributeBuilder and check values
        /// /// </summary>
        [Fact(DisplayName = "LearningDeliveryFAM - Count Correct"), Trait("Funding Service Attribute Builders", "Unit")]
        public void AttributeBuilder_LearningDeliveryFAM_CountCorrect()
        {
            //ARRANGE
            //Use Test Helpers

            //ACT
            var learningDeliveryFAMAttributes = SetupLearningDeliveryFAMAttributes
                ("1", DateTime.Parse("2018-07-31"), DateTime.Parse("2018-06-30"), "ADL");

            //ASSERT
            learningDeliveryFAMAttributes.Count.Should().Be(4);
        }

        /// <summary>
        /// Return LearningDeliveryFAM Attributes from AttributeBuilder and check values
        /// /// </summary>
        [Fact(DisplayName = "LearningDeliveryFAM - LearnDelFAMCode Correct"), Trait("Funding Service Attribute Builders", "Unit")]
        public void AttributeBuilder_LearningDeliveryFAM_LearnDelFAMCodeValueCorrect()
        {
            //ARRANGE
            //Use Test Helpers

            //ACT
            var learningDeliveryFAMAttributes = SetupLearningDeliveryFAMAttributes
                ("1", DateTime.Parse("2018-07-31"), DateTime.Parse("2018-06-30"), "ADL");

            //ASSERT
            AttributeValue(learningDeliveryFAMAttributes, "LearnDelFAMCode").Should().Be("1");
        }

        /// <summary>
        /// Return LearningDeliveryFAM Attributes from AttributeBuilder and check values
        /// /// </summary>
        [Fact(DisplayName = "LearningDeliveryFAM - LearnDelFAMDateFrom Correct"), Trait("Funding Service Attribute Builders", "Unit")]
        public void AttributeBuilder_LearningDeliveryFAM_LearnDelFAMDateFromValueCorrect()
        {
            //ARRANGE
            //Use Test Helpers

            //ACT
            var learningDeliveryFAMAttributes = SetupLearningDeliveryFAMAttributes
                ("1", DateTime.Parse("2018-07-31"), DateTime.Parse("2018-06-30"), "ADL");

            //ASSERT
            AttributeValue(learningDeliveryFAMAttributes, "LearnDelFAMDateFrom").Should().Be(DateTime.Parse("2018-07-31"));
        }

        /// <summary>
        /// Return LearningDeliveryFAM Attributes from AttributeBuilder and check values
        /// /// </summary>
        [Fact(DisplayName = "LearningDeliveryFAM - LearnDelFAMDateFrom NULL Correct"), Trait("Funding Service Attribute Builders", "Unit")]
        public void AttributeBuilder_LearningDeliveryFAM_LearnDelFAMDateFromNULLCorrect()
        {
            //ARRANGE
            //Use Test Helpers

            //ACT
            var learningDeliveryFAMAttributes = SetupLearningDeliveryFAMAttributes
                ("1", null, DateTime.Parse("2018-06-30"), "ADL");

            //ASSERT
            AttributeValue(learningDeliveryFAMAttributes, "LearnDelFAMDateFrom").Should().BeNull();
        }

        /// <summary>
        /// Return LearningDeliveryFAM Attributes from AttributeBuilder and check values
        /// /// </summary>
        [Fact(DisplayName = "LearningDeliveryFAM - LearnDelFAMDateTo Correct"), Trait("Funding Service Attribute Builders", "Unit")]
        public void AttributeBuilder_LearningDeliveryFAM_LearnDelFAMDateToValueCorrect()
        {
            //ARRANGE
            //Use Test Helpers

            //ACT
            var learningDeliveryFAMAttributes = SetupLearningDeliveryFAMAttributes
                ("1", DateTime.Parse("2018-07-31"), DateTime.Parse("2018-06-30"), "ADL");

            //ASSERT
            AttributeValue(learningDeliveryFAMAttributes, "LearnDelFAMDateTo").Should().Be(DateTime.Parse("2018-06-30"));
        }

        /// <summary>
        /// Return LearningDeliveryFAM Attributes from AttributeBuilder and check values
        /// /// </summary>
        [Fact(DisplayName = "LearningDeliveryFAM - LearnDelFAMDateTo NULL Correct"), Trait("Funding Service Attribute Builders", "Unit")]
        public void AttributeBuilder_LearningDeliveryFAM_LearnDelFAMDateToNULLCorrect()
        {
            //ARRANGE
            //Use Test Helpers

            //ACT
            var learningDeliveryFAMAttributes = SetupLearningDeliveryFAMAttributes
                ("1", DateTime.Parse("2018-07-31"), null, "ADL");

            //ASSERT
            AttributeValue(learningDeliveryFAMAttributes, "LearnDelFAMDateTo").Should().BeNull();
        }
        
        /// <summary>
        /// Return LearningDeliveryFAM Attributes from AttributeBuilder and check values
        /// /// </summary>
        [Fact(DisplayName = "LearningDeliveryFAM - LearnDelFAMType Correct"), Trait("Funding Service Attribute Builders", "Unit")]
        public void AttributeBuilder_LearningDeliveryFAM_LearnDelFAMTypeValueCorrect()
        {
            //ARRANGE
            //Use Test Helpers

            //ACT
            var learningDeliveryFAMAttributes = SetupLearningDeliveryFAMAttributes
                ("1", DateTime.Parse("2018-07-31"), DateTime.Parse("2018-06-30"), "ADL");

            //ASSERT
            AttributeValue(learningDeliveryFAMAttributes, "LearnDelFAMType").Should().Be("ADL");
        }

        #endregion

        #region LearningDeliverySfaAreaCost Attributes

        /// <summary>
        /// Return LearningDeliverySfaAreaCost Attributes from AttributeBuilder and check values
        /// /// </summary>
        [Fact(DisplayName = "LearningDeliverySFA AreaCost - Attributes Exists"), Trait("Funding Service Attribute Builders", "Unit")]
        public void AttributeBuilder_LearningDeliverySFAAC_Exists()
        {
            //ARRANGE
            //Use Test Helpers

            //ACT
            var learningDeliverySfaAreaCostAttributes = SetupLearningDeliverySfaAreaCostAttributes
                (DateTime.Parse("2018-07-31"), DateTime.Parse("2018-06-30"), 1.2m);

            //ASSERT
            learningDeliverySfaAreaCostAttributes.Should().NotBeEmpty();
        }

        /// <summary>
        /// Return LearningDeliverySfaAreaCost Attributes from AttributeBuilder and check values
        /// /// </summary>
        [Fact(DisplayName = "LearningDeliverySFA AreaCost - Count Correct"), Trait("Funding Service Attribute Builders", "Unit")]
        public void AttributeBuilder_LearningDeliverySFAAC_CountCorrect()
        {
            //ARRANGE
            //Use Test Helpers

            //ACT
            var learningDeliverySfaAreaCostAttributes = SetupLearningDeliverySfaAreaCostAttributes
                (DateTime.Parse("2018-07-31"), DateTime.Parse("2018-06-30"), 1.2m);

            //ASSERT
            learningDeliverySfaAreaCostAttributes.Count.Should().Be(3);
        }

        /// <summary>
        /// Return LearningDeliverySfaAreaCost Attributes from AttributeBuilder and check values
        /// /// </summary>
        [Fact(DisplayName = "LearningDeliverySFA AreaCost - AreaCosEffectiveFrom Correct"), Trait("Funding Service Attribute Builders", "Unit")]
        public void AttributeBuilder_LearningDeliverySFAAC_AreaCosEffectiveFromCorrect()
        {
            //ARRANGE
            //Use Test Helpers

            //ACT
            var learningDeliverySfaAreaCostAttributes = SetupLearningDeliverySfaAreaCostAttributes
                (DateTime.Parse("2018-07-31"), DateTime.Parse("2018-06-30"), 1.2m);

            //ASSERT
            AttributeValue(learningDeliverySfaAreaCostAttributes,"AreaCosEffectiveFrom").Should().Be(DateTime.Parse("2018-07-31"));
        }

        /// <summary>
        /// Return LearningDeliverySfaAreaCost Attributes from AttributeBuilder and check values
        /// /// </summary>
        [Fact(DisplayName = "LearningDeliverySFA AreaCost - AreaCosEffectiveFrom NULL Correct"), Trait("Funding Service Attribute Builders", "Unit")]
        public void AttributeBuilder_LearningDeliverySFAAC_AreaCosEffectiveFromNULLCorrect()
        {
            //ARRANGE
            //Use Test Helpers

            //ACT
            var learningDeliverySfaAreaCostAttributes = SetupLearningDeliverySfaAreaCostAttributes
                (null, DateTime.Parse("2018-06-30"), 1.2m);

            //ASSERT
            AttributeValue(learningDeliverySfaAreaCostAttributes, "AreaCosEffectiveFrom").Should().BeNull();
        }

        /// <summary>
        /// Return LearningDeliverySfaAreaCost Attributes from AttributeBuilder and check values
        /// /// </summary>
        [Fact(DisplayName = "LearningDeliverySFA AreaCost - AreaCosEffectiveTo Correct"), Trait("Funding Service Attribute Builders", "Unit")]
        public void AttributeBuilder_LearningDeliverySFAAC_AreaCosEffectiveToCorrect()
        {
            //ARRANGE
            //Use Test Helpers

            //ACT
            var learningDeliverySfaAreaCostAttributes = SetupLearningDeliverySfaAreaCostAttributes
                (DateTime.Parse("2018-07-31"), DateTime.Parse("2018-06-30"), 1.2m);

            //ASSERT
            AttributeValue(learningDeliverySfaAreaCostAttributes, "AreaCosEffectiveTo").Should().Be(DateTime.Parse("2018-06-30"));
        }

        /// <summary>
        /// Return LearningDeliverySfaAreaCost Attributes from AttributeBuilder and check values
        /// /// </summary>
        [Fact(DisplayName = "LearningDeliverySFA AreaCost - AreaCosEffectiveTo NULL Correct"), Trait("Funding Service Attribute Builders", "Unit")]
        public void AttributeBuilder_LearningDeliverySFAAC_AreaCosEffectiveToNULLCorrect()
        {
            //ARRANGE
            //Use Test Helpers

            //ACT
            var learningDeliverySfaAreaCostAttributes = SetupLearningDeliverySfaAreaCostAttributes
                (DateTime.Parse("2018-07-31"), null, 1.2m);

            //ASSERT
            AttributeValue(learningDeliverySfaAreaCostAttributes, "AreaCosEffectiveTo").Should().BeNull();
        }

        /// <summary>
        /// Return LearningDeliverySfaAreaCost Attributes from AttributeBuilder and check values
        /// /// </summary>
        [Fact(DisplayName = "LearningDeliverySFA AreaCost - AreaCosFactor Correct"), Trait("Funding Service Attribute Builders", "Unit")]
        public void AttributeBuilder_LearningDeliverySFAAC_AreaCosFactorCorrect()
        {
            //ARRANGE
            //Use Test Helpers

            //ACT
            var learningDeliverySfaAreaCostAttributes = SetupLearningDeliverySfaAreaCostAttributes
                (DateTime.Parse("2018-07-31"), DateTime.Parse("2018-06-30"), 1.2m);

            //ASSERT
            AttributeValue(learningDeliverySfaAreaCostAttributes, "AreaCosFactor").Should().Be(1.2m);
        }


        #endregion


        #region LearningDeliveryLarsFunding Attributes

        /// <summary>
        /// Return LearningDeliveryLarsFunding Attributes from AttributeBuilder and check values
        /// /// </summary>
        [Fact(DisplayName = "LearningDeliveryLarsFunding - Attributes Exists"), Trait("Funding Service Attribute Builders", "Unit")]
        public void AttributeBuilder_LearningDeliveryLarsFunding_Exists()
        {
            //ARRANGE
            //Use Test Helpers

            //ACT
            var learningDeliveryLarsFundingAttributes = SetupLearningDeliveryLarsFundingAttributes
                ("Matrix", DateTime.Parse("2017-08-31"), DateTime.Parse("2018-06-30"), 1.8m, "A");

            //ASSERT
            learningDeliveryLarsFundingAttributes.Should().NotBeEmpty();
        }

        /// <summary>
        /// Return LearningDeliveryLarsFunding Attributes from AttributeBuilder and check values
        /// /// </summary>
        [Fact(DisplayName = "LearningDeliveryLarsFunding - Count Correct"), Trait("Funding Service Attribute Builders", "Unit")]
        public void AttributeBuilder_LearningDeliveryLarsFunding_CountCorrect()
        {
            //ARRANGE
            //Use Test Helpers

            //ACT
            var learningDeliveryLarsFundingAttributes = SetupLearningDeliveryLarsFundingAttributes
                ("Matrix", DateTime.Parse("2017-08-31"), DateTime.Parse("2018-06-30"), 1.8m, "A");

            //ASSERT
            learningDeliveryLarsFundingAttributes.Count.Should().Be(5);
        }

        /// <summary>
        /// Return LearningDeliveryLarsFunding Attributes from AttributeBuilder and check values
        /// /// </summary>
        [Fact(DisplayName = "LearningDeliveryLarsFunding - LARSFundCategory Correct"), Trait("Funding Service Attribute Builders", "Unit")]
        public void AttributeBuilder_LearningDeliveryLarsFunding_LARSFundCategoryCorrect()
        {
            //ARRANGE
            //Use Test Helpers

            //ACT
            var learningDeliveryLarsFundingAttributes = SetupLearningDeliveryLarsFundingAttributes
                ("Matrix", DateTime.Parse("2017-08-31"), DateTime.Parse("2018-06-30"), 1.8m, "A");

            //ASSERT
            AttributeValue(learningDeliveryLarsFundingAttributes, "LARSFundCategory").Should().Be("Matrix");
        }

        /// <summary>
        /// Return LearningDeliveryLarsFunding Attributes from AttributeBuilder and check values
        /// /// </summary>
        [Fact(DisplayName = "LearningDeliveryLarsFunding - LARSFundEffectiveFrom Correct"), Trait("Funding Service Attribute Builders", "Unit")]
        public void AttributeBuilder_LearningDeliveryLarsFunding_LARSFundEffectiveFromCorrect()
        {
            //ARRANGE
            //Use Test Helpers

            //ACT
            var learningDeliveryLarsFundingAttributes = SetupLearningDeliveryLarsFundingAttributes
                ("Matrix", DateTime.Parse("2017-08-31"), DateTime.Parse("2018-06-30"), 1.8m, "A");

            //ASSERT
            AttributeValue(learningDeliveryLarsFundingAttributes, "LARSFundEffectiveFrom").Should().Be(DateTime.Parse("2017-08-31"));
        }

        /// <summary>
        /// Return LearningDeliveryLarsFunding Attributes from AttributeBuilder and check values
        /// /// </summary>
        [Fact(DisplayName = "LearningDeliveryLarsFunding - LARSFundEffectiveTo Correct"), Trait("Funding Service Attribute Builders", "Unit")]
        public void AttributeBuilder_LearningDeliveryLarsFunding_LARSFundEffectiveToCorrect()
        {
            //ARRANGE
            //Use Test Helpers

            //ACT
            var learningDeliveryLarsFundingAttributes = SetupLearningDeliveryLarsFundingAttributes
                ("Matrix", DateTime.Parse("2017-08-31"), DateTime.Parse("2018-06-30"), 1.8m, "A");

            //ASSERT
            AttributeValue(learningDeliveryLarsFundingAttributes, "LARSFundEffectiveTo").Should().Be(DateTime.Parse("2018-06-30"));
        }

        /// <summary>
        /// Return LearningDeliveryLarsFunding Attributes from AttributeBuilder and check values
        /// /// </summary>
        [Fact(DisplayName = "LearningDeliveryLarsFunding - LARSFundEffectiveTo Correct"), Trait("Funding Service Attribute Builders", "Unit")]
        public void AttributeBuilder_LearningDeliveryLarsFunding_LARSFundEffectiveToNULLCorrect()
        {
            //ARRANGE
            //Use Test Helpers

            //ACT
            var learningDeliveryLarsFundingAttributes = SetupLearningDeliveryLarsFundingAttributes
                ("Matrix", DateTime.Parse("2017-08-31"), null, 1.8m, "A");

            //ASSERT
            AttributeValue(learningDeliveryLarsFundingAttributes, "LARSFundEffectiveTo").Should().BeNull();
        }

        /// <summary>
        /// Return LearningDeliveryLarsFunding Attributes from AttributeBuilder and check values
        /// /// </summary>
        [Fact(DisplayName = "LearningDeliveryLarsFunding - LARSFundWeightedRate Correct"), Trait("Funding Service Attribute Builders", "Unit")]
        public void AttributeBuilder_LearningDeliveryLarsFunding_LARSFundWeightedRateCorrect()
        {
            //ARRANGE
            //Use Test Helpers

            //ACT
            var learningDeliveryLarsFundingAttributes = SetupLearningDeliveryLarsFundingAttributes
                ("Matrix", DateTime.Parse("2017-08-31"), DateTime.Parse("2018-06-30"), 1.8m, "A");

            //ASSERT
            AttributeValue(learningDeliveryLarsFundingAttributes, "LARSFundWeightedRate").Should().Be(1.8m);
        }

        /// <summary>
        /// Return LearningDeliveryLarsFunding Attributes from AttributeBuilder and check values
        /// /// </summary>
        [Fact(DisplayName = "LearningDeliveryLarsFunding - LARSFundWeightedRate NULL Correct"), Trait("Funding Service Attribute Builders", "Unit")]
        public void AttributeBuilder_LearningDeliveryLarsFunding_LARSFundWeightedRateNULLCorrect()
        {
            //ARRANGE
            //Use Test Helpers

            //ACT
            var learningDeliveryLarsFundingAttributes = SetupLearningDeliveryLarsFundingAttributes
                ("Matrix", DateTime.Parse("2017-08-31"), DateTime.Parse("2018-06-30"), null, "A");

            //ASSERT
            AttributeValue(learningDeliveryLarsFundingAttributes, "LARSFundWeightedRate").Should().BeNull();
        }

        /// <summary>
        /// Return LearningDeliveryLarsFunding Attributes from AttributeBuilder and check values
        /// /// </summary>
        [Fact(DisplayName = "LearningDeliveryLarsFunding - LARSFundWeightingFactor Correct"), Trait("Funding Service Attribute Builders", "Unit")]
        public void AttributeBuilder_LearningDeliveryLarsFunding_LARSFundWeightingFactorCorrect()
        {
            //ARRANGE
            //Use Test Helpers

            //ACT
            var learningDeliveryLarsFundingAttributes = SetupLearningDeliveryLarsFundingAttributes
                ("Matrix", DateTime.Parse("2017-08-31"), DateTime.Parse("2018-06-30"), 1.8m, "A");

            //ASSERT
            AttributeValue(learningDeliveryLarsFundingAttributes, "LARSFundWeightingFactor").Should().Be("A");
        }

        #endregion

        #region Test Helpers

        private static IDictionary<string, IAttributeData> SetupGlobalAttributes(int ukprn, string larsVersion, string postcodeAreaCostVersion)
        {
            IAttributeBuilder<IAttributeData> attributeBuilder = new AttributeBuilder();

            return attributeBuilder.BuildGlobalAttributes(ukprn, larsVersion, postcodeAreaCostVersion);
        }

        private static IDictionary<string, IAttributeData> SetupLearnerAttributes(string learnRefNumber)
        {
            IAttributeBuilder<IAttributeData> attributeBuilder = new AttributeBuilder();

            return attributeBuilder.BuildLearnerAttributes(learnRefNumber);
        }

        private static IDictionary<string, IAttributeData> SetupLearningDeliveryAttributes
        (
         int aimSeqNumber,
         int compStatus,
         DateTime? learnActEndDate,
         string learnAimRefType,
         DateTime? learnPlanEndDate,
         DateTime? learnStartDate,
         string lrnDelFAM_ADL,
         string lrnDelFAM_RES,
         string notionalNVQLevelv2,
         DateTime? origLearnStartDate,
         long? otherFundAdj,
         int? outcome,
         long? priorLearnFundAdj,
         int? regulatedCreditValue
        )
        {
            IAttributeBuilder<IAttributeData> attributeBuilder = new AttributeBuilder();

            return attributeBuilder.BuildLearningDeliveryAttributes(
                aimSeqNumber,
                compStatus,
                learnActEndDate,
                learnAimRefType,
                learnPlanEndDate,
                learnStartDate,
                lrnDelFAM_ADL,
                lrnDelFAM_RES,
                notionalNVQLevelv2,
                origLearnStartDate,
                otherFundAdj,
                outcome,
                priorLearnFundAdj,
                regulatedCreditValue);
        }

        private static IDictionary<string, IAttributeData> SetupLearningDeliveryFAMAttributes(
            string learnDelFAMCode, DateTime? learnDelFAMDateFrom, DateTime? learnDelFAMDateTo, string learnDelFAMType)
        {
            IAttributeBuilder<IAttributeData> attributeBuilder = new AttributeBuilder();

            return attributeBuilder.BuildLearningDeliveryFAMAttributes(learnDelFAMCode,learnDelFAMDateFrom,learnDelFAMDateTo, learnDelFAMType);
        }
        private static IDictionary<string, IAttributeData> SetupLearningDeliverySfaAreaCostAttributes(
            DateTime? areaCosEffectiveFrom, DateTime? areaCosEffectiveTo, decimal areaCosFactor)
        {
            IAttributeBuilder<IAttributeData> attributeBuilder = new AttributeBuilder();

            return attributeBuilder.BuildLearningDeliverySfaAreaCostAttributes(areaCosEffectiveFrom, areaCosEffectiveTo, areaCosFactor);
        }

        private static IDictionary<string, IAttributeData> SetupLearningDeliveryLarsFundingAttributes(
            string larsFundCategory, DateTime larsFundEffectiveFrom, DateTime? larsFundEffectiveTo, decimal? larsFundWeightedRate, string larsFundWeightingFactor)
        {
            IAttributeBuilder<IAttributeData> attributeBuilder = new AttributeBuilder();

            return attributeBuilder.BuildLearningDeliveryLarsFundingAttributes(larsFundCategory, larsFundEffectiveFrom, larsFundEffectiveTo, larsFundWeightedRate, larsFundWeightingFactor);
        }

        private static object AttributeValue(IDictionary<string, IAttributeData> dictionary, string attributeName)
        {
            return dictionary.Where(k => k.Key == attributeName).Select(v => v.Value.Value).Single();
        }

        #endregion
    }
}
