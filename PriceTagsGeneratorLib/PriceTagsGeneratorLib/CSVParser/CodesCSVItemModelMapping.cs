using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CsvHelper.Configuration;
using PriceTagsGeneratorLib.Models;


namespace PriceTagsGeneratorLib.CSVParser
{
    class CodesCSVItemModelMapping : CsvClassMap<CodesCSVItemModel>
    {
        public CodesCSVItemModelMapping()
        {
            Map(m => m.Code).Name("CODE");
            Map(m => m.Price).Name("PRICE");
        }
    }
}
