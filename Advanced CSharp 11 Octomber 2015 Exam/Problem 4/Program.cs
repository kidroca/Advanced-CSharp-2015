namespace Problem4
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    class Program
    {
        static void Main()
        {
            // "singer @venue ticketsPrice ticketsCount"
            // There will be no redundant whitespaces anywhere
            // Aggregate the data by venue and by singer

            var inputPattern = 
                new Regex(@"(?<singer>^[\w ]+?)[ ]@(?<venue>[\w ]+?)[ ](?<ticketPrice>\d+?)[ ](?<ticketCount>\d+)$");

            var venueSinger = new Dictionary<string, Dictionary<string, long>>();

            string inputLine;
            while ((inputLine = Console.ReadLine()) != "End")
            {
                var match = inputPattern.Match(inputLine);

                if (!match.Success)
                {
                    continue;
                }

                string venue = match.Groups["venue"].Value
                    , singer = match.Groups["singer"].Value;

                int ticketPrice,
                    ticketCount;

                if (!int.TryParse(match.Groups["ticketPrice"].Value, out ticketPrice) ||
                    !int.TryParse(match.Groups["ticketCount"].Value, out ticketCount))
                {
                    continue;
                }

                if (!venueSinger.ContainsKey(venue))
                {
                    venueSinger[venue] = new Dictionary<string, long>();
                }

                if (!venueSinger[venue].ContainsKey(singer))
                {
                    venueSinger[venue][singer] = 0;
                }

                venueSinger[venue][singer] += ticketCount * ticketPrice;
            }

            foreach (var venue in venueSinger.Keys)
            {
                Console.WriteLine(venue);

                var sortedSingers = venueSinger[venue]
                    .OrderByDescending(singer => singer.Value);

                foreach (var singer in sortedSingers)
                {
                    Console.WriteLine(
                        "#  {0} -> {1}"
                        , singer.Key
                        , singer.Value);
                }
            }
        }
    }
}
