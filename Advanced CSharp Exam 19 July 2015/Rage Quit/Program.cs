namespace RageQuit
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Text.RegularExpressions;

    class Program
    {
        static void Main()
        {
            string chochkosBulshit = Console.ReadLine();

            var ragePattern = new Regex(@"(?<rage>[^\d]{1,20})(?<count>\d{1,2})");

            var uniqueSymbols = new HashSet<char>();

            var resultBuilder = new StringBuilder();

            foreach (Match m in ragePattern.Matches(chochkosBulshit))
            {
                string rage = m.Groups["rage"].Value.ToUpper();

                int repetitions;
                int.TryParse(m.Groups["count"].Value, out repetitions);

                if (repetitions != 0)
                {
                    foreach (char c in rage)
                    {
                        uniqueSymbols.Add(c);
                    }

                    resultBuilder.Insert(resultBuilder.Length, rage, repetitions);
                }
            }

            resultBuilder.Insert(0, string.Format("Unique symbols used: {0}\n", uniqueSymbols.Count));

            Console.WriteLine(resultBuilder.ToString());
        }
    }
}
