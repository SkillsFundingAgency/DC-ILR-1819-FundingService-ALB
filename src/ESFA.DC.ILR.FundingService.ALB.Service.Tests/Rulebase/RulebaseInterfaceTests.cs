using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Xml;
using ESFA.DC.Data.LARS.Model.Interfaces;
using ESFA.DC.Data.Postcodes.Model;
using ESFA.DC.Data.LARS.Model;
using ESFA.DC.Data.Postcodes.Model.Interfaces;
using ESFA.DC.ILR.FundingService.ALB.ExternalData;
using ESFA.DC.ILR.FundingService.ALB.ExternalData.Interface;
using ESFA.DC.ILR.FundingService.ALB.Service.Builders;
using ESFA.DC.ILR.FundingService.ALB.Service.Builders.Interface;
using ESFA.DC.ILR.Model;
using ESFA.DC.ILR.Model.Interface;
using ESFA.DC.OPA.Model.Interface;
using ESFA.DC.OPA.XSRC.Model.Interface.XSRCEntity;
using ESFA.DC.OPA.XSRC.Model.XSRCEntity;
using ESFA.DC.OPA.XSRC.Service.Interface;
using ESFA.DC.OPA.XSRC.Service;
using ESFA.DC.OPA.XSRC.Model.Interface.XSRC;
using ESFA.DC.OPA.XSRC.Model.XSRC;
using ESFA.DC.Serialization.Interfaces;
using ESFA.DC.Serialization.Xml;
using ESFA.DC.TestHelpers.Mocks;
using FluentAssertions;
using Moq;
using Xunit;

namespace ESFA.DC.ILR.FundingService.ALB.Service.Tests.Rulebase
{
    public class RulebaseInterfaceTests
    {
        private const string academicYear = "1718";
        private const string rulebaseName = "Loans Bursary 17_18";
        private const string rulebaseFolder = "Rulebase";
        private const string rulebaseMasterFolder = "RulebaseMasterFiles";
        private const string xsrcName = "ALBInputs";

        /// <summary>
        /// Return Rulebase and check version numbers
        /// </summary>
        [Fact(DisplayName = "RulebaseVersion - AcademicYear Exists"), Trait("Rulebase Interface", "Unit")]
        public void RulebaseVersion_AcademicYear_Exists()
        {
            //ARRANGE
            var rulebaseVersion = GetVersion(rulebaseFolder);

            //ACT
            var year = rulebaseVersion.Substring(0, 4);

            //ASSERT
            year.Should().NotBeNull();
        }

        /// <summary>
        /// Return Rulebase and check version numbers
        /// </summary>
        [Fact(DisplayName = "RulebaseVersion - AcademicYear Correct"), Trait("Rulebase Interface", "Unit")]
        public void RulebaseVersion_AcademicYear_Correct()
        {
            //ARRANGE
            var rulebaseVersion = GetVersion(rulebaseFolder);

            //ACT
            var year = rulebaseVersion.Substring(0, 4);

            //ASSERT
            year.Should().Be(academicYear);
        }


        /// <summary>
        /// Return Rulebase and check version numbers
        /// </summary>
        [Fact(DisplayName = "RulebaseVersion - AcademicYear Matches Previous"), Trait("Rulebase Interface", "Unit")]
        public void RulebaseVersion_AcademicYear_Match()
        {
            //ARRANGE
            var rulebaseVersion = GetVersion(rulebaseFolder);
            var masterRulebaseVersion = GetVersion(rulebaseMasterFolder);

            //ACT
            var acaedmicYear = rulebaseVersion.Substring(0, 4);
            var masterAcaedmicYear = rulebaseVersion.Substring(0, 4);

            //ASSERT
            masterAcaedmicYear.Should().Be(acaedmicYear);
        }

