using Microsoft.VisualStudio.TestTools.UnitTesting;
using MigrationToTallyERP9;
using System.Collections.Generic;

namespace MigrationToTallyERP9.Tests
{
    [TestClassAttribute]
    public class TestBarcodeValueGen
    {
        [TestMethodAttribute]
        public void CheckGenerateBarcodeValue()
        {
            int a = BarcodeValueGen.Instance.GetMeNewBarcode();

            Assert.AreEqual<int>(100000, a);
        }
    }
}