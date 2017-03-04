using System;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;

namespace TallyXMLReader.XmlGenerators
{
    public class BatchAllocationsList
    {
        private static readonly string xmlFileName = "BATCHALLOCATIONS.LIST";

        public static void CreateBatchAllocationsListXml(XElement tallyXml, string itemName, params string[] args)
        {
            XElement batchAllocXml = XmlComponentGenerator.CreateXmlFromTemplate(xmlFileName, args);
            
            AddXmlToCorrespondingInventoriesList(tallyXml, batchAllocXml, itemName);
        }

        /// <summary>
        /// Adds an item's BATCHALLOCATIONS.LIST to it's corresponding ALLINVENTORIES.LIST
        /// </summary>
        /// <param name="tallyXml"></param>
        /// <param name="batchAllocXml"></param>
        /// <param name="stockItemName"></param>
        private static void AddXmlToCorrespondingInventoriesList(XElement tallyXml, XElement batchAllocXml, string stockItemName)
        {
            //Add BatchAllocationXml to the AllInventory list that has the same item name
            XElement[] res = tallyXml.XPathSelectElements("//REQUESTDATA//VOUCHER/ALLINVENTORIES.LIST").Where<XElement>(el => el.XPathSelectElement($"./STOCKITEMNAME[.='{stockItemName}']") != null).ToArray();

            if(res.Length<=0)
            {
                Console.WriteLine($"Error could not find corresponsing AllInventoryList for {stockItemName}");
                return;
            }
            
            /* If no BatchAllocations xml present, then add the xml at the end of AllInventory.List xml
            otherwise add next to the last BatchAllocations xml */
            var batchAllocNodes = res[0].XPathSelectElements("./BATCHALLOCATIONS.LIST");
            
            if(batchAllocNodes==null)
                res[0].LastNode.AddAfterSelf(batchAllocXml);
            else
                batchAllocNodes.Last().AddAfterSelf(batchAllocXml);
        }

    }
}