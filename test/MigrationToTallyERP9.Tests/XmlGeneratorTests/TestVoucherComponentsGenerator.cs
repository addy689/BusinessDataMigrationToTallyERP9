using Microsoft.VisualStudio.TestTools.UnitTesting;
using MigrationToTallyERP9.XmlGenerators;
using System.Xml.XPath;
using System.Linq;

namespace MigrationToTallyERP9.Tests.XmlGeneratorTests
{
    [TestClassAttribute]
    public class TestVoucherComponentsGenerator
    {
        [TestMethodAttribute]
        public void IsBatchAddedToCorrectInventoryEntryList()
        {
            var tallyXml = XmlComponentGenerator.TallyXml;

            Voucher.CreateVoucherXml(tallyXml, "voucherremoteid-123", "20170110", "20170112", "Purchase", "PUR1", "JULIA", "1", "{0}");

            AllInventoryEntriesList.CreateAllInventoryEntriesListXml(tallyXml, "Item4desc1",
                    "Item4desc2", "Item4desc3", "AL-RG Necklace", "10.00", "{0}", "IMPORT PURCHASE");

            BatchAllocationsList.CreateBatchAllocationsListXml(tallyXml, "AL-RG Necklace", "Kent Box", "-70.00", "7", "7");

            bool actualVal = tallyXml.XPathSelectElement("//REQUESTDATA//VOUCHER/ALLINVENTORYENTRIES.LIST/BATCHALLOCATIONS.LIST") != null;
            
            bool expectedVal = true;

            Assert.AreEqual(expectedVal, actualVal);
        }

        [TestMethodAttribute]
        public void IsBatchesAndVoucherTotalCorrect()
        {
            var tallyXml = XmlComponentGenerator.TallyXml;

            Voucher.CreateVoucherXml(tallyXml, "voucherremoteid-123", "20170110", "20170112", "Purchase",
                             "PUR1", "JULIA", "1", "{0}");

            //FOR "AL-RG Necklace"
            AllInventoryEntriesList.CreateAllInventoryEntriesListXml(tallyXml, "Item4desc1",
                "Item4desc2", "Item4desc3", "AL-RG Necklace", "10.00", "{0}", "IMPORT PURCHASE");

            BatchAllocationsList.CreateBatchAllocationsListXml(tallyXml, "AL-RG Necklace", "Kent Box", "-70.00", "7", "7");
            
            BatchAllocationsList.CreateBatchAllocationsListXml(tallyXml, "AL-RG Necklace", "Kent Display", "-50.00", "5", "5");

            //FOR "AL-RG Ring"
            AllInventoryEntriesList.CreateAllInventoryEntriesListXml(tallyXml, "Item5desc1",
                    "Item5desc2", "Item5desc3", "AL-RG Ring", "5.00", "{0}", "IMPORT PURCHASE");
            BatchAllocationsList.CreateBatchAllocationsListXml(tallyXml, "AL-RG Ring", "Kent Box",
                 "-100.00", "20", "20");
            BatchAllocationsList.CreateBatchAllocationsListXml(tallyXml, "AL-RG Ring", "Kent Display", "-150.00", "30", "30");

            string[] expectedBatchesSums = new string[] { "-120.00", "-250.00" };
            string[] expectedBatchesQtys = new string[] { "12", "50" };

            float expectedVoucherSum = 370.00f;
            double actualVoucherSum = -AllInventoryEntriesList.CalculateAndFillInventoryEntryAmounts(tallyXml);

            Assert.AreEqual(expectedVoucherSum, actualVoucherSum);
            Assert.AreEqual("No", ComputationHelper.IsDeemedPositive((float)actualVoucherSum));

            var allInvEntries = tallyXml.XPathSelectElements("//REQUESTDATA//VOUCHER/ALLINVENTORYENTRIES.LIST").ToArray();

            Assert.AreEqual(expectedBatchesSums.Length, allInvEntries.Length);
            for (int i = 0; i < allInvEntries.Length; i++)
            {
                string billedQty = allInvEntries[i].Element("BILLEDQTY").Value;
                string actualQty = allInvEntries[i].Element("ACTUALQTY").Value;

                Assert.AreEqual(expectedBatchesSums[i], allInvEntries[i].Element("AMOUNT").Value);
                Assert.AreEqual(expectedBatchesQtys[i], ComputationHelper.ExtractNumericQtyFromString(billedQty));
                Assert.AreEqual(expectedBatchesQtys[i], ComputationHelper.ExtractNumericQtyFromString(actualQty));
            }
        }

        [TestMethodAttribute]
        public void TestRegex()
        {
            Assert.AreEqual("21", ComputationHelper.ExtractNumericQtyFromString(" 21 no"));
        }

    }
}