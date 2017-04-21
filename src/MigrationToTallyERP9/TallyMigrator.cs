using System;
using System.Linq;
using System.Xml.Linq;
using System.IO;
using MigrationToTallyERP9.CSVParser;
using MigrationToTallyERP9.XmlGenerators;

namespace MigrationToTallyERP9
{
    public class TallyMigrator
    {
        public static void ProcessDataDirs()
        {
            //Clean output directory so that older files do not get copied to Google Drive again 
            DirectoryInfo di = new DirectoryInfo(Configurations.OutputDir);
            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }

            //Open log file for writing assigned barcode range corresponding to each input data directory
            WriteToCSVFiles.InitiateBarcodesLogging();
            
            //traverse each directory and process the two CSVs 
            var inputDataDirs = Directory.EnumerateDirectories(Configurations.InputCSVDataDir);
            foreach (var dir in inputDataDirs)
            {
                var headerFile = dir + "/" + Configurations.TallyHeaderCsvFileNameWithExtension;
                var itemsFile = dir + "/" + Configurations.TallyItemsCsvFileNameWithExtension;

                TallyMigrator.Migrate(headerFile, itemsFile, Path.GetFileName(dir));
            }

            WriteToCSVFiles.CloseBarcodesLogging();
        }

        public static void Migrate(string headerCSV, string itemsCSV, string outputFileNameSuffix)
        {
            string timeStamp = DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss");
            string outputXmlFileName = $"{timeStamp}_TallyInventoryImport_{outputFileNameSuffix}.xml";

            //Load the main Tally XML document to which data is to be added
            XElement tallyXmlDoc = XmlComponentGenerator.TallyXml;

            //Parse the input CSVs using CSVHelper library
            var headerCSVParams = ParseCSVFiles.ParseCSV<HeaderCSVParams, HeaderCSVParamsMap>(headerCSV);
            var itemsCSVParams = ParseCSVFiles.ParseCSV<ItemsCSVParams, ItemsCSVParamsMap>(itemsCSV);

            //Create the XML data components to be added using the parsed CSV and template Xmls
            TallyXmlCreator tallyXmlCreator = new TallyXmlCreator(headerCSVParams.First(), itemsCSVParams, tallyXmlDoc);
            tallyXmlCreator.Create(outputXmlFileName);

            //Write final xml to disk
            if (!Directory.Exists(Configurations.OutputDir))
                Directory.CreateDirectory(Configurations.OutputDir);

            FileStream fs = new FileStream(Configurations.OutputDir + "/" + outputXmlFileName, FileMode.Create);
            tallyXmlDoc.Save(fs);            
        }

    }


}
