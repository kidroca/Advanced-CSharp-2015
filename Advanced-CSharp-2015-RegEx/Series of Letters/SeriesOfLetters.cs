namespace SoftUni.Homeworks.AdvancedCSharp.Regex
{
    using System;
    using System.Text.RegularExpressions;
    using HomeworkHelpers;

    /// <summary>
    /// Write a program that reads a string from the console and replaces all series 
    /// of consecutive identical letters with a single one.
    /// </summary>
    class SeriesOfLetters
    {
        static TextHelper helper = new TextHelper();

        static void Main()
        {
            helper.SetupConsole();

            helper.PrintHeading("Series of Letters");

            Console.Write("Enter some string string: ");
            string input = helper.ReadConsoleInColor(ConsoleColor.Blue);

            helper.PrintColorText("\nResult: ", ConsoleColor.DarkRed);
            string result = FilterRepeatingLetters(input);

            helper.PrintColorText(result, ConsoleColor.DarkGray);

            helper.Restart(Main);
        }

        private static string FilterRepeatingLetters(string input)
        {
            // match every symbol followed by the same symbol (except the last one)
            var regex = new Regex(@"(.)(?=\1)");
            return regex.Replace(input, "");
        }
    }
}