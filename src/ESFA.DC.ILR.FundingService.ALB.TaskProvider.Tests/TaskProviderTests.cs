using System;
using System.Collections.Generic;
using System.Linq;
using ESFA.DC.ILR.FundingService.ALB.InternalData;
using ESFA.DC.ILR.FundingService.ALB.InternalData.Interface;
using ESFA.DC.ILR.FundingService.ALB.OrchestrationService;
using ESFA.DC.ILR.FundingService.ALB.OrchestrationService.Interface;
using ESFA.DC.ILR.FundingService.ALB.Service;
using ESFA.DC.ILR.FundingService.ALB.Service.Interface;
using ESFA.DC.ILR.FundingService.ALB.TaskProvider.Interface;
using ESFA.DC.ILR.FundingService.ALB.TaskProvider.Service;
using ESFA.DC.IO.Dictionary;
using ESFA.DC.IO.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;

namespace ESFA.DC.ILR.FundingService.ALB.TaskProvider.Tests
{
    public class TaskProviderTests
    {
        /// <summary>
        /// Run TaskProvider
        /// </summary>
        [Fact(DisplayName = "TaskProvider - Exists"), Trait("Funding Service", "Unit")]
        public void ProcessFunding_Exists()
        {
            // ARRANGE

            // ACT

            // ASSERT
        }
    }
}
