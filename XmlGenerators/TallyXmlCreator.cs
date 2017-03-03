using System;
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
                Voucher.CreateVoucherXml(tallyXml, headerParams.BillDate, headerParams.RecvDate, headerParams.VchType,
                             headerParams.InvNo, headerParams.PartyName, headerParams.VchNo);

                Ledger.CreateLedgerXml(tallyXml, headerParams.PartyName, headerParams.CountryName);
            }
        }

        public static void CreateTallyXmlsUsingItemsParams(IEnumerable<ItemsCSVParams> itemsCSVParams, XElement tallyXml)
        {
            foreach (ItemsCSVParams itemParams in itemsCSVParams)
            {
                Godown.CreateGodownXml(tallyXml, "", itemParams.GodownParent);
                Godown.CreateGodownXml(tallyXml, itemParams.GodownParent, itemParams.GodownChild);

                StockGroup.CreateStockGroupXml(tallyXml, "", itemParams.StockGroupParent);
                StockGroup.CreateStockGroupXml(tallyXml, itemParams.StockGroupParent, itemParams.StockGroupChild);

                StockCategory.CreateStockCategoryXml(tallyXml, "", itemParams.StockCategoryParent);
                StockCategory.CreateStockCategoryXml(tallyXml, itemParams.StockCategoryParent, itemParams.StockCategoryChild);

                StockItem.CreateStockItemXml(tallyXml, itemParams.ItemName, itemParams.PartNo, itemParams.StockGroupChild,
                                    itemParams.StockCategoryChild, itemParams.Barcode, itemParams.CP, itemParams.SP,
                                    itemParams.ImgPath, itemParams.Desc1, itemParams.Desc2, itemParams.Desc3);

                string amt = Computations.CalculateAmount(itemParams.CP,itemParams.Qty);
                string partyName = Voucher.GetPartyNameFromVoucherXml(tallyXml);
                AllInventoriesList.CreateAllInventoriesListXml(tallyXml, itemParams.Desc1, itemParams.Desc2, itemParams.Desc3,
                                            itemParams.ItemName, Computations.IsDeemedPositive(amt), amt, itemParams.CP,
                                            itemParams.Qty, itemParams.GodownChild, partyName);
            }
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