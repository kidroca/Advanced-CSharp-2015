namespace SoftUni.Homeworks.AdvancedCSharp.Strings
{
    using System;
    using System.Text;
    using HomeworkHelpers;

    /// <summary>
    /// Write a program that converts a string to a sequence of C# Unicode character literals. 
    /// </summary>
    class UnicodeCharacters
    {
        static TextHelper helper = new TextHelper();

        static void Main()
        {
            helper.SetupConsole();

            helper.PrintHeading("Unicode Characters");

            Console.Write("Enter a string for conversion: ");
            string input = helper
                .ReadConsoleInColor(ConsoleColor.Blue);

            helper.PrintColorText("\nResult: ", ConsoleColor.DarkRed);
            string result = GetUnicodeLiterals(input);

            helper.PrintColorText(result, ConsoleColor.DarkGray);

            helper.Restart(Main);
        }

        private static string GetUnicodeLiterals(string input)
        {
            var result = new StringBuilder();

            foreach (var symbol in input)
            {
                result.Append(string.Format("\\u{0:X4}", Convert.ToUInt16(symbol)));
            }

            return result.ToString();
        }
    }
}
