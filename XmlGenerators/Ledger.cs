using System.Xml.XPath;
using System.Xml.Linq;
using System.Linq;


namespace TallyXMLReader.XmlGenerators
{
    public class Ledger
    {
        private static readonly string xmlFileName = "LEDGER";

        public static void CreateLedgerXml(XElement tallyXml, params string[] args)
        {
            XElement ledgerXml = XmlComponentGenerator.CreateXmlFromTemplate(xmlFileName, args);

            //now add it to TallyXml
            XElement parentNode = tallyXml.XPathSelectElements("//REQUESTDATA/TALLYMESSAGE").First();
            parentNode.LastNode.AddAfterSelf(ledgerXml);
        }
    }
}