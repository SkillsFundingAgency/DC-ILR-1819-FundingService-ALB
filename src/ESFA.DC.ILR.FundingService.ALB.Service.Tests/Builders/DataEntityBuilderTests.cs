using System;
using System.Collections.Generic;
using System.Linq;
using ESFA.DC.ILR.FundingService.ALB.ExternalData.Interface;
using ESFA.DC.ILR.FundingService.ALB.ExternalData.LARS.Model;
using ESFA.DC.ILR.FundingService.ALB.ExternalData.Postcodes.Model;
using ESFA.DC.ILR.FundingService.ALB.Service.Builders;
using ESFA.DC.ILR.FundingService.ALB.Service.Builders.Interface;
using ESFA.DC.ILR.Model;
using ESFA.DC.ILR.Model.Interface;
using ESFA.DC.OPA.Model;
using ESFA.DC.OPA.Model.Interface;
using FluentAssertions;
using Moq;
using Xunit;

namespace ESFA.DC.ILR.FundingService.ALB.Service.Tests.Builders
{
    public class DataEntityBuilderTests
    {
        #region Entity Builder

        /// <summary>
        /// Return Entity from EntityBuilder and check values
        /// </summary>
        [Fact(DisplayName = "EntityBuilder - Global Entity Exists"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void EntityBuilder_Global_Exists()
        {
            //ARRANGE
            // Use Test Helpers

            //ACT
            var dataEntity = SetupDataEntity().Single();

            //ASSERT
            Entity(dataEntity).Should().NotBeNull();
        }

        /// <summary>
        /// Return Entity from EntityBuilder and check values
        /// </summary>
        [Fact(DisplayName = "EntityBuilder - Global Entity Name Correct"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void EntityBuilder_Global_EntityNameCorrect()
        {
            //ARRANGE
            // Use Test Helpers

            //ACT
            var dataEntity = SetupDataEntity().Single();

            //ASSERT
            Entity(dataEntity).EntityName.Should().BeEquivalentTo("global");
        }

        /// <summary>
        /// Return Entity from EntityBuilder and check values
        /// </summary>
        [Fact(DisplayName = "EntityBuilder - Global IsGlobal True"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void EntityBuilder_Global_IsGlobalTrue()
        {
            //ARRANGE
            // Use Test Helpers

            //ACT
            var dataEntity = SetupDataEntity().Single();

            //ASSERT
            Entity(dataEntity).IsGlobal.Should().BeTrue();
        }

        /// <summary>
        /// Return Entity from EntityBuilder and check values
        /// </summary>
        [Fact(DisplayName = "EntityBuilder - Global Children Count Correct"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void EntityBuilder_Global_ChildrenCountCorrect()
        {
            //ARRANGE
            // Use Test Helpers

            //ACT
            var dataEntity = SetupDataEntity().Single();

            //ASSERT
            Entity(dataEntity).Children.Count.Should().Be(1);
        }

        /// <summary>
        /// Return Entity from EntityBuilder and check values
        /// </summary>
        [Fact(DisplayName = "EntityBuilder - Global Children Correct"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void EntityBuilder_Global_ChildrenCorrect()
        {
            //ARRANGE
            // Use Test Helpers

            //ACT
            var dataEntity = SetupDataEntity().Single();

            //ASSERT
            Entity(dataEntity).Children.Select(c => c.EntityName).Should().BeEquivalentTo("Learner");
        }

        /// <summary>
        /// Return Entity from EntityBuilder and check values
        /// </summary>
        [Fact(DisplayName = "EntityBuilder - Global Parent Correct"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void EntityBuilder_Global_ParentCorrect()
        {
            //ARRANGE
            // Use Test Helpers

            //ACT
            var dataEntity = SetupDataEntity().Single();

            //ASSERT
            Entity(dataEntity).Parent.Should().BeNull();
        }

        /// <summary>
        /// Return Entity from EntityBuilder and check values
        /// </summary>
        [Fact(DisplayName = "EntityBuilder - Global Attributes Count Correct"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void EntityBuilder_Global_AttributesCountCorrect()
        {
            //ARRANGE
            // Use Test Helpers

            //ACT
            var dataEntity = SetupDataEntity().Single();

            //ASSERT
            Entity(dataEntity).Attributes.Count.Should().Be(3);
        }

        /// <summary>
        /// Return Entity from EntityBuilder and check values
        /// </summary>
        [Fact(DisplayName = "EntityBuilder - Global Attributes Correct"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void EntityBuilder_Global_AttributesCorrect()
        {
            //ARRANGE
            // Use Test Helpers

            //ACT
            var dataEntity = SetupDataEntity().Single();

            //ASSERT
            Entity(dataEntity).Attributes.Should().BeEquivalentTo(ExpectedGlobalAttributes());
        }

        /// <summary>
        /// Return Entity from EntityBuilder and check values
        /// </summary>
        [Fact(DisplayName = "EntityBuilder - Learner Entity Exists"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void EntityBuilder_Learner_Exists()
        {
            //ARRANGE
            // Use Test Helpers

            //ACT
            var dataEntity = SetupDataEntity().Single();
            var learnerEntity = dataEntity.Children.Single();

            //ASSERT
            Entity(learnerEntity).Should().NotBeNull();
        }

        /// <summary>
        /// Return Entity from EntityBuilder and check values
        /// </summary>
        [Fact(DisplayName = "EntityBuilder - Learner Entity Name Correct"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void EntityBuilder_Learner_EntityNameCorrect()
        {
            //ARRANGE
            // Use Test Helpers

            //ACT
            var dataEntity = SetupDataEntity().Single();
            var learnerEntity = dataEntity.Children.Single();

            //ASSERT
            Entity(learnerEntity).EntityName.Should().BeEquivalentTo("Learner");
        }

        /// <summary>
        /// Return Entity from EntityBuilder and check values
        /// </summary>
        [Fact(DisplayName = "EntityBuilder - Learner IsGlobal False"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void EntityBuilder_Learner_IsGlobalFalse()
        {
            //ARRANGE
            // Use Test Helpers

            //ACT
            var dataEntity = SetupDataEntity().Single();
            var learnerEntity = dataEntity.Children.Single();

            //ASSERT
            Entity(learnerEntity).IsGlobal.Should().BeFalse();
        }

        /// <summary>
        /// Return Entity from EntityBuilder and check values
        /// </summary>
        [Fact(DisplayName = "EntityBuilder - Learner Children Count Correct"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void EntityBuilder_Learner_ChildrenCountCorrect()
        {
            //ARRANGE
            // Use Test Helpers

            //ACT
            var dataEntity = SetupDataEntity().Single();
            var learnerEntity = dataEntity.Children.Single();

            //ASSERT
            Entity(learnerEntity).Children.Count.Should().Be(1);
        }

        /// <summary>
        /// Return Entity from EntityBuilder and check values
        /// </summary>
        [Fact(DisplayName = "EntityBuilder - Learner Children Correct"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void EntityBuilder_Learner_ChildrenCorrect()
        {
            //ARRANGE
            // Use Test Helpers

            //ACT
            var dataEntity = SetupDataEntity().Single();
            var learnerEntity = dataEntity.Children.Single();

            //ASSERT
            Entity(learnerEntity).Children.Select(c => c.EntityName).Should().BeEquivalentTo("LearningDelivery");
        }

        /// <summary>
        /// Return Entity from EntityBuilder and check values
        /// </summary>
        [Fact(DisplayName = "EntityBuilder - Learner Parent Correct"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void EntityBuilder_Learner_ParentCorrect()
        {
            //ARRANGE
            // Use Test Helpers

            //ACT
            var dataEntity = SetupDataEntity().Single();
            var learnerEntity = dataEntity.Children.Single();

            //ASSERT
            Entity(learnerEntity).Parent.Should().BeNull();
        }

        /// <summary>
        /// Return Entity from EntityBuilder and check values
        /// </summary>
        [Fact(DisplayName = "EntityBuilder - Learner Attributes Count Correct"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void EntityBuilder_Learner_AttributesCountCorrect()
        {
            //ARRANGE
            // Use Test Helpers

            //ACT
            var dataEntity = SetupDataEntity().Single();
            var learnerEntity = dataEntity.Children.Single();

            //ASSERT
            Entity(learnerEntity).Attributes.Count.Should().Be(1);
        }

        /// <summary>
        /// Return Entity from EntityBuilder and check values
        /// </summary>
        [Fact(DisplayName = "EntityBuilder - Learner Attributes Correct"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void EntityBuilder_Learner_AttributesCorrect()
        {
            //ARRANGE
            // Use Test Helpers

            //ACT
            var dataEntity = SetupDataEntity().Single();
            var learnerEntity = dataEntity.Children.Single();

            //ASSERT
            Entity(learnerEntity).Attributes.Should().BeEquivalentTo(ExpectedLearnerAttributes());
        }

        /// <summary>
        /// Return Entity from EntityBuilder and check values
        /// </summary>
        [Fact(DisplayName = "EntityBuilder - LearningDelivery Entity Exists"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void EntityBuilder_LearningDelivery_Exists()
        {
            //ARRANGE
            // Use Test Helpers

            //ACT
            var dataEntity = SetupDataEntity().Single();
            var learnerEntity = dataEntity.Children.Single();
            var learningDeliveryEntity = learnerEntity.Children.Single();

            //ASSERT
            Entity(learningDeliveryEntity).Should().NotBeNull();
        }

        /// <summary>
        /// Return Entity from EntityBuilder and check values
        /// </summary>
        [Fact(DisplayName = "EntityBuilder - LearningDelivery Entity Name Correct"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void EntityBuilder_LearningDelivery_EntityNameCorrect()
        {
            //ARRANGE
            // Use Test Helpers

            //ACT
            var dataEntity = SetupDataEntity().Single();
            var learnerEntity = dataEntity.Children.Single();
            var learningDeliveryEntity = learnerEntity.Children.Single();

            //ASSERT
            Entity(learningDeliveryEntity).EntityName.Should().BeEquivalentTo("LearningDelivery");
        }

        /// <summary>
        /// Return Entity from EntityBuilder and check values
        /// </summary>
        [Fact(DisplayName = "EntityBuilder - LearningDelivery IsGlobal False"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void EntityBuilder_LearningDelivery_IsGlobalFalse()
        {
            //ARRANGE
            // Use Test Helpers

            //ACT
            var dataEntity = SetupDataEntity().Single();
            var learnerEntity = dataEntity.Children.Single();
            var learningDeliveryEntity = learnerEntity.Children.Single();

            //ASSERT
            Entity(learningDeliveryEntity).IsGlobal.Should().BeFalse();
        }

        /// <summary>
        /// Return Entity from EntityBuilder and check values
        /// </summary>
        [Fact(DisplayName = "EntityBuilder - LearningDelivery Children Count Correct"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void EntityBuilder_LearningDelivery_ChildrenCountCorrect()
        {
            //ARRANGE
            // Use Test Helpers

            //ACT
            var dataEntity = SetupDataEntity().Single();
            var learnerEntity = dataEntity.Children.Single();
            var learningDeliveryEntity = learnerEntity.Children.Single();

            //ASSERT
            Entity(learningDeliveryEntity).Children.Count.Should().Be(5);
        }

        /// <summary>
        /// Return Entity from EntityBuilder and check values
        /// </summary>
        [Fact(DisplayName = "EntityBuilder - LearningDelivery Children Correct"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void EntityBuilder_LearningDelivery_ChildrenCorrect()
        {
            //ARRANGE
            // Use Test Helpers

            //ACT
            var dataEntity = SetupDataEntity().Single();
            var learnerEntity = dataEntity.Children.Single();
            var learningDeliveryEntity = learnerEntity.Children.Single();

            //ASSERT
            var expectedEntityNames = new List<string>
            {
                "LearningDeliveryFAM",
                "LearningDeliveryFAM",
                "LearningDeliveryFAM",
                "SFA_PostcodeAreaCost",
                "LearningDeliveryLARS_Funding"
            };

            var actualEntityNames = Entity(learningDeliveryEntity).Children.Select(c => c.EntityName).ToList();

            expectedEntityNames.Should().BeEquivalentTo(actualEntityNames);
        }

        /// <summary>
        /// Return Entity from EntityBuilder and check values
        /// </summary>
        [Fact(DisplayName = "EntityBuilder - LearningDelivery Parent Correct"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void EntityBuilder_LearningDelivery_ParentCorrect()
        {
            //ARRANGE
            // Use Test Helpers

            //ACT
            var dataEntity = SetupDataEntity().Single();
            var learnerEntity = dataEntity.Children.Single();
            var learningDeliveryEntity = learnerEntity.Children.Single();

            //ASSERT
            Entity(learningDeliveryEntity).Parent.Should().BeNull();
        }

        /// <summary>
        /// Return Entity from EntityBuilder and check values
        /// </summary>
        [Fact(DisplayName = "EntityBuilder - LearningDelivery Attributes Count Correct"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void EntityBuilder_LearningDelivery_AttributesCountCorrect()
        {
            //ARRANGE
            // Use Test Helpers

            //ACT
            var dataEntity = SetupDataEntity().Single();
            var learnerEntity = dataEntity.Children.Single();
            var learningDeliveryEntity = learnerEntity.Children.Single();

            //ASSERT
            Entity(learningDeliveryEntity).Attributes.Count.Should().Be(14);
        }

        /// <summary>
        /// Return Entity from EntityBuilder and check values
        /// </summary>
        [Fact(DisplayName = "EntityBuilder - LearningDelivery Attributes Correct"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void EntityBuilder_LearningDelivery_AttributesCorrect()
        {
            //ARRANGE
            // Use Test Helpers

            //ACT
            var dataEntity = SetupDataEntity().Single();
            var learnerEntity = dataEntity.Children.Single();
            var learningDeliveryEntity = learnerEntity.Children.Single();

            //ASSERT
            Entity(learningDeliveryEntity).Attributes.Should().BeEquivalentTo(ExpectedLearningDeliveryAttributes());
        }

        /// <summary>
        /// Return Entity from EntityBuilder and check values
        /// </summary>
        [Fact(DisplayName = "EntityBuilder - LearningDeliveryFAM Entities Exists"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void EntityBuilder_LearningDeliveryFAM_Exists()
        {
            //ARRANGE
            // Use Test Helpers

            //ACT
            var dataEntity = SetupDataEntity().Single();
            var learnerEntity = dataEntity.Children.Single();
            var learningDeliveryEntity = learnerEntity.Children.Single();
            var learningDeliveryFAM = learningDeliveryEntity.Children.Where(c => c.EntityName == "LearningDeliveryFAM");

            //ASSERT
            Entities(learningDeliveryFAM).Should().NotBeNull();
        }

        /// <summary>
        /// Return Entity from EntityBuilder and check values
        /// </summary>
        [Fact(DisplayName = "EntityBuilder - LearningDeliveryFAM Entity Name Correct"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void EntityBuilder_LearningDeliveryFAM_EntityNameCorrect()
        {
            //ARRANGE
            // Use Test Helpers

            //ACT
            var dataEntity = SetupDataEntity().Single();
            var learnerEntity = dataEntity.Children.Single();
            var learningDeliveryEntity = learnerEntity.Children.Single();
            var learningDeliveryFAM = learningDeliveryEntity.Children.Where(c => c.EntityName == "LearningDeliveryFAM");

            //ASSERT
            Entities(learningDeliveryFAM).First().EntityName.Should().BeEquivalentTo("LearningDeliveryFAM");
        }

        /// <summary>
        /// Return Entity from EntityBuilder and check values
        /// </summary>
        [Fact(DisplayName = "EntityBuilder - LearningDeliveryFAM IsGlobal False"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void EntityBuilder_LearningDeliveryFAM_IsGlobalFalse()
        {
            //ARRANGE
            // Use Test Helpers

            //ACT
            var dataEntity = SetupDataEntity().Single();
            var learnerEntity = dataEntity.Children.Single();
            var learningDeliveryEntity = learnerEntity.Children.Single();
            var learningDeliveryFAM = learningDeliveryEntity.Children.Where(c => c.EntityName == "LearningDeliveryFAM");

            //ASSERT
            Entities(learningDeliveryFAM).First().IsGlobal.Should().BeFalse();
        }

        /// <summary>
        /// Return Entity from EntityBuilder and check values
        /// </summary>
        [Fact(DisplayName = "EntityBuilder - LearningDeliveryFAM Children Count Correct"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void EntityBuilder_LearningDeliveryFAM_ChildrenCountCorrect()
        {
            //ARRANGE
            // Use Test Helpers

            //ACT
            var dataEntity = SetupDataEntity().Single();
            var learnerEntity = dataEntity.Children.Single();
            var learningDeliveryEntity = learnerEntity.Children.Single();
            var learningDeliveryFAM = learningDeliveryEntity.Children.Where(c => c.EntityName == "LearningDeliveryFAM");

            //ASSERT
            Entities(learningDeliveryFAM).First().Children.Count.Should().Be(0);
        }

        /// <summary>
        /// Return Entity from EntityBuilder and check values
        /// </summary>
        [Fact(DisplayName = "EntityBuilder - LearningDeliveryFAM Children Correct"),Trait("Funding Service DataEntity Builders", "Unit")]
        public void EntityBuilder_LearningDeliveryFAM_ChildrenCorrect()
        {
            //ARRANGE
            // Use Test Helpers

            //ACT
            var dataEntity = SetupDataEntity().Single();
            var learnerEntity = dataEntity.Children.Single();
            var learningDeliveryEntity = learnerEntity.Children.Single();
            var learningDeliveryFAM = learningDeliveryEntity.Children.Where(c => c.EntityName == "LearningDeliveryFAM");

            //ASSERT
            Entities(learningDeliveryFAM).First().Children.Should().BeNullOrEmpty();
    }

        /// <summary>
        /// Return Entity from EntityBuilder and check values
        /// </summary>
        [Fact(DisplayName = "EntityBuilder - LearningDeliveryFAM Parent Correct"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void EntityBuilder_LearningDeliveryFAM_ParentCorrect()
        {
            //ARRANGE
            // Use Test Helpers

            //ACT
            var dataEntity = SetupDataEntity().Single();
            var learnerEntity = dataEntity.Children.Single();
            var learningDeliveryEntity = learnerEntity.Children.Single();
            var learningDeliveryFAM = learningDeliveryEntity.Children.Where(c => c.EntityName == "LearningDeliveryFAM");

            //ASSERT
            Entities(learningDeliveryFAM).First().Parent.Should().BeNull();
        }

        /// <summary>
        /// Return Entity from EntityBuilder and check values
        /// </summary>
        [Fact(DisplayName = "EntityBuilder - LearningDeliveryFAM Attributes Count Correct"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void EntityBuilder_LearningDeliveryFAM_AttributesCountCorrect()
        {
            //ARRANGE
            // Use Test Helpers

            //ACT
            var dataEntity = SetupDataEntity().Single();
            var learnerEntity = dataEntity.Children.Single();
            var learningDeliveryEntity = learnerEntity.Children.Single();
            var learningDeliveryFAM = learningDeliveryEntity.Children.Where(c => c.EntityName == "LearningDeliveryFAM");

            //ASSERT
            Entities(learningDeliveryFAM).First().Attributes.Count.Should().Be(4);
        }

        /// <summary>
        /// Return Entity from EntityBuilder and check values
        /// </summary>
        [Fact(DisplayName = "EntityBuilder - LearningDeliveryFAM Attributes Correct"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void EntityBuilder_LearningDeliveryFAM_AttributesCorrect()
        {
            //ARRANGE
            //Use Test Helpers

            //ACT
            var dataEntity = SetupDataEntity().Single();
            var learnerEntity = dataEntity.Children.Single();
            var learningDeliveryEntity = learnerEntity.Children.Single();
            var learningDeliveryFAM = learningDeliveryEntity.Children.Where(c => c.EntityName == "LearningDeliveryFAM");

            //ASSERT
            Entities(learningDeliveryFAM).First().Attributes.Should().BeEquivalentTo(ExpectedLearningDeliveryFAMAttributes());
        }

        /// <summary>
        /// Return Entity from EntityBuilder and check values
        /// </summary>
        [Fact(DisplayName = "EntityBuilder - SFA_PostcodeAreaCost Entities Exists"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void EntityBuilder_SFA_PostcodeAreaCost_Exists()
        {
            //ARRANGE
            // Use Test Helpers

            //ACT
            var dataEntity = SetupDataEntity().Single();
            var learnerEntity = dataEntity.Children.Single();
            var learningDeliveryEntity = learnerEntity.Children.Single();
            var sfaPostcodeAreaCost = learningDeliveryEntity.Children.Where(c => c.EntityName == "SFA_PostcodeAreaCost");

            //ASSERT
            Entities(sfaPostcodeAreaCost).Should().NotBeNull();
        }

        /// <summary>
        /// Return Entity from EntityBuilder and check values
        /// </summary>
        [Fact(DisplayName = "EntityBuilder - SFA_PostcodeAreaCost Entity Name Correct"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void EntityBuilder_SFA_PostcodeAreaCost_EntityNameCorrect()
        {
            //ARRANGE
            // Use Test Helpers

            //ACT
            var dataEntity = SetupDataEntity().Single();
            var learnerEntity = dataEntity.Children.Single();
            var learningDeliveryEntity = learnerEntity.Children.Single();
            var sfaPostcodeAreaCost = learningDeliveryEntity.Children.Where(c => c.EntityName == "SFA_PostcodeAreaCost");

            //ASSERT
            Entities(sfaPostcodeAreaCost).First().EntityName.Should().BeEquivalentTo("SFA_PostcodeAreaCost");
        }

        /// <summary>
        /// Return Entity from EntityBuilder and check values
        /// </summary>
        [Fact(DisplayName = "EntityBuilder - SFA_PostcodeAreaCost IsGlobal False"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void EntityBuilder_SFA_PostcodeAreaCost_IsGlobalFalse()
        {
            //ARRANGE
            // Use Test Helpers

            //ACT
            var dataEntity = SetupDataEntity().Single();
            var learnerEntity = dataEntity.Children.Single();
            var learningDeliveryEntity = learnerEntity.Children.Single();
            var sfaPostcodeAreaCost = learningDeliveryEntity.Children.Where(c => c.EntityName == "SFA_PostcodeAreaCost");

            //ASSERT
            Entities(sfaPostcodeAreaCost).First().IsGlobal.Should().BeFalse();
        }

        /// <summary>
        /// Return Entity from EntityBuilder and check values
        /// </summary>
        [Fact(DisplayName = "EntityBuilder - SFA_PostcodeAreaCost Children Count Correct"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void EntityBuilder_SFA_PostcodeAreaCost_ChildrenCountCorrect()
        {
            //ARRANGE
            // Use Test Helpers

            //ACT
            var dataEntity = SetupDataEntity().Single();
            var learnerEntity = dataEntity.Children.Single();
            var learningDeliveryEntity = learnerEntity.Children.Single();
            var sfaPostcodeAreaCost = learningDeliveryEntity.Children.Where(c => c.EntityName == "SFA_PostcodeAreaCost");

            //ASSERT
            Entities(sfaPostcodeAreaCost).First().Children.Count.Should().Be(0);
        }

        /// <summary>
        /// Return Entity from EntityBuilder and check values
        /// </summary>
        [Fact(DisplayName = "EntityBuilder - SFA_PostcodeAreaCost Children Correct"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void EntityBuilder_SFA_PostcodeAreaCost_ChildrenCorrect()
        {
            //ARRANGE
            // Use Test Helpers

            //ACT
            var dataEntity = SetupDataEntity().Single();
            var learnerEntity = dataEntity.Children.Single();
            var learningDeliveryEntity = learnerEntity.Children.Single();
            var sfaPostcodeAreaCost = learningDeliveryEntity.Children.Where(c => c.EntityName == "SFA_PostcodeAreaCost");

            //ASSERT
            Entities(sfaPostcodeAreaCost).First().Children.Should().BeNullOrEmpty();
        }

        /// <summary>
        /// Return Entity from EntityBuilder and check values
        /// </summary>
        [Fact(DisplayName = "EntityBuilder - SFA_PostcodeAreaCost Parent Correct"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void EntityBuilder_SFA_PostcodeAreaCost_ParentCorrect()
        {
            //ARRANGE
            // Use Test Helpers

            //ACT
            var dataEntity = SetupDataEntity().Single();
            var learnerEntity = dataEntity.Children.Single();
            var learningDeliveryEntity = learnerEntity.Children.Single();
            var sfaPostcodeAreaCost = learningDeliveryEntity.Children.Where(c => c.EntityName == "SFA_PostcodeAreaCost");

            //ASSERT
            Entities(sfaPostcodeAreaCost).First().Parent.Should().BeNull();
        }

        /// <summary>
        /// Return Entity from EntityBuilder and check values
        /// </summary>
        [Fact(DisplayName = "EntityBuilder - SFA_PostcodeAreaCost Attributes Count Correct"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void EntityBuilder_SFA_PostcodeAreaCost_AttributesCountCorrect()
        {
            //ARRANGE
            // Use Test Helpers

            //ACT
            var dataEntity = SetupDataEntity().Single();
            var learnerEntity = dataEntity.Children.Single();
            var learningDeliveryEntity = learnerEntity.Children.Single();
            var sfaPostcodeAreaCost = learningDeliveryEntity.Children.Where(c => c.EntityName == "SFA_PostcodeAreaCost");

            //ASSERT
            Entities(sfaPostcodeAreaCost).First().Attributes.Count.Should().Be(3);
        }

        /// <summary>
        /// Return Entity from EntityBuilder and check values
        /// </summary>
        [Fact(DisplayName = "EntityBuilder - SFA_PostcodeAreaCost Attributes Correct"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void EntityBuilder_SFA_PostcodeAreaCost_AttributesCorrect()
        {
            //ARRANGE
            //Use Test Helpers

            //ACT
            var dataEntity = SetupDataEntity().Single();
            var learnerEntity = dataEntity.Children.Single();
            var learningDeliveryEntity = learnerEntity.Children.Single();
            var sfaPostcodeAreaCost = learningDeliveryEntity.Children.Where(c => c.EntityName == "SFA_PostcodeAreaCost");

            //ASSERT
            Entities(sfaPostcodeAreaCost).First().Attributes.Should().BeEquivalentTo(ExpectedLearningDeliverySFAPostcodeAreaCostAttributes());
        }
        
        /// <summary>
        /// Return Entity from EntityBuilder and check values
        /// </summary>
        [Fact(DisplayName = "EntityBuilder - LearningDeliveryLARS_Funding Entities Exists"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void EntityBuilder_LearningDeliveryLARS_Funding_Exists()
        {
            //ARRANGE
            // Use Test Helpers

            //ACT
            var dataEntity = SetupDataEntity().Single();
            var learnerEntity = dataEntity.Children.Single();
            var learningDeliveryEntity = learnerEntity.Children.Single();
            var learningDeliveryLARSFunding = learningDeliveryEntity.Children.Where(c => c.EntityName == "LearningDeliveryLARS_Funding");

            //ASSERT
            Entities(learningDeliveryLARSFunding).Should().NotBeNull();
        }

        /// <summary>
        /// Return Entity from EntityBuilder and check values
        /// </summary>
        [Fact(DisplayName = "EntityBuilder - LearningDeliveryLARS_Funding Entity Name Correct"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void EntityBuilder_LearningDeliveryLARS_Funding_EntityNameCorrect()
        {
            //ARRANGE
            // Use Test Helpers

            //ACT
            var dataEntity = SetupDataEntity().Single();
            var learnerEntity = dataEntity.Children.Single();
            var learningDeliveryEntity = learnerEntity.Children.Single();
            var learningDeliveryLARSFunding = learningDeliveryEntity.Children.Where(c => c.EntityName == "LearningDeliveryLARS_Funding");

            //ASSERT
            Entities(learningDeliveryLARSFunding).First().EntityName.Should().BeEquivalentTo("LearningDeliveryLARS_Funding");
        }

        /// <summary>
        /// Return Entity from EntityBuilder and check values
        /// </summary>
        [Fact(DisplayName = "EntityBuilder - LearningDeliveryLARS_Funding IsGlobal False"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void EntityBuilder_LearningDeliveryLARS_Funding_IsGlobalFalse()
        {
            //ARRANGE
            // Use Test Helpers

            //ACT
            var dataEntity = SetupDataEntity().Single();
            var learnerEntity = dataEntity.Children.Single();
            var learningDeliveryEntity = learnerEntity.Children.Single();
            var learningDeliveryLARSFunding = learningDeliveryEntity.Children.Where(c => c.EntityName == "LearningDeliveryLARS_Funding");

            //ASSERT
            Entities(learningDeliveryLARSFunding).First().IsGlobal.Should().BeFalse();
        }

        /// <summary>
        /// Return Entity from EntityBuilder and check values
        /// </summary>
        [Fact(DisplayName = "EntityBuilder - LearningDeliveryLARS_Funding Children Count Correct"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void EntityBuilder_LearningDeliveryLARS_Funding_ChildrenCountCorrect()
        {
            //ARRANGE
            // Use Test Helpers

            //ACT
            var dataEntity = SetupDataEntity().Single();
            var learnerEntity = dataEntity.Children.Single();
            var learningDeliveryEntity = learnerEntity.Children.Single();
            var learningDeliveryLARSFunding = learningDeliveryEntity.Children.Where(c => c.EntityName == "LearningDeliveryLARS_Funding");

            //ASSERT
            Entities(learningDeliveryLARSFunding).First().Children.Count.Should().Be(0);
        }

        /// <summary>
        /// Return Entity from EntityBuilder and check values
        /// </summary>
        [Fact(DisplayName = "EntityBuilder - LearningDeliveryLARS_Funding Children Correct"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void EntityBuilder_LearningDeliveryLARS_Funding_ChildrenCorrect()
        {
            //ARRANGE
            // Use Test Helpers

            //ACT
            var dataEntity = SetupDataEntity().Single();
            var learnerEntity = dataEntity.Children.Single();
            var learningDeliveryEntity = learnerEntity.Children.Single();
            var learningDeliveryLARSFunding = learningDeliveryEntity.Children.Where(c => c.EntityName == "LearningDeliveryLARS_Funding");

            //ASSERT
            Entities(learningDeliveryLARSFunding).First().Children.Should().BeNullOrEmpty();
        }

        /// <summary>
        /// Return Entity from EntityBuilder and check values
        /// </summary>
        [Fact(DisplayName = "EntityBuilder - LearningDeliveryLARS_Funding Parent Correct"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void EntityBuilder_LearningDeliveryLARS_Funding_ParentCorrect()
        {
            //ARRANGE
            // Use Test Helpers

            //ACT
            var dataEntity = SetupDataEntity().Single();
            var learnerEntity = dataEntity.Children.Single();
            var learningDeliveryEntity = learnerEntity.Children.Single();
            var learningDeliveryLARSFunding = learningDeliveryEntity.Children.Where(c => c.EntityName == "LearningDeliveryLARS_Funding");

            //ASSERT
            Entities(learningDeliveryLARSFunding).First().Parent.Should().BeNull();
        }

        /// <summary>
        /// Return Entity from EntityBuilder and check values
        /// </summary>
        [Fact(DisplayName = "EntityBuilder - LearningDeliveryLARS_Funding Attributes Count Correct"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void EntityBuilder_LearningDeliveryLARS_Funding_AttributesCountCorrect()
        {
            //ARRANGE
            // Use Test Helpers

            //ACT
            var dataEntity = SetupDataEntity().Single();
            var learnerEntity = dataEntity.Children.Single();
            var learningDeliveryEntity = learnerEntity.Children.Single();
            var learningDeliveryLARSFunding = learningDeliveryEntity.Children.Where(c => c.EntityName == "LearningDeliveryLARS_Funding");

            //ASSERT
            Entities(learningDeliveryLARSFunding).First().Attributes.Count.Should().Be(5);
        }

        /// <summary>
        /// Return Entity from EntityBuilder and check values
        /// </summary>
        [Fact(DisplayName = "EntityBuilder - LearningDeliveryLARS_Funding Attributes Correct"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void EntityBuilder_LearningDeliveryLARS_Funding_AttributesCorrect()
        {
            //ARRANGE
            //Use Test Helpers

            //ACT
            var dataEntity = SetupDataEntity().Single();
            var learnerEntity = dataEntity.Children.Single();
            var learningDeliveryEntity = learnerEntity.Children.Single();
            var learningDeliveryLARSFunding = learningDeliveryEntity.Children.Where(c => c.EntityName == "LearningDeliveryLARS_Funding");

            //ASSERT
            Entities(learningDeliveryLARSFunding).First().Attributes.Should().BeEquivalentTo(ExpectedLearningDeliveryLARSFundingAttributes());
        }

        #endregion

        #region Global Entity

        /// <summary>
        /// Return Global Entity from DataEntityBuilder and check values
        /// </summary>
        [Fact(DisplayName = "Global - Entity Exists"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void DataEntityBuilder_Global_Exists()
        {
            //ARRANGE
            // Use Test Helpers
            
            //ACT
            var globalEntity = SetupGlobalEntity();

            //ASSERT
            globalEntity.Should().NotBeNull();
        }

        /// <summary>
        /// Return Global Entity from DataEntityBuilder and check values
        /// </summary>
        [Fact(DisplayName = "Global - Entity Name Correct"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void DataEntityBuilder_Global_EntityNameCorrect()
        {
            //ARRANGE
            // Use Test Helpers

            //ACT
            var globalEntity = SetupGlobalEntity();

            //ASSERT
            globalEntity.EntityName.Should().Be("global");
        }

        /// <summary>
        /// Return Global Entity from DataEntityBuilder and check values
        /// </summary>
        [Fact(DisplayName = "Global - IsGlobal True"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void DataEntityBuilder_Global_IsGlobal()
        {
            //ARRANGE
            // Use Test Helpers

            //ACT
            var globalEntity = SetupGlobalEntity();

            //ASSERT
            globalEntity.IsGlobal.Should().BeTrue();
        }

        /// <summary>
        /// Return Global Entity from DataEntityBuilder and check values
        /// </summary>
        [Fact(DisplayName = "Global - Children Count Correct"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void DataEntityBuilder_Global_ChildrenCorrect()
        {
            //ARRANGE
            // Use Test Helpers

            //ACT
            var globalEntity = SetupGlobalEntity();

            //ASSERT
            globalEntity.Children.Count.Should().Be(0);
        }

        /// <summary>
        /// Return Global Entity from DataEntityBuilder and check values
        /// </summary>
        [Fact(DisplayName = "Global - Parent Correct"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void DataEntityBuilder_Global_ParentCorrect()
        {
            //ARRANGE
            // Use Test Helpers

            //ACT
            var globalEntity = SetupGlobalEntity();

            //ASSERT
            globalEntity.Parent.Should().BeNull();
        }

        /// <summary>
        /// Return Global Entity from DataEntityBuilder and check values
        /// </summary>
        [Fact(DisplayName = "Global - Entity Attributes Exist"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void DataEntityBuilder_Global_AttributesExist()
        {
            //ARRANGE
            // Use Test Helpers

            //ACT
            var globalEntity = SetupGlobalEntity();

            //ASSERT
            globalEntity.Attributes.Should().NotBeNull();
        }

        /// <summary>
        /// Return Global Entity from DataEntityBuilder and check values
        /// </summary>
        [Fact(DisplayName = "Global - Entity Attribute Count"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void DataEntityBuilder_Global_AttributeCount()
        {
            //ARRANGE
            // Use Test Helpers

            //ACT
            var globalEntity = SetupGlobalEntity();

            //ASSERT
            globalEntity.Attributes.Count.Should().Be(3);
        }

        /// <summary>
        /// Return Global Entity from DataEntityBuilder and check values
        /// </summary>
        [Fact(DisplayName = "Global - Entity Attributes Correct"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void DataEntityBuilder_Global_AttributesCorrect()
        {
            //ARRANGE
            var expectedAttributes = ExpectedGlobalAttributes();

            //ACT
            var globalEntity = SetupGlobalEntity();

            //ASSERT
            globalEntity.Attributes.Should().BeEquivalentTo(expectedAttributes);
        }

        #endregion

        #region Learner Entity

        /// <summary>
        /// Return Learner Entity from DataEntityBuilder and check values
        /// </summary>
        [Fact(DisplayName = "Learner - Entity Exists"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void DataEntityBuilder_Learner_Exists()
        {
            //ARRANGE
            // Use Test Helpers

            //ACT
            var learnerEntity = SetupLearnerEntity();

            //ASSERT
            learnerEntity.Should().NotBeNull();
        }

        /// <summary>
        /// Return Learner Entity from DataEntityBuilder and check values
        /// </summary>
        [Fact(DisplayName = "Learner - Entity Name Correct"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void DataEntityBuilder_Learner_EntityNameCorrect()
        {
            //ARRANGE
            // Use Test Helpers

            //ACT
            var learnerEntity = SetupLearnerEntity();

            //ASSERT
            learnerEntity.EntityName.Should().Be("Learner");
        }

        /// <summary>
        /// Return Learner Entity from DataEntityBuilder and check values
        /// </summary>
        [Fact(DisplayName = "Learner - IsGlobal Correct"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void DataEntityBuilder_Learner_IsGlobal()
        {
            //ARRANGE
            // Use Test Helpers

            //ACT
            var learnerEntity = SetupLearnerEntity();

            //ASSERT
            learnerEntity.IsGlobal.Should().BeFalse();
        }

        /// <summary>
        /// Return Learner Entity from DataEntityBuilder and check values
        /// </summary>
        [Fact(DisplayName = "Learner - Children Correct"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void DataEntityBuilder_Learner_ChildrenCorrect()
        {
            //ARRANGE
            // Use Test Helpers

            //ACT
            var learnerEntity = SetupLearnerEntity();

            //ASSERT
            learnerEntity.Children.Count.Should().Be(0);
        }

        /// <summary>
        /// Return Learner Entity from DataEntityBuilder and check values
        /// </summary>
        [Fact(DisplayName = "Learner - Parent Correct"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void DataEntityBuilder_Learner_ParentCorrect()
        {
            //ARRANGE
            // Use Test Helpers

            //ACT
            var learnerEntity = SetupLearnerEntity();

            //ASSERT
            learnerEntity.Parent.Should().BeNull();
        }

        /// <summary>
        /// Return Learner Entity from DataEntityBuilder and check values
        /// </summary>
        [Fact(DisplayName = "Learner - Attributes Exist"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void DataEntityBuilder_Learner_AttributesExist()
        {
            //ARRANGE
            // Use Test Helpers

            //ACT
            var learnerEntity = SetupLearnerEntity();

            //ASSERT
            learnerEntity.Attributes.Should().NotBeEmpty();
        }

        /// <summary>
        /// Return Learner Entity from DataEntityBuilder and check values
        /// </summary>
        [Fact(DisplayName = "Learner - Attributes Count Correct"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void DataEntityBuilder_Learner_AttributesCountCorrect()
        {
            //ARRANGE
            // Use Test Helpers

            //ACT
            var learnerEntity = SetupLearnerEntity();

            //ASSERT
            learnerEntity.Attributes.Count.Should().Be(1);
        }

        /// <summary>
        /// Return Learner Entity from DataEntityBuilder and check values
        /// </summary>
        [Fact(DisplayName = "Learner - Attributes Correct"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void DataEntityBuilder_Learner_AttributesCorrect()
        {
            //ARRANGE
            var expectedAttributes = ExpectedLearnerAttributes();

            //ACT
            var learnerEntity = SetupLearnerEntity();

            //ASSERT
            learnerEntity.Attributes.Should().BeEquivalentTo(expectedAttributes);
        }

        #endregion

        #region LearningDelivery Entity

        /// <summary>
        /// Return LearningDelivery Entity from DataEntityBuilder and check values
        /// </summary>
        [Fact(DisplayName = "LearningDelivery - Entity Exists"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void DataEntityBuilder_LearningDelivery_Exists()
        {
            // ARRANGE
            // Use Test Helpers

            //ACT
            var learningDeliveryEntity = SetupLearningDeliveryEntity();

            //ASSERT
            learningDeliveryEntity.Should().NotBeNull();
        }

        /// <summary>
        /// Return LearningDelivery Entity from DataEntityBuilder and check values
        /// </summary>
        [Fact(DisplayName = "LearningDelivery - Entity Name Correct"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void DataEntityBuilder_LearningDelivery_EntityNameCorrect()
        {
            // ARRANGE
            // Use Test Helpers

            //ACT
            var learningDeliveryEntity = SetupLearningDeliveryEntity();

            //ASSERT
            learningDeliveryEntity.EntityName.Should().Be("LearningDelivery");
        }

        /// <summary>
        /// Return LearningDelivery Entity from DataEntityBuilder and check values
        /// </summary>
        [Fact(DisplayName = "LearningDelivery - IsGlobal False"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void DataEntityBuilder_LearningDelivery_IsGlobalFalse()
        {
            // ARRANGE
            // Use Test Helpers

            //ACT
            var learningDeliveryEntity = SetupLearningDeliveryEntity();

            //ASSERT
            learningDeliveryEntity.IsGlobal.Should().BeFalse();
        }

        /// <summary>
        /// Return LearningDelivery Entity from DataEntityBuilder and check values
        /// </summary>
        [Fact(DisplayName = "LearningDelivery - Children Count Correct"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void DataEntityBuilder_LearningDelivery_ChildrenCountCorrect()
        {
            // ARRANGE
            // Use Test Helpers

            //ACT
            var learningDeliveryEntity = SetupLearningDeliveryEntity();

            //ASSERT
            learningDeliveryEntity.Children.Count.Should().Be(0);
        }

        /// <summary>
        /// Return LearningDelivery Entity from DataEntityBuilder and check values
        /// </summary>
        [Fact(DisplayName = "LearningDelivery - Parent Correct"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void DataEntityBuilder_LearningDelivery_ParentCorrect()
        {
            // ARRANGE
            // Use Test Helpers

            //ACT
            var learningDeliveryEntity = SetupLearningDeliveryEntity();

            //ASSERT
            learningDeliveryEntity.Parent.Should().BeNull();
        }

        /// <summary>
        /// Return LearningDelivery Entity from DataEntityBuilder and check values
        /// </summary>
        [Fact(DisplayName = "LearningDelivery - Attributes Exist"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void DataEntityBuilder_LearningDelivery_AttributesExist()
        {
            // ARRANGE
            // Use Test Helpers

            //ACT
            var learningDeliveryEntity = SetupLearningDeliveryEntity();

            //ASSERT
            learningDeliveryEntity.Attributes.Should().NotBeEmpty();
        }

        /// <summary>
        /// Return LearningDelivery Entity from DataEntityBuilder and check values
        /// </summary>
        [Fact(DisplayName = "LearningDelivery - Attributes Count Correct"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void DataEntityBuilder_LearningDelivery_AttributesCountCorrect()
        {
            // ARRANGE
            // Use Test Helpers

            //ACT
            var learningDeliveryEntity = SetupLearningDeliveryEntity();

            //ASSERT
            learningDeliveryEntity.Attributes.Count.Should().Be(14);
        }

        /// <summary>
        /// Return LearningDelivery Entity from DataEntityBuilder and check values
        /// </summary>
        [Fact(DisplayName = "LearningDelivery - Attributes Correct"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void DataEntityBuilder_LearningDelivery_AttributesCorrect()
        {
            // ARRANGE
            var expectedLearningDelivery = ExpectedLearningDeliveryAttributes();

            //ACT
            var learningDeliveryEntity = SetupLearningDeliveryEntity();

            //ASSERT
            learningDeliveryEntity.Attributes.Should().BeEquivalentTo(expectedLearningDelivery);
        }

        #endregion

        #region LearningDeliveryFAM Entity

        /// <summary>
        /// Return LearningDeliveryFAM Entity from DataEntityBuilder and check values
        /// </summary>
        [Fact(DisplayName = "LearningDeliveryFAM - Entity Exists"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void DataEntityBuilder_LearningDeliveryFAM_Exists()
        {
            // ARRANGE
            // Use Test Helpers

            //ACT
            var learningDeliveryFAMEntity = SetupLearningDeliveryFAMEntity();

            //ASSERT
            learningDeliveryFAMEntity.Should().NotBeNull();
        }

        /// <summary>
        /// Return LearningDeliveryFAM Entity from DataEntityBuilder and check values
        /// </summary>
        [Fact(DisplayName = "LearningDeliveryFAM - Entity Name Correct"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void DataEntityBuilder_LearningDeliveryFAM_EntityNameCorrect()
        {
            // ARRANGE
            // Use Test Helpers

            //ACT
            var learningDeliveryFAMEntity = SetupLearningDeliveryFAMEntity();

            //ASSERT
            learningDeliveryFAMEntity.EntityName.Should().Be("LearningDeliveryFAM");
        }

        /// <summary>
        /// Return LearningDeliveryFAM Entity from DataEntityBuilder and check values
        /// </summary>
        [Fact(DisplayName = "LearningDeliveryFAM - IsGlobal False"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void DataEntityBuilder_LearningDeliveryFAM_IsGlobalFalse()
        {
            // ARRANGE
            // Use Test Helpers

            //ACT
            var learningDeliveryFAMEntity = SetupLearningDeliveryFAMEntity();

            //ASSERT
            learningDeliveryFAMEntity.IsGlobal.Should().BeFalse();
        }

        /// <summary>
        /// Return LearningDeliveryFAM Entity from DataEntityBuilder and check values
        /// </summary>
        [Fact(DisplayName = "LearningDeliveryFAM - Children Count Correct"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void DataEntityBuilder_LearningDeliveryFAM_ChildrenCountCorrect()
        {
            // ARRANGE
            // Use Test Helpers

            //ACT
            var learningDeliveryFAMEntity = SetupLearningDeliveryFAMEntity();

            //ASSERT
            learningDeliveryFAMEntity.Children.Count.Should().Be(0);
        }

        /// <summary>
        /// Return LearningDeliveryFAM Entity from DataEntityBuilder and check values
        /// </summary>
        [Fact(DisplayName = "LearningDeliveryFAM - Parent Correct"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void DataEntityBuilder_LearningDeliveryFAM_ParentCorrect()
        {
            // ARRANGE
            // Use Test Helpers

            //ACT
            var learningDeliveryFAMEntity = SetupLearningDeliveryFAMEntity();

            //ASSERT
            learningDeliveryFAMEntity.Parent.Should().BeNull();
        }

        /// <summary>
        /// Return LearningDeliveryFAM Entity from DataEntityBuilder and check values
        /// </summary>
        [Fact(DisplayName = "LearningDeliveryFAM - Attributes Exist"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void DataEntityBuilder_LearningDeliveryFAM_AttributesExist()
        {
            // ARRANGE
            // Use Test Helpers

            //ACT
            var learningDeliveryFAMEntity = SetupLearningDeliveryFAMEntity();

            //ASSERT
            learningDeliveryFAMEntity.Attributes.Should().NotBeEmpty();
        }

        /// <summary>
        /// Return LearningDeliveryFAM Entity from DataEntityBuilder and check values
        /// </summary>
        [Fact(DisplayName = "LearningDeliveryFAM - Attributes Count Correct"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void DataEntityBuilder_LearningDeliveryFAM_AttributesCountCorrect()
        {
            // ARRANGE
            // Use Test Helpers

            //ACT
            var learningDeliveryFAMEntity = SetupLearningDeliveryFAMEntity();

            //ASSERT
            learningDeliveryFAMEntity.Attributes.Count.Should().Be(4);
        }

        /// <summary>
        /// Return LearningDeliveryFAM Entity from DataEntityBuilder and check values
        /// </summary>
        [Fact(DisplayName = "LearningDeliveryFAM - Attributes Correct"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void DataEntityBuilder_LearningDeliveryFAM_AttributesCorrect()
        {
            // ARRANGE
            var expectedLearningDeliveryFAM = ExpectedLearningDeliveryFAMAttributes();

            //ACT
            var learningDeliveryFAMEntity = SetupLearningDeliveryFAMEntity();

            //ASSERT
            learningDeliveryFAMEntity.Attributes.Should().BeEquivalentTo(expectedLearningDeliveryFAM);
        }

        #endregion

        #region LearningDeliverySFAPostcodeAreaCost Entity

        /// <summary>
        /// Return LearningDeliverySFAPostcodeAreaCost Entity from DataEntityBuilder and check values
        /// </summary>
        [Fact(DisplayName = "LearningDeliverySFAPostcodeAreaCost - Entity Exists"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void DataEntityBuilder_LearningDeliverySFAPostcodeAreaCost_EntityExists()
        {
            // ARRANGE
            //Use Test Helpers

            //ACT
            var learningDeliverySFAPostcodeAreaCostEntity = SetupLearningDeliverySFAPostcodeAreaCostEntity();

            //ASSERT
            learningDeliverySFAPostcodeAreaCostEntity.Should().NotBeNull();
        }

        /// <summary>
        /// Return LearningDeliverySFAPostcodeAreaCost Entity from DataEntityBuilder and check values
        /// </summary>
        [Fact(DisplayName = "LearningDeliverySFAPostcodeAreaCost - Entity Name Correct"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void DataEntityBuilder_LearningDeliverySFAPostcodeAreaCost_EntityNameCorrect()
        {
            // ARRANGE
            // Use Test Helpers

            //ACT
            var learningDeliverySFAPostcodeAreaCostEntity = SetupLearningDeliverySFAPostcodeAreaCostEntity();

            //ASSERT
            learningDeliverySFAPostcodeAreaCostEntity.EntityName.Should().Be("SFA_PostcodeAreaCost");
        }

        /// <summary>
        /// Return LearningDeliverySFAPostcodeAreaCost Entity from DataEntityBuilder and check values
        /// /// </summary>
        [Fact(DisplayName = "LearningDeliverySFAPostcodeAreaCost - IsGlobal False"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void DataEntityBuilder_LearningDeliverySFAPostcodeAreaCost_IsGlobalFalse()
        {
            // ARRANGE
            // Use Test Helpers

            //ACT
            var learningDeliverySFAPostcodeAreaCostEntity = SetupLearningDeliverySFAPostcodeAreaCostEntity();

            //ASSERT
            learningDeliverySFAPostcodeAreaCostEntity.IsGlobal.Should().BeFalse();
        }

        /// <summary>
        /// Return LearningDeliverySFAPostcodeAreaCost Entity from DataEntityBuilder and check values
        /// /// </summary>
        [Fact(DisplayName = "LearningDeliverySFAPostcodeAreaCost - Children Count Correct"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void DataEntityBuilder_LearningDeliverySFAPostcodeAreaCost_ChildrenCountCorrect()
        {
            // ARRANGE
            // Use Test Helpers

            //ACT
            var learningDeliverySFAPostcodeAreaCostEntity = SetupLearningDeliverySFAPostcodeAreaCostEntity();

            //ASSERT
            learningDeliverySFAPostcodeAreaCostEntity.Children.Count.Should().Be(0);
        }

        /// <summary>
        /// Return LearningDeliverySFAPostcodeAreaCost Entity from DataEntityBuilder and check values
        /// /// </summary>
        [Fact(DisplayName = "LearningDeliverySFAPostcodeAreaCost - Parent Correct"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void DataEntityBuilder_LearningDeliverySFAPostcodeAreaCost_ParentCorrect()
        {
            // ARRANGE
            // Use Test Helpers

            //ACT
            var learningDeliverySFAPostcodeAreaCostEntity = SetupLearningDeliverySFAPostcodeAreaCostEntity();

            //ASSERT
            learningDeliverySFAPostcodeAreaCostEntity.Parent.Should().BeNull();
        }

        /// <summary>
        /// Return LearningDeliverySFAPostcodeAreaCost Entity from DataEntityBuilder and check values
        /// /// </summary>
        [Fact(DisplayName = "LearningDeliverySFAPostcodeAreaCost - Attributes Exist"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void DataEntityBuilder_LearningDeliverySFAPostcodeAreaCost_AttributesExist()
        {
            // ARRANGE
            // Use Test Helpers

            //ACT
            var learningDeliverySFAPostcodeAreaCostEntity = SetupLearningDeliverySFAPostcodeAreaCostEntity();

            //ASSERT
            learningDeliverySFAPostcodeAreaCostEntity.Attributes.Should().NotBeEmpty();
        }

        /// <summary>
        /// Return LearningDeliverySFAPostcodeAreaCost Entity from DataEntityBuilder and check values
        /// /// </summary>
        [Fact(DisplayName = "LearningDeliverySFAPostcodeAreaCost - Attributes Count Correct"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void DataEntityBuilder_LearningDeliverySFAPostcodeAreaCost_AttributesCountCorrect()
        {
            // ARRANGE
            // Use Test Helpers

            //ACT
            var learningDeliverySFAPostcodeAreaCostEntity = SetupLearningDeliverySFAPostcodeAreaCostEntity();

            //ASSERT
            learningDeliverySFAPostcodeAreaCostEntity.Attributes.Count.Should().Be(3);
        }

        /// <summary>
        /// Return LearningDeliverySFAPostcodeAreaCost Entity from DataEntityBuilder and check values
        /// /// </summary>
        [Fact(DisplayName = "LearningDeliverySFAPostcodeAreaCost - Attributes Correct"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void DataEntityBuilder_LearningDeliverySFAPostcodeAreaCost_AttributesCorrect()
        {
            // ARRANGE
            var expectedLearningDeliverySFAPostcodeAreaCost = ExpectedLearningDeliverySFAPostcodeAreaCostAttributes();

            //ACT
            var learningDeliverySFAPostcodeAreaCostEntity = SetupLearningDeliverySFAPostcodeAreaCostEntity();

            //ASSERT
            learningDeliverySFAPostcodeAreaCostEntity.Attributes.Should().BeEquivalentTo(expectedLearningDeliverySFAPostcodeAreaCost);
        }

        #endregion

        #region LearningDeliveryLARSFunding Entity

        /// <summary>
        /// Return LearningDeliveryLARSFunding Entity from DataEntityBuilder and check values
        /// </summary>
        [Fact(DisplayName = "LearningDeliveryLARSFunding - Entity Exists"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void DataEntityBuilder_LearningDeliveryLARSFunding_EntityExists()
        {
            // ARRANGE
            //Use Test Helpers

            //ACT
            var learningDeliveryLARSFundingEntity = SetupLearningDeliveryLARSFundingEntity();

            //ASSERT
            learningDeliveryLARSFundingEntity.Should().NotBeNull();
        }

        /// <summary>
        /// Return LearningDeliveryLARSFunding Entity from DataEntityBuilder and check values
        /// </summary>
        [Fact(DisplayName = "LearningDeliveryLARSFunding - Entity Name Correct"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void DataEntityBuilder_LearningDeliveryLARSFunding_EntityNameCorrect()
        {
            // ARRANGE
            // Use Test Helpers

            //ACT
            var learningDeliveryLARSFundingEntity = SetupLearningDeliveryLARSFundingEntity();

            //ASSERT
            learningDeliveryLARSFundingEntity.EntityName.Should().Be("LearningDeliveryLARS_Funding");
        }

        /// <summary>
        /// Return LearningDeliveryLARSFunding Entity from DataEntityBuilder and check values
        /// /// </summary>
        [Fact(DisplayName = "LearningDeliveryLARSFunding - IsGlobal False"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void DataEntityBuilder_LearningDeliveryLARSFunding_IsGlobalFalse()
        {
            // ARRANGE
            // Use Test Helpers

            //ACT
            var learningDeliveryLARSFundingEntity = SetupLearningDeliveryLARSFundingEntity();

            //ASSERT
            learningDeliveryLARSFundingEntity.IsGlobal.Should().BeFalse();
        }

        /// <summary>
        /// Return LearningDeliverySFAPostcodeAreaCost Entity from DataEntityBuilder and check values
        /// /// </summary>
        [Fact(DisplayName = "LearningDeliverySFAPostcodeAreaCost - Children Count Correct"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void DataEntityBuilder_LearningDeliveryLARSFunding_ChildrenCountCorrect()
        {
            // ARRANGE
            // Use Test Helpers

            //ACT
            var learningDeliveryLARSFundingEntity = SetupLearningDeliveryLARSFundingEntity();

            //ASSERT
            learningDeliveryLARSFundingEntity.Children.Count.Should().Be(0);
        }

        /// <summary>
        /// Return LearningDeliveryLARSFunding Entity from DataEntityBuilder and check values
        /// /// </summary>
        [Fact(DisplayName = "LearningDeliveryLARSFunding - Parent Correct"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void DataEntityBuilder_LearningDeliveryLARSFunding_ParentCorrect()
        {
            // ARRANGE
            // Use Test Helpers

            //ACT
            var learningDeliveryLARSFundingEntity = SetupLearningDeliveryLARSFundingEntity();

            //ASSERT
            learningDeliveryLARSFundingEntity.Parent.Should().BeNull();
        }

        /// <summary>
        /// Return LearningDeliveryLARSFunding Entity from DataEntityBuilder and check values
        /// /// </summary>
        [Fact(DisplayName = "LearningDeliveryLARSFunding - Attributes Exist"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void DataEntityBuilder_LearningDeliveryLARSFunding_AttributesExist()
        {
            // ARRANGE
            // Use Test Helpers

            //ACT
            var learningDeliveryLARSFundingEntity = SetupLearningDeliveryLARSFundingEntity();

            //ASSERT
            learningDeliveryLARSFundingEntity.Attributes.Should().NotBeEmpty();
        }

        /// <summary>
        /// Return LearningDeliveryLARSFunding Entity from DataEntityBuilder and check values
        /// /// </summary>
        [Fact(DisplayName = "LearningDeliveryLARSFunding - Attributes Count Correct"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void DataEntityBuilder_LearningDeliveryLARSFunding_AttributesCountCorrect()
        {
            // ARRANGE
            // Use Test Helpers

            //ACT
            var learningDeliveryLARSFundingEntity = SetupLearningDeliveryLARSFundingEntity();

            //ASSERT
            learningDeliveryLARSFundingEntity.Attributes.Count.Should().Be(5);
        }

        /// <summary>
        /// Return LearningDeliveryLARSFunding Entity from DataEntityBuilder and check values
        /// /// </summary>
        [Fact(DisplayName = "LearningDeliveryLARSFunding - Attributes Correct"), Trait("Funding Service DataEntity Builders", "Unit")]
        public void DataEntityBuilder_LearningDeliveryLARSFunding_AttributesCorrect()
        {
            // ARRANGE
            var expectedLearningDeliveryLARSFunding = ExpectedLearningDeliveryLARSFundingAttributes();

            //ACT
            var learningDeliveryLARSFundingEntity = SetupLearningDeliveryLARSFundingEntity();

            //ASSERT
            learningDeliveryLARSFundingEntity.Attributes.Should().BeEquivalentTo(expectedLearningDeliveryLARSFunding);
        }

        #endregion

        #region Test Helpers
        
        private IDataEntity Entity(IDataEntity entity)
        {
            return entity;
        }

        private IEnumerable<IDataEntity> Entities(IEnumerable<IDataEntity> entities)
        {
            return entities;
        }

        #region Expected Results

        private IDictionary<string, IAttributeData> ExpectedGlobalAttributes()
        {
           return new Dictionary<string, IAttributeData>
           {
                {"UKPRN", new AttributeData("UKPRN", 12345678)},
                {"LARSVersion", new AttributeData("LARSVersion", "Version_005")},
                {"PostcodeAreaCostVersion", new AttributeData("PostcodeAreaCostVersion", "Version_003")}
            };
        }

        private IDictionary<string, IAttributeData> ExpectedLearnerAttributes()
        {
            return new Dictionary<string, IAttributeData>
            {
                {"LearnRefNumber", new AttributeData("LearnRefNumber", "Learner1")}
            };
        }

        private IDictionary<string, IAttributeData> ExpectedLearningDeliveryAttributes()
        {
            return new Dictionary<string, IAttributeData>
            {
                {"AimSeqNumber", new AttributeData("AimSeqNumber", 1)},
                {"CompStatus", new AttributeData("CompStatus", 1)},
                {"LearnActEndDate", new AttributeData("LearnActEndDate", DateTime.Parse("2018-06-30"))},
                {"LearnAimRefType", new AttributeData("LearnAimRefType", "032")},
                {"LearnPlanEndDate", new AttributeData("LearnPlanEndDate", DateTime.Parse("2018-07-30"))},
                {"LearnStartDate", new AttributeData("LearnStartDate", DateTime.Parse("2017-08-30"))},
                {"LrnDelFAM_ADL", new AttributeData("LrnDelFAM_ADL", "1")},
                {"LrnDelFAM_RES", new AttributeData("LrnDelFAM_RES", "1")},
                {"NotionalNVQLevelv2", new AttributeData("NotionalNVQLevelv2", "100")},
                {"OrigLearnStartDate", new AttributeData("OrigLearnStartDate", DateTime.Parse("2017-08-30"))},
                {"OtherFundAdj", new AttributeData("OtherFundAdj", null)},
                {"Outcome", new AttributeData("Outcome", null)},
                {"PriorLearnFundAdj", new AttributeData("PriorLearnFundAdj", null)},
                {"RegulatedCreditValue", new AttributeData("RegulatedCreditValue", 180)}
            };
        }

        private IDictionary<string, IAttributeData> ExpectedLearningDeliveryFAMAttributes()
        {
            return new Dictionary<string, IAttributeData>
            {
                { "LearnDelFAMCode",new AttributeData("LearnDelFAMCode", "1") },
                { "LearnDelFAMDateFrom",new AttributeData("LearnDelFAMDateFrom", DateTime.Parse("2017-08-30")) },
                { "LearnDelFAMDateTo",new AttributeData("LearnDelFAMDateTo", DateTime.Parse("2017-10-31")) },
                { "LearnDelFAMType",new AttributeData("LearnDelFAMType", "ADL") }
            };
        }

        private IDictionary<string, IAttributeData> ExpectedLearningDeliverySFAPostcodeAreaCostAttributes()
        {
            return new Dictionary<string, IAttributeData>
            {
                { "AreaCosEffectiveFrom",new AttributeData("AreaCosEffectiveFrom", DateTime.Parse("2000-08-30")) },
                { "AreaCosEffectiveTo",new AttributeData("AreaCosEffectiveTo", null) },
                { "AreaCosFactor",new AttributeData("AreaCosFactor", 1.2m) }
            };
        }

        private IDictionary<string, IAttributeData> ExpectedLearningDeliveryLARSFundingAttributes()
        {
            return new Dictionary<string, IAttributeData>
            {
                { "LARSFundCategory",new AttributeData("LARSFundCategory", "Matrix") },
                { "LARSFundEffectiveFrom",new AttributeData("LARSFundEffectiveFrom",  DateTime.Parse("2010-08-30")) },
                { "LARSFundEffectiveTo",new AttributeData("LARSFundEffectiveTo", null) },
                { "LARSFundWeightedRate",new AttributeData("LARSFundWeightedRate", 12345m) },
                { "LARSFundWeightingFactor",new AttributeData("LARSFundWeightingFactor", "B") }
            };
        }

        #endregion

        #region Test Set Up

        private IReferenceDataCache SetupReferenceDataMock()
        {
            return Mock.Of<IReferenceDataCache>(l => 
                l.LARSCurrentVersion == "Version_005"
                && l.PostcodeCurrentVersion == "Version_003"
                && l.LARSLearningDelivery == new Dictionary<string, LARSLearningDelivery>
                {
                    { "123456", new LARSLearningDelivery
                        {
                            LearnAimRef = "123456",
                            LearnAimRefType = "032",
                            NotionalNVQLevelv2 = "100",
                            RegulatedCreditValue = 180
                        }
                    }
                }
                && l.SfaAreaCost == new Dictionary<string, IEnumerable<SfaAreaCost>>
                {
                    { "CV1 2WT", new List<SfaAreaCost>
                        {
                            new SfaAreaCost
                            {
                                Postcode = "CV1 2WT",
                                EffectiveFrom = DateTime.Parse("2000-08-30"),
                                AreaCostFactor = 1.2m
                            }
                        }
                    }
                }
                && l.LARSFunding == new Dictionary<string, IEnumerable<LARSFunding>>
                {
                    {
                        "123456", new List<LARSFunding>
                        {
                            new LARSFunding
                            {
                                LearnAimRef = "123456",
                                EffectiveFrom =  DateTime.Parse("2010-08-30"),
                                WeightingFactor = "B",
                                RateWeighted = 12345m,
                                FundingCategory = "Matrix"
                            }
                        }
                    }
                }
                );
        }

        private IEnumerable<IDataEntity> SetupDataEntity()
        {
            var referenceDataCacheMock = SetupReferenceDataMock();
            IAttributeBuilder<IAttributeData> attributeBuilder = new AttributeBuilder();
            var dataEntityBuilder = new DataEntityBuilder(referenceDataCacheMock, attributeBuilder);

            return dataEntityBuilder.EntityBuilder(12345678, TestLearner);
        }

        private IDataEntity SetupGlobalEntity()
        {
            var referenceDataCacheMock = SetupReferenceDataMock();
            IAttributeBuilder<IAttributeData> attributeBuilder = new AttributeBuilder();
            var globalBuilder = new DataEntityBuilder(referenceDataCacheMock, attributeBuilder);

            return globalBuilder.GlobalEntity(12345678);
        }

        private IDataEntity SetupLearnerEntity()
        {
            var referenceDataCacheMock = SetupReferenceDataMock();
            IAttributeBuilder<IAttributeData> attributeBuilder = new AttributeBuilder();
            var learnerBuilder = new DataEntityBuilder(referenceDataCacheMock, attributeBuilder);

            return learnerBuilder.LearnerEntity("Learner1");
        }

        private IDataEntity SetupLearningDeliveryEntity()
        {
            var referenceDataCacheMock = SetupReferenceDataMock();
            IAttributeBuilder<IAttributeData> attributeBuilder = new AttributeBuilder();
            var learningDeilveryBuilder = new DataEntityBuilder(referenceDataCacheMock, attributeBuilder);

            LARSLearningDelivery larsLearningDelivery =
                referenceDataCacheMock.LARSLearningDelivery.Select(lars => lars.Value).SingleOrDefault(); 

            return learningDeilveryBuilder.LearningDeliveryEntity(TestLearningDelivery, larsLearningDelivery);
        }

        private IDataEntity SetupLearningDeliveryFAMEntity()
        {
            var referenceDataCacheMock = SetupReferenceDataMock();
            IAttributeBuilder<IAttributeData> attributeBuilder = new AttributeBuilder();
            var learningDeilveryFAMBuilder = new DataEntityBuilder(referenceDataCacheMock, attributeBuilder);
            
            return learningDeilveryFAMBuilder.LearningDeliveryFAMEntity(TestLearningDeliveryFAM);
        }

        private IDataEntity SetupLearningDeliverySFAPostcodeAreaCostEntity()
        {
            var referenceDataCacheMock = SetupReferenceDataMock();
            IAttributeBuilder<IAttributeData> attributeBuilder = new AttributeBuilder();
            var learningDeilverySFAAreaCostBuilder = new DataEntityBuilder(referenceDataCacheMock, attributeBuilder);

            IEnumerable<SfaAreaCost> SFAAreaCost = referenceDataCacheMock.SfaAreaCost.Select(s => s.Value).Single();

            return learningDeilverySFAAreaCostBuilder.SFAAreaCostEntity(SFAAreaCost.FirstOrDefault());
        }

        private IDataEntity SetupLearningDeliveryLARSFundingEntity()
        {
            var referenceDataCacheMock = SetupReferenceDataMock();
            IAttributeBuilder<IAttributeData> attributeBuilder = new AttributeBuilder();
            var learningDeilveryLARSFundingBuilder = new DataEntityBuilder(referenceDataCacheMock, attributeBuilder);

            IEnumerable<LARSFunding> LARSFunding = referenceDataCacheMock.LARSFunding.Select(l => l.Value).Single();

            return learningDeilveryLARSFundingBuilder.LARSFundingEntity(LARSFunding.Single());
        }

        #endregion

        #region Test Input Data

        private static readonly ILearningDelivery TestLearningDelivery = new MessageLearnerLearningDelivery
        {
            LearnAimRef = "123456",
            AimSeqNumber = 1,
            CompStatus = 1,
            DelLocPostCode = "CV1 2WT",
            LearnActEndDateSpecified = true,
            LearnActEndDate = DateTime.Parse("2018-06-30"),
            LearnStartDate = DateTime.Parse("2017-08-30"),
            LearnPlanEndDate = DateTime.Parse("2018-07-30"),
            OrigLearnStartDateSpecified = true,
            OrigLearnStartDate = DateTime.Parse("2017-08-30"),
            OtherFundAdjSpecified = false,
            OutcomeSpecified = false,
            PriorLearnFundAdjSpecified = false,
            LearningDeliveryFAM = new[]
            {
                new MessageLearnerLearningDeliveryLearningDeliveryFAM
                {
                    LearnDelFAMCode = "100",
                    LearnDelFAMType = "SOF",
                    LearnDelFAMDateFromSpecified = true,
                    LearnDelFAMDateFrom = DateTime.Parse("2017-08-30"),
                    LearnDelFAMDateToSpecified = true,
                    LearnDelFAMDateTo =  DateTime.Parse("2017-10-30")
                },
                new MessageLearnerLearningDeliveryLearningDeliveryFAM
                {
                    LearnDelFAMCode = "1",
                    LearnDelFAMType = "ADL",
                    LearnDelFAMDateFromSpecified = true,
                    LearnDelFAMDateFrom = DateTime.Parse("2017-10-31"),
                    LearnDelFAMDateToSpecified = true,
                    LearnDelFAMDateTo =  DateTime.Parse("2018-11-30")
                },
                new MessageLearnerLearningDeliveryLearningDeliveryFAM
                {
                    LearnDelFAMCode = "1",
                    LearnDelFAMType = "RES",
                    LearnDelFAMDateFromSpecified = true,
                    LearnDelFAMDateFrom = DateTime.Parse("2017-12-01"),
                    LearnDelFAMDateToSpecified = false
                }
            }
        };

        private readonly ILearningDeliveryFAM TestLearningDeliveryFAM = new MessageLearnerLearningDeliveryLearningDeliveryFAM
        {
            LearnDelFAMCode = "1",
            LearnDelFAMType = "ADL",
            LearnDelFAMDateFromSpecified = true,
            LearnDelFAMDateFrom = DateTime.Parse("2017-08-30"),
            LearnDelFAMDateToSpecified = true,
            LearnDelFAMDateTo = DateTime.Parse("2017-10-31")
        };


        private readonly IEnumerable<ILearner> TestLearner = new[] 
        {
            new MessageLearner
            {           
                LearnRefNumber = "Learner1",
                LearningDelivery = new[]
                {
                    new MessageLearnerLearningDelivery
                    {
                        LearnAimRef = "123456",
                        AimSeqNumber = 1,
                        CompStatus = 1,
                        DelLocPostCode = "CV1 2WT",
                        LearnActEndDateSpecified = true,
                        LearnActEndDate = DateTime.Parse("2018-06-30"),
                        LearnStartDate = DateTime.Parse("2017-08-30"),
                        LearnPlanEndDate = DateTime.Parse("2018-07-30"),
                        OrigLearnStartDateSpecified = true,
                        OrigLearnStartDate = DateTime.Parse("2017-08-30"),
                        OtherFundAdjSpecified = false,
                        OutcomeSpecified = false,
                        PriorLearnFundAdjSpecified = false,
                        LearningDeliveryFAM = new[]
                        {
                            new MessageLearnerLearningDeliveryLearningDeliveryFAM
                            {
                                LearnDelFAMCode = "1",
                                LearnDelFAMType = "ADL",
                                LearnDelFAMDateFromSpecified = true,
                                LearnDelFAMDateFrom = DateTime.Parse("2017-08-30"),
                                LearnDelFAMDateToSpecified = true,
                                LearnDelFAMDateTo =  DateTime.Parse("2017-10-31")
                               
                            },
                            new MessageLearnerLearningDeliveryLearningDeliveryFAM
                            {
                                LearnDelFAMCode = "100",
                                LearnDelFAMType = "SOF",
                                LearnDelFAMDateFromSpecified = true,
                                LearnDelFAMDateFrom = DateTime.Parse("2017-10-31"),
                                LearnDelFAMDateToSpecified = true,
                                LearnDelFAMDateTo =  DateTime.Parse("2017-11-30")
                            },
                            new MessageLearnerLearningDeliveryLearningDeliveryFAM
                            {
                                LearnDelFAMCode = "1",
                                LearnDelFAMType = "RES",
                                LearnDelFAMDateFromSpecified = true,
                                LearnDelFAMDateFrom = DateTime.Parse("2017-12-01"),
                                LearnDelFAMDateToSpecified = false
                            }
                        }
                    }
                }
            }
        };
        
        #endregion
       
        #endregion
    }
}
