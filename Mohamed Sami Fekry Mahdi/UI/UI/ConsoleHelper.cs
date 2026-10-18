using SimpleBankAccount.UI;
using System;
namespace SimpleBankAccount.UI
{
    public static class ConsoleHelper
    {
        public static void ShowHeader()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("         ===================================================");
            Console.WriteLine("         ====            Simple Bank Account            ====");
            Console.WriteLine("         ===================================================");
            Console.WriteLine("         ====                By: Team1                  ====");
            Console.WriteLine("         ===================================================");
            Console.WriteLine("         ====            Welcome to Our Bank            ====");
            Console.WriteLine("         ===================================================");
            Console.ResetColor();
            Console.WriteLine();
        }
        public static string ReadString(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input))
                    return input.Trim();
                ShowError("Input cannot be empty.");
            }
        }
        public static int ReadInt(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                if (int.TryParse(input, out int value))
                    return value;
                ShowError("Invalid input. Please enter a whole number.");
            }
        }
        public static decimal ReadDecimal(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                if (decimal.TryParse(input, out decimal value))
                    return value;
                ShowError("Invalid amount. Please enter a valid number.");
            }
        }
        public static void ShowSuccess(string message)
        {
            WriteColored($"[SUCCESS] {message}", ConsoleColor.Green);
        }
        public static void ShowError(string message)
        {
            WriteColored($"[ERROR] {message}", ConsoleColor.Red);
        }
        public static void ShowInfo(string message)
        {
            WriteColored(message, ConsoleColor.Cyan);
        }
        public static void Pause()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey(true);
        }
        private static void WriteColored(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
