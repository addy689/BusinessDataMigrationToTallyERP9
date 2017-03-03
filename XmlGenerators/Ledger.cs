using System.Xml.Linq;

namespace TallyXMLReader.XmlGenerators
{
    public class Ledger
    {
        private static readonly string xmlFileName = "LEDGER";

        public static void CreateLedgerXml(XElement tallyXml, params string[] args)
        {
            XElement ledgerXml = XmlComponentGenerator.CreateXmlFromTemplate(xmlFileName, args);
            //now add it
        }
    }
}