namespace ArraySlider
{
    using System;
    using System.Linq;
    using System.Numerics;

    /// <summary>
    /// Exam Link: https://judge.softuni.bg/Contests/Practice/Index/92
    /// </summary>
    class Program
    {
        static char[] splitChars = { '\t', ' ' };

        static void Main()
        {
            BigInteger[] fillsArray = Console
                .ReadLine()
                .Trim()
                .Split(splitChars, StringSplitOptions.RemoveEmptyEntries)
                .Select(BigInteger.Parse)
                .ToArray();

            DoCommands(fillsArray);

            Console.WriteLine("[{0}]", string.Join(", ", fillsArray));
        }

        private static void DoCommands(BigInteger[] fillsArray)
        {
            long currentIndex = 0
                , length = fillsArray.Length;

            string currentCommand;
            while ((currentCommand = Console.ReadLine()) != "stop")
            {
                string[] commandParts = currentCommand.Split(' ');
                long offset = long.Parse(commandParts[0])
                    , operand = long.Parse(commandParts[2]);

                string operation = commandParts[1];

                currentIndex = SetCurrentIndex(currentIndex, offset, length);

                switch (operation)
                {
                    case "&":
                        fillsArray[currentIndex] &= operand;
                        break;
                    case "|":
                        fillsArray[currentIndex] |= operand;
                        break;
                    case "^":
                        fillsArray[currentIndex] ^= operand;
                        break;
                    case "+":
                        fillsArray[currentIndex] += operand;
                        break;
                    case "-":
                        fillsArray[currentIndex] -= operand;
                        break;
                    case "*":
                        fillsArray[currentIndex] *= operand;
                        break;
                    case "/":
                        fillsArray[currentIndex] /= operand;
                        break;
                    default:
                        break;
                }

                if (fillsArray[currentIndex] < 0)
                {
                    fillsArray[currentIndex] = 0;
                }
            }
        }

        private static long SetCurrentIndex(long currentIndex, long offset, long length)
        {
            offset %= length;

            currentIndex += offset;

            if (currentIndex < 0)
            {
                return currentIndex + length;
            }
            else if (currentIndex >= length)
            {
                return currentIndex - length;
            }
            else
            {
                return currentIndex;
            }
        }
    }
}
