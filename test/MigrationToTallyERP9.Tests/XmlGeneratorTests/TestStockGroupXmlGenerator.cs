using Microsoft.VisualStudio.TestTools.UnitTesting;
using MigrationToTallyERP9.XmlGenerators;

namespace MigrationToTallyERP9.Tests.XmlGeneratorTests
{
    [TestClassAttribute]
    public class TestStockGroupXmlGenerator
    {
        private string[] stockGroupArgs = new string[] {"Alloy Jewellery", "Alloy Necklaces"};

        [TestMethodAttribute]
        public void IsAlreadyCreatedStockGroupXml()
        {
            var tallyXml = XmlComponentGenerator.TallyXml;
            
            StockGroup.CreateStockGroupXml(tallyXml, stockGroupArgs);
            bool actualValue = StockGroup.IsAlreadyCreated(stockGroupArgs[1], tallyXml);

            bool expectedValue = true;
            Assert.AreEqual(expectedValue, actualValue);
        }

    }
}