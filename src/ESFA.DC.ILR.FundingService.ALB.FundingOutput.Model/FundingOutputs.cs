using System;
using System.Collections.Generic;
using System.Text;
using ESFA.DC.ILR.FundingService.ALB.FundingOutput.Model.Attribute;

namespace ESFA.DC.ILR.FundingService.ALB.FundingOutput.Model
{
    public class FundingOutputs
    {
        public GlobalAttribute Global { get; set; }

        public LearnerAttribute[] Learners { get; set; }
    }
}
