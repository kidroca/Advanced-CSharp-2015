namespace SoftUni.Homeworks.AdvancedCSharp.Arrays
{
    using System;
    using System.Linq;

    /// <summary>
    /// This problem is from the Java Basics Exam (8 February 2015). You may check your solution here.
    /// https://judge.softuni.bg/Contests/Practice/Index/69
    /// 
    /// Write a program to read an array of numbers from the console, sort them and print them back 
    /// on the console. The numbers should be entered from the console on a single line, separated 
    /// by a space.
    /// </summary>
    class LegoBlocks
    {
        static double[][] ReadJaggedArray(int rows)
        {
            var jagged = new double[rows][];

            for (int i = 0; i < rows; i++)
            {
                Console.Write("Enter numbers for row {0}: ", i); // Comment out for Judge
                Console.ForegroundColor = ConsoleColor.White;
                double[] arr = Console
                    .ReadLine()
                    .Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(double.Parse)
                    .ToArray();

                Console.ResetColor();

                jagged[i] = arr;
            }

            return jagged;
        }

        static double[][] FitBlocks(double[][] blockA, double[][] blockB)
        {
            int rows = blockA.Length;
            int expectedCols = blockA[0].Length + blockB[0].Length;

            double[][] result = new double[rows][];
            for (int i = 0; i < rows; i++)
            {
                int colsA = blockA[i].Length,
                    colsB = blockB[i].Length;

                if (colsA + colsB != expectedCols)
                {
                    throw new ApplicationException(); // No message not to interfere with the Judge
                }
                else
                {
                    double[] currentRow = blockA[i]
                        .Concat(blockB[i].Reverse())
                        .ToArray();

                    result[i] = currentRow;
                }
            }

            return result;
        }

        static int CountTotalNumberOfCells(double[][] blockA, double[][] blockB)
        {
            int cellsInA = 0;
            Array.ForEach(blockA, arr => cellsInA += arr.Length);

            int cellsInB = 0;
            Array.ForEach(blockB, arr => cellsInB += arr.Length);

            return cellsInA + cellsInB;
        }

        // No helpers to be able to copy - paste to Judge
        static void Main()
        {
            Console.Clear(); // Comment out for Judge
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Lego Blocks \n\n"); // Comment out for Judge
            Console.ResetColor();

            Console.Write("Enter number of rows: "); // Comment out for Judge
            Console.ForegroundColor = ConsoleColor.White;
            int rows = int.Parse(Console.ReadLine());
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("First Array (Block)"); // Comment out for Judge
            Console.ResetColor();
            double[][] blockA = ReadJaggedArray(rows);

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Second Array (Block)"); // Comment out for Judge
            Console.ResetColor();
            double[][] blockB = ReadJaggedArray(rows);

            try
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Green;
                double[][] matchedMatrix = FitBlocks(blockA, blockB);
                foreach (var row in matchedMatrix)
                {
                    string rowData = string.Join(", ", row);
                    Console.WriteLine("[{0}]", rowData);
                }
                Console.ResetColor();
            }
            catch (ApplicationException)
            {
                int totalCells = CountTotalNumberOfCells(blockA, blockB);
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("The total number of cells is: {0}", totalCells);
                Console.ResetColor();
            }

            // Comment out all lines bellow for Judge
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("\n\nPRESS ANY KEY TO RESTART");
            Console.CursorVisible = false;
            Console.ReadKey(true);
            Console.CursorVisible = true;
            Console.ResetColor();
            Main();
        }
    }
}
