namespace ESFA.DC.ILR.FundingService.ALB.FundingOutput.Interface.Attribute
{
    public interface ILearningDeliveryAttribute
    {
        int AimSeqNumber { get; }

        ILearningDeliveryAttributeData LearningDeliveryAttributeDatas { get; }

        ILearningDeliveryPeriodisedAttribute[] LearningDeliveryPeriodisedAttributes { get; }
    }
}