using System.Xml.Linq;
using System.IO;
using MigrationToTallyERP9.CSVParser;
using MigrationToTallyERP9.XmlGenerators;

namespace MigrationToTallyERP9
{
    public class TallyMigrator
    {
        public static void Migrate(string headerCSV, string itemsCSV)
        {
            //Load the main Tally XML document to which data is to be added
            XElement tallyXmlDoc = XmlComponentGenerator.TallyXml;

            //Parse the input CSVs using CSVHelper library
            var headerCSVParams = ParseCSVFiles.ParseCSV<HeaderCSVParams, HeaderCSVParamsMap>(headerCSV);
            var itemsCSVParams = ParseCSVFiles.ParseCSV<ItemsCSVParams, ItemsCSVParamsMap>(itemsCSV);

            //Create the XML data components to be added using the parsed CSV and template Xmls
            TallyXmlCreator.CreateTallyXmlsUsingHeaderParams(headerCSVParams, tallyXmlDoc);
            TallyXmlCreator.CreateTallyXmlsUsingItemsParams(itemsCSVParams, tallyXmlDoc);
            TallyXmlCreator.DoPostProcessing(tallyXmlDoc);

            //Write final xml to disk
            FileStream fs = new FileStream(Configurations.OutputDir + "TallyInventoryImport.xml", FileMode.Create);
            tallyXmlDoc.Save(fs);
        }

    }


}
