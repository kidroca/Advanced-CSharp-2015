namespace SoftUni.Homeworks.AdvancedCSharp.Multidimetional
{
    using System;
    using System.Collections.Generic;
    using HomeworkHelpers;

    /// <summary>
    /// Write a program that reads some text from the console and counts the occurrences of each
    /// character in it. Print the results in alphabetical (lexicographical) order.
    /// </summary>
    class CountSymbolsOccurence
    {
        static TextHelper Helper = new TextHelper();

        static void Main()
        {
            Helper.SetupConsole();

            Helper.PrintColorText("Count Symbols\n\n", "cyan");

            Helper.PrintColorText("Enter text for sampling: ", "white");
            string text = Console.ReadLine();
            Console.WriteLine();

            var dict = new SortedDictionary<char, int>();

            foreach (char symbol in text)
            {
                if (dict.ContainsKey(symbol))
                {
                    dict[symbol]++;
                }
                else
                {
                    dict[symbol] = 1;
                }
            }
           
            Console.WriteLine("Result: ");
            foreach (var pair in dict)
            {
                Helper.PrintColorText(
                    string.Format("{0}: {1} {2}\n"
                    , pair.Key
                    , pair.Value
                    , pair.Value == 1 ? "time" : "times"), "green");
            }

            Helper.Restart(Main);
        }
    }
}
