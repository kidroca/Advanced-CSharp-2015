namespace SoftUni.Homeworks.AdvancedCSharp.Regex
{
    using System;
    using System.Text;
    using System.Text.RegularExpressions;

    /// <summary>
    /// This problem is from the JavaScript Basics Exam (9 January 2015). 
    /// You may check the description and submit your solution here.
    /// https://judge.softuni.bg/Contests/Practice/Index/56
    /// </summary>
    class ParagraphExtractr
    {
        static void Main()
        {
            string html = Console.ReadLine();

            StringBuilder rawText = ExtractParagraphsContent(html);

            string decodedText = DecodeIluminatiEncrition(rawText);

            Console.WriteLine(decodedText);
        }

        private static string DecodeIluminatiEncrition(StringBuilder encriptedRawText)
        {
            for (int i = 0; i < encriptedRawText.Length; i++)
            {
                if ('a' <= encriptedRawText[i] && encriptedRawText[i] <= 'm')
                {
                    encriptedRawText[i] = (char)(encriptedRawText[i] + 13);
                }
                else if ('n' <= encriptedRawText[i] && encriptedRawText[i] <= 'z')
                {
                    encriptedRawText[i] = (char)(encriptedRawText[i] - 13);
                }
            }

            return encriptedRawText.ToString();
        }

        private static StringBuilder ExtractParagraphsContent(string html)
        {
            var textBuilder = new StringBuilder();

                                            // <p> is enpugh for the assignment the | is in
                                            // case <p class="sometext">
            var paragraphPattern = new Regex(@"(?:(?:<p>)|(?:<p\s+[^<]*?>))(?<text>[\s\S]*?)<\/p>");

            var garbagePattern = new Regex(@"[^a-z0-9]+");

            foreach (Match m in paragraphPattern.Matches(html))
            {
                string currentText = garbagePattern
                    .Replace(m.Groups["text"].Value, " ");

                textBuilder
                    .Append(currentText);
            }

            return textBuilder;
        }
    }
}