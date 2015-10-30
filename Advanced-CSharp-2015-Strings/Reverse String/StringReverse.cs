namespace SoftUni.Homeworks.AdvancedCSharp.Strings
{
    using System;
    using System.Linq;
    using HomeworkHelpers;

    /// <summary>
    /// Write a program that reads a string from the console, reverses it and prints the 
    /// result back at the console.
    /// </summary>
    class StringReverse
    {
        static TextHelper helper = new TextHelper();

        static void Main()
        {
            helper.SetupConsole();

            helper.PrintHeading("Reverse String");        
            Console.Write("Enter a string to reverse: ");
            string input = helper.ReadConsoleInColor(ConsoleColor.Blue);

            helper.PrintColorText("\nResult: ", ConsoleColor.DarkRed);
            string result = ReverseString(input);

            helper.PrintColorText(result, ConsoleColor.DarkGray);

            helper.Restart(Main);
        }

        private static string ReverseString(string input)
        {
            return new string(input.ToCharArray().Reverse().ToArray());
        }
    }
}