        /// <summary>
        /// Return Rulebase and check version numbers
        /// </summary>
        [Fact(DisplayName = "RulebaseVersion - MajorVersion Exists"), Trait("Rulebase Interface", "Unit")]
        public void RulebaseVersion_InterfaceVersion_Exists()
        {
            //ARRANGE
            var rulebaseVersion = GetVersion(rulebaseFolder);

            //ACT
            var interfaceVersion = rulebaseVersion.Substring(5, 2);

            //ASSERT
            interfaceVersion.Should().NotBeNull();
        }

        /// <summary>
        /// Return Rulebase and check version numbers
        /// </summary>
        [Fact(DisplayName = "RulebaseVersion - MajorVersion Matches Previous"), Trait("Rulebase Interface", "Unit")]
        public void RulebaseVersion_InterfaceVersion_Match()
        {
            //ARRANGE
            var rulebaseVersion = GetVersion(rulebaseFolder);
            var masterRulebaseVersion = GetVersion(rulebaseMasterFolder);

            //ACT
            var interfaceVersion = rulebaseVersion.Substring(5, 2);
            var masterInterfaceVersion = rulebaseVersion.Substring(5, 2);

            //ASSERT
            masterInterfaceVersion.Should().Be(interfaceVersion);
        }

        /// <summary>
        /// Return Rulebase XSRC and check files are as expected.
        /// </summary>
        [Fact(DisplayName = "XSRC - XML File Not Empty"), Trait("Rulebase Interface", "Unit")]
        public void XSRC_File_NotNull()
        {
            //ARRANGE
            var xsrcFile = GetXSRC(rulebaseFolder);

            //ACT
            var xml = xsrcFile.OuterXml.ToString();

            //ASSERT
            xml.Should().NotBeNull();
        }

        /// <summary>
        /// Return Rulebase XSRC and check files are as expected.
        /// </summary>
        [Fact(DisplayName = "XSRC - XML Files Match"), Trait("Rulebase Interface", "Unit")]
        public void XSRC_File_FilesMatch()
        {
            //ARRANGE
            var xsrcFile = GetXSRC(rulebaseFolder);
            var masterXsrcFile = GetXSRC(rulebaseMasterFolder);

            //ACT
            var xml = xsrcFile.OuterXml.ToString();
            var masterXml = masterXsrcFile.OuterXml.ToString();

            //ASSERT
            masterXml.Should().BeEquivalentTo(xml);
        }

        /// <summary>
        /// Return Rulebase XSRC and check files are as expected.
        /// </summary>
        [Fact(DisplayName = "XSRC - XML File Check Entities Expected"), Trait("Rulebase Interface", "Unit")]
        public void XSRC_File_CheckEntitiesExpected()
        {
            //ARRANGE
            var xsrcFile = GetXSRC(rulebaseFolder);

            //ACT
            var entities = GetXRSCEntities(xsrcFile);

            //ASSERT
            expectedEntities.Should().BeEquivalentTo(entities);
        }

        /// <summary>
        /// Return Rulebase XSRC and check files are as expected.
        /// </summary>
        [Fact(DisplayName = "XSRC - XML File Check Entities Match"), Trait("Rulebase Interface", "Unit")]
        public void XSRC_File_CheckEntitiesMatch()
        {
            //ARRANGE
            var xsrcFile = GetXSRC(rulebaseFolder);
            var masterXsrcFile = GetXSRC(rulebaseMasterFolder);

            //ACT
            var entities = GetXRSCEntities(xsrcFile);
            var masterEntities = GetXRSCEntities(masterXsrcFile);

            //ASSERT
            masterEntities.Should().BeEquivalentTo(entities);
        }

        /// <summary>
        /// Return Rulebase XSRC and check files are as expected.
        /// </summary>
        [Fact(DisplayName = "XSRC - XML File Check Attributes Expected"), Trait("Rulebase Interface", "Unit")]
        public void XSRC_File_CheckAttributesExpected()
        {
            //ARRANGE
            var xsrcFile = GetXSRC(rulebaseFolder);

            //ACT
            var attributes = GetXRSCAttributes(xsrcFile);

            //ASSERT
            expectedAttributes.Should().BeEquivalentTo(attributes);
        }

