namespace SoftUni.Homeworks.AdvancedCSharp.Multidimetional
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    /// <summary>
    /// This problem is originally from the JavaScript Basics Exam (28 July 2014).
    /// You may check your solution here. https://judge.softuni.bg/Contests/Practice/Index/84
    /// 
    /// You are given a sequence of text lines. Assume these text lines form a matrix of
    /// characters (pad the missing positions with spaces to build a rectangular matrix).
    /// Write a program to rotate the matrix by 90, 180, 270, 360, … degrees. Print the
    /// result at the console as sequence of strings.
    /// </summary>
    class StringMatrixRotation
    {
        static void Main()
        {
            var rxPattern = new Regex(@"\d+");

            int degreesOfRotation = int.Parse(
                rxPattern.Match(
                    Console.ReadLine()).Value);

            List<string> renderedStuff = ReadInputLines();

            char[,] asMatrix = ConvertToCharMatrix(renderedStuff);

            int numberOfRotations = (degreesOfRotation / 90) % 4;

            for (int i = 0; i < numberOfRotations; i++)
            {
                asMatrix = Rotate90deg(asMatrix);
            }

            PrintMatrix(asMatrix);
        }

        static void PrintMatrix(char[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i, j]);
                }

                Console.WriteLine();
            }
        }

        static char[,] Rotate90deg(char[,] matrix)
        {
            int maxRows = matrix.GetLength(0),
                maxCols = matrix.GetLength(1);

            var rotated90 = new char[maxCols, maxRows];

            for (int i = 0; i < maxRows; i++)
            {
                for (int j = 0; j < maxCols; j++)
                {
                    int invertedI = (maxRows - 1) - i;

                    rotated90[j, invertedI] = matrix[i,j];
                }
            }

            return rotated90;
        }

        static char[,] ConvertToCharMatrix(List<string> input)
        {
            int maxRows = input.Count,
                maxCols = 0;

            foreach (var str in input)
            {
                if (str.Length > maxCols)
                {
                    maxCols = str.Length;
                }
            }

            var matrix = new char[maxRows, maxCols];

            for (int i = 0; i < maxRows; i++)
            {
                int inputLineLength = input[i].Length;

                for (int j = 0; j < maxCols; j++)
                {
                    if (j >= inputLineLength)
                    {
                        matrix[i, j] = ' ';
                    }
                    else
                    {
                        matrix[i, j] = input[i][j];
                    }
                }
            }

            return matrix;
        }

        static List<string> ReadInputLines()
        {
            string textStream = string.Empty;

            var renderedStuff = new List<string>();

            while (textStream != "END")
            {
                textStream = Console.ReadLine();

                renderedStuff.Add(textStream);
            }

            renderedStuff.RemoveAt(renderedStuff.Count - 1);

            return renderedStuff;
        }
    }
}
