using CsvHelper.Configuration;

namespace MigrationToTallyERP9.CSVParser
{
    public sealed class HeaderCSVParamsMap : CsvClassMap<HeaderCSVParams>
    {
        public HeaderCSVParamsMap()
        {
            Map(m => m.VchRemoteId).Name("VOUCHER_REMOTEID");
            Map(m => m.PartyName).Name("PARTY_LEDGER");
            Map(m => m.CountryName).Name("COUNTRY_NAME");
            Map(m => m.BillDate).Name("BILL_DATE");
            Map(m => m.RecvDate).Name("RECEIVE_DATE");
            Map(m => m.VchType).Name("VOUCHER_TYPE");
            Map(m => m.InvNo).Name("INVOICE_NO");
            Map(m => m.VchNo).Name("VOUCHER_NO");
            Map(m => m.PurchLedger).Name("PURCH_LEDGER");
        }
    }
}