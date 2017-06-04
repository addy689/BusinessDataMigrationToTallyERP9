using System;
using System.Collections.Generic;
using System.Linq;
using PriceTagsGeneratorLib.CSVParser;

namespace PriceTagsGeneratorLib.Models
{
    public class CodesCSVModel : ICSVModel
    {
        string csvFilePath;
        
        List<CodesCSVItemModel> csvRecords;

        public CodesCSVModel(string csvFilePath)
        {
            this.csvFilePath = csvFilePath;
        }

        #region Interface members
        public List<CodesCSVItemModel> CSVRecords
        {
            get { return csvRecords; }
        }
        
        public void Process()
        {
            csvRecords = CSVFileParser.Parse<CodesCSVItemModel, CodesCSVItemModelMapping>(this.csvFilePath).ToList();
        }
        #endregion

    }
}
