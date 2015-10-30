namespace SoftUni.Homeworks.AdvancedCSharp.Strings
{
    using System;
    using HomeworkHelpers;

    /// <summary>
    /// Write a program to find how many times a given string appears in a given text as 
    /// substring. The text is given at the first input line. The search string is given
    /// at the second input line. The output is an integer number. Please ignore the
    /// character casing. Overlapping between occurrences is allowed.
    /// </summary>
    class SubstringOccurrences
    {
        static TextHelper helper = new TextHelper();

        static void Main()
        {
            helper.SetupConsole();

            helper.PrintHeading("Count Substring Occurrences ");
            Console.Write("Enter some text: ");
            string text = helper.ReadConsoleInColor(ConsoleColor.Blue);

            Console.Write("Enter the substring: ");
            string pattern = helper.ReadConsoleInColor(ConsoleColor.Blue);

            helper.PrintColorText("\nResult: ", ConsoleColor.DarkRed);
            int result = CountOccurrences(text, pattern);

            helper.PrintColorText(
                string.Format("The nice phrase '{0}' occurrs: {1} {2}"
                , pattern
                , result
                , result == 1 ? "time" : "times"), ConsoleColor.DarkGray);

            helper.Restart(Main);
        }

        static int CountOccurrences(string text, string pattern, bool caseSensitive = false)
        {
            int index = -1,
                count = 0;

            StringComparison casing = 
                caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;

            index = text.IndexOf(pattern, index + 1, casing);
            while (index != -1) 
            {
                count++;
                index = text.IndexOf(pattern, index + 1, casing);
            }
            
            return count;
        }
    }
}
