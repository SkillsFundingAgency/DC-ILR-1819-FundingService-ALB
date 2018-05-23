using ESFA.DC.ILR.FundingService.ALB.FundingOutput.Model.Interface.Attribute;

namespace ESFA.DC.ILR.FundingService.ALB.FundingOutput.Model.Attribute
{
    public class LearningDeliveryAttribute : ILearningDeliveryAttribute
    {
        public int AimSeqNumber { get; set; }

        public ILearningDeliveryAttributeData LearningDeliveryAttributeDatas { get; set; }

        public ILearningDeliveryPeriodisedAttribute[] LearningDeliveryPeriodisedAttributes { get; set; }
    }
}
