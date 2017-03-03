using System.Xml.Linq;

namespace TallyXMLReader.XmlGenerators
{
    public class StockItem
    {
        private static readonly string xmlFileName = "STOCKITEM";
        
        public static void CreateStockItemXml(XElement tallyXml, params string[] args)
        {
            XElement stockItemXml = XmlComponentGenerator.CreateXmlFromTemplate(xmlFileName, args);
            //now add it
        }
    }
}