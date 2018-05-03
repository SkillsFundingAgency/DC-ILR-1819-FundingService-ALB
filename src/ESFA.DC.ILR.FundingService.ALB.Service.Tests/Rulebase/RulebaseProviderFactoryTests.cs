using ESFA.DC.ILR.FundingService.ALB.Service.Rulebase;
using Xunit;
using FluentAssertions;
using ESFA.DC.OPA.Service.Interface.Rulebase;
using ESFA.DC.OPA.Service.Rulebase;
using System.Reflection;
using System.IO;

namespace ESFA.DC.ILR.FundingService.ALB.Service.Tests.Rulebase
{
    public class RulebaseProviderFactoryTests
    {
        /// <summary>
        /// Return RulebaseProviderFactory from the Funding Service
        /// </summary>
        [Fact(DisplayName = "RulebaseProviderFactory - Instance Exists"), Trait("Funding Service", "Unit")]
        public void RulebaseProviderFactory_InstanceExists()
        {
            //ARRANGE
            // Use Test Helpers

            //ACT
            
            //ASSERT
            BuildRulebaseProviderFactory().Should().NotBeNull();
        }

        /// <summary>
        /// Return RulebaseProviderFactory from the Funding Service
        /// </summary>
        [Fact(DisplayName = "RulebaseProviderFactory - Return Types Correct"), Trait("Funding Service", "Unit")]
        public void RulebaseProviderFactory_ReturnTypesCorrect()
        {
            //ARRANGE
            // Use Test Helpers

            //ACT

            //ASSERT
            BuildRulebaseProviderFactory().Should().BeOfType<RulebaseProvider>();
        }

        /// <summary>
        /// Return RulebaseProviderFactory from the Funding Service
        /// </summary>
        [Fact(DisplayName = "RulebaseProviderFactory - RulebaseProvider Stream Exists"), Trait("Funding Service", "Unit")]
        public void RulebaseProviderFactory_InstanceNotCorrect()
        {
            //ARRANGE
            var factory = BuildTestRulebaseProviderFactory("ESFA.DC.ILR.FundingService.ALB.Service.Tests.Rulebase.Loans Bursary 17_18.zip");

            //ACT
            var stream = factory.GetStream(Assembly.GetExecutingAssembly());

            //ASSERT
            stream.Should().NotBeNull();
        }

        #region Test Helpers

        private IRulebaseProvider BuildRulebaseProviderFactory()
        {
            var factory = new RulebaseProviderFactory();
            return factory.Build();            
        }
        
        private IRulebaseProvider BuildTestRulebaseProviderFactory(string rulebaseZipPath)
        {
            return new RulebaseProvider(rulebaseZipPath);
        }
      
        #endregion
    }
}
