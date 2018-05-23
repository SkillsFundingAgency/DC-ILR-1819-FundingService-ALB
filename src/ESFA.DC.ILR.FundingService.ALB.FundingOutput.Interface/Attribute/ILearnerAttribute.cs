namespace ESFA.DC.ILR.FundingService.ALB.FundingOutput.Interface.Attribute
{
    public interface ILearnerAttribute
    {
        string LearnRefNumber { get; }

        ILearnerPeriodisedAttribute[] LearnerPeriodisedAttributes { get; }

        ILearningDeliveryAttribute[] LearningDeliveryAttributes { get; }
    }
}