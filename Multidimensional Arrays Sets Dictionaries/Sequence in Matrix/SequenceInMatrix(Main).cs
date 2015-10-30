namespace SoftUni.Homeworks.AdvancedCSharp.Multidimetional
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using HomeworkHelpers;

    /// <summary>
    /// We are given a matrix of strings of size N x M. Sequences in the matrix we define as sets of 
    /// severa neighbour elements located on the same line, column or diagonal. Write a program that finds
    /// the longest sequence of equal strings in the matrix.
    /// 
    /// ... I overdone it a bit ;)
    /// </summary>
    class SequenceInMatrix
    {
        static TextHelper Helper = new TextHelper();

        static MatrixSequenceChecks patternChecks = new MatrixSequenceChecks();

        static int paddingSize = 4;

        static char[] splitChars = { ' ', ',' };

        static void Main()
        {
            Helper.SetupConsole();

            Helper.PrintColorText("Sequence in Matrix\n\n", "cyan");

            int[] dimensions = ReadDimensions("matrix");

            string[,] myMatrix = new string[dimensions[0], dimensions[1]];
            //{
            //    { "ha", "fifi", "ho", "hi" },
            //    { "fo", "ha", "hi", "xx" },
            //    { "xxx", "ho", "ha", "xx" },
            //};

            ReadMatrix(myMatrix);

            MatrixPattern<string> longestPattern = GetLongestPattern(myMatrix);

            PrintMatrix(myMatrix, longestPattern.PatternMask, paddingSize + 1);
            Console.WriteLine();

            Console.Write("Element: ");
            Helper.PrintColorText(string.Format("'{0}'\n", longestPattern.Footprint), "cyan");
            Console.Write("Sequence Length: ");
            Helper.PrintColorText(string.Format("{0}\n", longestPattern.Length), "cyan");

            Helper.Restart(Main);
        }

        static MatrixPattern<T> GetLongestPattern<T>(T[,] matrix)
            where T : IConvertible
        {
            MatrixPattern<T> result = null;

            List<Func<int, int, T, T[,], bool[,], int>> sequenceChecks =
                new List<Func<int, int, T, T[,], bool[,], int>>
            {
                patternChecks.CheckHorizontalSequence,
                patternChecks.CheckVerticalSequence,
                patternChecks.CheckRightDiagonalSequence,
                patternChecks.CheckLeftDiagonalSequence
            };

            int maxSequenceLength = 0;

            int n = matrix.GetLength(0),
                k = matrix.GetLength(1);

            bool[,] mask = new bool[n, k];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < k; j++)
                {
                    var currentPattern = new MatrixPattern<T>(matrix[i, j], i, j);

                    int lenghtAsString = matrix[i, j].ToString().Length;

                    if (lenghtAsString > paddingSize)
                    {
                        paddingSize = lenghtAsString;
                    }

                    int currentSequenceLength = CheckSequences(matrix, currentPattern, mask, sequenceChecks);
                    if (currentSequenceLength > maxSequenceLength)
                    {
                        maxSequenceLength = currentSequenceLength;

                        result = currentPattern;
                    }
                }
            }

            return result;
        }

        private static int CheckSequences<T>(T[,] matrix, MatrixPattern<T> pattern, bool[,] mask
            , List<Func<int, int, T, T[,], bool[,], int>> callbacks)
            where T : IConvertible
        {
            T footprint = pattern.Footprint;

            int count = 0,
                top = pattern.StartRow,
                left = pattern.StartCol;

            foreach (var check in callbacks)
            {
                Array.Clear(mask, 0, mask.Length);
                count = check(top, left, footprint, matrix, mask);

                if (count > pattern.Length)
                {
                    pattern.Length = count;
                    pattern.PatternMask = (bool[,])mask.Clone();
                }
            }

            return pattern.Length;
        }

        static void ReadArray<T>(ref T[] arr)
        {
            arr = Console
                    .ReadLine()
                    .Split(splitChars, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x =>
                    {
                        if (paddingSize < x.Length)
                        {
                            paddingSize = x.Length;
                        }

                        return (T)Convert.ChangeType(x, typeof(T));
                    })
                    .ToArray();
        }

        static void ReadMatrix<T>(T[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                Helper.PrintColorText(string.Format("Enter data for row {0}: ", i), "white");
                T[] currentLine = new T[matrix.GetLength(0)];
                ReadArray(ref currentLine);

                for (int j = 0; j < currentLine.Length; j++)
                {
                    matrix[i, j] = currentLine[j];
                }
            }
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

        static void PrintMatrix<T>(T[,] matrix
            , bool[,] mask, int padding) where T : IConvertible
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

                    if (mask[i, j])
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
