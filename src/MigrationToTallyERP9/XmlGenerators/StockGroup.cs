using System.Xml.Linq;
using System.Linq;
using System.Xml.XPath;

namespace MigrationToTallyERP9.XmlGenerators
{
    public class StockGroup
    {
        private static readonly string xmlFileName = "STOCKGROUP";
        
        public static void CreateStockGroupXml(XElement tallyXml, params string[] args)
        {
            XElement stockGroupXml = XmlComponentGenerator.CreateXmlFromTemplate(xmlFileName, args);
            
            //now add it to TallyXml
            XElement parentNode = tallyXml.XPathSelectElements("//REQUESTDATA/TALLYMESSAGE").First();
            parentNode.LastNode.AddAfterSelf(stockGroupXml);
        }
        
        public static bool IsAlreadyCreated(string groupName, XElement tallyXml)
        {
            XElement xml = tallyXml.XPathSelectElement($"//REQUESTDATA/TALLYMESSAGE/STOCKGROUP[@NAME='{groupName}']");

            if(xml != null)
                return true;
            
            return false;
        }
    }
}