namespace PrimesInRangeAsync
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    class Program
    {
        static void Main()
        {
            Console.WriteLine("Getting primes");
            var task = Task.Run(() => PrimesInRange(1, 100000));

            Console.WriteLine("Type something while waiting");
            while (!task.IsCompleted)
            {
                Console.ReadLine();
            }

            Console.WriteLine("Task completed! Press any key to print results");
            Console.ReadKey(true);

            PrintNumbers(task.Result);
        }

        static List<int> PrimesInRange(int rangeFirst, int rangeLast)
        {
            List<int> primes = new List<int>();

            for (int number = rangeFirst; number < rangeLast; number++)
            {
                bool isPrime = true;
                for (int divider = 2; divider < number; divider++)
                {
                    if (number % divider == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }
                if (isPrime)
                {
                    primes.Add(number);
                }
            }

            return primes;
        }

        static void PrintPrimesInRange(int rangeFirst, int rangeLast)
        {
            List<int> primesInRange = PrimesInRange(rangeFirst, rangeLast);

            Console.WriteLine("Primes from {0} to {1} calculated. Print now (y/n)?", rangeFirst, rangeLast);
            string userAnswer = Console.ReadLine();

            if (userAnswer == "y" || userAnswer == "Y")
            {
                foreach (var prime in primesInRange)
                {
                    Console.WriteLine(prime);
                }
            }
        }

        static void PrintNumbers(List<int> numbers)
        {
            foreach (var number in numbers)
            {
                Console.Write("{0} ", number);
            }
        }
    }
}
