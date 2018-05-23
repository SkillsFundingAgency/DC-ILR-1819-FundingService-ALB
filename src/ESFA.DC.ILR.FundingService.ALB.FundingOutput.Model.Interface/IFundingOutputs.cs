using ESFA.DC.ILR.FundingService.ALB.FundingOutput.Model.Interface.Attribute;

namespace ESFA.DC.ILR.FundingService.ALB.FundingOutput.Model.Interface
{
    public interface IFundingOutputs
    {
        IGlobalAttribute Global { get; }

        ILearnerAttribute[] Learners { get; }
    }
}