        /// <summary>
        /// Return Rulebase XSRC and check files are as expected.
        /// </summary>
        [Fact(DisplayName = "XSRC - XML File Check Attributes Match"), Trait("Rulebase Interface", "Unit")]
        public void XSRC_File_CheckAttributesMatch()
        {
            //ARRANGE
            var xsrcFile = GetXSRC(rulebaseFolder);
            var masterXsrcFile = GetXSRC(rulebaseMasterFolder);

            //ACT
            var attributes = GetXRSCAttributes(xsrcFile);
            var masterattributes = GetXRSCAttributes(masterXsrcFile);

            //ASSERT
            masterattributes.Should().BeEquivalentTo(attributes);
        }

        /// <summary>
        /// Return Rulebase XSRC and check files are as expected.
        /// </summary>
        [Fact(DisplayName = "XSRC - Deserialize file - Object Exists"), Trait("Rulebase Interface", "Unit")]
        public void XSRC_Object_NotNull()
        {
            //ARRANGE
            var xsrc = DeserializedXSRC(rulebaseFolder);

            //ACT

            //ASSERT
            xsrc.Should().NotBeNull();
        }

        /// <summary>
        /// Return Rulebase XSRC and check files are as expected.
        /// </summary>
        [Fact(DisplayName = "XSRC - Deserialize file - Object Matches"), Trait("Rulebase Interface", "Unit")]
        public void XSRC_Object_Matches()
        {
            //ARRANGE
            var xsrc = DeserializedXSRC(rulebaseFolder);
            var MasterXsrc = DeserializedXSRC(rulebaseMasterFolder);

            //ACT

            //ASSERT
            MasterXsrc.Should().BeEquivalentTo(xsrc);
        }

        /// <summary>
        /// Return Rulebase XSRC and check files are as expected.
        /// </summary>
        [Fact(DisplayName = "XSRCEntity - Object Exists"), Trait("Rulebase Interface", "Unit")]
        public void XSRCEntity_Object_NotNull()
        {
            //ARRANGE

            //ACT
            var entity = XSRCBuilder(rulebaseFolder);

            //ASSERT
            entity.Should().NotBeNull();
        }

        /// <summary>
        /// Return Rulebase XSRC and check files are as expected.
        /// </summary>
        [Fact(DisplayName = "XSRCEntity - Object Matches"), Trait("Rulebase Interface", "Unit")]
        public void XSRCEntity_Object_Matches()
        {
            //ARRANGE

            //ACT
            var entity = XSRCBuilder(rulebaseFolder);
            var masterEntity = XSRCBuilder(rulebaseMasterFolder);

            //ASSERT
            masterEntity.Should().BeEquivalentTo(entity);
        }

        /// <summary>
        /// Return Rulebase XSRC and check files are as expected.
        /// </summary>
        [Fact(DisplayName = "XSRCEntity - XSRC Entities Matches Linq Query"), Trait("Rulebase Interface", "Unit")]
        public void XSRCEntity_Entties_MatchesLinq()
        {
            //ARRANGE
            var entityXSRC = XSRCBuilder(rulebaseFolder);
            var entityLinq = LinqEntityBuilder();

            //ACT
            var xsrcList = GetXSRCEntityList(entityXSRC.GlobalEntity);
            var linqList = GetLinqEntityList(entityLinq.First());

            //ASSERT
            var xsrc = xsrcList.Select(v => v).Distinct();
            var linq = linqList.Select(v => v).Distinct();

            xsrc.Should().BeEquivalentTo(linq);
        }

        /// <summary>
        /// Return Rulebase XSRC and check files are as expected.
        /// </summary>
        [Fact(DisplayName = "XSRCEntity - Base XSRC Entities Matches Linq Query"), Trait("Rulebase Interface", "Unit")]
        public void XSRCEntity_BaseEntties_MatchesLinq()
        {
            //ARRANGE
            var entityXSRC = XSRCBuilder(rulebaseMasterFolder);
            var entityLinq = LinqEntityBuilder();

            //ACT
            var xsrcList = GetXSRCEntityList(entityXSRC.GlobalEntity);
            var linqList = GetLinqEntityList(entityLinq.First());

            //ASSERT
            var xsrc = xsrcList.Select(v => v).Distinct();
            var linq = linqList.Select(v => v).Distinct();

            xsrc.Should().BeEquivalentTo(linq);
        }

