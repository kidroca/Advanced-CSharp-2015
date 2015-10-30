namespace Problem3
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    class Program
    {
        static void Main()
        {
            var doubles = new List<string>();
            var integers = new List<string>();

            var dict = new Dictionary<string, List<string>>();
            dict["int"] = integers;
            dict["double"] = doubles;

            var intDoublePattern =
                new Regex(
                    @"(?<type>double|int)\s(?<name>[a-z][A-Za-z]{0,24})");
            // @"(?<type>double|int)\s(?<name>[a-z][A-Za-z]{0,24})"
            // (?< type >\bdouble |\bint)\s + (?< name >[A - Za - z]{ 1,25}?\b)

            string inputLine;
            while (!(inputLine = Console.ReadLine()).StartsWith(@"//END_OF_CODE"))
            {

                foreach (Match m in intDoublePattern.Matches(inputLine))
                {
                    if (!m.Success)
                    {
                        continue;
                    }

                    dict[m.Groups["type"].Value]
                        .Add(m.Groups["name"].Value);
                }
            }

            doubles.Sort(StringComparer.Ordinal);
            integers.Sort(StringComparer.Ordinal);

            string doublesResult = "None";
            if (doubles.Count > 0)
            {
                doublesResult = string.Join(", ", doubles);
            }

            Console.WriteLine("Doubles: {0}", doublesResult);

            string integersResult = "None";
            if (integers.Count > 0)
            {
                integersResult = string.Join(", ", integers);
            }

            Console.WriteLine("Ints: {0}", integersResult);

        }
    }
}
