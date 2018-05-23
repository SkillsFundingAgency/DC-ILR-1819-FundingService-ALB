﻿using ESFA.DC.ILR.FundingService.ALB.FundingOutput.Model.Interface.Attribute;

namespace ESFA.DC.ILR.FundingService.ALB.FundingOutput.Model.Attribute
{
    public class GlobalAttribute : IGlobalAttribute
    {
        public int UKPRN { get; set; }

        public string LARSVersion { get; set; }

        public string PostcodeAreaCostVersion { get; set; }

        public string RulebaseVersion { get; set; }
    }
}
