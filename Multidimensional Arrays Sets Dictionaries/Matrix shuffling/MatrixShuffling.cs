namespace SoftUni.Homeworks.AdvancedCSharp.Multidimetional
{
    using System;
    using System.Linq;
    using HomeworkHelpers;

    /// <summary>
    /// Write a program which reads a string matrix from the console and performs certain operations
    /// with its elements. User input is provided like in the problem above – first you read the
    /// dimensions and then the data. Remember, you are not required to do this step first, you may
    /// add this functionality later.  
    /// 
    /// Your program should then receive commands in format: "swap x1 y1 x2 y2" where
    /// x1, x2, y1, y2 are coordinates in the matrix.In order for a command to be valid, it should
    /// start with the "swap" keyword along with four valid coordinates (no more, no less). You
    /// should swap the values at the given coordinates(cell[x1, y1] with cell[x2, y2]) and print
    /// the matrix at each step(thus you'll be able to check if the operation was performed correctly).
    /// 
    /// If the command is not valid (doesn't contain the keyword "swap", has fewer or more coordinates
    /// entered or the given coordinates do not exist), print "Invalid input!" and move on to the next
    /// command. Your program should finish when the string "END" is entered. 
    /// </summary>
    class MatrixShuffling
    {
        static TextHelper Helper = new TextHelper();

        static int paddingSize = 4;

        static char[] splitChars = { ' ', ',' };

        static void Main()
        {
            Helper.SetupConsole();

            Helper.PrintColorText("Matrix shuffling\n\n", "cyan");

            int[] dimensions = ReadDimensions("matrix");

            string[,] myMatrix = new string[dimensions[0], dimensions[1]];
            //{
            //    { 1, 5, 5, 2, 4 },
            //    { 2, 1, 4, 14, 3 },
            //    { 3, 7, 11, 2, 8 },
            //    { 4, 8, 12, 16, 4 }
            //};
            ReadMatrix(myMatrix);

            ShuffleMatrix(myMatrix);

            Helper.Restart(Main);
        }

        private static void ShuffleMatrix<T>(T[,] myMatrix)
        {
            Console.Clear();

            Helper.PrintColorText("Your Matrix: \n\n", "white");
            PrintMatrix(myMatrix, paddingSize + 1);

            bool readingCommands = true;
            while (readingCommands)
            {
                Console.WriteLine();
                Helper.PrintColorText("The console is listening for your command(eg. 'swap 0 0 1 1'): ", "white");

                string[] input = null;
                ReadArray(ref input);

                bool printError = false;

                if (input[0].ToLower() == "end")
                {
                    readingCommands = false;
                    continue;
                }

                if (input[0].ToLower() == "swap" && input.Length == 5)
                {
                    int i1 = int.Parse(input[1]),
                        j1 = int.Parse(input[2]),
                        i2 = int.Parse(input[3]),
                        j2 = int.Parse(input[4]);

                    if (ValidateIndexes(myMatrix, i1, j1, i2, j2))
                    {
                        SwapMatrixIndexes(myMatrix, i1, j1, i2, j2);
                    }
                    else
                    {
                        printError = true;
                    }
                }
                else
                {
                    printError = true;
                }

                if (printError)
                {
                    Helper.PrintColorText("\nInvalid input! Try again.\n", "red");
                }
                else
                {
                    Helper.PrintColorText("Success, press a key to continue...", "green");
                    Console.ReadKey(true);
                    Console.Clear();

                    Helper.PrintColorText("Your Matrix: \n\n", "white");
                    PrintMatrix(myMatrix, paddingSize + 1);
                }
            }
        }

        private static bool ValidateIndexes<T>(T[,] myMatrix, int i1, int j1, int i2, int j2)
        {
            int rows = myMatrix.GetLength(0);
            int cols = myMatrix.GetLength(1);

            if ((i1 < 0 || rows <= i1) ||
                (j1 < 0 || cols <= j1) ||
                (i2 < 0 || rows <= i2) ||
                (j2 < 0 || cols <= j2))
            {
                return false;
            }
            else
            {
                return true;
            }
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
            , int padding = 4, int bestTop = -1, int bestLeft = -1, int height = 0, int width = 0)
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

        static void SwapMatrixIndexes<T>(T[,] matrix, int i1, int j1, int i2, int j2)
        {
            T swapSpace = matrix[i1, j1];

            matrix[i1, j1] = matrix[i2, j2];
            matrix[i2, j2] = swapSpace;
        }
    }
}
