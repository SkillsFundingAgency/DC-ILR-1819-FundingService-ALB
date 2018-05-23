using System;
using System.Collections.Generic;
using System.Text;
using ESFA.DC.ILR.FundingService.ALB.FundingOutput.Interface;
using ESFA.DC.ILR.FundingService.ALB.FundingOutput.Interface.Attribute;
using ESFA.DC.ILR.FundingService.ALB.FundingOutput.Model.Attribute;

namespace ESFA.DC.ILR.FundingService.ALB.FundingOutput.Model
{
    public class FundingOutputs : IFundingOutputs
    {
        public IGlobalAttribute Global { get; set; }

        public ILearnerAttribute[] Learners { get; set; }
    }
}
