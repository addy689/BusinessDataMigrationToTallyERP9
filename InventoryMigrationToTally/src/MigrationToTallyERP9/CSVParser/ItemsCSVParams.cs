namespace MigrationToTallyERP9.CSVParser
{
    public class ItemsCSVParams
    {
        private string itemName;
        private string partNo;
        private string barcode;
        private string cp;
        private string sp;
        private string billedqty;
        private string actualQty;
        private string imgPath;
        private string desc1;
        private string desc2;
        private string desc3;
        private string stockGroupParent;
        private string stockGroupChild;
        private string stockCategoryParent;
        private string stockCategoryChild;
        private string godownParent;
        private string godownChild;

        public string ItemName
        {
            get { return itemName; }
            set { itemName = ParseCSVFiles.GetValidatedInput(value); }
        }
        public string PartNo
        {
            get { return partNo; }
            set { partNo = ParseCSVFiles.GetValidatedInput(value); }
        }
        public string Barcode
        {
            get { return barcode; }
            set { barcode = ParseCSVFiles.GetValidatedInput(value); }
        }
        public string CP
        {
            get { return cp; }
            set { cp = ParseCSVFiles.GetValidatedInput(value); }
        }
        public string SP
        {
            get { return sp; }
            set { sp = ParseCSVFiles.GetValidatedInput(value); }
        }
        public string BilledQty
        {
            get { return billedqty; }
            set { billedqty = ParseCSVFiles.GetValidatedInput(value); }
        }
        public string ActualQty
        {
            get { return actualQty; }
            set { actualQty = ParseCSVFiles.GetValidatedInput(value); }
        }
        public string ImgPath
        {
            get { return imgPath; }
            set { imgPath = ParseCSVFiles.GetValidatedInput(value); }
        }
        public string Desc1
        {
            get { return desc1; }
            set { desc1 = ParseCSVFiles.GetValidatedInput(value); }
        }
        public string Desc2
        {
            get { return desc2; }
            set { desc2 = ParseCSVFiles.GetValidatedInput(value); }
        }
        public string Desc3
        {
            get { return desc3; }
            set { desc3 = ParseCSVFiles.GetValidatedInput(value); }
        }
        public string StockGroupParent
        {
            get { return stockGroupParent; }
            set { stockGroupParent = ParseCSVFiles.GetValidatedInput(value); }
        }
        public string StockGroupChild
        {
            get { return stockGroupChild; }
            set { stockGroupChild = ParseCSVFiles.GetValidatedInput(value); }
        }
        public string StockCategoryParent
        {
            get { return stockCategoryParent; }
            set { stockCategoryParent = ParseCSVFiles.GetValidatedInput(value); }
        }
        public string StockCategoryChild
        {
            get { return stockCategoryChild; }
            set { stockCategoryChild = ParseCSVFiles.GetValidatedInput(value); }
        }
        public string GodownParent
        {
            get { return godownParent; }
            set { godownParent = ParseCSVFiles.GetValidatedInput(value); }
        }
        public string GodownChild
        {
            get { return godownChild; }
            set { godownChild = ParseCSVFiles.GetValidatedInput(value); }
        }
    }
}