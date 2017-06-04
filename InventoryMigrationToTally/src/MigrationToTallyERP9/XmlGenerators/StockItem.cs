using System.Xml.XPath;
using System.Xml.Linq;
using System.Linq;

namespace MigrationToTallyERP9.XmlGenerators
{
    public class StockItem
    {
        public static readonly string xmlFileName = "STOCKITEM";

        public static void CreateStockItemXml(XElement tallyXml, params string[] args)
        {
            XElement stockItemXml = XmlComponentGenerator.CreateXmlFromTemplate(xmlFileName, args);

            //now add it to TallyXml
            XElement parentNode = tallyXml.XPathSelectElements("//REQUESTDATA/TALLYMESSAGE").First();
            parentNode.Add(stockItemXml.FirstNode);
        }

        public static bool IsAlreadyCreated(string itemName, XElement tallyXml)
        {
            XElement xml = tallyXml.XPathSelectElement($"//REQUESTDATA/TALLYMESSAGE/STOCKITEM[@NAME='{itemName}']");

            if (xml != null)
                return true;

            return false;
        }
    }
}