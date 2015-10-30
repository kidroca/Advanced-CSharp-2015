namespace SoftUni.Homeworks.AdvancedCSharp.Methods
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Write a method which takes an array of any type and sorts it. Use bubble sort or 
    /// selection sort (your own implementation). You may re-use your code from a previous
    /// homework and modify it.
    /// 
    /// Use a generic method(read in Internet about generic methods in C#). Make sure that
    /// what you're trying to sort can be sorted – your method should work with numbers,
    /// strings, dates, etc., but not necessarily with custom classes like Student.
    /// </summary>
    class GenericArraySort
    {
        static void Main()
        {
            Console.WriteLine(
                "Feel free to add more test inline or uncommenting the code and using the console");

            //string[] input = ReadInput("something");

            int[] integers = // ParseInputArray(input, (double)1);
            {
                3, 44, 38, 5, 47, 15, 36, 26, 27, 2, 46, 4, 19, 50, 48
            };
            Console.Write("Integers: ");
            PrintCollection(integers);

            SelectSort(integers);

            Console.Write("  Sorted: ");
            PrintCollection(integers);
            Console.WriteLine();

            string[] strings =
            {
                "zaz", "cba", "baa", "azis"
            };

            Console.Write("Strings: ");
            PrintCollection(strings);

            SelectSort(strings);

            Console.Write(" Sorted: ");
            PrintCollection(strings);
            Console.WriteLine();

            DateTime[] dates =
            {
                new DateTime(2002, 3, 1),
                new DateTime(2015, 5, 6),
                new DateTime(2014, 1, 1)
            };

            Console.Write(" Dates: ");
            PrintCollection(dates);

            SelectSort(dates);

            Console.Write("Sorted: ");
            PrintCollection(dates);
            Console.WriteLine();

            Console.WriteLine("Even with list :)");
            var decimals = new List<decimal>()
            {
                234.000011M, 120.23M, 67M, 43M, 234.00001M, 56M, 1000M, 0.0001M, 0.0000001M, 43M
            };
            Console.Write("Decimals: ");
            PrintCollection(decimals);

            SelectSort(decimals);

            Console.Write("  Sorted: ");
            PrintCollection(decimals);
            Console.WriteLine();
        }

        private static string[] ReadInput(string type)
        {
            Console.Write("enter an array of {0} separated by space: ", type);
            string[] input = Console
                .ReadLine()
                .Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);

            return input;
        }

        /// <summary>
        /// Parses an array of strings to array of T
        /// </summary>
        /// <typeparam name="T">A type implementing IConvertible interface</typeparam>
        /// <param name="numbers">array of strings</param>
        /// <param name="sample">a sample of the desired type to parse to, it's value is irrelevant</param>
        /// <returns></returns>
        static T[] ParseInputArray<T>(string[] numbers, T sample) where T : IConvertible
        {
            T[] parsed = new T[numbers.Length];

            for (int i = 0; i < numbers.Length; i++)
            {
                parsed[i] = (T)Convert.ChangeType((numbers[i]), typeof(T));
            }

            return parsed;
        }

        /// <summary>
        /// Sorts a collection using the default comparer for the given type,
        /// and the Selection Sort algorithm 
        /// </summary>
        /// <typeparam name="T">A type that is comparable</typeparam>
        /// <param name="collection">Indexed collection like Array and List</param>
        static void SelectSort<T>(IList<T> collection)
            where T : IComparable
        {
            int length = collection.Count,
                nextIndex = 0;

            while (nextIndex <  length - 1)
            {
                int indexOfCurrentMin = nextIndex;

                for (int i = nextIndex + 1; i < length; i++)
                {
                    if (collection[i].CompareTo(collection[indexOfCurrentMin]) < 0)
                    {
                        indexOfCurrentMin = i;                        
                    }
                }

                if (nextIndex != indexOfCurrentMin)
                {
                    Swap(collection, nextIndex, indexOfCurrentMin);
                }

                nextIndex++;
            }
        }

        static void PrintCollection<T>(IEnumerable<T> collection)
        {
            // If you use a lot of printing and decide to change the way printing prints
            // you can do it here, instead of editing everly line with Console.WriteLine(...)
            Console.WriteLine(string.Join(", ", collection));
        }

        private static void Swap<T>(IList<T> collection, int i, int j) 
        {
            T temp = collection[i];

            collection[i] = collection[j];
            collection[j] = temp;
        }
    }
}
