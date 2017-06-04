using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Text;
using System.Threading.Tasks;

namespace PriceTagsGeneratorLib.Models
{
    public interface ICSVModel
    {
        List<CodesCSVItemModel> CSVRecords { get; }
        void Process();
    }
}
