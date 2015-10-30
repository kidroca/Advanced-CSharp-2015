namespace HomeworkHelpers
{
    using System;

    public class TextHelper
    {
        public void PrintColorText(string text, ConsoleColor color)
        {
            var previousColor = Console.ForegroundColor;
            Console.ForegroundColor = color;

            Console.Write(text);

            Console.ForegroundColor = previousColor;
        }

        public void PrintColorText(string text, string color)
        {
            switch (color.ToLower())
            {
                case "green":
                    this.PrintColorText(text, ConsoleColor.Green);
                    break;
                case "cyan":
                    this.PrintColorText(text, ConsoleColor.Cyan);
                    break;
                case "white":
                    this.PrintColorText(text, ConsoleColor.White);
                    break;
                case "red":
                    this.PrintColorText(text, ConsoleColor.Red);
                    break;
                case "gray":
                    this.PrintColorText(text, ConsoleColor.Gray);
                    break;
                default:
                    Console.Write(text);
                    break;
            }
        }
    }
}
