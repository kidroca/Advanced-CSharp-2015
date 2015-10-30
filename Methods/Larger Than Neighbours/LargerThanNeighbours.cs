namespace SoftUni.Homeworks.AdvancedCSharp.Methods
{
    using System;
    using System.Linq;

    /// <summary>
    /// Write a method that checks if the element at given position in a given array of integers
    /// is larger than its two neighbours (when such exist).
    /// </summary>
    class LargerThanNeighbours
    {
        static void Main()
        {
            Console.Clear();

            Console.Write("enter an array of integers separated by space: ");
            int[] numbers = Console
                .ReadLine()
                .Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();
            //{
            //    1, 3, 4, 5, 1, 0, 5
            //};

            for (int i = 0; i < numbers.Length; i++)
            {
                Console.WriteLine("{0} is larger than it's neigbours: {1}", numbers[i], IsLargerThanNeigbours(numbers, i));
            }

            Console.WriteLine("Press any key to restart or Ctrl + C to Exit");
            Console.ReadKey(true);
            Main();
        }

        private static bool IsLargerThanNeigbours(int[] arr, int i)
        {
            int leftNeighbor = 0,
                rightNeighbor = arr.Length - 1;

            // When i is on the border we are using the start or the end of the array 
            // for the other neighbour
            if (i == 0)
            {
                leftNeighbor = rightNeighbor;
                rightNeighbor = i + 1;
            }
            else if (i == rightNeighbor)
            {
                rightNeighbor = 0;
                leftNeighbor = i - 1;
            }
            else
            {
                leftNeighbor = i - 1;
                rightNeighbor = i + 1;
            }

            return arr[i] > arr[leftNeighbor] && arr[i] > arr[rightNeighbor];
        }
    }
}
