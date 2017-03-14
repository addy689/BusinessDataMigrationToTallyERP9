using System.IO;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Linq;
using System.Collections.Generic;
using MigrationToTallyERP9.CSVParser;

namespace MigrationToTallyERP9.XmlGenerators
{
    public class TallyXmlCreator
    {
        private static HeaderCSVParams headerParams;
        private static XElement voucherXmlElement;
        public static void CreateTallyXmlsUsingHeaderParams(IEnumerable<HeaderCSVParams> headerCSVParams, XElement tallyXml)
        {
            headerParams = headerCSVParams.First();

            //Note that voucher xml is being filled with {0} for values
            //that need to be computed during post-processing
            voucherXmlElement = Voucher.CreateVoucherXml(tallyXml, headerParams.VchRemoteId, headerParams.BillDate, headerParams.RecvDate, headerParams.VchType,
                            headerParams.InvNo, headerParams.PartyName, headerParams.VchNo, "{0}");

            Ledger.CreatePartyLedgerXml(tallyXml, headerParams.PartyName, headerParams.CountryName);
            Ledger.CreatePurchaseLedgerXml(tallyXml, headerParams.PurchLedger);
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
                if (!StockItem.IsAlreadyCreated(itemParams.ItemName, tallyXml))
                {
                    StockItem.CreateStockItemXml(tallyXml, itemParams.ItemName, itemParams.PartNo,
                        itemParams.StockGroupChild, itemParams.StockCategoryChild, itemParams.Barcode,
                        itemParams.CP, itemParams.SP, itemParams.ImgPath, itemParams.Desc1,
                        itemParams.Desc2, itemParams.Desc3);
                }

                //Check if AllInventoryEntriesList has already been created for a stock item
                //and if not, create it (note that xml is being filled with {0} for values
                //that need to be computed during post-processing)
                if (!AllInventoryEntriesList.IsAlreadyCreated(itemParams.ItemName, voucherXmlElement))
                {
                    AllInventoryEntriesList.CreateAllInventoryEntriesListXml(voucherXmlElement, itemParams.Desc1,
                        itemParams.Desc2, itemParams.Desc3, itemParams.ItemName, itemParams.CP, "{0}",
                        headerParams.PurchLedger);
                }

                //Create and add BatchAllocationsList xml
                float batchCP = -ComputationHelper.CalculateAmount(itemParams.CP, itemParams.BilledQty);
                BatchAllocationsList.CreateBatchAllocationsListXml(voucherXmlElement, itemParams.ItemName, itemParams.GodownChild,
                    batchCP.ToString("0.00"), itemParams.ActualQty, itemParams.BilledQty);
            }
        }

        /// <summary>
        /// For each inventory item, compute sum of BatchAllocations.list amounts, billed qty and actual qty. 
        /// Simultaneously, compute voucher amount for all inventory items
        /// </summary>
        /// <param name="tallyXml"></param>
        public static void DoPostProcessing(XElement tallyXml)
        {
            float voucherAmt = -AllInventoryEntriesList.CalculateAndFillInventoryEntryAmounts(voucherXmlElement);

            //Add the voucher amt to the xml
            Voucher.UpdateFinalAmtInVoucherXml(voucherAmt, voucherXmlElement);

            //Add voucher xml to tally XML
            XElement parentNode = tallyXml.XPathSelectElements("//REQUESTDATA/TALLYMESSAGE").First();

            parentNode.Add(voucherXmlElement);
        }        
    }

    public class XmlComponentGenerator
    {
        public static XElement TallyXml
        {
            get { return XElement.Load(Configurations.XmlTemplatesDir + "TALLYMAIN.xml"); }
        }

        /// <summary>
        /// Fetches the template xml for @xmlTemplateName and fills it using values supplied in @args
        /// </summary>
        /// <param name="xmlTemplateName">template xml to be fetched and filled</param>
        /// <param name="args">values to fill in @xmlTemplateName</param>
        /// <returns></returns>
        public static XElement CreateXmlFromTemplate(string xmlTemplateName, params string[] args)
        {
            string xmlTemplateFile = Configurations.XmlTemplatesDir + $"{xmlTemplateName}.xml";

            string xmlTemplateString = File.OpenText(xmlTemplateFile).ReadToEnd();
            string filledXmlString = string.Format(xmlTemplateString, args);

            return XElement.Parse(filledXmlString);
        }
    }
}