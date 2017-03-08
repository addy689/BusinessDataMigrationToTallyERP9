using System;
using System.Text.RegularExpressions;

namespace MigrationToTallyERP9.XmlGenerators
{
    public class ComputationHelper
    {
        public static string IsDeemedPositive(float amount)
        {
            return amount > 0 ? "No" : "Yes";
        }

        public static float CalculateAmount(string costPrice, string quantity)
        {
            float cp;
            int qty;

            if (!float.TryParse(costPrice, out cp))
                Console.WriteLine($"Unable to parse {costPrice} as float");

            if (!int.TryParse(quantity, out qty))
                Console.WriteLine($"Unable to parse {quantity} as int");

            return (cp * qty);
        }

        public static string ExtractNumericQtyFromString(string qty)
        {
            string pattern = @".*?([0-9]+).*";
            Regex regex = new Regex(pattern);

            Match match = regex.Match(qty);
            if (match.Success)
                return match.Groups[1].Value;

            return "Could not extract qty";
        }

    }
}