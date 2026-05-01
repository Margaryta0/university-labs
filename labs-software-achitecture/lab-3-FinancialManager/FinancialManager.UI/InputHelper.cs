using System;

namespace FinancialManager.UI
{
    public static class InputHelper
    {
        public static int ReadInt(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out int result))
                    return result;
                Console.WriteLine("  Введіть ціле число.");
            }
        }

        public static decimal ReadDecimal(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                var input = Console.ReadLine()?.Replace(',', '.');
                if (decimal.TryParse(input,
                    System.Globalization.NumberStyles.Number,
                    System.Globalization.CultureInfo.InvariantCulture,
                    out decimal result))
                    return result;
                Console.WriteLine("  Введіть числове значення (наприклад: 1500,50).");
            }
        }

        public static DateTime ReadDate(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                var input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                    return DateTime.Today;
                if (DateTime.TryParseExact(input, "dd.MM.yyyy",
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.None,
                    out DateTime result))
                    return result;
                Console.WriteLine("  Формат дати: дд.мм.рррр (або Enter для сьогодні).");
            }
        }

        public static string ReadString(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine() ?? "";
        }
    }
}
