namespace SoftUni.CSharp.ExamPreparation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    /// <summary>
    /// This assignment is from the Advanced C# Exam Preparation 2015 September.
    /// You can check out the description and submit your solution here
    /// https://judge.softuni.bg/Contests/Practice/Index/84
    /// </summary>
    class ReverseCapitalWords
    {
        static Regex capitalWordsMatch = new Regex(@"(?<=\b|\d)[A-Z]+?(?=\b|\d)");

        static void Main()
        {
            var resultBuilder = new StringBuilder();

            string currentLine;
            while ((currentLine = Console.ReadLine()) != "END")
            {
                string processedLine = ReverseCapitalsAndInflatePalindromes(currentLine);
                resultBuilder
                    .Append(System.Security.SecurityElement.Escape(processedLine))
                    .Append(Environment.NewLine);
            }

            Console.WriteLine(resultBuilder.ToString());
        }

        private static string ReverseCapitalsAndInflatePalindromes(string currentLine)
        {
            Regex pattern = capitalWordsMatch;

            var capitalsAndPalindromesEvaluator = new MatchEvaluator(ReplaceCapitals);

            currentLine = pattern.Replace(currentLine, capitalsAndPalindromesEvaluator);

            return currentLine;
        }

        private static string ReplaceCapitals(Match m)
        {
            string reversedWord = ReverseString(m.Value);
            if (reversedWord.Equals(m.Value, StringComparison.Ordinal))
            {
                reversedWord = InflateWord(reversedWord);
            }

            return reversedWord;
        }

        private static string InflateWord(string word)
        {
            var characters = new char[word.Length * 2];

            for (int i = 0, j = 0; i < word.Length; i++, j += 2)
            {
                characters[j] = word[i];
                characters[j + 1] = word[i];
            }

            return new string(characters);
        }

        static string ReverseString(string str)
        {
            return new string(str
                .ToCharArray()
                .Reverse()
                .ToArray());
        }
    }
}
