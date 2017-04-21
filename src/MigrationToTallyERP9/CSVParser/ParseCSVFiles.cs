using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using CsvHelper;
using CsvHelper.Configuration;

namespace MigrationToTallyERP9.CSVParser
{
    public class ParseCSVFiles
    {
        public static IEnumerable<T> ParseCSV<T,TMap>(string inputCSVFile)
            where T:class
            where TMap:CsvClassMap<T>
        {
            TextReader tr = File.OpenText(inputCSVFile);

            var csv = new CsvReader(tr);
            csv.Configuration.RegisterClassMap<TMap>();
            csv.Configuration.TrimFields = true;
                
            return csv.GetRecords<T>();
        }

        public static string GetValidatedInput(string inputStr)
        {
            string correctedStr = inputStr;
            
            if(correctedStr.Contains("--"))
                correctedStr = correctedStr.Replace("--", "- -");
            
            if(correctedStr.Contains("&"))
                correctedStr = correctedStr.Replace('&','-');
            
            if(correctedStr.Contains("\""))
            {
                //Determine if the " occurs after a number, if so replace " with i
                //else replace " with empty string
                Regex regex = new Regex(".[0-9]+\"");
                var matches = regex.Matches(correctedStr);

                if(matches[0].Success)
                    correctedStr = correctedStr.Replace("\"","i");
                else
                    correctedStr = correctedStr.Replace("\"","");
            }

            return correctedStr;
        }

    }

}