using System.IO;
using System.Xml.Linq;
using System.Collections.Generic;
using TallyXMLReader.CSVParser;

namespace TallyXMLReader.XmlGenerators
{
    public class TallyXmlCreator
    {
        public static void CreateTallyXmlsUsingHeaderParams(IEnumerable<HeaderCSVParams> headerCSVParams, XElement tallyXml)
        {
            //create and insert into TallyXml
            foreach (HeaderCSVParams headerParams in headerCSVParams)
            {
                //Note that voucher xml is being filled with {0} for values
                //that need to be computed during post-processing
                Voucher.CreateVoucherXml(tallyXml, headerParams.BillDate, headerParams.RecvDate, headerParams.VchType,
                             headerParams.InvNo, headerParams.PartyName, headerParams.VchNo, "{0}");

                Ledger.CreateLedgerXml(tallyXml, headerParams.PartyName, headerParams.CountryName);
            }
        }

        public static void CreateTallyXmlsUsingItemsParams(IEnumerable<ItemsCSVParams> itemsCSVParams, XElement tallyXml)
        {
            foreach (ItemsCSVParams itemParams in itemsCSVParams)
            {
                //Create and add Parent+Child Godown xmls
                if (!Godown.IsAlreadyCreated(itemParams.GodownParent, tallyXml))
                {
                    Godown.CreateGodownXml(tallyXml, "", itemParams.GodownParent);
                }

                if (!Godown.IsAlreadyCreated(itemParams.GodownChild, tallyXml))
                {
                    Godown.CreateGodownXml(tallyXml, itemParams.GodownParent, itemParams.GodownChild);
                }

                //Create and add Parent+Child StockGroup xmls
                if (!StockGroup.IsAlreadyCreated(itemParams.StockGroupParent, tallyXml))
                {
                    StockGroup.CreateStockGroupXml(tallyXml, "", itemParams.StockGroupParent);
                }

                if (!StockGroup.IsAlreadyCreated(itemParams.StockGroupChild, tallyXml))
                {
                    StockGroup.CreateStockGroupXml(tallyXml, itemParams.StockGroupParent,
                                            itemParams.StockGroupChild);
                }

                //Create and add Parent+Child StockCategory xmls
                if (!StockCategory.IsAlreadyCreated(itemParams.StockCategoryParent, tallyXml))
                {
                    StockCategory.CreateStockCategoryXml(tallyXml, "", itemParams.StockCategoryParent);
                }

                if (!StockCategory.IsAlreadyCreated(itemParams.StockCategoryChild, tallyXml))
                {
                    StockCategory.CreateStockCategoryXml(tallyXml, itemParams.StockCategoryParent,
                                            itemParams.StockCategoryChild);
                }

                //Create and add StockItem xml
                StockItem.CreateStockItemXml(tallyXml, itemParams.ItemName, itemParams.PartNo,
                    itemParams.StockGroupChild, itemParams.StockCategoryChild, itemParams.Barcode,
                    itemParams.CP, itemParams.SP, itemParams.ImgPath, itemParams.Desc1,
                    itemParams.Desc2, itemParams.Desc3);


                //Check if AllInventoryEntriesList has already been created for a stock item
                //and if not, create it (note that xml is being filled with {0} for values
                //that need to be computed during post-processing)
                if (!AllInventoryEntriesList.IsAlreadyCreated(itemParams.ItemName, tallyXml))
                {
                    AllInventoryEntriesList.CreateAllInventoryEntriesListXml(tallyXml, itemParams.Desc1,
                        itemParams.Desc2, itemParams.Desc3, itemParams.ItemName, itemParams.CP, "{0}");
                }

                //Create and add BatchAllocationsList xml
                float batchCP = -ComputationHelper.CalculateAmount(itemParams.CP, itemParams.BilledQty);
                BatchAllocationsList.CreateBatchAllocationsListXml(tallyXml, itemParams.ItemName, itemParams.GodownChild,
                    batchCP.ToString("0.00"), itemParams.ActualQty, itemParams.BilledQty);


                //MAY NEED TO ADD CODE FOR ACCOUNTINGALLOCATIONS.LIST xml. CHECK WITH DAD
                // string partyName = Voucher.GetPartyNameFromVoucherXml(tallyXml);
            }
        }

        /// <summary>
        /// For each inventory item, compute sum of BatchAllocations.list amounts, billed qty and actual qty. 
        /// Simultaneously, compute voucher amount for all inventory items
        /// </summary>
        /// <param name="tallyXml"></param>
        public static void DoPostProcessing(XElement tallyXml)
        {
            float voucherAmt = -AllInventoryEntriesList.CalculateAndFillInventoryEntryAmounts(tallyXml);

            //Add the voucher amt to the xml
            Voucher.UpdateFinalAmtInVoucherXml(voucherAmt, tallyXml);
        }
    }

    public class XmlComponentGenerator
    {
        private readonly static string xmlTemplatesDir = @"/mnt/hgfs/SharedWithVM/TallyApp/TemplateXmls";
        public static string XmlTemplatesDir { get { return xmlTemplatesDir; } }

        /// <summary>
        /// Fetches the template xml for @xmlTemplateName and fills it using values supplied in @args
        /// </summary>
        /// <param name="xmlTemplateName">template xml to be fetched and filled</param>
        /// <param name="args">values to fill in @xmlTemplateName</param>
        /// <returns></returns>
        public static XElement CreateXmlFromTemplate(string xmlTemplateName, params string[] args)
        {
            string xmlTemplateFile = XmlTemplatesDir + $"/{xmlTemplateName}.xml";

            string xmlTemplateString = File.OpenText(xmlTemplateFile).ReadToEnd();
            string filledXmlString = string.Format(xmlTemplateString, args);

            return XElement.Parse(filledXmlString);
        }
    }
}