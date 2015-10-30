namespace SoftUni.Homeworks.AdvancedCSharp.Strings
{
    using System;
    using System.Text;
    using System.Text.RegularExpressions;

    /// <summary>
    /// This problem is from the Java Basics exam (8 February 2015). You may check
    /// the description and post your solution here: 
    /// https://judge.softuni.bg/Contests/Practice/Index/69
    /// </summary>
    class NakovManiacov
    {
        static int lowercaseBottom = 'a' - 1;

        static int upercaseBottom = 'A' - 1;

        static void Main()
        {
            string input = Console.ReadLine();

            var regex = new Regex(@"([a-z])(\d+)([a-z])", RegexOptions.IgnoreCase);

            MatchCollection matches = regex.Matches(input);

            double nakovSum = CalculateNakovSum(matches);

            Console.WriteLine("{0:F2}", nakovSum);
        }

        private static double CalculateNakovSum(MatchCollection matches)
        {
            double sum = 0;

            foreach (Match m in matches)
            {
                char firstLetter = m.Groups[1].Value[0];
                double number = double.Parse(m.Groups[2].Value);
                char secondLetter = m.Groups[3].Value[0];

                number = FirsLetterCalculations(number, firstLetter);

                number = SecondLetterCalculations(number, secondLetter);

                sum += number;
            }

            return sum;
        }

        private static double SecondLetterCalculations(double number, char secondLetter)
        {
            if (char.IsUpper(secondLetter))
            {
                number -= secondLetter - upercaseBottom;
            }
            else
            {
                number += secondLetter - lowercaseBottom;
            }

            return number;
        }

        private static double FirsLetterCalculations(double number, char firstLetter)
        {
            if (char.IsUpper(firstLetter))
            {
                number /= firstLetter - upercaseBottom;
            }
            else
            {
                number *= firstLetter - lowercaseBottom;
            }

            return number;
        }

        private static string GetUnicodeLiterals(string input)
        {
            var result = new StringBuilder();

            foreach (var symbol in input)
            {
                result.Append(string.Format("\\u{0:X4}", Convert.ToUInt16(symbol)));
            }

            return result.ToString();
        }
    }
}
