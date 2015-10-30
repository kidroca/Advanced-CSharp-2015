namespace SoftUni.Homeworks.AdvancedCSharp.Streams
{  
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using HomeworkHelpers;

    /// <summary>
    /// Write a program that reads a list of words from the file words.txt and finds how
    /// many times each of the words is contained in another file text.txt. Matching
    /// should be case-insensitive.
    /// 
    /// Write the results in file results.txt.Sort the words by frequency in descending
    /// order. Use StreamReader in combination with StreamWriter.
    /// </summary>
    internal class WordCount
    {
        private static ConsoleHelper helper = new ConsoleHelper();

        private static void Main()
        {
            helper.Setup();

            helper.PrintHeading("Word Count");

            string pathToWords = "../../words.txt",
                pathToText = "../../text.txt",
                pathToResult = "../../result.txt";

            try
            {
                Console.WriteLine("Press any key to start...\n");
                Console.ReadKey(true);

                List<string> wordsOfInterest = GetUniqueWordsFromFile(pathToWords);

                string textToQuery = GetText(pathToText);

                Dictionary<string, int> result = QueryText(wordsOfInterest, textToQuery);

                SaveResult(
                    result
                        .OrderByDescending(pair => pair.Value)
                        .ToDictionary(pair => pair.Key, pair => pair.Value), 
                    pathToResult);

                helper.PrintColorText(
                "\nSuccess - See result.txt at the root of the project: "
                , ConsoleColor.DarkGreen);
            }
            catch (FileNotFoundException)
            {
                helper.PrintColorText("Sorry, either the path is invalid or some of the files are missing.\nPlase make sure to add file words.txt and text.txt in the project root folder", "red");
            }

            helper.Restart(Main);
        }

        private static void SaveResult(
            Dictionary<string, int> wordsAndCount, string pathToResult)
        {
            using (var textWriter = new StreamWriter(pathToResult))
            {
                foreach (var pair in wordsAndCount)
                {
                    textWriter.WriteLine("{0} - {1}", pair.Key, pair.Value);
                }
            }
        }

        private static Dictionary<string, int> QueryText(List<string> words, string textToQuery)
        {
            var result = new Dictionary<string, int>();

            foreach (var word in words)
            {
                var pattern = new Regex(string.Format(@"\b{0}\b", word), RegexOptions.IgnoreCase);
                result[word] =
                    pattern.Matches(textToQuery).Count;
            }

            return result;
        }

        private static string GetText(string pathToText)
        {
            string text = string.Empty;
            using (var textReader = new StreamReader(pathToText))
            {
                text = textReader.ReadToEnd();
            }

            return text;
        }

        private static List<string> GetUniqueWordsFromFile(string pathToWords)
        {
            string text = GetText(pathToWords);

            return text
                 .Split(
                     new string[] { " ", ",", ".", "!", "?", ";", Environment.NewLine, "\t", },
                     StringSplitOptions.RemoveEmptyEntries)
                 .Distinct(StringComparer.OrdinalIgnoreCase)
                 .ToList();              
        }
    }
}