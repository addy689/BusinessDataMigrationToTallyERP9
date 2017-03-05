using System.IO;
using System.Collections.Generic;
using CsvHelper;

namespace MigrationToTallyERP9.CSVParser
{
    public class ParseCSVFiles
    {
        public static IEnumerable<HeaderCSVParams> ParseHeaderCSV(string headerCSVFile)
        {
            TextReader tr = File.OpenText(headerCSVFile);

            var csv = new CsvReader(tr);
            csv.Configuration.RegisterClassMap<HeaderCSVParamsMap>();
                
            return csv.GetRecords<HeaderCSVParams>();
        }

        public static IEnumerable<ItemsCSVParams> ParseItemsCSV(string itemsCSVFile)
        {
            TextReader tr = File.OpenText(itemsCSVFile);

            var csv = new CsvReader(tr);
            csv.Configuration.RegisterClassMap<ItemsCSVParamsMap>();
                
            return csv.GetRecords<ItemsCSVParams>();
        }
    }
}