using System.Xml.Linq;

namespace TallyXMLReader.XmlGenerators
{
    public class StockGroup
    {
        private static readonly string xmlFileName = "STOCKGROUP";
        public static XElement GetXml(params string[] args)
        {
            return XmlComponentGenerator.CreateXmlFromTemplate(xmlFileName, args);
        }

        public static void CreateStockGroupXml(XElement tallyXml, params string[] args)
        {
            XElement stockGroupXml = StockGroup.GetXml(args);
            //now add it
        }
        
        public static bool CheckIfAlreadyCreated(string childGroupName, XElement xml)
        {
            return true;
        }
    }
}