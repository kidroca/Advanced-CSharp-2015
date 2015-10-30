namespace SoftUni.Homeworks.AdvancedCSharp.Regex
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    /// <summary>
    /// This problem is originally from the JavaScript Basics Exam (22 November 2014). 
    /// You may check the description and submit your solution here.
    /// https://judge.softuni.bg/Contests/Practice/Index/84
    /// </summary>
    class ExtractQuery
    {
        static void Main()
        {
            List<string> dataLines = ReadInput();

            List<Dictionary<string, List<string>>> queries =
                GetQueryParams(dataLines);

            foreach (var q in queries)
            {
                foreach (var key in q.Keys)
                {
                    Console.Write(
                        "{0}=[{1}]"
                        , key
                        , string.Join(", ", q[key]));
                }

                Console.WriteLine();
            }
        }

        static List<Dictionary<string, List<string>>> GetQueryParams(List<string> data)
        {
            var queries = new List<Dictionary<string, List<string>>>();

            var queryPattern = new Regex(@"(?<=^|[&?])(?<key>[^\s?]*?)=(?<val>[^\s?]*?)(?=$|&)");

            foreach (var line in data)
            {
                var currentQuery = new Dictionary<string, List<string>>();
                foreach (Match m in queryPattern.Matches(line))
                {
                    string key = RefineString(m.Groups["key"].Value);
                    string value = RefineString(m.Groups["val"].Value);

                    if (currentQuery.ContainsKey(key))
                    {
                        currentQuery[key].Add(value);
                    }
                    else
                    {
                        currentQuery[key] = new List<string>() { value };
                    }
                }

                queries.Add(currentQuery);
            }

            return queries;
        }

        private static string RefineString(string value)
        {
            var whiteSpaceToBe = new Regex(@"([+]+|(?:%20)+)+");

            string result = whiteSpaceToBe
                .Replace(value, " ")
                .Trim();

            return result;
        }

        static List<string> ReadInput()
        {
            var dataLines = new List<string>();

            string currentLine = String.Empty;
            while (currentLine != "END")
            {
                currentLine = Console.ReadLine();
                dataLines.Add(currentLine);
            }

            dataLines.RemoveAt(dataLines.Count - 1);

            return dataLines;
        }
    }
}