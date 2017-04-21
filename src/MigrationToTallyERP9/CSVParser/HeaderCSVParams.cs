namespace MigrationToTallyERP9.CSVParser
{
    public class HeaderCSVParams
    {
        private string vchRemoteId;
        private string partyName;
        private string countryName;
        private string billDate;
        private string recvDate;
        private string vchType;
        private string invNo;
        private string vchNo;
        private string purchLedger;        
        
        public string VchRemoteId
        {
            get { return vchRemoteId; }
            set { vchRemoteId = ParseCSVFiles.GetValidatedInput(value); }
        }
        public string PartyName
        {
            get { return partyName; }
            set { partyName = ParseCSVFiles.GetValidatedInput(value); }
        }
        public string CountryName
        {
            get { return countryName; }
            set { countryName = ParseCSVFiles.GetValidatedInput(value); }
        }
        public string BillDate
        {
            get { return billDate; }
            set { billDate = ParseCSVFiles.GetValidatedInput(value); }
        }
        public string RecvDate
        {
            get { return recvDate; }
            set { recvDate = ParseCSVFiles.GetValidatedInput(value); }
        }
        public string VchType
        {
            get { return vchType; }
            set { vchType = ParseCSVFiles.GetValidatedInput(value); }
        }
        public string InvNo
        {
            get { return invNo; }
            set { invNo = ParseCSVFiles.GetValidatedInput(value); }
        }
        public string VchNo
        {
            get { return vchNo; }
            set { vchNo = ParseCSVFiles.GetValidatedInput(value); }
        }
        public string PurchLedger
        {
            get { return purchLedger; }
            set { purchLedger = ParseCSVFiles.GetValidatedInput(value); }
        }
    }
}