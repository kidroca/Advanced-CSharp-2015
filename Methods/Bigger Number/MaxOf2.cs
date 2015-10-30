namespace SoftUni.Homeworks.AdvancedCSharp.Methods
{
    using System;

    /// <summary>
    /// Write a method GetMax() with two parameters that returns the larger of two integers. Write
    /// a program that reads 2 integers from the console and prints the largest of them using the method GetMax().
    /// </summary>
    class MaxOf2
    {
        static void Main()
        {
            Console.Clear();

            Console.Write("Enter the first number: ");
            int first = int.Parse(Console.ReadLine());

            Console.Write("Enter the second number: ");
            int second = int.Parse(Console.ReadLine());

            Console.WriteLine("The bigger number is: {0}",GetMax(first, second));

            Console.WriteLine("Press any key to restart or Ctrl + C to Exit");
            Console.ReadKey(true);
            Main();
        }

        static T GetMax<T>(T a, T b) where T : IComparable
        {
            int compare = a.CompareTo(b);

            if (compare < 0)
            {
                return b;
            }
            else
            {
                return a;
            }
        }
    }
}
