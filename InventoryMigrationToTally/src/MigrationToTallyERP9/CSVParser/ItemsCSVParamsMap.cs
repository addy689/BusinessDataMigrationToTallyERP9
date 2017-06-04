using CsvHelper.Configuration;

namespace MigrationToTallyERP9.CSVParser
{
    public sealed class ItemsCSVParamsMap : CsvClassMap<ItemsCSVParams>
    {
        public ItemsCSVParamsMap()
        {
            Map(m => m.ItemName).Name("item_name");
            Map(m => m.PartNo).Name("part_no");
            Map(m => m.Barcode).Name("barcode");
            Map(m => m.CP).Name("cp");
            Map(m => m.SP).Name("sp");
            Map(m => m.BilledQty).Name("qty");
            Map(m => m.ActualQty).Name("qty");
            Map(m => m.ImgPath).Name("img_path");
            Map(m => m.Desc1).Name("desc1");
            Map(m => m.Desc2).Name("desc2");
            Map(m => m.Desc3).Name("desc3");
            Map(m => m.StockGroupParent).Name("group0");
            Map(m => m.StockGroupChild).Name("group1");
            Map(m => m.StockCategoryParent).Name("category0");
            Map(m => m.StockCategoryChild).Name("category1");
            Map(m => m.GodownParent).Name("godown0");
            Map(m => m.GodownChild).Name("godown1");
        }
    }
}