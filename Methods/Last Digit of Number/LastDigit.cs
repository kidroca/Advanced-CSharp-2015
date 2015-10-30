namespace SoftUni.Homeworks.AdvancedCSharp.Methods
{
    using System;

    /// <summary>
    /// Write a method that returns the last digit of a given integer as an English word.
    /// Test the method with different input values. Ensure you name the method properly.
    /// </summary>
    class MaxOf2
    {
        static void Main()
        {
            Console.Clear();

            Console.Write("Enter a number: ");
            int testNumber = int.Parse(Console.ReadLine());

            Console.WriteLine("Last digit is: {0}", GetLastDigitAsWord(testNumber));

            Console.WriteLine("Press any key to restart or Ctrl + C to Exit");
            Console.ReadKey(true);
            Main();
        }
        
        static string GetLastDigitAsWord(int number)
        {
            int lastDigit = number % 10;

            switch (lastDigit)
            {
                case 1: return "one";
                case 2: return "two";
                case 3: return "three";
                case 4: return "four";
                case 5: return "five";
                case 6: return "six";
                case 7: return "seven";
                case 8: return "eight";
                case 9: return "nine";
                case 0: return "zero";

                default:
                    throw new ArgumentOutOfRangeException("Otivame na kuppooon");
            }
        }
    }
}
