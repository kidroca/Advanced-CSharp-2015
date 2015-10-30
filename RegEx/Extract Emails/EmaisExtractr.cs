namespace SoftUni.Homeworks.AdvancedCSharp.Regex
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;
    using HomeworkHelpers;
    using System.Collections.Generic;

    /// <summary>
    /// Write a program to extract all email addresses from a given text. The text comes at
    /// the only input line. Print the emails on the console, each at a separate line.
    /// Emails are considered to be in format <user>@<host>, where: 
    ///     <user> is a sequence of letters and digits, where '.', '-' and '_' can appear
    ///     between them. Examples of valid users: "stephan", "mike03", "s.johnson",
    ///     "st_steward", "softuni-bulgaria", "12345". Examples of invalid users: ''--123",
    ///     ".....", "nakov_-", "_steve", ".info". 
    /// 
    ///     <host> is a sequence of at least two words, separated by dots '.'. Each word
    ///     is sequence of letters and can have hyphens '-' between the letters. Examples
    ///     of hosts: "softuni.bg", "software-university.com", "intoprogramming.info",
    ///     "mail.softuni.org". Examples of invalid hosts: "helloworld", ".unknown.soft.",
    ///     "invalid-host-", "invalid-". 
    ///     
    ///     Examples of valid emails: info@softuni-bulgaria.org, kiki@hotmail.co.uk,
    ///     no-reply@github.com, s.peterson@mail.uu.net, 
    ///     info-bg@software-university.software.academy.
    /// 
    ///     Examples of invalid emails: --123@gmail.com, …@mail.bg, .info@info.info,
    ///     _steve@yahoo.cn, mike@helloworld, mike@.unknown.soft., s.johnson@invalid-.
    /// </summary>
    class EmaisExtractr
    {
        static TextHelper helper = new TextHelper();

        static void Main()
        {
            helper.SetupConsole();

            helper.PrintHeading("Extract Emails");

            string text = ReadInput();

            if (string.Compare(text, "test", StringComparison.OrdinalIgnoreCase) == 0)
            {
                text = GetTestValue();
            }

            helper.PrintColorText("Input:\n", ConsoleColor.DarkRed);
            helper.PrintColorText(text, ConsoleColor.DarkBlue);

            SortedSet<string> result = ExtractEmails(text);
            helper.PrintColorText(
                string.Format(
                    "\n\nResult: extracted {0} {1}\n"
                    , result.Count
                    , result.Count == 1 ? "email" : "emails")
                , ConsoleColor.DarkRed);

            helper.PrintColorText(string.Join("\n", result), ConsoleColor.DarkGreen);

            helper.Restart(Main);
        }

        private static SortedSet<string> ExtractEmails(string text)
        {
            var emailPattern =
                new Regex(@"(?<=^|[\s"",])[A-Za-z0-9]+(?!\w*\.{2})(?:[\w-]|[.])*[A-Za-z0-9]+[@][A-Za-z]+(?!\w*\.{2})(?:[A-Za-z\-]|\.)*(?<!\.)\.[A-Za-z]+(?=$|[""\s,.'])");

            var emials = new SortedSet<string>();

            foreach (Match m in emailPattern.Matches(text))
            {
                emials.Add(m.Value);
            }

            return emials;
        }

        private static string GetTestValue()
        {
            string[] valid = new string[]
            {
                "VALID:",
                "email@example.com",
                "firstname.lastname@example.com",
                "softuni-bulgaria@mail.softuni.org",
                "st_steward@mail.softuni.org",
                "email@subdomain.example.com",
                "soft.uni-bulgaria@mail.soft-uni.org",
                "1234567890@example.com",
                "email@example-one.com",
                "email@example.name",
                "email@example.museum",
                "email@example.co.jp",
                "firstname-lastname@example.com"
            };

            valid[0] = string.Format("VALID: {0} emails\n", valid.Length - 1);

            string[] invalid = new string[]
            {
                "\nINVALID:\n",
                "--123@softuni.bg",
                "email@[123.123.123.123]",
                "\"email\"@example.com",
                "_______@example.com",
                ".....@softuni.bg",
                "nakov_-@softuni.bg",
                "_steve@softuni.bg",
                ".info@softuni.bg",
                "much.\"more\\ unusual\"@example.com",
                "very.unusual.\"@\".unusual.com@example.com",
                "very.\"(),:;<>[]\".VERY.\"very@\\\\\\ \"very\".unusual@strange.example.com",
                "plainaddress",
                "#@%^%#$@#$@#.com",
                "@example.com",
                "Joe Smith <email@example.com>",
                "email@example@example.com",
                ".email@example.com",
                "email..email@example.com",
                "あいうえお@example.com",
                "email@example",
                "email@-example.com",
                "email@111.222.333.44444",
                "email@example..com",
                "Abc..123@example.com",
                "\"(),:;<>[\\]@example.com",
                "this\\ is\"really\"not\\\\allowed@example.com"
            };

            return string.Join("\n", valid.Concat(invalid));
        }

        private static string ReadInput()
        {
            Console.Write("Enter a text containing emails ");
            Console.Write("or type ");
            helper.PrintColorText("test", ConsoleColor.Blue);
            Console.Write(" to run built in tests: ");
            string input = helper.ReadConsoleInColor(ConsoleColor.Blue);

            return input;
        }
    }
}