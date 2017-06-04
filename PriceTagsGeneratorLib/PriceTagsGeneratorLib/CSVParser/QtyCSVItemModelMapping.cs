using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CsvHelper.Configuration;
using PriceTagsGeneratorLib.Models;

namespace PriceTagsGeneratorLib.CSVParser
{
    class QtyCSVItemModelMapping : CsvClassMap<QtyCSVItemModel>
    {
        public QtyCSVItemModelMapping()
        {
            Map(m => m.Qty).Name("QUANTITY");
            Map(m => m.Price).Name("PRICE");
        }
    }
}