        /// <summary>
        /// Return Rulebase XSRC and check files are as expected.
        /// </summary>
        [Fact(DisplayName = "XSRCEntity - XSRC Attributes Matches Linq Query"), Trait("Rulebase Interface", "Unit")]
        public void XSRCEntity_Attributes_MatchesLinq()
        {
            //ARRANGE
            var entityXSRC = XSRCBuilder(rulebaseFolder);
            var entityLinq = LinqEntityBuilder();

            //ACT
            var xsrcList = GetXSRCAttributeList(entityXSRC.GlobalEntity);
            var linqList = GetLinqAttributeList(entityLinq.First());

            //ASSERT
            var xsrc = xsrcList.Select(v => v).Distinct();
            var linq = linqList.Select(v => v).Distinct();

            xsrc.Should().BeEquivalentTo(linq);
        }

        /// <summary>
        /// Return Rulebase XSRC and check files are as expected.
        /// </summary>
        [Fact(DisplayName = "XSRCEntity - Base XSRC Attributes Matches Linq Query"), Trait("Rulebase Interface", "Unit")]
        public void XSRCEntity_BaseAttributes_MatchesLinq()
        {
            //ARRANGE
            var entityXSRC = XSRCBuilder(rulebaseMasterFolder);
            var entityLinq = LinqEntityBuilder();

            //ACT
            var xsrcList = GetXSRCAttributeList(entityXSRC.GlobalEntity);
            var linqList = GetLinqAttributeList(entityLinq.First());

            //ASSERT
            var xsrc = xsrcList.Select(v => v).Distinct();
            var linq = linqList.Select(v => v).Distinct();

            xsrc.Should().BeEquivalentTo(linq);
        }

        #region Test Helpers

        private IList<string> expectedEntities = new List<string>
            {
                "global",
                "learner",
                "learningdelivery",
                "learningdeliveryfam",
                "learningdeliverypostcodeareacostreferencedata",
                "learningdeliverylarsfunding",
            };

        private IList<string> expectedAttributes = new List<string>
            {
                "UKPRN",
                "LARSVersion",
                "PostcodeAreaCostVersion",
                "LearnRefNumber",
                "LearnActEndDate",
                "LearnPlanEndDate",
                "LearnStartDate",
                "AimSeqNumber",
                "OrigLearnStartDate",
                "CompStatus",
                "Outcome",
                "LearnAimRefType",
                "NotionalNVQLevelv2",
                "RegulatedCreditValue",
                "PriorLearnFundAdj",
                "OtherFundAdj",
                "LrnDelFAM_ADL",
                "LrnDelFAM_RES",
                "LearnDelFAMCode",
                "LearnDelFAMType",
                "LearnDelFAMDateFrom",
                "LearnDelFAMDateTo",
                "AreaCosEffectiveFrom",
                "AreaCosEffectiveTo",
                "AreaCosFactor",
                "LARSFundCategory",
                "LARSFundEffectiveFrom",
                "LARSFundEffectiveTo",
                "LARSFundWeightedRate",
                "LARSFundWeightingFactor",
            };

        IList<string> linqEntityList = new List<string>();

        private IList<string> GetLinqEntityList(IDataEntity entity)
        {
            linqEntityList.Add(entity.EntityName);

            foreach (var child in entity.Children)
            {
                GetLinqEntityList(child);
            }

            return linqEntityList;
        }

        IList<string> xsrcEntityList = new List<string>();

        private IList<string> GetXSRCEntityList(IXsrcEntity entity)
        {
            xsrcEntityList.Add(entity.PublicName);

            foreach (var child in entity.Children)
            {
                GetXSRCEntityList(child);
            }

            return xsrcEntityList;
        }

