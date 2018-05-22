namespace ESFA.DC.ILR.FundingService.ALB.FundingOutput.Model.Attribute
{
    public class LearnerAttribute
    {
        public string LearnRefNumber { get; set; }

        public LearnerPeriodisedAttribute[] LearnerPeriodisedAttributes { get; set; }

        public LearningDeliveryAttribute[] LearningDeliveryAttributes { get; set; }
    }
}
