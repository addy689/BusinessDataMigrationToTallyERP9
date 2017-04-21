using CsvHelper.Configuration;

namespace MigrationToTallyERP9.CSVParser
{
    public sealed class HeaderCSVParamsMap : CsvClassMap<HeaderCSVParams>
    {
        public HeaderCSVParamsMap()
        {
            Map(m => m.VchRemoteId).Name("voucher_remoteid");
            Map(m => m.PartyName).Name("party_ledger");
            Map(m => m.CountryName).Name("country_name");
            Map(m => m.BillDate).Name("bill_date");
            Map(m => m.RecvDate).Name("receive_date");
            Map(m => m.VchType).Name("voucher_type");
            Map(m => m.InvNo).Name("invoice_no");
            Map(m => m.VchNo).Name("voucher_no");
            Map(m => m.PurchLedger).Name("purch_ledger");
        }
    }
}