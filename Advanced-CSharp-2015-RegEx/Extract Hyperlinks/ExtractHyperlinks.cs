namespace SoftUni.Homeworks.AdvancedCSharp.Regex
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Text.RegularExpressions;

    /// <summary>
    /// This problem is originally from the JavaScript Basics Exam (27 July 2014). 
    /// You may check the description and submit your solution here.
    /// https://judge.softuni.bg/Contests/Practice/Index/84
    /// </summary>
    class ExtractHyperlinks
    {
        static void Main()
        {
            string html = ReadInput();

            List<string> links = ExtractLinks(html);

            Console.WriteLine(string.Join("\n", links));
        }

        private static List<string> ExtractLinks(string html)
        {
            var ancorHrefPattern = 
                new Regex(@"<a\s+[^>]*?\bhref\s*?=\s*?(""|')(?<href>[\S\s]*?)(\1)[\S\s]*?>");

            var extracted = new List<string>();

            foreach (Match m in ancorHrefPattern.Matches(html))
            {
                extracted.Add(m.Groups["href"].Value);
            }

            return extracted;
        }

        static string ReadInput()
        {
            var html = new StringBuilder();

            string currentLine = String.Empty;
            while(currentLine != "END")
            {
                currentLine = Console.ReadLine();
                html
                    .Append(currentLine)
                    .Append("\n");
            }

            html.Remove(html.Length - 4, 3);

            return html.ToString();
        }
    }
}