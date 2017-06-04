using System.Xml.Linq;
using System.Xml.XPath;
using System.Linq;

namespace MigrationToTallyERP9.XmlGenerators
{
    public class AllInventoryEntriesList
    {
        private static readonly string xmlFileName = "ALLINVENTORYENTRIES.LIST";
        
        public static void CreateAllInventoryEntriesListXml(XElement voucherXml, params string[] args)
        {
            XElement allInvXml = XmlComponentGenerator.CreateXmlFromTemplate(xmlFileName, args);
            
            //now add it to TallyXml
            voucherXml.Add(allInvXml);
        }

        public static bool IsAlreadyCreated(string stockItemName, XElement voucherXml)
        {
            XElement xml = voucherXml.XPathSelectElement($"./ALLINVENTORYENTRIES.LIST/STOCKITEMNAME[.='{stockItemName}']");

            if(xml != null)
                return true;
            
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="voucherXml"></param>
        /// <returns>Total Voucher amount</returns>
        public static double CalculateAndFillInventoryEntryAmounts(XElement voucherXml)
        {
            float totalAmt = 0.0f;

            var allInventoryEntriesList = voucherXml.XPathSelectElements("./ALLINVENTORYENTRIES.LIST");
            foreach (var entry in allInventoryEntriesList)
            {
                float inventoryEntryAmt;
                int billedQty, actualQty;
                string isDeemedPositive;

                //Sum all the BatchAllocationsList values, and add them to the inventory entry xml...
                inventoryEntryAmt = entry.XPathSelectElements("./BATCHALLOCATIONS.LIST/AMOUNT")
                                .Sum(x => float.Parse(x.Value));
                
                billedQty = entry.XPathSelectElements("./BATCHALLOCATIONS.LIST/BILLEDQTY")
                                .Sum(x => int.Parse(ComputationHelper.ExtractNumericQtyFromString(x.Value)));

                actualQty = entry.XPathSelectElements("./BATCHALLOCATIONS.LIST/ACTUALQTY")
                                .Sum(x => int.Parse(ComputationHelper.ExtractNumericQtyFromString(x.Value)));
                
                isDeemedPositive = ComputationHelper.IsDeemedPositive(inventoryEntryAmt);

                //...by filling all the {0} strings in the AllInventoryEntriesList xml
                entry.XPathSelectElement("./AMOUNT").Value = 
                            string.Format(entry.XPathSelectElement("./AMOUNT").Value, inventoryEntryAmt.ToString("0.00"));
                
                entry.XPathSelectElement("./BILLEDQTY").Value = 
                            string.Format(entry.XPathSelectElement("./BILLEDQTY").Value, billedQty.ToString());
                 
                entry.XPathSelectElement("./ACTUALQTY").Value = 
                            string.Format(entry.XPathSelectElement("./ACTUALQTY").Value, actualQty.ToString());
                
                entry.XPathSelectElement("./ISDEEMEDPOSITIVE").Value = 
                            string.Format(entry.XPathSelectElement("./ISDEEMEDPOSITIVE").Value, isDeemedPositive);                                                                         
                
                entry.XPathSelectElement("./ACCOUNTINGALLOCATIONS.LIST/AMOUNT").Value = 
                            string.Format(entry.XPathSelectElement("./ACCOUNTINGALLOCATIONS.LIST/AMOUNT").Value, inventoryEntryAmt.ToString("0.00"));
                
                entry.XPathSelectElement("./ACCOUNTINGALLOCATIONS.LIST/ISDEEMEDPOSITIVE").Value = 
                            string.Format(entry.XPathSelectElement("./ACCOUNTINGALLOCATIONS.LIST/ISDEEMEDPOSITIVE").Value, isDeemedPositive);                                                                         
                
                
                //update the total voucher amount
                totalAmt += inventoryEntryAmt;
            }

            return totalAmt;
        }
    
    }
}