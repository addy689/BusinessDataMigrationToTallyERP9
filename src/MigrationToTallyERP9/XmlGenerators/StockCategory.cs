using System.Xml.XPath;
using System.Xml.Linq;
using System.Linq;

namespace MigrationToTallyERP9.XmlGenerators
{
    public class StockCategory
    {
        private static readonly string xmlFileName = "STOCKCATEGORY";
        
        public static void CreateStockCategoryXml(XElement tallyXml, params string[] args)
        {
            XElement stockCategoryXml = XmlComponentGenerator.CreateXmlFromTemplate(xmlFileName, args);
            
            //now add it to TallyXml
            XElement parentNode = tallyXml.XPathSelectElements("//REQUESTDATA/TALLYMESSAGE").First();
            parentNode.Add(stockCategoryXml);
        }

        public static bool IsAlreadyCreated(string categoryName, XElement tallyXml)
        {
            XElement xml = tallyXml.XPathSelectElement($"//REQUESTDATA/TALLYMESSAGE/STOCKCATEGORY[@NAME='{categoryName}']");

            if(xml != null)
                return true;
            
            return false;
        }
    }
}