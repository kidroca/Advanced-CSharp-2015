namespace SoftUni.Homeworks.AdvancedCSharp.Arrays
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using HomeworkHelpers;

    /// <summary>
    /// Write a program that reads an array of strings and finds in it all sequences of equal elements
    /// (comparison should be case-sensitive). The input strings are given as a single line, separated
    /// by a space.
    /// </summary>
    class EqualStrings
    {
        static TextHelper Helper = new TextHelper();

        static void Main()
        {
            Console.Clear();
            Helper.PrintColorText("Sequences of Equal Strings \n\n", "cyan");

            Console.Write("Enter words/strings separated by space character: ");

            Console.ForegroundColor = ConsoleColor.White;
            string[] stringsArr = Console
                .ReadLine()
                .Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries)
                .ToArray();

            Console.ResetColor();

            // We are creating a list of sub lists (just like Folder containing Folders containing other stuff)
            List<List<string>> sequences = new List<List<string>>();

            int index = 0;
            while (index < stringsArr.Length)
            {
                // We are creating a list for the current sequence
                List<string> currentSequence = new List<string>();

                string currentString = stringsArr[index];
                currentSequence.Add(currentString);

                index++;
                while (index < stringsArr.Length && 
                    currentString == stringsArr[index])
                {
                    // while the next element is equal to the previous add it to the sequence
                    currentSequence.Add(currentString);
                    index++;
                }

                // Add the sequence to the list of lists 
                sequences.Add(currentSequence);
            }


            Console.WriteLine();
            Console.WriteLine("Sequences: ");
            // Join the elements of each sequence and display them (or just display 
            // the first element of the sequence n times where n is the .Count of the sequence)
            for (int i = 0; i < sequences.Count; i++)
            {
                string currentSequence = string.Join(" ", sequences[i]);
                Console.Write("{0}: ", i + 1);
                Helper.PrintColorText(currentSequence, "green");

                Console.WriteLine();
            }

            Helper.PrintColorText("\n\nPRESS ANY KEY TO RESTART", "red");
            Console.CursorVisible = false;
            Console.ReadKey(true);
            Console.CursorVisible = true;
            Main();
        }
    }
}


