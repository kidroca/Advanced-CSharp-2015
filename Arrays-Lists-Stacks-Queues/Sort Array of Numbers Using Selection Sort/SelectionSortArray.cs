namespace SoftUni.Homeworks.AdvancedCSharp.Arrays
{
    using System;
    using System.Linq;
    using HomeworkHelpers;

    /// <summary>
    /// Write a program to sort an array of numbers and then print them back on the console. The numbers should
    /// be entered from the console on a single line, separated by a space. Refer to the examples for problem 1.
    /// Note: Do not use the built-in sorting method, you should write your own. Use the selection sort
    /// algorithm. (https://en.wikipedia.org/wiki/Selection_sort)
    /// Hint: To understand the sorting process better you may use a visual aid, e.g. Visualgo.
    /// (http://visualgo.net/sorting.html)
    /// </summary>
    class SelectionSortArray
    {
        static TextHelper Helper = new TextHelper();

        static void Main()
        {
            Console.Clear();
            Helper.PrintColorText("Selection Sort Array \n\n", "cyan");

            Console.Write("Enter numbers separated by space character: ");

            Console.ForegroundColor = ConsoleColor.White;
            double[] numbers = Console
                .ReadLine()
                .Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(double.Parse)
                .ToArray();

            Console.ResetColor();

            // This is the left-most unsorted index which will be swapped
            int leftSwapIndex = 0;

            while (leftSwapIndex < numbers.Length - 1)
            {
                // The next minimum value searched (initialy the first unsorted position is assigned)
                double nextMinValue = numbers[leftSwapIndex];
                // The index at which the minimum value is found will be stored in rightSwapIndex (if found)
                int rightSwapIndex = leftSwapIndex;

                // The cycle starts from the second unsorted position
                for (int i = leftSwapIndex + 1; i < numbers.Length; i++)
                {
                    if (nextMinValue > numbers[i])
                    {
                        nextMinValue = numbers[i];
                        rightSwapIndex = i;
                    }
                }

                // Swap values only if the indexes are not the same index
                if (leftSwapIndex != rightSwapIndex)
                {
                    numbers[rightSwapIndex] = numbers[leftSwapIndex];
                    numbers[leftSwapIndex] = nextMinValue;
                }

                // Since this index is now sorted move one up
                leftSwapIndex++;
            }
            Console.WriteLine();
            Console.Write("Sorted: ");
            Helper.PrintColorText(string.Join(" ", numbers), "green");

            Helper.PrintColorText("\n\nPRESS ANY KEY TO RESTART", "red");
            Console.CursorVisible = false;
            Console.ReadKey(true);
            Console.CursorVisible = true;
            Main();
        }
    }
}
