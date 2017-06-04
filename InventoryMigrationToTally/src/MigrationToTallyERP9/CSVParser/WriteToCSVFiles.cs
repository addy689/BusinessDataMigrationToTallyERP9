using System.Collections.Generic;
using System.IO;
using CsvHelper;

namespace MigrationToTallyERP9.CSVParser
{
    public class WriteToCSVFiles
    {
        private static CsvWriter csv;
        public static void InitiateBarcodesLogging()
        {
            FileStream fs = new FileStream(Configurations.BarcodeCSVFilePath, FileMode.Append);
            var sw = new StreamWriter(fs);
            csv = new CsvWriter(sw);
        }
        
        public static void WriteBarcodesDataToFile(BarcodeCSVParams dataToWrite)
        {
            csv.WriteField(dataToWrite.FirstBarcodeAssigned);
            csv.WriteField(dataToWrite.LastBarcodeAssigned);
            csv.WriteField(dataToWrite.AssociatedOutputXml);
            csv.WriteField(dataToWrite.TimeStamp);
            csv.NextRecord();
        }

        public static void CloseBarcodesLogging()
        {
            csv.Dispose();
        }
    }
}