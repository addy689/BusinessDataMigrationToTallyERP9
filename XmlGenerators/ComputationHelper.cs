using System;

namespace TallyXMLReader.XmlGenerators
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

    }
}