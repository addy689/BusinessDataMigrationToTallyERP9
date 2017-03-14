namespace MigrationToTallyERP9
{
    public class Configurations
    {
        public static string XmlTemplatesDir
        {
            get
            {
                return @"/home/addy689/VSCodeProjects/DotNetCoreApps/PamJoeHandcraftedApps/src/MigrationToTallyERP9/XmlGenerators/TemplateXmlFiles/";
            }
        }
        
        public static string InputCSVDataDir
        {
            get
            {
                return @"/home/addy689/VSCodeProjects/DotNetCoreApps/PamJoeHandcraftedApps/src/MigrationToTallyERP9/CSVParser/InputCSVData/";
            }
        }

        public static string OutputDir
        {
            get
            {
                return @"/home/addy689/VSCodeProjects/DotNetCoreApps/PamJoeHandcraftedApps/src/MigrationToTallyERP9/";
            }
        }
    }
}