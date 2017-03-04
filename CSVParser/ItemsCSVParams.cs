namespace TallyXMLReader.CSVParser
{
    public class ItemsCSVParams
    {
        public string ItemName { get; set; }
        public string PartNo { get; set; }
        public string Barcode { get; set; }
        public string CP { get; set; }
        public string SP { get; set; }
        public string BilledQty { get; set; }
        public string ActualQty { get; set; }
        public string ImgPath { get; set; }
        public string Desc1 { get; set; }
        public string Desc2 { get; set; }
        public string Desc3 { get; set; }
        public string StockGroupParent { get; set; }
        public string StockGroupChild { get; set; }
        public string StockCategoryParent { get; set; }
        public string StockCategoryChild { get; set; }
        public string GodownParent { get; set; }
        public string GodownChild { get; set; }

        public string PartyName { get; set; }

        // public ItemsCSVParams(string itemName, string partNo, string barcode,
        //  string cp, string sp, string qty, string imgPath, string desc1, string desc2,
        //  string desc3, string stockGroupParent, string stockGroupChild,
        //  string stockCategoryParent, string stockCategoryChild,
        //  string godownParent, string godownChild)
        // {
        //     this.ItemName = itemName;
        //     this.PartNo = partNo;
        //     this.Barcode = barcode;
        //     this.CP = cp;
        //     this.SP = sp;
        //     this.Qty = qty;
        //     this.ImgPath = imgPath;
        //     this.Desc1 = desc1;
        //     this.Desc2 = desc2;
        //     this.Desc3 = desc3;
        //     this.StockGroupParent = stockGroupParent;
        //     this.StockGroupChild = stockGroupChild;
        //     this.StockCategoryParent = stockCategoryParent;
        //     this.StockCategoryChild = stockCategoryChild;
        //     this.GodownParent = godownParent;
        //     this.GodownChild = godownChild;
        // }
    }
}