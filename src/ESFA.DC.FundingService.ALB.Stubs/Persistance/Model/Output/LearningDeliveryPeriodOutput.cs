namespace ESFA.DC.ILR.FundingService.ALB.Stubs.Persistance.Model.Output
{
    public class LearningDeliveryPeriodOutput
    {
        public int UKPRN { get; set; }

        public string LearnRefNumber { get; set; }

        public int AimSeqNumber { get; set; }

        public int Period { get; set; }

        public int ALBCode { get; set; }

        public decimal ALBSupportPayment { get; set; }

        public decimal AreaUpliftBalPayment { get; set; }

        public decimal AreaUpliftOnProgPayment { get; set; }
    }
}
