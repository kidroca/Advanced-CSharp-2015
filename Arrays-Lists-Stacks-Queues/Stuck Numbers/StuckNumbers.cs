namespace SoftUni.Homeworks.AdvancedCSharp.Arrays
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// This problem is from the Java Basics Exam (1 June 2014). You may check your solution here.
    /// https://judge.softuni.bg/Contests/Practice/Index/14
    /// 
    /// You are given n numbers.Write a program to find among these numbers all sets of 4 numbers {a, b, c, d},
    /// such that a|b == c|d, where a ≠ b ≠ c ≠ d.Assume that "a|b" means to append the number b after a.We call
    /// these numbers { a, b, c, d} stuck numbers: if we append a and b, we get the same result as if we
    /// appended c and d.Note that the numbers a, b, c and d should be distinct(a ≠ b ≠ c ≠ d).
    /// </summary>
    class StuckNumbers
    {
        static List<string> stuckNumbers = new List<string>();

        /// <summary>
        /// Breaks down a collection to all possible subsets of K elements and preforms
        /// a callback function on each subset
        /// </summary>
        /// <param name="numbers">The collection from which subsets will be extracted</param>
        /// <param name="usedPositions">A bool[] mask</param>
        /// <param name="startIndex">currentStartIndex</param>
        /// <param name="currentSize">currentSize</param>
        /// <param name="k">Lenght of the extracted subsets</param>
        /// <param name="callback">A function to call upon each valid subset</param>
        static void GetAllSubsetsOfSizeK(int[] numbers, bool[] usedPositions, int startIndex, int currentSize
            , int k, Action<List<int>> callback)
        {
            if (currentSize == k)
            {
                var currentSubset = new List<int>();

                for (int i = 0; i < numbers.Length; i++)
                {
                    if (usedPositions[i])
                    {
                        currentSubset.Add(numbers[i]);
                    }
                }

                callback(currentSubset);
            } 
            else if (startIndex < numbers.Length)
            {
                usedPositions[startIndex] = true;
                GetAllSubsetsOfSizeK(numbers, usedPositions, startIndex + 1, currentSize + 1, k, callback);

                usedPositions[startIndex] = false;
                GetAllSubsetsOfSizeK(numbers, usedPositions, startIndex + 1, currentSize, k, callback);
            }     
        }

        /// <summary>
        /// Executes a function for each possible permutation(!combination) of a given collection
        /// </summary>
        /// <param name="data">The collection to permutate</param>
        /// <param name="n">Length of the collection</param>
        /// <param name="i">current index</param>
        /// <param name="callback">The function to execute on each permutation</param>
        static void ForEachPermutation(List<int> data, int n, int i, Action<List<int>> callback)
        {
            if (i >= n - 1)
            {
                callback(data);
            }
            else
            {
                ForEachPermutation(data, n, i + 1, callback);
                for (int j = i + 1; j < n; j++)
                {
                    Swap(data, i, j);
                    ForEachPermutation(data, n, i + 1, callback);
                    Swap(data, i, j);
                }
            }
        }

        static void TestForStuckNumber(List<int> data)
        {
            int a = data[0];
            int b = data[1];
            int c = data[2];
            int d = data[3];

            int stuckerA = (a * GetPowersOfTen(b, 10)) + b;
            int stuckerB = (c * GetPowersOfTen(d, 10)) + d;

            if (stuckerA == stuckerB)
            {
                stuckNumbers.Add(string.Format(
                    "{0}|{1}=={2}|{3}"
                    , data[0]
                    , data[1]
                    , data[2]
                    , data[3]));
            }
        }

        static void Swap(List<int> list, int i, int j)
        {
            int memo = list[i];

            list[i] = list[j];
            list[j] = memo;
        }

        static int GetPowersOfTen(int number, int power)
        {
            if (number / 10 == 0)
            {
                return power;
            }

            return GetPowersOfTen(number /= 10, power *= 10);
        }

        /// <summary>
        /// An "adapter" function to pass the data through additional filters without storing it (Less memory usage)
        /// </summary>
        /// <param name="data"></param>
        static void AdpaterFunction(List<int> data)
        {
            ForEachPermutation(data, 4, 0, TestForStuckNumber);
        }

        static void Main()
        {
            int count = int.Parse(Console.ReadLine());

            int[] numbers = Console
                .ReadLine()
                .Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            GetAllSubsetsOfSizeK(numbers, new bool[numbers.Length], 0, 0, 4, AdpaterFunction);

            if (stuckNumbers.Count > 0)
            {
                foreach (var item in stuckNumbers)
                {
                    Console.WriteLine(item);
                }
            }
            else
            {
                Console.WriteLine("No");
            }
        }
    }
}
