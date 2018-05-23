using System;

namespace ESFA.DC.ILR.FundingService.ALB.FundingOutput.Model.Interface.Attribute
{
    public interface ILearningDeliveryAttributeData
    {
        bool? Achieved { get; }

        int? ActualNumInstalm { get; }

        bool? AdvLoan { get; }

        DateTime? ApplicFactDate { get; }

        string ApplicProgWeightFact { get; }

        decimal? AreaCostFactAdj { get; }

        decimal? AreaCostInstalment { get; }

        string FundLine { get; }

        bool? FundStart { get; }

        DateTime? LiabilityDate { get; }

        bool? LoanBursAreaUplift { get; }

        bool? LoanBursSupp { get; }

        int? OutstndNumOnProgInstalm { get; }

        int? PlannedNumOnProgInstalm { get; }

        decimal? WeightedRate { get; }
    }
}