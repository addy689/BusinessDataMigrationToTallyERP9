using System;
using System.Collections.Generic;
using System.Text;

namespace PriceTagsGeneratorLib
{
    public interface IConfigReader
    {
        //returns a configuration value from the config store using the 'key' parameter
        string GetConfiguration(string key);

        //returns the complete pathname of the formatted price tags spreadsheet (output file)
        string GetOutputFileName();

        //calculates and returns the maximum item tags that can be accommodated in a spreadsheet page
        int MaximumItemTagsPerPage { get; }
    }
}
