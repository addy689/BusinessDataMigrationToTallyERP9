using System;
using System.Collections.Generic;
using System.Linq;
using PriceTagsGeneratorLib.CSVParser;

namespace PriceTagsGeneratorLib.Models
{
    public class QtyCSVModel : ICSVModel
    {
        string csvFilePath;
        int beginCode;
        string codePrefixStr;
        
        List<CodesCSVItemModel> csvRecords;

        public QtyCSVModel(string csvFilePath, int beginCode, string codePrefixStr)
        {
            this.csvFilePath = csvFilePath;
            this.beginCode = beginCode;
            this.codePrefixStr = codePrefixStr;
        }

        #region Interface members
        public List<CodesCSVItemModel> CSVRecords
        {
            get { return csvRecords; }
        }

        public void Process()
        {
            IEnumerable<QtyCSVItemModel> qtyBasedRecords = CSVFileParser.Parse<QtyCSVItemModel, QtyCSVItemModelMapping>(this.csvFilePath);

            //Convert qty-based records to code-based records, utilising beginCode and codePrefixStr
            List<CodesCSVItemModel> codeBasedRecords = new List<CodesCSVItemModel>();
            int currentCode = this.beginCode;
            int qty;
            foreach (var record in qtyBasedRecords)
            {
                if (!int.TryParse(record.Qty, out qty))
                    throw new FormatException(string.Format("Quantity {0} (for price {1}) is not an integer!", record.Qty, record.Price));

                for (int i = 0; i < qty; i++)
                {
                    codeBasedRecords.Add(new CodesCSVItemModel
                    {
                        Code = this.codePrefixStr + currentCode.ToString(),
                        Price = record.Price
                    });

                    currentCode++;
                }
            }

             csvRecords = codeBasedRecords;
        }
        #endregion
    }
}
