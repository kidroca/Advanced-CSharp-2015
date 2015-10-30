namespace SoftUni.Homeworks.AdvancedCSharp.Arrays
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using HomeworkHelpers;

    /// <summary>
    /// Write a program that reads N floating-point numbers from the console. Your task is to separate 
    /// them in two sets, one containing only the round numbers (e.g. 1, 1.00, etc.) and the other 
    /// containing the floating-point numbers with non-zero fraction. Print both arrays along with 
    /// their minimum, maximum, sum and average (rounded to two decimal places). 
    /// </summary>
    class Categorize
    {
        static TextHelper Helper = new TextHelper();

        static void Main()
        {
            Console.Clear();
            Helper.PrintColorText("Categorize Numbers and Find Min / Max / Average \n\n", "cyan");
            Console.Write("Enter numbers separated by space character: ");

            Console.ForegroundColor = ConsoleColor.White;
            double[] numbers = Console
                .ReadLine()
                .Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(double.Parse)
                .ToArray();

            Console.ResetColor();

            List<double> fractionalNumbers = new List<double>();
            List<double> wholeNumbers = new List<double>();

            foreach (double number in numbers)
            {
                if (number % 1 == 0)
                {
                    wholeNumbers.Add(number);
                }
                else
                {
                    fractionalNumbers.Add(number);
                }
            }

            if (fractionalNumbers.Count > 0)
            {
                Console.WriteLine();
                Console.Write("Fractional Numbers: [ ");
                Console.ForegroundColor = ConsoleColor.Green;

                fractionalNumbers.ForEach(n => Console.Write("{0} ", n));
                Console.ResetColor();
                Console.WriteLine(
                    "] -> min: {0}, max: {1}, sum: {2}, avg: {3:F2}"
                    , fractionalNumbers.Min()
                    , fractionalNumbers.Max()
                    , fractionalNumbers.Sum()
                    , fractionalNumbers.Average());
            }

            if (wholeNumbers.Count > 0)
            {
                Console.WriteLine();
                Console.Write("Whole Numbers: [ ");
                Console.ForegroundColor = ConsoleColor.Green;

                wholeNumbers.ForEach(n => Console.Write("{0} ", n));
                Console.ResetColor();
                Console.WriteLine(
                    "] -> min: {0}, max: {1}, sum: {2}, avg: {3:F2}"
                    , wholeNumbers.Min()
                    , wholeNumbers.Max()
                    , wholeNumbers.Sum()
                    , wholeNumbers.Average());
            }

            Helper.PrintColorText("\n\nPRESS ANY KEY TO RESTART", "red");
            Console.CursorVisible = false;
            Console.ReadKey(true);
            Console.CursorVisible = true;
            Main();
        }
    }
}
