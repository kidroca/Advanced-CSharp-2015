namespace SoftUni.Homeworks.AdvancedCSharp.Arrays
{
    using System;
    using System.Linq;
    using HomeworkHelpers;

    /// <summary>
    /// Write a program to read an array of numbers from the console, sort them and print them back 
    /// on the console. The numbers should be entered from the console on a single line, separated 
    /// by a space.
    /// </summary>
    class SortArray
    {
        static TextHelper Helper = new TextHelper();

        static void Main()
        {
            Console.Clear();
            Helper.PrintColorText("Sort Array \n\n", "cyan");

            Console.Write("Enter numbers separated by space character: ");

            // If you want to split a string at more than one characters you can pass a char[] containing
            // the characters you want to split at, and as a bonus you can use the StringSplitOptions
            // which is handy if for example you enter 2 spaces instead of one
            Console.ForegroundColor = ConsoleColor.White;
            double[] numbers = Console
                .ReadLine()
                .Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(double.Parse)
                .ToArray();

            Console.ResetColor();

            // Since Tasks that do not allow to use the built in functionallity state that explicitly in the 
            // Task description (And this task doesn't) we are allowed to use the built in .Sort() method.
            Array.Sort(numbers);

            Console.WriteLine();
            Console.Write("Sorted: ");

            string result = string.Join(" ", numbers);
            Helper.PrintColorText(result, "green");

            Helper.PrintColorText("\n\nPRESS ANY KEY TO RESTART", "red");
            Console.CursorVisible = false;
            Console.ReadKey(true);
            Console.CursorVisible = true;
            Main();  // Cool || uncool way to make the program restart ... forever (Until StackOverFlow || Ctrl + X to exit)
        }
    }
}
