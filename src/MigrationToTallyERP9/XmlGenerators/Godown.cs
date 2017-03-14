using System.Xml.XPath;
using System.Xml.Linq;
using System.Linq;

namespace MigrationToTallyERP9.XmlGenerators
{
    public class Godown
    {
        private static readonly string xmlFileName = "GODOWN";
        
        public static void CreateGodownXml(XElement tallyXml, params string[] args)
        {
            XElement godownXml = XmlComponentGenerator.CreateXmlFromTemplate(xmlFileName, args);
            
            //now add it to TallyXml
            XElement parentNode = tallyXml.XPathSelectElements("//REQUESTDATA/TALLYMESSAGE").First();
            parentNode.Add(godownXml);
        }

        public static bool IsAlreadyCreated(string godownName, XElement tallyXml)
        {
            XElement xml = tallyXml.XPathSelectElement($"//REQUESTDATA/TALLYMESSAGE/GODOWN[@NAME='{godownName}']");

            if(xml != null)
                return true;
            
            return false;
        }
    }
}