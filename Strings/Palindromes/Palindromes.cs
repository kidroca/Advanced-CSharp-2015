namespace SoftUni.Homeworks.AdvancedCSharp.Strings
{
    using System;
    using System.Collections.Generic;
    using HomeworkHelpers;
    using System.Linq;

    /// <summary>
    /// Write a program that extracts from a given text all palindromes, e.g. ABBA, lamal,
    /// exe and prints them on the console on a single line, separated by comma and space.
    /// Use spaces, commas, dots, question marks and exclamation marks as word delimiters.
    /// Print only unique palindromes, sorted lexicographically.
    /// </summary>
    class Palindromes
    {
        static TextHelper helper = new TextHelper();

        static void Main()
        {
            helper.SetupConsole();

            helper.PrintHeading("Palindromes ");

            Console.Write("Enter some text with palindromes(type ");
            helper.PrintColorText("test", ConsoleColor.Blue);
            Console.Write(" to run some built in tests): "); 
            string input = helper.ReadConsoleInColor(ConsoleColor.Blue);
            string[] words;

            if (string.Compare(input.Trim(), "test", StringComparison.OrdinalIgnoreCase) == 0)
            {
                words = GetTestValues();
            }
            else
            {
                words = input
                .Split(new char[] { ' ', ',', '!', '?', '.' }, StringSplitOptions.RemoveEmptyEntries);
            }

            helper.PrintColorText("\nResult: ", ConsoleColor.DarkRed);

            SortedSet<string> result = GetPalindromes(words);

            helper.PrintColorText(string.Join(", ", result), ConsoleColor.DarkGray);
            helper.PrintColorText(string.Format("\nCount: {0}", result.Count), ConsoleColor.DarkRed);

            helper.Restart(Main);
        }

        private static SortedSet<string> GetPalindromes(string[] input)
        {
            var palindromes = new SortedSet<string>();

            foreach (var word in input)
            {
                if (IsPalindrome(word))
                {
                    palindromes.Add(word);
                }
            }

            return palindromes;
        }

        private static bool IsPalindrome(string word)
        {
            if (string.CompareOrdinal(word, ReverseString(word)) == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
            //while (isPalindrome && word.Length > 1)
            //{
            //    Match match = regex.Match(word);

            //    if (string.Compare(
            //        match.Groups[1].Value
            //        , match.Groups[3].Value
            //        , StringComparison.Ordinal) == 0)
            //    {
            //        // if first and last characters are equal slice them off from the string and 
            //        // continue the loop with the newly formed string.
            //        word = match.Groups[1].Value;
            //    }
            //    else
            //    {
            //        isPalindrome = false;
            //    }
        }

        private static string ReverseString(string input)
        {
            return new string(input.ToCharArray().Reverse().ToArray());
        }

        private static string[] GetTestValues()
        {
           var result = new string[]
                {
                    "neverOddOreven", "lammal", "racecar",
                    "eve", "maddam", "noon", "aibohphobia",
                    "kinnikinnik", "redivider", "releveler",
                    "reviver", "rotator"
                };

            helper.PrintColorText(
                string.Format("\nTest Mode, loaded {0} palindromes\n", result.Length)
                    , ConsoleColor.DarkCyan);

            return result;
        }
    }
}
