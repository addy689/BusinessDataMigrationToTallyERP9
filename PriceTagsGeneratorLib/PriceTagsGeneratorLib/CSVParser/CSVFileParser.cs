using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;

namespace PriceTagsGeneratorLib.CSVParser
{
    class CSVFileParser
    {
        public static IEnumerable<T> Parse<T, TMap>(string csvFile)
            where T : class 
            where TMap : CsvClassMap<T>
        {
            TextReader tr = File.OpenText(csvFile);

            var csv = new CsvReader(tr);
            csv.Configuration.RegisterClassMap<TMap>();
            csv.Configuration.TrimFields = true;

            return csv.GetRecords<T>();
        }
    }
}
