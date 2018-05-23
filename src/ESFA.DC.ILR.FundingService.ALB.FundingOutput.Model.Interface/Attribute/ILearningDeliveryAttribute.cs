namespace ESFA.DC.ILR.FundingService.ALB.FundingOutput.Model.Interface.Attribute
{
    public interface ILearningDeliveryAttribute
    {
        int AimSeqNumber { get; }

        ILearningDeliveryAttributeData LearningDeliveryAttributeDatas { get; }

        ILearningDeliveryPeriodisedAttribute[] LearningDeliveryPeriodisedAttributes { get; }
    }
}