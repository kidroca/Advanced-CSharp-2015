namespace HomeworkHelpers
{
    using System;

    public class TextHelper
    {
        public void SetupConsole()
        {
            Console.Clear();
            try
            {
                Console.SetWindowSize(120, Console.WindowHeight);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                // Prevent Crashing if Console window is set too wide
            }
        }

        public void Restart(Action restartCallback)
        {
            this.PrintColorText("\n\nPRESS ANY KEY TO RESTART \n(Ctrl + C to exit)\n\n", "red");
            Console.CursorVisible = false;
            Console.ReadKey(true);
            Console.CursorVisible = true;
            restartCallback(); 
        }

        public void PrintColorText(string text, ConsoleColor color)
        {
            var previousColor = Console.ForegroundColor;
            Console.ForegroundColor = color;

            Console.Write(text);

            Console.ForegroundColor = previousColor;
        }

        public void PrintColorText(string text, ConsoleColor color, ConsoleColor background)
        {
            var previousBackground = Console.BackgroundColor;
            Console.BackgroundColor = background;

            this.PrintColorText(text, color);

            Console.BackgroundColor = previousBackground;
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
