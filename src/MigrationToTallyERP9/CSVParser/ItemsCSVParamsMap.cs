using CsvHelper.Configuration;

namespace MigrationToTallyERP9.CSVParser
{
    public sealed class ItemsCSVParamsMap : CsvClassMap<ItemsCSVParams>
    {
        public ItemsCSVParamsMap()
        {
            Map(m => m.ItemName).Name("ITEM_NAME");
            Map(m => m.PartNo).Name("PART_NO");
            Map(m => m.Barcode).Name("BARCODE");
            Map(m => m.CP).Name("CP");
            Map(m => m.SP).Name("SP");
            Map(m => m.BilledQty).Name("QTY");
            Map(m => m.ActualQty).Name("QTY");
            Map(m => m.ImgPath).Name("IMG_PATH");
            Map(m => m.Desc1).Name("DESC1");
            Map(m => m.Desc2).Name("DESC2");
            Map(m => m.Desc3).Name("DESC3");
            Map(m => m.StockGroupParent).Name("GROUP0");
            Map(m => m.StockGroupChild).Name("GROUP1");
            Map(m => m.StockCategoryParent).Name("CATEGORY0");
            Map(m => m.StockCategoryChild).Name("CATEGORY1");
            Map(m => m.GodownParent).Name("GODOWN0");
            Map(m => m.GodownChild).Name("GODOWN1");
        }
    }
}