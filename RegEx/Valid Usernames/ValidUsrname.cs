namespace SoftUni.Homeworks.AdvancedCSharp.Regex
{
    using System;
    using System.Text.RegularExpressions;

    /// <summary>
    /// This problem is from the Java Basics Exam (21 September 2014 Evening). 
    /// You may check the description and submit your solution here.
    /// https://judge.softuni.bg/Contests/Practice/Index/34
    /// </summary>
    class ValidUsrname
    {
        static void Main()
        {
            string input = Console.ReadLine();

            var usernames = ExtractUsernames(input);

            if (usernames.Count > 0)
            {
                string[] bestPair = FindLongestConsecutivePair(usernames);

                Console.WriteLine(string.Join("\n", bestPair));
            }  
        }

        private static string[] FindLongestConsecutivePair(MatchCollection usernames)
        {
            int bestLength = 0,
                fromIndex = 0;

            if (usernames.Count == 1)
            {
                Console.WriteLine(usernames[0].Value);

                return new string[]
                {
                    usernames[0].Value
                };
            }

            for (int i = 1; i < usernames.Count; i++)
            {
                string current = usernames[i].Value,
                    prev = usernames[i - 1].Value;

                int currentLength = current.Length + prev.Length;

                if (currentLength > bestLength)
                {
                    bestLength = currentLength;
                    fromIndex = i - 1;
                }
            }

            return new string[]
            {
                usernames[fromIndex].Value,
                usernames[fromIndex + 1].Value
            };
        }

        private static MatchCollection ExtractUsernames(string data)
        {
            var usernamePattern =
                new Regex(@"\b[a-z][\w]{2,24}(?=[\/\\)\s]|$)", RegexOptions.IgnoreCase);

            return usernamePattern.Matches(data);
        }
    }
}