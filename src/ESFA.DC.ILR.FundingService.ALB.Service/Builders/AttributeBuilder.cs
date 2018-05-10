using System;
using System.Collections.Generic;
using ESFA.DC.ILR.FundingService.ALB.Service.Builders.Interface;
using ESFA.DC.OPA.Model;
using ESFA.DC.OPA.Model.Interface;

namespace ESFA.DC.ILR.FundingService.ALB.Service.Builders
{
    public class AttributeBuilder : IAttributeBuilder<IAttributeData>
    {
        #region Constants

        // Global
        private const string LARSVersion = "LARSVersion";
        private const string UKPRN = "UKPRN";
        private const string PostcodeAreaCostVersion = "PostcodeAreaCostVersion";

        // Learner
        private const string LearnRefNumber = "LearnRefNumber";

        // LearningDelivery
        private const string AimSeqNumber = "AimSeqNumber";
        private const string CompStatus = "CompStatus";
        private const string LearnActEndDate = "LearnActEndDate";
        private const string LearnAimRefType = "LearnAimRefType";
        private const string LearnPlanEndDate = "LearnPlanEndDate";
        private const string LearnStartDate = "LearnStartDate";
        private const string LrnDelFAM_ADL = "LrnDelFAM_ADL";
        private const string LrnDelFAM_RES = "LrnDelFAM_RES";
        private const string NotionalNVQLevelv2 = "NotionalNVQLevelv2";
        private const string OrigLearnStartDate = "OrigLearnStartDate";
        private const string OtherFundAdj = "OtherFundAdj";
        private const string Outcome = "Outcome";
        private const string PriorLearnFundAdj = "PriorLearnFundAdj";
        private const string RegulatedCreditValue = "RegulatedCreditValue";

        // LearningDeliveryFAM
        private const string LearnDelFAMCode = "LearnDelFAMCode";
        private const string LearnDelFAMDateFrom = "LearnDelFAMDateFrom";
        private const string LearnDelFAMDateTo = "LearnDelFAMDateTo";
        private const string LearnDelFAMType = "LearnDelFAMType";

        // SFAPostcodeAreaCost
        private const string AreaCosEffectiveFrom = "AreaCosEffectiveFrom";
        private const string AreaCosEffectiveTo = "AreaCosEffectiveTo";
        private const string AreaCosFactor = "AreaCosFactor";

        // LearningDeliveryLARSFunding
        private const string LARSFundCategory = "LARSFundCategory";
        private const string LARSFundEffectiveFrom = "LARSFundEffectiveFrom";
        private const string LARSFundEffectiveTo = "LARSFundEffectiveTo";
        private const string LARSFundWeightedRate = "LARSFundWeightedRate";
        private const string LARSFundWeightingFactor = "LARSFundWeightingFactor";

        #endregion

        public IDictionary<string, IAttributeData> BuildGlobalAttributes(int ukprn, string larsVersion, string postcodeAreaCostVersion)
        {
            return new Dictionary<string, IAttributeData>
            {
                { UKPRN,  new AttributeData(UKPRN, ukprn) },
                { LARSVersion, new AttributeData(LARSVersion, larsVersion) },
                { PostcodeAreaCostVersion,  new AttributeData(PostcodeAreaCostVersion, postcodeAreaCostVersion) },
            };
        }

        public IDictionary<string, IAttributeData> BuildLearnerAttributes(string learnRefNumber)
        {
            return new Dictionary<string, IAttributeData>
            {
                { LearnRefNumber, new AttributeData(LearnRefNumber, learnRefNumber) },
            };
        }

