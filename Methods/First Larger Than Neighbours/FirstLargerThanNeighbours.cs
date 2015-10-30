namespace SoftUni.Homeworks.AdvancedCSharp.Methods
{
    using System;
    using System.Linq;

    /// <summary>
    /// Write a method that returns the index of the first element in array that is larger than its 
    /// neighbours, or -1 if there's no such element. Use the method from the previous exercise
    /// </summary>
    class FirstLargerThanNeighbours
    {
        static void Main()
        {
            Console.Clear();

            Console.Write("enter an array of integers separated by space: ");
            decimal[] numbers = Console
                .ReadLine()
                .Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(decimal.Parse)
                .ToArray();
            //{
            //    1, 3, 4, 5, 1, 0, 5
            //};

            Console.WriteLine(
                "The index of the first number larger than its neighbors is: {0}"
                , IndexOfFirstLargerThanNeigbours(numbers));           

            Console.WriteLine("Press any key to restart or Ctrl + C to Exit");
            Console.ReadKey(true);
            Main();
        }

        private static int IndexOfFirstLargerThanNeigbours<T>(T[] numbers) where T : IComparable
        {
            for (int i = 0; i < numbers.Length; i++)
            {
                if (IsLargerThanNeigbours(numbers, i))
                {
                    return i;
                }
            }

            return -1;
        }

        private static bool IsLargerThanNeigbours<T>(T[] arr, int i) where T : IComparable
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

            return arr[i].CompareTo(arr[leftNeighbor]) > 0 && arr[i].CompareTo(arr[rightNeighbor]) > 0;
        }
    }
}
