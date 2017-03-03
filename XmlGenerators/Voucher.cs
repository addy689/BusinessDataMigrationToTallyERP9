using System.Xml.Linq;

namespace TallyXMLReader.XmlGenerators
{
    public class Voucher
    {
        private static readonly string xmlFileName = "VOUCHER";
        
        /// <summary>
        /// Creates a Voucher xml by filling values into a template, and adds it to the main tallyXml DOM object
        /// </summary>
        /// <param name="tallyXml">The main Tally Xml DOM object</param>
        /// <param name="args">The values to be filled in the template xml</param>
        public static void CreateVoucherXml(XElement tallyXml, params string[] args)
        {
            XElement voucherXml = XmlComponentGenerator.CreateXmlFromTemplate(xmlFileName, args);
            //now add it
        }

        
        // public static void ComputeTotalAmount(XElement tallyXml)
        // {
        //     //use allInventoriesListElements to compute total ledger amount,
        //     //and insert it into appropriate tag under voucherXml
        // }

        // public static void AddInventoryListToVoucherXml(XElement tallyXml)
        // {
            
        // }

        public static string GetPartyNameFromVoucherXml(XElement tallyXml)
        {
            return null;
        }

    }
}