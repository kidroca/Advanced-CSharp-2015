namespace SoftUni.Homeworks.AdvancedCSharp.Multidimetional
{
    using System;
    using System.Collections.Generic;
    using HomeworkHelpers;

    /// <summary>
    /// Being a nerd means writing programs about night clubs instead of actually going to ones.
    /// Spiro is a nerd and he decided to summarize some info about the most popular night clubs
    /// around the world. 
    /// 
    /// He's come up with the following structure – he'll summarize the data by city, where each
    /// city will have a list of venues and each venue will have a list of performers.Help him
    /// finish the program, so he can stop staring at the computer screen and go get himself a 
    /// cold beer.
    /// 
    /// You'll receive the input from the console. There will be an arbitrary number of lines
    /// until you receive the string "END". Each line will contain data in format:
    /// "city;venue;performer". If either city, venue or performer don't exist yet in the
    /// database, add them. If either the city and/or venue already exist, update their info.
    /// 
    /// Cities should remain in the order in which they were added, venues should be sorted 
    /// alphabetically and performers should be unique (per venue) and also sorted alphabetically.
    /// 
    /// Print the data by listing the cities and for each city its venues (on a new line starting
    /// with "->") and performers(separated by comma and space). Check the examples to get the
    /// idea.And grab a beer when you're done, you deserve it. Spiro is buying.
    /// </summary>
    class NightLife
    {
        static TextHelper Helper = new TextHelper();

        static char[] separators =
        {
            ';',
        };

        static ConsoleColor infoColor = ConsoleColor.DarkMagenta;

        static void Main()
        {
            Helper.SetupConsole();
            Helper.PrintColorText("Night Life\n\n", "cyan");

            var userInput = new List<string>();
            //{
            //    "Sofia; Biad; Preslava",
            //    "Pernik; Stadion na mira; Vinkel",
            //    "New York; Statue of Liberty; Krisko",
            //    "Oslo; everywhere; Behemoth",
            //    "Pernik; Letishteto; RoYaL",
            //    "Pernik; Stadion na mira; Bankin",
            //    "Pernik; Stadion na mira; Vinkel",
            //    "END"
            //};

            Helper.PrintColorText("Hello Spiro, give me all the data: ", "white");

            string userEntry = string.Empty;
            do
            {
                userEntry = Console.ReadLine();
                userInput.Add(userEntry);

            } while (userEntry != "END");

            userInput.RemoveAt(userInput.Count - 1);

            Dictionary<string
                , SortedDictionary<string, SortedSet<string>>> citiesAndVenues =
                    PareseInputToDictionary(userInput);

            Helper.PrintColorText("\n\nOutput:\n\n", "green");
            foreach (var city in citiesAndVenues)
            {
                PrintCityData(city);
            }

            Helper.Restart(Main);
        }

        private static Dictionary<string, SortedDictionary<string, SortedSet<string>>> 
            PareseInputToDictionary(List<string> userInput)
        {
            var citiesAndVenues = new Dictionary<string, SortedDictionary<string, SortedSet<string>>>();

            foreach (var line in userInput)
            {
                string[] CityVenuePerformer = SplitLine(line, separators);

                string city = CityVenuePerformer[0],
                    venue = CityVenuePerformer[1],
                    performer = CityVenuePerformer[2];

                if (citiesAndVenues.ContainsKey(city))
                {
                    if (citiesAndVenues[city].ContainsKey(venue))
                    {
                        citiesAndVenues[city][venue].Add(performer);
                    }
                    else
                    {
                        citiesAndVenues[city].Add(venue, new SortedSet<string>() { performer });
                    }
                }
                else
                {
                    var venuPerformer = new SortedDictionary<string, SortedSet<string>>()
                    {
                        {venue, new SortedSet<string>() { performer } }
                    };

                    citiesAndVenues.Add(city, venuPerformer);
                }
            }

            return citiesAndVenues;
        }

        private static string[] SplitLine(string line, char[] separators)
        {
            string[] result = line.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            if (result.Length != 3)
            {
                throw new ApplicationException(
                    "Unexpected count of elements on the line. Expected 3, Received: " +
                    result.Length);
            }

            return result;
        }

        private static void PrintCityData(KeyValuePair<string, SortedDictionary<string, SortedSet<string>>> city)
        {
            Helper.PrintColorText(string.Format("{0}\n", city.Key), infoColor);
            foreach (var venue in city.Value)
            {
                Console.Write("->{0}: ", venue.Key);
                Console.WriteLine(string.Join(", ", venue.Value));
            }
        }
    }
}
