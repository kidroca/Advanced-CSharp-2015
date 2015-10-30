namespace SoftUni.Homeworks.AdvancedCSharp.Methods
{
    using System;
    using System.Collections.Generic;
    // An external librray that adds support for more mathematical opperations on generic types
    using MiscUtil; 

    /// <summary>
    /// Write methods to calculate the minimum, maximum, average, sum and product of a given set
    /// of numbers. Overload the methods to work with numbers of type double and decimal.
    /// 
    /// Note: Do not use LINQ.
    /// </summary>
    class NumberCalculations
    {
        static void Main()
        {
            // My Note: Instead of overloading the methods I made them work with any type that supports
            // the requested opperations like int, double, decimal etc...
            Console.Clear();

            string[] input = ReadInput("doubles");

            double[] doubles = ParseInputArray(input, (double)1);

            Console.WriteLine("Doubles: ");
            PrintResult(doubles);

            Console.WriteLine("\nNow Lets try with decimal");
            input = ReadInput("decimals");

            decimal[] decimals = ParseInputArray(input, (decimal)1);

            Console.WriteLine("Decimals: ");
            PrintResult(decimals);


            Console.WriteLine("Press any key to restart or Ctrl + C to Exit");
            Console.ReadKey(true);
            Main();
        }

        private static string[] ReadInput(string type)
        {
            Console.Write("enter an array of {0} separated by space: ", type);
            string[] input = Console
                .ReadLine()
                .Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);

            return input;
        }

        private static void PrintResult<T>(T[] numbers)
            where T : IComparable
        {
            Console.WriteLine(
                "\tMinimum: {0} \n\tMaximum: {1} \n\tAverage: {2} \n\tSum: {3} \n\tProduct: {4}"
                , GetMin(numbers)
                , GetMax(numbers)
                , GetAverage(numbers)
                , GetSum(numbers)
                , GetProduct(numbers));
        }

        private static T GetProduct<T>(T[] numbers)
        {
            T product = numbers[0];

            for (int i = 1; i < numbers.Length; i++)
            {
                product = Operator.Multiply(product, numbers[i]);
            }

            return product;
        }

        private static T GetSum<T>(IEnumerable<T> numbers)
        {
            T sum = Operator<T>.Zero;

            foreach (var value in numbers)
            {
                // The compiler will skip this check for non nullable types like Int32
                if (value != null)
                {
                    sum = Operator.Add(sum, value);
                }
            }

            return sum;
        }

        private static T GetAverage<T>(IEnumerable<T> numbers)
        {
            T sum = Operator<T>.Zero;
            int count = 0;

            foreach (var value in numbers)
            {
                // null values are not counted for the average result, 
                // The compiler will skip this check for non nullable types like Int32
                if (value != null)
                {
                    sum = Operator.Add(sum, value);
                    count++;
                }
            }

            if (count == 0)
            {
                sum = default(T);
                if (sum != null)
                {
                    throw new InvalidOperationException();
                }

                return sum;
            }
            else
            {
                return Operator.DivideInt32(sum, count);
            }
        }

        private static T GetMax<T>(T[] numbers) where T : IComparable
        {
            T max = numbers[0];

            for (int i = 1; i < numbers.Length; i++)
            {
                if (numbers[i].CompareTo(max) > 0)
                {
                    max = numbers[i];
                }
            }

            return max;
        }

        private static T GetMin<T>(T[] numbers) where T : IComparable
        {
            T min = numbers[0];

            for (int i = 1; i < numbers.Length; i++)
            {
                if (min.CompareTo(numbers[i]) > 0)
                {
                    min = numbers[i];
                }
            }

            return min;   
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
    }
}
