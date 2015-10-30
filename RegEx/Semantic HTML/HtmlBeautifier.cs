namespace SoftUni.Homeworks.AdvancedCSharp.Regex
{
    using System;
    using System.Text;
    using System.Text.RegularExpressions;

    /// <summary>
    /// This problem is originally from the PHP Basics Exam (31 August 2014).  
    /// You may check the description and submit your solution here.
    /// https://judge.softuni.bg/Contests/Practice/Index/56
    /// </summary>
    class HtmlBeautifier
    {
        static void Main()
        {
            string html = ReadInput();

            //LOL
            string semantic = BeautifyHTML(html);
            semantic = BeautifyHTML(semantic);
            semantic = BeautifyHTML(semantic);

            Console.WriteLine(semantic);
        }

        private static string BeautifyHTML(string html)
        {
            var mainPattern = 
                new Regex(@"<div (?<attr>.*?(?<ref>(class|id)\s*?=\s*""(?<tag>.*?)"").*?)>(?<inner>[\s\S]*?)<\/div>\s*<!--\s*\k<tag>\s*-->");

            var whiteSpace = new Regex(@"\s+");

            foreach (Match m in mainPattern.Matches(html))
            {
                string tag = m.Groups["tag"].Value,
                    attributes = m.Groups["attr"].Value,
                    inner = m.Groups["inner"].Value,
                    reference = m.Groups["ref"].Value;

                attributes = attributes.Replace(reference, "").Trim();
                attributes = whiteSpace.Replace(attributes, " ");

                if (attributes.Length > 0)
                {
                    attributes = " " + attributes;
                }

                var replacement = new StringBuilder();
                replacement
                    .Append(string.Format("<{0}{1}>", tag, attributes))
                    .Append(inner)
                    .Append(string.Format("</{0}>", tag));

                html = mainPattern.Replace(html, replacement.ToString());
            }

            return html;
        }

        static string ReadInput()
        {
            var html = new StringBuilder();

            string currentLine = String.Empty;
            while (currentLine != "END")
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