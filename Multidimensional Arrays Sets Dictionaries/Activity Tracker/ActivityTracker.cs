namespace SoftUni.Homeworks.AdvancedCSharp.Multidimetional
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// This problem is from the Java Basics Exam (3 September 2014). You may check your solution 
    /// here.
    /// 
    /// You are part of the server side application team of brand new and promising activity
    /// tracking company. Their product "The Spy" is constantly sending data to the server.
    /// The data represents the distance walked in meters for every user in format:
    ///     24/07/2014 Angel 4600
    ///     24/07/2014 Pesho 3200
    ///     25/07/2014 Angel 6500
    ///     01/08/2014 Pesho 5600
    ///     03/08/2014 Ivan 11400
    /// 
    /// Write a program to aggregate the data and print the activity of each user per month.The
    /// months should be represented in ascending order. The users should be ordered
    /// alphabetically and the data should be represented in the following
    /// way: <month>: <user>(<distance>), <user>(<distance>),… For the data above the
    /// output should be:
    ///     7: Angel(11100), Pesho(3200)
    ///     8: Ivan(11400), Pesho(5600)
    /// </summary>
    class ActivityTracker
    {
        static CultureInfo dateFormat = CultureInfo.CreateSpecificCulture("bg");

        static void Main()
        {
            int dataLinesCount = int.Parse(Console.ReadLine());

            SortedDictionary<DateTime, SortedDictionary<string, float>> activityInfo =
                ParseInputLines(dataLinesCount);

            PrintOutput2(activityInfo);
        }

        private static void PrintOutput(SortedDictionary<DateTime, SortedDictionary<string, float>> activityInfo)
        {
            var resultBuilder = new StringBuilder();

            var gropedByMonth = activityInfo
                .GroupBy(x => x.Key.Month)
                .ToDictionary(x => x.Key);

            foreach (var group in gropedByMonth)
            {
                resultBuilder.Append(string.Format("{0}: ", group.Key));

                var users = new SortedDictionary<string, float>();

                foreach (var date in group.Value)
                {
                    foreach (var user in date.Value)
                    {
                        if (users.ContainsKey(user.Key))
                        {
                            users[user.Key] += user.Value;
                        }
                        else
                        {
                            users.Add(user.Key, user.Value);
                        }
                    }
                }

                foreach (var user in users)
                {
                    if (user.Key == users.Keys.Last())
                    {
                        resultBuilder.Append(string.Format("{0}({1})", user.Key, user.Value));
                    }
                    else
                    {
                        resultBuilder.Append(string.Format("{0}({1}), ", user.Key, user.Value));
                    }
                }

                resultBuilder.Append("\n");
            }

            Console.WriteLine(resultBuilder.ToString());
            // Po - slojno ne mojah da go napravya pffff 
        }

        static SortedDictionary<DateTime, SortedDictionary<string, float>>
            ParseInputLines(int lines)
        {
            var activityData = new SortedDictionary<DateTime, SortedDictionary<string, float>>();

            for (int i = 0; i < lines; i++)
            {
                string[] dataLine = Console
                    .ReadLine()
                    .Split(' ');

                var date = DateTime.Parse(dataLine[0], dateFormat);

                string user = dataLine[1];
                float distance = float.Parse(dataLine[2]);

                if (activityData.ContainsKey(date))
                {
                    if (activityData[date].ContainsKey(user))
                    {
                        activityData[date][user] += distance;
                    }
                    else
                    {
                        activityData[date][user] = distance;
                    }   
                }
                else
                {
                    activityData.Add(date, new SortedDictionary<string, float>());
                    activityData[date][user] = distance;
                }
            }

            return activityData;
        }

        private static void PrintOutput2(SortedDictionary<DateTime, SortedDictionary<string, float>> activityInfo)
        {
            var resultBuilder = new StringBuilder();

            var gropedByMonth = (from C in activityInfo
                                 group C by new
                                 {
                                     Month = C.Key.Month,
                                     Date = C.Value
                                 }
                                 into months
                                 select new
                                 {
                                     months.Key
                                 });
                

            foreach (var group in gropedByMonth)
            {
                resultBuilder.Append(string.Format("{0}: ", group.Key));

                

                var users = new SortedDictionary<string, float>();

                //foreach (var date in group.Value)
                //{
                //    foreach (var user in date.Value)
                //    {
                //        Console.WriteLine(date.Value);
                //        if (users.ContainsKey(user.Key))
                //        {
                //            users[user.Key] += user.Value;
                //        }
                //        else
                //        {
                //            users.Add(user.Key, user.Value);
                //        }
                //    }
                //}

                //foreach (var user in users)
                //{
                //    if (user.Key == users.Keys.Last())
                //    {
                //        resultBuilder.Append(string.Format("{0}({1})", user.Key, user.Value));
                //    }
                //    else
                //    {
                //        resultBuilder.Append(string.Format("{0}({1}), ", user.Key, user.Value));
                //    }
                //}

                resultBuilder.Append("\n");
            }

            Console.WriteLine(resultBuilder.ToString());
            // Po - slojno ne mojah da go napravya pffff 
        }
    }
}
