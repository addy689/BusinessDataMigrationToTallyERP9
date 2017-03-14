using System.IO;
using System.Collections.Generic;
using CsvHelper;
using CsvHelper.Configuration;

namespace MigrationToTallyERP9.CSVParser
{
    public class ParseCSVFiles
    {
        public static IEnumerable<T> ParseCSV<T,TMap>(string headerCSVFile)
            where T:class
            where TMap:CsvClassMap<T>
        {
            TextReader tr = File.OpenText(headerCSVFile);

            var csv = new CsvReader(tr);
            csv.Configuration.RegisterClassMap<TMap>();
            csv.Configuration.TrimFields = true;
                
            return csv.GetRecords<T>();
        }

    }
}