        IList<string> linqAttributeList = new List<string>();

        private IList<string> GetLinqAttributeList(IDataEntity entity)
        {
            foreach (var attribute in entity.Attributes)
            {
                linqAttributeList.Add(attribute.Key);
            }

            foreach (var child in entity.Children)
            {
                GetLinqAttributeList(child);
            }

            return linqAttributeList;
        }

        IList<string> xsrcAttributeList = new List<string>();

        private IList<string> GetXSRCAttributeList(IXsrcEntity entity)
        {
            foreach (var attribute in entity.Attributes)
            {
                xsrcAttributeList.Add(attribute.PublicName);
            }

            foreach (var child in entity.Children)
            {
                GetXSRCAttributeList(child);
            }

            return xsrcAttributeList;
        }

        private XsrcGlobal XSRCBuilder(string folderName)
        {
            IXsrcEntityBuilder builder = new XsrcEntityBuilder(@"" + folderName + "//" + xsrcName + ".xsrc");

            return builder.BuildXsrc();
        }


        private IRoot DeserializedXSRC(string folderName)
        {
            var stream = new FileStream(@"" + folderName + "//" + xsrcName + ".xsrc", FileMode.Open);

            ISerializationService serializationService = new XmlSerializationService();

            var root = serializationService.Deserialize<Root>(stream);

            stream.Close();

            return root;
        }

        private IList<string> GetXRSCEntities(XmlDocument xml)
        {
            IList<string> entities = new List<string>();
            var elemList = xml.GetElementsByTagName("entity");
            for (int i = 0; i < elemList.Count; i++)
            {
                var e = elemList[i];

                entities.Add(e.Attributes[0].Value);
            }

            return entities;
        }

        private IList<string> GetXRSCAttributes(XmlDocument xml)
        {
            IList<string> attributes = new List<string>();
            var elemList = xml.GetElementsByTagName("attribute");
            for (int i = 0; i < elemList.Count; i++)
            {
                var e = elemList[i];

                attributes.Add(e.Attributes["public-name"].Value);
            }

            return attributes;
        }

        private string GetVersion(string folderName)
        {
            var zipStream = new FileStream(@"" + folderName + "//" + rulebaseName + ".zip", FileMode.Open);
            var doc = GetXmlDocumentFromZip(zipStream);
            zipStream.Close();

            XmlElement root = doc.DocumentElement;
            var nodes = root.LastChild;
            XmlNodeList elemList = doc.GetElementsByTagName("conclude");

            IDictionary<string, string> attributes = new Dictionary<string, string>();

            for (int i = 0; i < elemList.Count; i++)
            {
                var e = elemList[i];

                attributes.Add(e.Attributes["attr-id"].Value, e.InnerText);
            }

            return attributes.Where(k => k.Key == "RulebaseVersion").Select(v => v.Value).SingleOrDefault();
        }

        private XmlDocument GetXmlDocumentFromZip(FileStream stream)
        {
            ZipArchive zip = new ZipArchive(stream);

            var file = zip.Entries.First(f => f.Name == rulebaseName + ".xml").Open();

            XmlDocument doc = new XmlDocument();
            doc.Load(file);

            return doc;
        }

        private XmlDocument GetXSRC(string folderName)
        {
            var stream = new FileStream(@"" + folderName + "//" + xsrcName + ".xsrc", FileMode.Open);
            var doc = GetXmlDocument(stream);
            stream.Close();

            return doc;
        }

        private XmlDocument GetXmlDocument(FileStream stream)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(stream);

            return doc;
        }

        private IEnumerable<IDataEntity> LinqEntityBuilder()
        {
            IReferenceDataCache referenceDataCache = new ReferenceDataCache();
            IReferenceDataCachePopulationService referenceDataCachePopulationService = new ReferenceDataCachePopulationService(referenceDataCache, LARSMock().Object, PostcodesMock().Object);

            referenceDataCachePopulationService.Populate(new List<string> { "50094488", "60005415" }, new List<string> { "CV1 2WT" });
            IAttributeBuilder<IAttributeData> attributeBuilder = new AttributeBuilder();
            var dataEntityBuilder = new DataEntityBuilder(referenceDataCache, attributeBuilder);

            return dataEntityBuilder.EntityBuilder(12345678, TestMessage.Learners);
        }

