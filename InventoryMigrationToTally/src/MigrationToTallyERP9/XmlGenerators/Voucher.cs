using System.Xml.XPath;
using System.Xml.Linq;
using System.Linq;

namespace MigrationToTallyERP9.XmlGenerators
{
    public class Voucher
    {
        private static readonly string xmlFileName = "VOUCHER";
        
        /// <summary>
        /// Creates a Voucher xml by filling values into a template, and adds it to the main tallyXml DOM object
        /// </summary>
        /// <param name="tallyXml">The main Tally Xml DOM object</param>
        /// <param name="args">The values to be filled in the template xml</param>
        public static XElement CreateVoucherXml(XElement tallyXml, string vchRemoteId, params string[] args)
        {
            XElement voucherXml = XmlComponentGenerator.CreateXmlFromTemplate(xmlFileName, args);
            voucherXml.SetAttributeValue("REMOTEID", vchRemoteId);
            
            return voucherXml;

        }

        public static void UpdateFinalAmtInVoucherXml(double voucherAmt, XElement voucherXml)
        {
            string isDeemedPositive = ComputationHelper.IsDeemedPositive((float)voucherAmt);

            var elmt = voucherXml.XPathSelectElement("./LEDGERENTRIES.LIST");
            
            elmt.XPathSelectElement("./AMOUNT").Value = 
                        string.Format(elmt.XPathSelectElement("./AMOUNT").Value, voucherAmt.ToString("0.00"));
            
            elmt.XPathSelectElement("./BILLALLOCATIONS.LIST/AMOUNT").Value = 
                        string.Format(elmt.XPathSelectElement("./BILLALLOCATIONS.LIST/AMOUNT").Value, voucherAmt.ToString("0.00"));
                
            elmt.XPathSelectElement("./ISDEEMEDPOSITIVE").Value = 
                        string.Format(elmt.XPathSelectElement("./ISDEEMEDPOSITIVE").Value, isDeemedPositive);                                                                         
        }

    }
}