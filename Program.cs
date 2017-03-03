using System.Xml.Linq;
using TallyXMLReader.CSVParser;
using TallyXMLReader.XmlGenerators;

namespace TallyXMLReader
{
    public class Program
    {
        public static void Main(string[] args)
        {
            XElement tallyXmlDoc = XElement.Load(@"/mnt/hgfs/SharedWithVM/TallyApp/TemplateXmls/TallyXmlTemplate.xml");

            var headerCSVParams = ParseCSVFiles.ParseHeaderCSV(XmlComponentGenerator.XmlTemplatesDir + @"/HeaderCSV.xml");
            var itemsCSVParams = ParseCSVFiles.ParseItemsCSV(XmlComponentGenerator.XmlTemplatesDir + @"/HeaderCSV.xml");

            TallyXmlCreator.CreateTallyXmlsUsingHeaderParams(headerCSVParams, tallyXmlDoc);
            TallyXmlCreator.CreateTallyXmlsUsingItemsParams(itemsCSVParams, tallyXmlDoc);

            //create Data model for deserializing CSV
            //Read the main TallyXmlTemplate using XElement to get the DOM object
            //For each csv item, 
            //- fill all possible xml templates, and append to the TallyXML DOM object
            //Finally, compute total in Voucher from all entries in ALLINVENTORIES.LIST
            
        }

    }


}