        private static readonly Mock<ILARS> larsContextMock = new Mock<ILARS>();
        private static readonly Mock<IPostcodes> postcodesContextMock = new Mock<IPostcodes>();

        private Mock<ILARS> LARSMock()
        {
            var larsVersionMock = MockDBSetHelper.GetQueryableMockDbSet(MockLARSVersionArray());
            var larsLearningDeliveryMock = MockDBSetHelper.GetQueryableMockDbSet(MockLARSLearningDeliveryArray());
            var larsFundingMock = MockDBSetHelper.GetQueryableMockDbSet(MockLARSFundingArray());

            larsContextMock.Setup(x => x.LARS_Version).Returns(larsVersionMock);
            larsContextMock.Setup(x => x.LARS_LearningDelivery).Returns(larsLearningDeliveryMock);
            larsContextMock.Setup(x => x.LARS_Funding).Returns(larsFundingMock);

            return larsContextMock;
        }

        private Mock<IPostcodes> PostcodesMock()
        {
            var postcodesVersionMock = MockDBSetHelper.GetQueryableMockDbSet(MockPostcodesVersionArray());
            var sfaAreaCostMock = MockDBSetHelper.GetQueryableMockDbSet(MockSFAAreaCostArray());

            postcodesContextMock.Setup(x => x.SFA_PostcodeAreaCost).Returns(sfaAreaCostMock);
            postcodesContextMock.Setup(x => x.VersionInfos).Returns(postcodesVersionMock);

            return postcodesContextMock;
        }

        private static LARS_Version[] MockLARSVersionArray()
        {
            return new LARS_Version[]
            {
                larsVersionTestValue,
            };
        }

        readonly static LARS_Version larsVersionTestValue =
            new LARS_Version()
            {
                MajorNumber = 5,
                MinorNumber = 0,
                MaintenanceNumber = 0,
                MainDataSchemaName = "Version_005",
                RefDataSchemaName = "REF_Version_005",
                ActivationDate = DateTime.Parse("2017-07-01"),
                ExpiryDate = null,
                Description = "Fifth Version of LARS",
                Comment = null,
                Created_On = DateTime.Parse("2017-07-01"),
                Created_By = "System",
                Modified_On = DateTime.Parse("2018-07-01"),
                Modified_By = "System"
            };

        private static LARS_LearningDelivery[] MockLARSLearningDeliveryArray()
        {
            return new LARS_LearningDelivery[]
            {
                larsLearningDeliveryTestValue1,
                larsLearningDeliveryTestValue2
            };
        }

        readonly static LARS_LearningDelivery larsLearningDeliveryTestValue1 =
            new LARS_LearningDelivery()
            {
                LearnAimRef = "50094488",
                LearnAimRefTitle = "Test Learning Aim Title 50094488",
                LearnAimRefType = "0006",
                NotionalNVQLevel = "2",
                NotionalNVQLevelv2 = "2",
                CertificationEndDate = DateTime.Parse("2018-01-01"),
                OperationalStartDate = DateTime.Parse("2018-01-01"),
                OperationalEndDate = DateTime.Parse("2018-01-01"),
                RegulatedCreditValue = 180,
                EffectiveFrom = DateTime.Parse("2000-01-01"),
                EffectiveTo = null,
                Created_On = DateTime.Parse("2017-01-01"),
                Created_By = "TestUser",
                Modified_On = DateTime.Parse("2018-01-01"),
                Modified_By = "TestUser"
            };

