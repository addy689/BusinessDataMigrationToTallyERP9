using System.Xml.Linq;
using System.IO;
using MigrationToTallyERP9.CSVParser;
using MigrationToTallyERP9.XmlGenerators;

namespace MigrationToTallyERP9
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Load the main Tally XML document to which data is to be added
            XElement tallyXmlDoc = XmlComponentGenerator.TallyXml;

            //Parse the input CSVs using CSVHelper library
            var headerCSVParams = ParseCSVFiles.ParseHeaderCSV(Configurations.InputCSVDataDir + @"/SampleHeader.csv");
            var itemsCSVParams = ParseCSVFiles.ParseItemsCSV(Configurations.InputCSVDataDir + @"/SampleItems.csv");

            //Create the XML data components to be added using the parsed CSV and template Xmls
            TallyXmlCreator.CreateTallyXmlsUsingHeaderParams(headerCSVParams, tallyXmlDoc);
            TallyXmlCreator.CreateTallyXmlsUsingItemsParams(itemsCSVParams, tallyXmlDoc);
            TallyXmlCreator.DoPostProcessing(tallyXmlDoc);

            //??WHAT ABOUT ACCOUNTINGENTRIES.LISTs            
            FileStream fs = new FileStream(Configurations.XmlTemplatesDir + @"/FinalTallyXml.xml", FileMode.Create);
            tallyXmlDoc.Save(fs);
        }

    }


}
