namespace ESFA.DC.ILR.FundingService.ALB.FundingOutput.Model.Attribute
{
    public class LearningDeliveryAttribute
    {
        public int AimSeqNumber { get; set; }

        public LearningDeliveryAttributeData LearningDeliveryAttributeDatas { get; set; }

        public LearningDeliveryPeriodisedAttribute[] LearningDeliveryPeriodisedAttributes { get; set; }
    }
}
