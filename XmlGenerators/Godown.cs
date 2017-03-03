using System.Xml.Linq;

namespace TallyXMLReader.XmlGenerators
{
    public class Godown
    {
        private static readonly string xmlFileName = "GODOWN";
        
        public static void CreateGodownXml(XElement tallyXml, params string[] args)
        {
            XElement godownXml = XmlComponentGenerator.CreateXmlFromTemplate(xmlFileName, args);
            //now add it
        }

        public static bool CheckIfAlreadyCreated(string childGodownName, XElement xml)
        {
            return true;
        }
    }
}