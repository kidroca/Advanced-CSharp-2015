namespace PopulationCounter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;

    class Program
    {
        static void Main()
        {
            var dictCountries = new Dictionary<string, Dictionary<string, BigInteger>>();

            string input;
            while ((input = Console.ReadLine()) != "report")
            {
                string[] information = input.Split('|');
                string city = information[0],
                    country = information[1];

                long population = long.Parse(information[2]);

                if (!dictCountries.ContainsKey(country))
                {
                    dictCountries[country] = new Dictionary<string, BigInteger>();
                    dictCountries[country]["totalPopulation"] = 0;
                }

                dictCountries[country][city] = population;
                dictCountries[country]["totalPopulation"] += population;
            }

            var sortedCountries = dictCountries.OrderByDescending(country => country.Value["totalPopulation"]);
                        
            foreach (var pair in sortedCountries)
            {
                Console.WriteLine(
                    "{0} (total population: {1})", pair.Key, pair.Value["totalPopulation"]);

                var sortedCities = pair.Value
                    .OrderByDescending(city => city.Value);

                foreach (var city in sortedCities)
                {
                    if (city.Key == "totalPopulation")
                    {
                        continue;
                    }

                    Console.WriteLine("=>{0}: {1}", city.Key, city.Value);
                }
            }
        }
    }
}
