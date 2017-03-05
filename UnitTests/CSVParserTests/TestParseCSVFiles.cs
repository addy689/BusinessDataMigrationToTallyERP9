using Microsoft.VisualStudio.TestTools.UnitTesting;
using MigrationToTallyERP9.CSVParser;
using System.Linq;

namespace MigrationToTallyERP9.UnitTests.CSVParserTests
{
    [TestClassAttribute]
    public class TestParseCSVFiles
    {
        [TestMethodAttribute]
        public void ReadSampleHeaderCSV()
        {
            string sampleHeaderCSVFile = @"./UnitTests/CSVParserTests/TestSampleHeader.csv";

            var res = ParseCSVFiles.ParseHeaderCSV(sampleHeaderCSVFile).ToArray();
            Assert.AreEqual(1, res.Count());

            var headerParams = res[0];
            Assert.AreEqual("JULIA", headerParams.PartyName);
            Assert.AreEqual("VIETNAM", headerParams.CountryName);
            Assert.AreEqual("20170110", headerParams.BillDate);
            Assert.AreEqual("20170112", headerParams.RecvDate);
            Assert.AreEqual("Purchase", headerParams.VchType);
            Assert.AreEqual("PUR1", headerParams.InvNo);
            Assert.AreEqual("1", headerParams.VchNo);
        }

        [TestMethodAttribute]
        public void ReadSampleItemsCSV()
        {
            string sampleItemsCSVFile = @"./UnitTests/CSVParserTests/TestSampleItems.csv";

            var res = ParseCSVFiles.ParseItemsCSV(sampleItemsCSVFile).ToArray();
            Assert.AreEqual(1, res.Count());
            
            var itemParams = res[0];
            Assert.AreEqual("AL-RG Necklace", itemParams.ItemName);
            Assert.AreEqual("Item4partno", itemParams.PartNo);
            Assert.AreEqual("1234", itemParams.Barcode);
            Assert.AreEqual("10.00", itemParams.CP);
            Assert.AreEqual("16.00", itemParams.SP);
            Assert.AreEqual("7", itemParams.BilledQty);
            Assert.AreEqual("7", itemParams.ActualQty);
            Assert.AreEqual("xzyimg", itemParams.ImgPath);
            Assert.AreEqual("abc", itemParams.Desc1);
            Assert.AreEqual("def", itemParams.Desc2);
            Assert.AreEqual("ghi", itemParams.Desc3);
            Assert.AreEqual("Alloy Jewellery", itemParams.StockGroupParent);
            Assert.AreEqual("Alloy Necklaces", itemParams.StockGroupChild);
            Assert.AreEqual("XX", itemParams.StockCategoryParent);
            Assert.AreEqual("104-XX-NK", itemParams.StockCategoryChild);
            Assert.AreEqual("Kent", itemParams.GodownParent);
            Assert.AreEqual("Kent Box", itemParams.GodownChild);
            
        }
    }
}