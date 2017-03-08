using System.Xml.XPath;
using System.Xml.Linq;
using System.Linq;


namespace MigrationToTallyERP9.XmlGenerators
{
    /// <summary>
    /// Responsible for the creation of two types of Ledgers: PARTY and PURCHASE from
    /// the corresponding template Xmls
    /// </summary>
    public class Ledger
    {
        private static readonly string partyLedgerXmlName = "PARTYLEDGER";
        private static readonly string purchaseLedgerXmlName = "PURCHASELEDGER";

        public static void CreatePartyLedgerXml(XElement tallyXml, params string[] args)
        {
           CreateLedgerXml(tallyXml, partyLedgerXmlName, args);
        }

        public static void CreatePurchaseLedgerXml(XElement tallyXml, params string[] args)
        {
            CreateLedgerXml(tallyXml, purchaseLedgerXmlName, args);
        }

        private static void CreateLedgerXml(XElement tallyXml, string ledgerName, params string[] args)
        {
            XElement ledgerXml = XmlComponentGenerator.CreateXmlFromTemplate(ledgerName, args);

            //now add it to TallyXml
            XElement parentNode = tallyXml.XPathSelectElements("//REQUESTDATA/TALLYMESSAGE").First();
            parentNode.LastNode.AddAfterSelf(ledgerXml);
        }
    }
}