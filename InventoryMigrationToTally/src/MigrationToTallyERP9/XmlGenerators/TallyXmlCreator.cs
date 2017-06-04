using System;
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
        private XElement tallyXml;
        private HeaderCSVParams headerCSVParams;
        private IEnumerable<ItemsCSVParams> itemsCSVParams;
        
        private XElement voucherXmlElement;
                
        public TallyXmlCreator(HeaderCSVParams headerCSVParams, IEnumerable<ItemsCSVParams> itemsCSVParams, XElement tallyXml)
        {
            this.headerCSVParams = headerCSVParams;
            this.itemsCSVParams = itemsCSVParams;
            this.tallyXml = tallyXml;
        }
        
        public void Create(string outputXmlFileName)
        {
            CreateTallyXmlsUsingHeaderParams();
            CreateTallyXmlsUsingItemsParams(outputXmlFileName);
            DoPostProcessing();
        }

        private void CreateTallyXmlsUsingHeaderParams()
        {
            //Note that voucher xml is being filled with {0} for values
            //that need to be computed during post-processing
            voucherXmlElement = Voucher.CreateVoucherXml(tallyXml, headerCSVParams.VchRemoteId, headerCSVParams.BillDate, headerCSVParams.RecvDate, headerCSVParams.VchType,
                            headerCSVParams.InvNo, headerCSVParams.PartyName, headerCSVParams.VchNo, "{0}");

            Ledger.CreatePartyLedgerXml(tallyXml, headerCSVParams.PartyName, headerCSVParams.CountryName);
            Ledger.CreatePurchaseLedgerXml(tallyXml, headerCSVParams.PurchLedger);
        }

        private void CreateTallyXmlsUsingItemsParams(string outputXmlFileName)
        {
            List<string> assignedBarcodesList = new List<string>();

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
                    //Get the next available barcode and assign it
                    itemParams.Barcode = BarcodeValueGen.Instance.GetMeNewBarcode().ToString();
                    assignedBarcodesList.Add(itemParams.Barcode);

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
                        headerCSVParams.PurchLedger);
                }

                //Create and add BatchAllocationsList xml
                float batchCP = -ComputationHelper.CalculateAmount(itemParams.CP, itemParams.BilledQty);
                BatchAllocationsList.CreateBatchAllocationsListXml(voucherXmlElement, itemParams.ItemName, itemParams.GodownChild,
                    Math.Round(batchCP, 2).ToString("0.00"), itemParams.ActualQty, itemParams.BilledQty);
            }

            BarcodeCSVParams barcodesLogData = new BarcodeCSVParams();
            barcodesLogData.FirstBarcodeAssigned = assignedBarcodesList.First();
            barcodesLogData.LastBarcodeAssigned = assignedBarcodesList.Last();
            barcodesLogData.AssociatedOutputXml = outputXmlFileName;
            barcodesLogData.TimeStamp = DateTime.Now.ToString();
            
            WriteToCSVFiles.WriteBarcodesDataToFile(barcodesLogData);
        }

        /// <summary>
        /// For each inventory item, compute sum of BatchAllocations.list amounts, billed qty and actual qty. 
        /// Simultaneously, compute voucher amount for all inventory items
        /// </summary>
        private void DoPostProcessing()
        {
            double voucherAmt = -AllInventoryEntriesList.CalculateAndFillInventoryEntryAmounts(voucherXmlElement);

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
            get { return XElement.Load(Configurations.XmlTemplatesDir + "/TALLYMAIN.xml"); }
        }

        /// <summary>
        /// Fetches the template xml for @xmlTemplateName and fills it using values supplied in @args
        /// </summary>
        /// <param name="xmlTemplateName">template xml to be fetched and filled</param>
        /// <param name="args">values to fill in @xmlTemplateName</param>
        /// <returns></returns>
        public static XElement CreateXmlFromTemplate(string xmlTemplateName, params string[] args)
        {
            string xmlTemplateFile = Configurations.XmlTemplatesDir + $"/{xmlTemplateName}.xml";

            string xmlTemplateString = File.OpenText(xmlTemplateFile).ReadToEnd();
            string filledXmlString = string.Format(xmlTemplateString, args);

            return XElement.Parse(filledXmlString);
        }
    }
}