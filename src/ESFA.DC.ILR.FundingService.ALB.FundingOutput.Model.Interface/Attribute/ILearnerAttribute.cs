namespace ESFA.DC.ILR.FundingService.ALB.FundingOutput.Model.Interface.Attribute
{
    public interface ILearnerAttribute
    {
        string LearnRefNumber { get; }

        ILearnerPeriodisedAttribute[] LearnerPeriodisedAttributes { get; }

        ILearningDeliveryAttribute[] LearningDeliveryAttributes { get; }
    }
}