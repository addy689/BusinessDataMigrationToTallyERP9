using Microsoft.VisualStudio.TestTools.UnitTesting;
using MigrationToTallyERP9.CSVParser;
using System.Collections.Generic;
using System;

namespace MigrationToTallyERP9.Tests.CSVParserTests
{
    [TestClassAttribute]
    public class TestWriteToCSVFiles
    {
        [TestMethodAttribute]
        public void WriteToBarcodeCSVFile()
        {
            BarcodeCSVParams b = new BarcodeCSVParams();
            b.FirstBarcodeAssigned = "100";
            b.LastBarcodeAssigned = "104";
            b.TimeStamp = DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss");
            b.AssociatedOutputXml = "Output1";
            
            BarcodeCSVParams c = new BarcodeCSVParams();
            c.FirstBarcodeAssigned = "105";
            c.LastBarcodeAssigned = "110";
            c.TimeStamp = DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss");
            c.AssociatedOutputXml = "Output2";

            WriteToCSVFiles.InitiateBarcodesLogging();
            WriteToCSVFiles.WriteBarcodesDataToFile(b);
            WriteToCSVFiles.WriteBarcodesDataToFile(c);
            WriteToCSVFiles.CloseBarcodesLogging();
        }
    }
}