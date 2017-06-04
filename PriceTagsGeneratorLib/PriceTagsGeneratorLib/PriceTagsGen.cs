using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using PriceTagsGeneratorLib.Models;

namespace PriceTagsGeneratorLib
{
    public class PriceTagsGen
    {
        public void ProcessInputs(List<ICSVModel> inputData, IConfigReader configReader)
        {
            List<CodesCSVItemModel> itemsList = new List<CodesCSVItemModel>();

            foreach (var csvModel in inputData)
            {
                //Parse the CSV file
                csvModel.Process();

                //Append the records to the final list
                itemsList.AddRange(csvModel.CSVRecords);
            }

            SpreadsheetGenerator sg = new SpreadsheetGenerator();

            var columnsInPage = int.Parse(configReader.GetConfiguration("MaximumSpreadsheetColumnsToUse"));
            var spreadsheetDataRows = sg.ConstructNonFormattedSpreadsheetData(itemsList, columnsInPage);

            sg.ConvertToRichlyFormattedSpreadsheet(spreadsheetDataRows, configReader.GetOutputFileName(), configReader);

            //foreach (var i in a)
            //{
            //    Console.WriteLine();
            //    foreach (var j in i)
            //        Console.WriteLine(j.Code);
            //}
            //var a = configReader.GetLibSettings("SpreadsheetColumnsToCreate");
            //Console.WriteLine(a);
            //foreach (var item in itemsList)
            //    Console.WriteLine(item);
            //Console.ReadLine();
        }
    }
}
