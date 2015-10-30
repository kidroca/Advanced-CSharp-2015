namespace SoftUni.Homeworks.AdvancedCSharp.Multidimetional
{
    using System;
    using System.Linq;
    using HomeworkHelpers;

    /// <summary>
    /// Write a program that reads a rectangular integer matrix of size N x M and finds in it the
    /// square 3 x 3 that has maximal sum of its elements.
    /// On the first line, you will receive the rows N and columns M.On the next N lines you will
    /// receive each row with its columns.
    /// Print the elements of the 3 x 3 square as a matrix, along with their sum.
    /// </summary>
    class MaximalSum
    {
        static TextHelper Helper = new TextHelper();

        static char[] splitChars = { ' ', ',' };

        static int bestLeft = 0;

        static int bestTop = 0;

        static int maxValue = int.MinValue;

        static void Main()
        {
            Helper.SetupConsole();

            Helper.PrintColorText("Maximal Sum\n\n", "cyan");

            int[,] myMatrix = ReadInput();
            //{
            //    { 1, 5, 5, 2, 4 },
            //    { 2, 1, 4, 14, 3 },
            //    { 3, 7, 11, 2, 8 },
            //    { 4, 8, 12, 16, 4 }
            //};

            int[] boxDimensions = ReadDimensions("searched box(3 3)");

            while (boxDimensions[0] > myMatrix.GetLength(0) ||
                boxDimensions[1] > myMatrix.GetLength(1))
            {
                Console.WriteLine();
                Helper.PrintColorText("The specified box dimensions are wider than the actual matrix \n", "red");
                Helper.PrintColorText("Try Again \n", "red");
                boxDimensions = ReadDimensions("searched box(3x3)");
            }

            int maxSum = FindMaxSumOfELements(myMatrix, boxDimensions[0], boxDimensions[1]);
            Console.WriteLine("Max sum: {0}", maxSum);

            Console.WriteLine();
            Helper.PrintColorText("Your Matrix: \n\n", "white");

            string maxValueAsString = maxValue.ToString();
            int padding = maxValueAsString.Length > 3 ? maxValueAsString.Length + 1 : 3;

            PrintMatrix(myMatrix, padding, bestTop, bestLeft, boxDimensions[0], boxDimensions[1]);

            Helper.Restart(Main);
        }

        static int[,] ReadInput()
        {
            int[,] theMatrix;
            int[] dimensions = ReadDimensions("matrix");

            if (dimensions.Length > 1)
            {
                theMatrix = new int[dimensions[0], dimensions[1]];
            }
            else
            {
                theMatrix = new int[dimensions[0], dimensions[0]];
            }

            for (int i = 0; i < theMatrix.GetLength(0); i++)
            {
                Helper.PrintColorText(string.Format("Enter numbers for row {0}: ", i), "white");
                var currentLine = Console
                    .ReadLine()
                    .Split(splitChars, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                for (int j = 0; j < currentLine.Length; j++)
                {
                    theMatrix[i, j] = currentLine[j];
                }
            }

            return theMatrix;
        }

        static int[] ReadDimensions(string subject)
        {
            Helper.PrintColorText(string.Format("Enter the dimesions of the {0}: ", subject), "white");
            int[] dimensions = Console
                .ReadLine()
                .Split(splitChars, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            return dimensions;
        }

        static int FindMaxSumOfELements(int[,] container, int height, int width
            , int bestSum = int.MinValue, int left = 0, int top = 0)
        {
            int currentSum = CalculateCurrentSum(container, left, top, height, width);
            if (currentSum > bestSum)
            {
                bestSum = currentSum;
                bestLeft = left;
                bestTop = top;
            }

            if (left + width >= container.GetLength(1))
            {
                if (top + height >= container.GetLength(0))
                {
                    return bestSum;
                }
                else
                {
                    left = 0;
                    top++;
                }
            }
            else
            {
                left++;
            }

            return FindMaxSumOfELements(container, height, width, bestSum, left, top);
        }

        static int CalculateCurrentSum(int[,] container, int left, int top, int height, int width)
        {
            int currentSum = 0;

            for (int i = top; i < top + height; i++)
            {
                for (int j = left; j < left + width; j++)
                {
                    if (container[i, j] > maxValue)
                    {
                        maxValue = container[i, j];
                    }

                    currentSum += container[i, j];
                }
            }

            return currentSum;
        }

        static void PrintMatrix<T>(T[,] matrix
            ,int padding = 3, int bestTop = -1, int bestLeft = -1, int height = 0, int width = 0)
        {
            string resultTemplate = "{0,-" + padding + "}";
            string border = ' ' + new string('-', matrix.GetLength(1) * padding + padding - 1);
            string side = "|";

            Helper.PrintColorText(border, "white");
            Console.WriteLine();

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                Helper.PrintColorText(string.Format(resultTemplate, side), "white");

                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    T current = matrix[i, j];
                    string result = string.Format(resultTemplate, current);

                    if (bestTop <= i && i < bestTop + height &&
                        bestLeft <= j && j < bestLeft + width)
                    {
                        Helper.PrintColorText(result, ConsoleColor.Blue, ConsoleColor.DarkGray);
                    }
                    else
                    {
                        Console.Write(result);
                    }
                }

                Helper.PrintColorText(side, "white");
                Console.WriteLine();
            }


            Helper.PrintColorText(border, "white");
        }
    }
}
