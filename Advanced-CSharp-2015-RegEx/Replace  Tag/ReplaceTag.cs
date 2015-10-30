namespace SoftUni.Homeworks.AdvancedCSharp.Regex
{
    using System;
    using System.Net;
    using System.Text;
    using System.Text.RegularExpressions;
    using HomeworkHelpers;

    /// <summary>
    /// Write a program that reads a string from the console and replaces all series 
    /// of consecutive identical letters with a single one.
    /// </summary>
    class SeriesOfLetters
    {
        static TextHelper helper = new TextHelper();

        static void Main()
        {
            helper.SetupConsole();

            helper.PrintHeading("Replace <A> Tag ");

            string input = ReadInput();

            string html = String.Empty;
            if (string.Compare(input, "test", StringComparison.OrdinalIgnoreCase) == 0)
            {
                html = GetTestValue();
            }
            else
            {
                html = GetHTML(input);
            }

            helper.PrintColorText("Input:\n", ConsoleColor.DarkRed);
            helper.PrintColorText(html, ConsoleColor.DarkBlue);

            helper.PrintColorText("\nResult:\n", ConsoleColor.DarkRed);
            string result = ReplaceAncorTags(html, @"[URL href=${url}]${inner}[/URL]");

            helper.PrintColorText(result, ConsoleColor.DarkGreen);

            helper.Restart(Main);
        }

        private static string ReplaceAncorTags(string input, string replacement)
        {
            // the question marks "?" are placed in order to get the shortest possible match
            // for example the href=('|"")(?<url>.*?)(?=\1) group <url> will match only 'address'
            // from 'href="address" class="some"', if insted of '*?' there was only '*' it would 
            // have matched 'addres" class="some' (e.g. to the last possible '"')
            var regex = new Regex(
                @"<a.+href=('|"")(?<url>.*?)(?=\1).*?>(?<inner>[\s\S]*?)<\/a>");

            return regex.Replace(input, replacement);
        }

        private static string GetTestValue()
        {
            string value = @"
                        <ul>
                            <li>
                                <a href=""http://softuni.bg"">SoftUni</a>
                            </li>
                            <li>
                                <a href='http://softuni.bg'>SoftUni</a>
                            </li>
                            <li>
                                <a href=""http://softuni.bg'>SoftUni</a>
                            </li>
                       </ul>
                       ";

            return value;
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
            Console.Write("Enter a web address(");
            helper.PrintColorText(@"https://softuni.bg", ConsoleColor.Blue);
            Console.Write(") or type ");
            helper.PrintColorText("test", ConsoleColor.Blue);
            Console.Write(" to run built in tests: ");
            string input = helper.ReadConsoleInColor(ConsoleColor.Blue);

            return input;
        }
    }
}