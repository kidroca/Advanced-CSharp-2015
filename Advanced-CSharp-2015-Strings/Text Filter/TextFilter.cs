namespace SoftUni.Homeworks.AdvancedCSharp.Strings
{
    using System;
    using System.Text.RegularExpressions;
    using HomeworkHelpers;

    /// <summary>
    /// Write a program that takes a text and a string of banned words. All words included 
    /// in the ban list should be replaced with asterisks "*", equal to the word's length.
    /// The entries in the ban list will be separated by a comma and space ", ".
    /// The ban list should be entered on the first input line and the text on the second 
    /// input line.
    /// </summary>
    class TextFilter
    {
        static TextHelper helper = new TextHelper();

        static void Main()
        {
            helper.SetupConsole();

            helper.PrintHeading("Text Filter");

            Console.Write("Enter banned words: ");
            string[] patterns = helper
                .ReadConsoleInColor(ConsoleColor.Blue)
                .Split(new string[] { ", ", " " }, StringSplitOptions.RemoveEmptyEntries);

            Console.Write("Enter some text to test: ");
            string text = helper.ReadConsoleInColor(ConsoleColor.Blue);

            helper.PrintColorText("\nResult: ", ConsoleColor.DarkRed);
            string result = CensorBannedWords(text, patterns);

            helper.PrintColorText(result, ConsoleColor.DarkGray);

            helper.Restart(Main);
        }

        private static string CensorBannedWords(string text, string[] patterns
            , bool caseSensitive = true, char censoringCharacter = '*')
        {
            var options = RegexOptions.IgnoreCase;
            if (caseSensitive)
            {
                options = RegexOptions.None;
            }

            foreach (var p in patterns)
            {
                var regex = new Regex(p, options);
                text = regex.Replace(text, new string(censoringCharacter, p.Length));
            }

            return text;
        }
    }
}
