using System.Xml.Linq;

namespace TallyXMLReader.XmlGenerators
{
    public class StockCategory
    {
        private static readonly string xmlFileName = "STOCKCATEGORY";
        
        public static void CreateStockCategoryXml(XElement tallyXml, params string[] args)
        {
            XElement stockCategoryXml = XmlComponentGenerator.CreateXmlFromTemplate(xmlFileName, args);
            //now add it
        }

        public static bool CheckIfAlreadyCreated(string childCategoryName, XElement xml)
        {
            return true;
        }
    }
}