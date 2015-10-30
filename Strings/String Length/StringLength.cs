namespace SoftUni.Homeworks.AdvancedCSharp.Strings
{
    using System;
    using HomeworkHelpers;

    /// <summary>
    /// Write a program that reads from the console a string of maximum 20 characters. 
    /// If the length of the string is less than 20, the rest of the characters should be 
    /// filled with *. Print the resulting string on the console.
    /// </summary>
    class StringLength
    {
        static TextHelper helper = new TextHelper();

        static void Main()
        {
            helper.SetupConsole();

            helper.PrintHeading("String Length ");
            Console.Write("Enter some text: ");
            string input = helper.ReadConsoleInColor(ConsoleColor.Blue);

            helper.PrintColorText("\nResult: ", ConsoleColor.DarkRed);
            string result = UglifyText(input, 20);

            helper.PrintColorText(result, ConsoleColor.DarkGray);

            helper.Restart(Main);
        }

        private static string UglifyText(string input, int charactersCount)
        {
            int length = input.Length;

            if (length < charactersCount)
            {
                return string.Format("{0}{1}", input, new string('*', charactersCount - length));
            }
            else
            {
                return input.Substring(0, charactersCount);
            }
        }
    }
}