        public IDictionary<string, IAttributeData> BuildLearningDeliveryAttributes(
            long? aimSeqNumber,
            long? compStatus,
            DateTime? learnActEndDate,
            string learnAimRefType,
            DateTime? learnPlanEndDate,
            DateTime? learnStartDate,
            string lrnDelFAM_ADL,
            string lrnDelFAM_RES,
            string notionalNVQLevelv2,
            DateTime? origLearnStartDate,
            long? otherFundAdj,
            long? outcome,
            long? priorLearnFundAdj,
            long? regulatedCreditValue)
        {
            return new Dictionary<string, IAttributeData>
            {
                { AimSeqNumber, new AttributeData(AimSeqNumber, aimSeqNumber) },
                { CompStatus, new AttributeData(CompStatus, compStatus) },
                { LearnActEndDate, new AttributeData(LearnActEndDate, learnActEndDate) },
                { LearnAimRefType, new AttributeData(LearnAimRefType, learnAimRefType) },
                { LearnPlanEndDate, new AttributeData(LearnPlanEndDate, learnPlanEndDate) },
                { LearnStartDate, new AttributeData(LearnStartDate, learnStartDate) },
                { LrnDelFAM_ADL, new AttributeData(LrnDelFAM_ADL, lrnDelFAM_ADL) },
                { LrnDelFAM_RES, new AttributeData(LrnDelFAM_RES, lrnDelFAM_RES) },
                { NotionalNVQLevelv2, new AttributeData(NotionalNVQLevelv2, notionalNVQLevelv2) },
                { OrigLearnStartDate, new AttributeData(OrigLearnStartDate, origLearnStartDate) },
                { OtherFundAdj, new AttributeData(OtherFundAdj, otherFundAdj) },
                { Outcome, new AttributeData(Outcome, outcome) },
                { PriorLearnFundAdj, new AttributeData(PriorLearnFundAdj, priorLearnFundAdj) },
                { RegulatedCreditValue, new AttributeData(RegulatedCreditValue, regulatedCreditValue) },
            };
        }

        public IDictionary<string, IAttributeData> BuildLearningDeliveryFAMAttributes(string learnDelFAMCode, DateTime? learnDelFAMDateFrom, DateTime? learnDelFAMDateTo, string learnDelFAMType)
        {
            return new Dictionary<string, IAttributeData>
            {
                { LearnDelFAMCode, new AttributeData(LearnDelFAMCode, learnDelFAMCode) },
                { LearnDelFAMDateFrom, new AttributeData(LearnDelFAMDateFrom, learnDelFAMDateFrom) },
                { LearnDelFAMDateTo, new AttributeData(LearnDelFAMDateTo, learnDelFAMDateTo) },
                { LearnDelFAMType, new AttributeData(LearnDelFAMType, learnDelFAMType) },
            };
        }

        public IDictionary<string, IAttributeData> BuildLearningDeliverySfaAreaCostAttributes(DateTime? areaCosEffectiveFrom, DateTime? areaCosEffectiveTo, decimal areaCosFactor)
        {
            return new Dictionary<string, IAttributeData>
            {
                { AreaCosEffectiveFrom, new AttributeData(AreaCosEffectiveFrom, areaCosEffectiveFrom) },
                { AreaCosEffectiveTo, new AttributeData(AreaCosEffectiveTo, areaCosEffectiveTo) },
                { AreaCosFactor, new AttributeData(AreaCosFactor, areaCosFactor) },
            };
        }

        public IDictionary<string, IAttributeData> BuildLearningDeliveryLarsFundingAttributes(
            string larsFundCategory,
            DateTime larsFundEffectiveFrom,
            DateTime? larsFundEffectiveTo,
            decimal? larsFundWeightedRate,
            string larsFundWeightingFactor)
        {
            return new Dictionary<string, IAttributeData>
            {
                { LARSFundCategory, new AttributeData(LARSFundCategory, larsFundCategory) },
                { LARSFundEffectiveFrom, new AttributeData(LARSFundEffectiveFrom, larsFundEffectiveFrom) },
                { LARSFundEffectiveTo, new AttributeData(LARSFundEffectiveTo, larsFundEffectiveTo) },
                { LARSFundWeightedRate, new AttributeData(LARSFundWeightedRate, larsFundWeightedRate) },
                { LARSFundWeightingFactor, new AttributeData(LARSFundWeightingFactor, larsFundWeightingFactor) },
            };
        }
    }
}