        readonly static LARS_LearningDelivery larsLearningDeliveryTestValue2 =
           new LARS_LearningDelivery()
           {
               LearnAimRef = "60005415",
               LearnAimRefTitle = "Test Learning Aim Title 60005415",
               LearnAimRefType = "0006",
               NotionalNVQLevel = "4",
               NotionalNVQLevelv2 = "4",
               CertificationEndDate = DateTime.Parse("2018-01-01"),
               OperationalStartDate = DateTime.Parse("2018-01-01"),
               OperationalEndDate = DateTime.Parse("2018-01-01"),
               RegulatedCreditValue = 42,
               EffectiveFrom = DateTime.Parse("2000-01-01"),
               EffectiveTo = null,
               Created_On = DateTime.Parse("2017-01-01"),
               Created_By = "TestUser",
               Modified_On = DateTime.Parse("2018-01-01"),
               Modified_By = "TestUser"
           };

        private static LARS_Funding[] MockLARSFundingArray()
        {
            return new LARS_Funding[]
            {
                larsFundingTestValue1,
                larsFundingTestValue2
            };
        }

        readonly static LARS_Funding larsFundingTestValue1 =
            new LARS_Funding()
            {
                LearnAimRef = "50094488",
                FundingCategory = "Matrix",
                RateWeighted = 11356m,
                RateUnWeighted = null,
                WeightingFactor = "G",
                EffectiveFrom = DateTime.Parse("2000-01-01"),
                EffectiveTo = null,
                Created_On = DateTime.Parse("2017-01-01"),
                Created_By = "TestUser",
                Modified_On = DateTime.Parse("2018-01-01"),
                Modified_By = "TestUser"
            };

        readonly static LARS_Funding larsFundingTestValue2 =
          new LARS_Funding()
          {
              LearnAimRef = "60005415",
              FundingCategory = "Matrix",
              RateWeighted = 2583m,
              RateUnWeighted = null,
              WeightingFactor = "C",
              EffectiveFrom = DateTime.Parse("2000-01-01"),
              EffectiveTo = null,
              Created_On = DateTime.Parse("2017-01-01"),
              Created_By = "TestUser",
              Modified_On = DateTime.Parse("2018-01-01"),
              Modified_By = "TestUser"
          };

        private static VersionInfo[] MockPostcodesVersionArray()
        {
            return new VersionInfo[]
            {
                PostcodesVersionTestValue,
            };
        }

        readonly static VersionInfo PostcodesVersionTestValue =
            new VersionInfo
            {
                VersionNumber = "Version_002",
                DataSource = "Source",
                Comments = "Comments",
                ModifiedAt = DateTime.Parse("2018-01-01"),
                ModifiedBy = "System"
            };

        private static SFA_PostcodeAreaCost[] MockSFAAreaCostArray()
        {
            return new SFA_PostcodeAreaCost[]
            {
                SFAAreaCostTestValue1,
            };
        }

        readonly static SFA_PostcodeAreaCost SFAAreaCostTestValue1 =
          new SFA_PostcodeAreaCost()
          {
              MasterPostcode = new MasterPostcode { Postcode = "CV1 2WT" },
              Postcode = "CV1 2WT",
              AreaCostFactor = 1.2m,
              EffectiveFrom = DateTime.Parse("2000-01-01"),
              EffectiveTo = null,
          };

