namespace SoftUni.Homeworks.AdvancedCSharp.Regex
{
    using System;
    using System.Text.RegularExpressions;
    using HomeworkHelpers;
    using System.Collections.Generic;

    /// <summary>
    /// Write a program that reads a keyword and some text from the console and prints
    /// all sentences from the text, containing that word. A sentence is any sequence
    /// of words ending with '.', '!' or '?'.
    /// </summary>
    class SentenceExtractr
    {
        static TextHelper helper = new TextHelper();

        static void Main()
        {
            helper.SetupConsole();

            helper.PrintHeading("Sentence Extractor");

            string[] text = ReadInput();

            helper.PrintColorText("Key: ", ConsoleColor.DarkRed);
            Console.WriteLine(text[0]);

            helper.PrintColorText("Text: ", ConsoleColor.DarkRed);
            Console.WriteLine(text[1]);

            List<string> result = ExtractSentences(text[0], text[1]);

            helper.PrintColorText(
                string.Format(
                    "\n\nResult: extracted {0} {1}\n"
                    , result.Count
                    , result.Count == 1 ? "sentence" : "sentences")
                , ConsoleColor.DarkRed);

            helper.PrintColorText(string.Join("\n", result), ConsoleColor.DarkGreen);

            helper.Restart(Main);
        }

        private static List<string> ExtractSentences(string key, string text)
        {
            string template = @"(?:[A-Z][^.!?]*?\b{0}|\b{0})\b[^.!?]*[.!?]";
            var regex = new Regex(string.Format(template, key));

            var extracted = new List<string>();

            foreach (Match m in regex.Matches(text))
            {
                extracted.Add(m.Value.Trim());
            }

            return extracted;
        }

        private static string[] GetTestValue()
        {
            string[] test = new string[]
            {
                "is",
                "This is my cat! And this is my dog. We happily live in Paris – the most beautiful city in the world! Isn’t it great? Well it is :)"
            };

            return test;
        }

        private static string[] ReadInput()
        {
            Console.Write("Enter the keyword ");
            Console.Write("or type ");
            helper.PrintColorText("test", ConsoleColor.Blue);
            Console.Write(" to run built in tests: ");
            string input = helper.ReadConsoleInColor(ConsoleColor.Blue);

            string[] keyAndText = new string[2];
            if (string.Compare(input, "test", StringComparison.OrdinalIgnoreCase) == 0)
            {
                keyAndText = GetTestValue();
            }
            else
            {
                keyAndText[0] = input;

                Console.Write("Enter the text: ");
                keyAndText[1] = helper.ReadConsoleInColor(ConsoleColor.Blue);
            }

            return keyAndText;
        }
    }
}