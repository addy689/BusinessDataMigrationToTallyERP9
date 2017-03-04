using System.Xml.XPath;
using System.Xml.Linq;
using System.Linq;

namespace TallyXMLReader.XmlGenerators
{
    public class StockItem
    {
        private static readonly string xmlFileName = "STOCKITEM";
        
        public static void CreateStockItemXml(XElement tallyXml, params string[] args)
        {
            XElement stockItemXml = XmlComponentGenerator.CreateXmlFromTemplate(xmlFileName, args);
            
            //now add it to TallyXml
            XElement parentNode = tallyXml.XPathSelectElements("//REQUESTDATA/TALLYMESSAGE").First();
            parentNode.LastNode.AddAfterSelf(stockItemXml);
        }
    }
}