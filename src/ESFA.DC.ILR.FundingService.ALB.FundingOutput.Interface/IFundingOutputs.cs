using ESFA.DC.ILR.FundingService.ALB.FundingOutput.Interface.Attribute;

namespace ESFA.DC.ILR.FundingService.ALB.FundingOutput.Interface
{
    public interface IFundingOutputs
    {
        IGlobalAttribute Global { get; }

        ILearnerAttribute[] Learners { get; }
    }
}
