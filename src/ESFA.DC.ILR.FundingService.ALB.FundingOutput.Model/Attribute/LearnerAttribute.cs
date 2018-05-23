using ESFA.DC.ILR.FundingService.ALB.FundingOutput.Model.Interface.Attribute;

namespace ESFA.DC.ILR.FundingService.ALB.FundingOutput.Model.Attribute
{
    public class LearnerAttribute : ILearnerAttribute
    {
        public string LearnRefNumber { get; set; }

        public ILearnerPeriodisedAttribute[] LearnerPeriodisedAttributes { get; set; }

        public ILearningDeliveryAttribute[] LearningDeliveryAttributes { get; set; }
    }
}
