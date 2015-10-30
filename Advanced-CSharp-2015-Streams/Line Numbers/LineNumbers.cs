namespace SoftUni.Homeworks.AdvancedCSharp.Streams
{
    using System;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Text.RegularExpressions;
    using HomeworkHelpers;

    /// <summary>
    /// Write a program that reads a text file and inserts line numbers in front of each
    /// of its lines. The result should be written to another text file. Use StreamReader
    /// in combination with StreamWriter.
    /// </summary>
    internal class LineNumbers
    {
        private static ConsoleHelper helper = new ConsoleHelper();

        private static void Main()
        {
            helper.Setup();

            helper.PrintHeading("Line Numbers");

            string html = ReadInput();

            string text = RemoveTags(html);

            string pathToFile = "../../PlainText.txt";

            EnumerateLinesAndSave(text, pathToFile);

            helper.PrintColorText("Press any key to show created file.\n\n", ConsoleColor.DarkGreen);
            Console.ReadKey(true);

            var textStream = new StreamReader(pathToFile);

            using (textStream)
            {
                while (!textStream.EndOfStream)
                {
                    Console.WriteLine(textStream.ReadLine());
                }
            }

            helper.Restart(Main);
        }

        private static void EnumerateLinesAndSave(string text, string pathToFile)
        {
            using (var textWriter = new StreamWriter(pathToFile))
            {
                int lineNumber = 1;
                foreach (var line in text.Split(
                    new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    textWriter.WriteLine("{0} {1}", lineNumber, line);
                    lineNumber++;
                }
            }
        }

        private static string RemoveTags(string html)
        {
            var tagPattern = new Regex(@"<[\s\S]*?>");
            var moreThanOneSpace = new Regex(@"\s{2,}");
            html = tagPattern.Replace(html, string.Empty);

            return moreThanOneSpace.Replace(html, "\n");
        }

        private static string GetHTML(string url)
        {
            // most often the html string can't fit in the console buffer, set here 
            // a point where the string to be sliced
            int maxLength = 5000;

            string invalidUrlMessage = "Invalid URL, try again";

            var urlValidator = new Regex(@"((([A-Za-z]{3,9}:(?:\/\/)?)(?:[-;:&=\+\$,\w]+@)?[A-Za-z0-9.-]+|(?:www.|[-;:&=\+\$,\w]+@)[A-Za-z0-9.-]+)((?:\/[\+~%\/.\w-_]*)?\??(?:[-\+=&;%@.\w_]*)#?(?:[.\!\/\\w]*))?)");

            if (urlValidator.IsMatch(url))
            {
                using (var webClient = new WebClient())
                {
                    try
                    {
                        webClient.Encoding = Encoding.UTF8;
                        webClient.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0");
                        webClient.Headers.Add(HttpRequestHeader.AcceptCharset, "utf-8");
                        string html = webClient.DownloadString(url);

                        if (html.Length > maxLength)
                        {
                            return html.Substring(0, maxLength);
                        }
                        else
                        {
                            return html;
                        }
                    }
                    catch (WebException ex)
                    {
                        if (ex.Status == WebExceptionStatus.NameResolutionFailure)
                        {
                            return invalidUrlMessage;
                        }
                        else
                        {
                            throw ex;
                        }
                    }
                }
            }
            else
            {
                return invalidUrlMessage;
            }
        }

        private static string ReadInput()
        {
            Console.Write("Enter a valid web address(");
            helper.PrintColorText(@"https://softuni.bg", ConsoleColor.Blue);
            Console.WriteLine(") to extract text from:");
            string input = helper.ReadInColor(ConsoleColor.Blue);

            string html = GetHTML(input);

            while (html.StartsWith("Invalid"))
            {
                Console.WriteLine(html);

                Console.Write("Enter a valid web address(");
                helper.PrintColorText(@"https://softuni.bg", ConsoleColor.Blue);
                Console.WriteLine(") to extract text from:");
                input = helper.ReadInColor(ConsoleColor.Blue);
                html = GetHTML(input);
            }

            return html;
        }
    }
}