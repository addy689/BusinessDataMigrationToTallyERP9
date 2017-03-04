using System;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Collections.Generic;

using TallyXMLReader.CSVParser;
using TallyXMLReader.XmlGenerators;

namespace TallyXMLReader
{
    public class Program
    {
        public static void Main(string[] args)
        {
            XElement tallyXmlDoc = XElement.Load(@"/mnt/hgfs/SharedWithVM/TallyApp/TemplateXmls/TallyXmlTemplate.xml");

            // var stockItemName = "AL-RG Necklace";
            // XElement xml = tallyXmlDoc.XPathSelectElement($"//REQUESTDATA//VOUCHER/ALLINVENTORYENTRIES.LIST/STOCKITEMNAME[.='{stockItemName}']");
            
            // string a = "-40";
            // var r = float.Parse(a);
            // Console.WriteLine(r);
                        

            // string groupName = "Alloy";
            // XElement b = tallyXmlDoc.XPathSelectElement($"//REQUESTDATA/TALLYMESSAGE/STOCKGROUP[@NAME='{groupName}']");

            // Console.WriteLine(tallyXmlDoc.Element("CURRENCY").Name.ToString());

            var headerCSVParams = ParseCSVFiles.ParseHeaderCSV(XmlComponentGenerator.XmlTemplatesDir + @"/SampleHeaderCSV.xml");
            var itemsCSVParams = ParseCSVFiles.ParseItemsCSV(XmlComponentGenerator.XmlTemplatesDir + @"/SampleItemCSV.xml");

            TallyXmlCreator.CreateTallyXmlsUsingHeaderParams(headerCSVParams, tallyXmlDoc);
            TallyXmlCreator.CreateTallyXmlsUsingItemsParams(itemsCSVParams, tallyXmlDoc);
            TallyXmlCreator.DoPostProcessing(tallyXmlDoc);
            
            Console.WriteLine(tallyXmlDoc.ToString());
            //For each csv item, 
            //- fill all possible xml templates, and append to the TallyXML DOM object
            //Finally, compute total in Voucher from all entries in ALLINVENTORIES.LIST
            
        }

    }


}
