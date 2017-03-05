using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml.Linq;
using System.Xml.XPath;
using MigrationToTallyERP9.XmlGenerators;

namespace MigrationToTallyERP9.UnitTests.XmlGeneratorTests
{
    [TestClassAttribute]
    public class TestStockItemXmlGenerator
    {
        private string[] stockItemArgs = new string[] {"AL-RG Necklace", "Item4partno", "Alloy Necklaces", "104-XX-NK", "100001",
                    "10.00", "16.00", @"D:\TEMP\5.JPG", "Item4desc1", "Item4desc2", "Item4desc3"};

        [TestMethodAttribute]
        public void CheckIfStockItemXmlAdded()
        {
            var tallyXml = XmlComponentGenerator.TallyXml;
            StockItem.CreateStockItemXml(tallyXml, stockItemArgs);
            var actualLastNode = tallyXml.XPathSelectElement("//REQUESTDATA/TALLYMESSAGE").LastNode as XElement;

            string expectedLastNodeType = "STOCKITEM";
            string expectedLastNodeName = "AL-RG Necklace";
            XElement expectedStockItemXml = XElement.Load(@"./UnitTests/XmlGeneratorTests/FilledSTOCKITEM.xml").Element("STOCKITEM");

            Assert.AreEqual(expectedLastNodeType, actualLastNode.Name);
            Assert.AreEqual(expectedLastNodeName, actualLastNode.Attribute("NAME").Value);
            Assert.AreEqual(expectedStockItemXml.ToString(), actualLastNode.ToString());
        }

    }
}