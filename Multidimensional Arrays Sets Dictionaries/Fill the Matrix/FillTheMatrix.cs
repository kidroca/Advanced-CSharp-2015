namespace SoftUni.Homeworks.AdvancedCSharp.Multidimetional
{
    using System;
    using System.Linq;
    using HomeworkHelpers;

    /// <summary>
    /// Write two programs that fill and print a matrix of size N x N. Filling a matrix in the regular pattern
    ///  (top to bottom and left to right) is boring. Fill the matrix as described in both patterns below:
    /// 
    ///                                     1 5 9  13       1 8 9  16
    ///                                     2 6 10 14       2 7 10 15
    ///                                     3 7 11 15       3 6 11 14
    ///                                     4 8 12 16       4 5 12 13
    /// 
    /// </summary>
    class FillTheMatrix
    {
        static TextHelper Helper = new TextHelper();

        static void Main()
        {
            Helper.SetupConsole();

            Helper.PrintColorText("Fill the Matrix\n\n", "cyan");

            Helper.PrintColorText("Enter the matrix rows and cols: ", "white");
            int[] dimensions = Console
                .ReadLine()
                .Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            int[,] neoMatrix;

            int padding = 9;

            if (dimensions.Length > 1)
            {
                neoMatrix = new int[dimensions[0], dimensions[1]];
                padding = Math.Max(padding, Math.Max(dimensions[0], dimensions[1]));
            }
            else
            {
                neoMatrix = new int[dimensions[0], dimensions[0]];
                padding = Math.Max(dimensions[0], padding);
            }

            Helper.PrintColorText("Pick a fill pattern: press 'A' or 'B': ", "white");

            ConsoleKeyInfo pressed = new ConsoleKeyInfo();
            while (pressed.Key != ConsoleKey.A &&
                pressed.Key != ConsoleKey.B)
            {
                pressed = Console.ReadKey(true);
            }

            FillMatrix(neoMatrix, pressed.Key);

            Console.WriteLine();
            Helper.PrintColorText("Your Matrix: \n\n", "white");

            PrintMatrix(neoMatrix, padding / 3);

            Helper.Restart(Main); // Not very bright :)
        }

        static void FillMatrix(int[,] matrix, ConsoleKey pattern)
        {
            if (pattern == ConsoleKey.A)
            {
                FillPatternA(matrix);
                return;
            }

            if (pattern == ConsoleKey.B)
            {
                FillPatternB(matrix);
            }
        }

        static void PrintMatrix(int[,] matrix, int padding)
        {
            string printFormat = "{0," + padding + "}";
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Helper.PrintColorText(string.Format(printFormat, matrix[i, j]), "green");
                }

                Console.WriteLine();
            }
        }

        static void FillPatternA(int[,] matrix)
        {
            int elementCount = 1;

            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    matrix[j, i] = elementCount;
                    elementCount++;
                }
            }
        }

        static void FillPatternB(int[,] matrix)
        {
            int elementCount = 1;

            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    matrix[j, i] = elementCount;
                    elementCount++;
                }

                i++;
                if (i >= matrix.GetLength(1))
                {
                    break;
                }

                for (int j = matrix.GetLength(0) - 1; j >= 0; j--)
                {
                    matrix[j, i] = elementCount;
                    elementCount++;
                }
            }
        }
    }
}
