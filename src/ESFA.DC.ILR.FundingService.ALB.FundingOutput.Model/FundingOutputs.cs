using System;
using System.Collections.Generic;
using System.Text;
using ESFA.DC.ILR.FundingService.ALB.FundingOutput.Model.Interface;
using ESFA.DC.ILR.FundingService.ALB.FundingOutput.Model.Interface.Attribute;

namespace ESFA.DC.ILR.FundingService.ALB.FundingOutput.Model
{
    public class FundingOutputs : IFundingOutputs
    {
        public IGlobalAttribute Global { get; set; }

        public ILearnerAttribute[] Learners { get; set; }
    }
}
