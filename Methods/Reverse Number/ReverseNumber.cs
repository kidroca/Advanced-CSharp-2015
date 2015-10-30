namespace SoftUni.Homeworks.AdvancedCSharp.Methods
{
    using System;

    /// <summary>
    /// Write a method that reverses the digits of a given floating-point number.
    /// </summary>
    class ReverseNumber
    {
        static void Main()
        {
            Console.Clear();

            Console.Write("Enter a number to be reversed: ");
            float number = float.Parse(Console.ReadLine());

            Console.WriteLine("Reversed: {0}", GetReversedNumber(number));

            Console.WriteLine("Press any key to restart or Ctrl + C to Exit");
            Console.ReadKey(true);
            Main();
        }

        static T GetReversedNumber<T>(T number) where T : struct
        {
            string[] splittedByDecimalPoint = number
                .ToString()
                .Split('.');

            string result = ReverseString(splittedByDecimalPoint[0]);

            if (splittedByDecimalPoint.Length > 1)
            {
                string fractionalPart = ReverseString(splittedByDecimalPoint[1]);

                result = fractionalPart + '.' + result;
            }

            return (T)Convert.ChangeType((result), typeof(T));
        }

        private static string ReverseString(string s)
        {
            char[] asArray = s.ToCharArray();
            Array.Reverse(asArray);

            return new string(asArray);
        }
    }
}
