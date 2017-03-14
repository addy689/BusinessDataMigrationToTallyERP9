using System;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;

namespace MigrationToTallyERP9.XmlGenerators
{
    public class BatchAllocationsList
    {
        private static readonly string xmlFileName = "BATCHALLOCATIONS.LIST";

        public static void CreateBatchAllocationsListXml(XElement voucherXml, string itemName, params string[] args)
        {
            XElement batchAllocXml = XmlComponentGenerator.CreateXmlFromTemplate(xmlFileName, args);
            
            AddXmlToCorrespondingInventoriesList(voucherXml, batchAllocXml, itemName);
        }

        /// <summary>
        /// Adds an item's BATCHALLOCATIONS.LIST to it's corresponding ALLINVENTORYENTRIES.LIST
        /// </summary>
        /// <param name="voucherXml"></param>
        /// <param name="batchAllocXml"></param>
        /// <param name="stockItemName"></param>
        private static void AddXmlToCorrespondingInventoriesList(XElement voucherXml, XElement batchAllocXml, string stockItemName)
        {
            //Add BatchAllocationXml to the AllInventory list that has the same item name
            XElement[] res = voucherXml.XPathSelectElements("./ALLINVENTORYENTRIES.LIST").Where<XElement>(el => el.XPathSelectElement($"./STOCKITEMNAME[.='{stockItemName}']") != null).ToArray();

            if(res.Length<=0)
            {
                Console.WriteLine($"Error could not find corresponsing AllInventoryList for {stockItemName}");
                return;
            }
            
            /* If no BatchAllocations xml present, then add the xml at the end of AllInventory.List xml
            otherwise add next to the last BatchAllocations xml */
            var batchAllocNodes = res[0].XPathSelectElements("./BATCHALLOCATIONS.LIST").ToArray();
            
            if(batchAllocNodes.Count() == 0)
                res[0].Add(batchAllocXml);
            else
                batchAllocNodes.Last().AddAfterSelf(batchAllocXml);
        }

    }
}