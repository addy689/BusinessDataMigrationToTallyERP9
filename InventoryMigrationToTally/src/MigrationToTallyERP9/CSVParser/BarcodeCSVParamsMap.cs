using CsvHelper.Configuration;

namespace MigrationToTallyERP9.CSVParser
{
    public class BarcodeCSVParamsMap : CsvClassMap<BarcodeCSVParams>
    {
        public BarcodeCSVParamsMap()
        {
            Map(m => m.FirstBarcodeAssigned).Name("FIRST_BARCODE");
            Map(m => m.LastBarcodeAssigned).Name("LAST_BARCODE");
            Map(m => m.AssociatedOutputXml).Name("ASSOCIATED_TALLY_XML");
            Map(m => m.TimeStamp).Name("TIMESTAMP");
        }
    }
}