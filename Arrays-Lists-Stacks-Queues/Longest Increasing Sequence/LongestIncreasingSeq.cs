namespace SoftUni.Homeworks.AdvancedCSharp.Arrays
{
    using System;
    using System.Linq;
    using HomeworkHelpers;

    /// <summary>
    /// Write a program to find all increasing sequences inside an array of integers. The integers are
    /// given on a single line, separated by a space. Print the sequences in the order of their
    /// appearance in the input array, each at a single line. Separate the sequence elements by a space.
    /// Find also the longest increasing sequence and print it at the last line. If several sequences
    /// have the same longest length, print the left-most of them.
    /// </summary>
    class LongestIncreasingSeq
    {
        static TextHelper Helper = new TextHelper();

        static void Main()
        {
            Console.Clear();
            Helper.PrintColorText("Longest Increasing Sequence \n\n", "cyan");

            Console.Write("Enter numbers separated by single space character: ");

            Console.ForegroundColor = ConsoleColor.White;
            double[] sequences = Console
                .ReadLine()
                .Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(double.Parse)
                .ToArray();

            Console.ResetColor();

            // If we have the end index and a length we can easily determine the start index
            int endIndex = 0,
                currentSequenceLenght = 1,
                maxSequenceLength = 1;

            double prevNumber = sequences[0];
            Console.WriteLine("Sequences: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("{0} ", prevNumber);

            for (int i = 1; i < sequences.Length; i++)
            {
                // if the sequence is increasing count the length
                double currentNumber = sequences[i];
                if (currentNumber > prevNumber)
                {
                    currentSequenceLenght++;

                    if (currentSequenceLenght > maxSequenceLength)
                    {
                        maxSequenceLength = currentSequenceLenght;
                        endIndex = i;
                    }
                }
                else // sequence ended reset the counter 
                {
                    Console.WriteLine();
                    currentSequenceLenght = 1;
                }

                Console.Write("{0} ", currentNumber);
                prevNumber = currentNumber;
            }

            Console.ResetColor();
            Console.WriteLine();

            double[] longestSequence = sequences
                    .Skip(endIndex - (maxSequenceLength  - 1))
                    .Take(maxSequenceLength)
                    .ToArray();

            Console.Write("Longest: ");
            Helper.PrintColorText(string.Join(" ", longestSequence), "green");

            Helper.PrintColorText("\n\nPRESS ANY KEY TO RESTART", "red");
            Console.CursorVisible = false;
            Console.ReadKey(true);
            Console.CursorVisible = true;
            Main();          
        }
    }
}


