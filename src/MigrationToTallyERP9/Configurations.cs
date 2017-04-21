namespace MigrationToTallyERP9
{
    public class Configurations
    {
        public static string XmlTemplatesDir
        {
            get
            {
                return @"/home/addy689/VSCodeProjects/DotNetCoreApps/PamJoeHandcraftedApps/src/MigrationToTallyERP9/XmlGenerators/TemplateXmlFiles";
            }
        }
        
        public static string InputCSVDataDir
        {
            get
            {
                return @"/home/addy689/VSCodeProjects/DotNetCoreApps/PamJoeHandcraftedApps/src/MigrationToTallyERP9/CSVParser/InputCSVData";
            }
        }

        public static string OutputDir
        {
            get
            {
                return @"/home/addy689/VSCodeProjects/DotNetCoreApps/PamJoeHandcraftedApps/src/MigrationToTallyERP9/OutputXmlsForImport";
            }
        }

        public static string BarcodeCSVFilePath
        {
            get
            {
                return InputCSVDataDir + "/AssignedBarcodes.csv";
            }
        }

        public static string TallyItemsCsvFileNameWithExtension
        {
            get
            {
                return "SampleItems.csv";
            }
        }

        public static string TallyHeaderCsvFileNameWithExtension
        {
            get
            {
                return "SampleHeader.csv";
            }
        }
    }
}