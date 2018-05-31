using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using ESFA.DC.ILR.FundingService.ALB.FundingOutput.Model.Interface;
using ESFA.DC.Serialization.Interfaces;
using ESFA.DC.Serialization.Json;

namespace ESFA.DC.ILR.FundingService.ALB.Stubs.Persistance
{
    public class DataPersister
    {
        private static readonly CultureInfo Culture = CultureInfo.CreateSpecificCulture("en-GB");

        public void PersistData(IFundingOutputs fundingOutputs)
        {
            ISerializationService serializationService = new JsonSerializationService();

            var serializedOutputs = serializationService.Serialize(fundingOutputs);

            WriteToFile(serializedOutputs);
        }

        private static void WriteToFile(string stringOutput)
        {
           using (System.IO.StringWriter textWriter = new System.IO.StringWriter())
            {
                System.IO.File.WriteAllText(@"C:\Code\temp\ALBFundingService\Json_Output.json", stringOutput);
            }
        }
    }
}
