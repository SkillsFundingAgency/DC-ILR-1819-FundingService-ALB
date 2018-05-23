namespace ESFA.DC.ILR.FundingService.ALB.FundingOutput.Model.Interface.Attribute
{
    public interface IGlobalAttribute
    {
        int UKPRN { get; }

        string LARSVersion { get; }

        string PostcodeAreaCostVersion { get; }

        string RulebaseVersion { get; }
    }
}