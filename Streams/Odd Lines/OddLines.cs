namespace SoftUni.Homeworks.AdvancedCSharp.Streams
{
    using System;
    using System.IO;
    using HomeworkHelpers;

    /// <summary>
    /// Write a program that reads a text file and prints on the console its odd lines.
    /// Line numbers starts from 0. Use StreamReader.
    /// </summary>
    internal class OddLines
    {
        private static ConsoleHelper helper = new ConsoleHelper();

        private static void Main()
        {
            helper.Setup();

            helper.PrintHeading("Odd Lines ");

            string pathToFile = "../../PlainText.txt";

            helper.PrintColorText("Press any key to load ", ConsoleColor.DarkGreen);
            helper.PrintColorText("PlainText.txt\n\n", ConsoleColor.DarkGray);
            Console.ReadKey(true);

            helper.PrintColorText("OddLines: \n\n", ConsoleColor.DarkGray);

            var textStream = new StreamReader(pathToFile);

            using (textStream)
            {
                while (!textStream.EndOfStream)
                {
                    textStream.ReadLine();
                    Console.WriteLine(textStream.ReadLine());
                }
            }

            helper.Restart(Main);
        }   
    }
}