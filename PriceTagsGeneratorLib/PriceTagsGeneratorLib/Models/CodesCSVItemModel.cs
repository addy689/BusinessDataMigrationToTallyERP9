using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceTagsGeneratorLib.Models
{
    public class CodesCSVItemModel
    {
        public string Code
        { get; set; }

        public string Price
        { get; set; }

        public override string ToString()
        {
            return string.Format("{0}, {1}", Code, Price);
        }
    }
}
