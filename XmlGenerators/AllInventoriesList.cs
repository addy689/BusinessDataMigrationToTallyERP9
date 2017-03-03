using System.Xml.Linq;

namespace TallyXMLReader.XmlGenerators
{
    public class AllInventoriesList
    {
        private static readonly string xmlFileName = "ALLINVENTORIES.LIST";
        
        public static void CreateAllInventoriesListXml(XElement tallyXml, params string[] args)
        {
            XElement allInvXml = XmlComponentGenerator.CreateXmlFromTemplate(xmlFileName, args);
            //now add it
        }

        public static float GetAmount()
        {
            
        }   

        public static bool IsDeemedPositive()
        {

        } 
    }
}