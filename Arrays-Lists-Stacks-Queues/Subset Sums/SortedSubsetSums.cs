namespace SoftUni.Homeworks.AdvancedCSharp.Arrays
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using HomeworkHelpers;

    /// <summary>
    /// Write a program that reads from the console a number N and an array of integers given on a single
    /// line. Your task is to find all subsets within the array which have a sum equal to N and print
    /// them on the console (the order of printing is not important). Find only the unique subsets by
    /// filtering out repeating numbers first. In case there aren't any subsets with the desired sum,
    /// print "No matching subsets."
    /// 
    /// Modify the program you wrote for the previous problem to print the results in the following
    /// way: each line should contain the operands (numbers that form the desired sum) in ascending
    /// order; the lines containing fewer operands should be printed before those with more operands;
    /// when two lines have the same number of operands, the one containing the smallest operand should
    /// be printed first. If two or more lines contain the same number of operands and have the same
    /// smallest operand, the order of printing is not important.
    /// </summary>
    class SortedSubsetSums
    {
        static TextHelper Helper = new TextHelper();

        static void Main()
        {
            Console.Clear();
            Helper.PrintColorText("Subset Sums \n\n", "cyan");
            
            Console.Write("Enter Sum (Number N): ");
            Console.ForegroundColor = ConsoleColor.White;
            double expectedSum = double.Parse(Console.ReadLine());

            Console.ResetColor();
            Console.Write("Enter numbers separated by single space character: ");

            Console.ForegroundColor = ConsoleColor.White;
            List<double> numbers = Console
                .ReadLine()
                .Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(double.Parse)
                .Distinct()
                .ToList();

            Console.ResetColor();

            // Sublists seems to be the way
            List<List<double>> subsets = new List<List<double>>();

            // Magic formula of total possible subsets
            int totalSubsets = 1 << numbers.Count;

            // Thank you google, and God bless http://codesam.blogspot.bg/2011/03/find-all-subsets-of-given-set.html
            for (int i = 1; i < totalSubsets; i++)
            {
                int pos = 0;
                int bitmask = i;

                List<double> currentSubset = new List<double>();

                while (bitmask > 0)
                {
                    if ((bitmask & 1) == 1)
                    {
                        currentSubset.Add(numbers[pos]);
                    }

                    bitmask >>= 1;
                    pos++;
                }

                if (currentSubset.Sum() == expectedSum)
                {
                    currentSubset.Sort(); // This Line was added to make the result sorted
                    subsets.Add(currentSubset);
                }
            }

            Console.WriteLine();

            if (subsets.Count > 0)
            {
                // This block was added to make the result sorted
                var sortedSubsets = subsets
                    .OrderBy(s => s.Count)
                    .ThenBy(s => s[0]); // Since we sorted each list earlier the [0] element is the smallest opreand

                Console.WriteLine("Subsets: ");
                foreach (var subset in sortedSubsets)
                {
                    string formatted = string.Join(" + ", subset);

                    Helper.PrintColorText(string.Format("{0} = {1} \n", formatted, expectedSum), "green");
                }
            }
            else
            {
                Console.WriteLine("No matching subsets.");
            }

            Helper.PrintColorText("\n\nPRESS ANY KEY TO RESTART", "red");
            Console.CursorVisible = false;
            Console.ReadKey(true);
            Console.CursorVisible = true;
            Main();
        }
    }
}