        private readonly IMessage TestMessage = new Message
        {
            LearningProvider = new MessageLearningProvider
            {
                UKPRN = 12345678,
            },
            Learner = new MessageLearner[]
          {
                new MessageLearner
                {
                    LearnRefNumber = "Learner1",
                    LearningDelivery = new[]
                    {
                        new MessageLearnerLearningDelivery
                        {
                            LearnAimRef = "50094488",
                            AimSeqNumber = 1,
                            CompStatus = 1,
                            DelLocPostCode = "CV1 2WT",
                            LearnActEndDateSpecified = true,
                            LearnActEndDate = DateTime.Parse("2018-06-30"),
                            LearnStartDate = DateTime.Parse("2017-08-30"),
                            LearnPlanEndDate = DateTime.Parse("2018-07-30"),
                            OrigLearnStartDateSpecified = true,
                            OrigLearnStartDate = DateTime.Parse("2017-08-30"),
                            OtherFundAdjSpecified = false,
                            OutcomeSpecified = false,
                            PriorLearnFundAdjSpecified = false,
                            LearningDeliveryFAM = new[]
                            {
                                new MessageLearnerLearningDeliveryLearningDeliveryFAM
                                {
                                    LearnDelFAMCode = "1",
                                    LearnDelFAMType = "ADL",
                                    LearnDelFAMDateFromSpecified = true,
                                    LearnDelFAMDateFrom = DateTime.Parse("2017-08-30"),
                                    LearnDelFAMDateToSpecified = true,
                                    LearnDelFAMDateTo =  DateTime.Parse("2017-10-31")

                                },
                                new MessageLearnerLearningDeliveryLearningDeliveryFAM
                                {
                                    LearnDelFAMCode = "100",
                                    LearnDelFAMType = "SOF",
                                    LearnDelFAMDateFromSpecified = true,
                                    LearnDelFAMDateFrom = DateTime.Parse("2017-10-31"),
                                    LearnDelFAMDateToSpecified = true,
                                    LearnDelFAMDateTo =  DateTime.Parse("2017-11-30")
                                },
                                new MessageLearnerLearningDeliveryLearningDeliveryFAM
                                {
                                    LearnDelFAMCode = "1",
                                    LearnDelFAMType = "RES",
                                    LearnDelFAMDateFromSpecified = true,
                                    LearnDelFAMDateFrom = DateTime.Parse("2017-12-01"),
                                    LearnDelFAMDateToSpecified = false
                                }
                            }
                        }
                    }
                },
                new MessageLearner
                {
                    LearnRefNumber = "Learner2",
                    LearningDelivery = new[]
                    {
                        new MessageLearnerLearningDelivery
                        {
                            LearnAimRef = "60005415",
                            AimSeqNumber = 1,
                            CompStatus = 1,
                            DelLocPostCode = "CV1 2WT",
                            LearnActEndDateSpecified = true,
                            LearnActEndDate = DateTime.Parse("2018-06-30"),
                            LearnStartDate = DateTime.Parse("2017-08-30"),
                            LearnPlanEndDate = DateTime.Parse("2018-07-30"),
                            OrigLearnStartDateSpecified = true,
                            OrigLearnStartDate = DateTime.Parse("2017-08-30"),
                            OtherFundAdjSpecified = false,
                            OutcomeSpecified = false,
                            PriorLearnFundAdjSpecified = false,
                            LearningDeliveryFAM = new[]
                            {
                                new MessageLearnerLearningDeliveryLearningDeliveryFAM
                                {
                                    LearnDelFAMCode = "1",
                                    LearnDelFAMType = "ADL",
                                    LearnDelFAMDateFromSpecified = true,
                                    LearnDelFAMDateFrom = DateTime.Parse("2017-08-30"),
                                    LearnDelFAMDateToSpecified = true,
                                    LearnDelFAMDateTo =  DateTime.Parse("2017-10-31")

                                },
                                new MessageLearnerLearningDeliveryLearningDeliveryFAM
                                {
                                    LearnDelFAMCode = "100",
                                    LearnDelFAMType = "SOF",
                                    LearnDelFAMDateFromSpecified = true,
                                    LearnDelFAMDateFrom = DateTime.Parse("2017-10-31"),
                                    LearnDelFAMDateToSpecified = true,
                                    LearnDelFAMDateTo =  DateTime.Parse("2017-11-30")
                                },
                                new MessageLearnerLearningDeliveryLearningDeliveryFAM
                                {
                                    LearnDelFAMCode = "1",
                                    LearnDelFAMType = "RES",
                                    LearnDelFAMDateFromSpecified = true,
                                    LearnDelFAMDateFrom = DateTime.Parse("2017-12-01"),
                                    LearnDelFAMDateToSpecified = false
                                }
                            }
                        }
                    }
                }
            }
        };

        #endregion
    }
}
