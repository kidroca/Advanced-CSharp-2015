namespace Problem_1
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class Program
    {
        static void Main()
        {
            int[] trifonsArray = Console
                .ReadLine()
                .Trim()
                .Split(' ')
                .Select(int.Parse)
                .ToArray();

            //bool result = Exchange(trifonsArray, 5, out trifonsArray);
            //Console.WriteLine(string.Join(", ", trifonsArray));

            string currentCommand;
            while ((currentCommand = Console.ReadLine()) != "end")
            {
                string command = string.Empty;
                int value = 0;
                string oddEven = "even";

                string[] commandValue = currentCommand.Split(' ');

                if (currentCommand.StartsWith("exc"))
                {
                    
                    command = commandValue[0];
                    value = int.Parse(commandValue[1]);
                }
                else if (commandValue.Length == 3)
                {
                    command = string.Format("{0} {1}", commandValue[0], commandValue[2]);
                    value = int.Parse(commandValue[1]);
                }
                else
                {
                    command = string.Format("{0} {1}", commandValue[0], commandValue[1]);
                }

                int index;
                int[] collection;

                switch (command)
                {
                    case "exchange":
                        if (!Exchange(trifonsArray, value, out trifonsArray))
                        {
                            Console.WriteLine("Invalid index");
                        }

                        break;

                    case "max even":
                        if ((index = IndexOfMaxEvenOdd(trifonsArray)) != -1)
                        {
                            Console.WriteLine(index);
                        }
                        else
                        {
                            Console.WriteLine("No matches");
                        }

                        break;

                    case "max odd":
                        if ((index = IndexOfMaxEvenOdd(trifonsArray, odd: true)) != -1)
                        {
                            Console.WriteLine(index);
                        }
                        else
                        {
                            Console.WriteLine("No matches");
                        }

                        break;

                    case "min even":
                        if ((index = IndexOfMinEvenOdd(trifonsArray)) != -1)
                        {
                            Console.WriteLine(index);
                        }
                        else
                        {
                            Console.WriteLine("No matches");
                        }

                        break;

                    case "min odd":
                        if ((index = IndexOfMinEvenOdd(trifonsArray, odd: true)) != -1)
                        {
                            Console.WriteLine(index);
                        }
                        else
                        {
                            Console.WriteLine("No matches");
                        }

                        break;

                    case "first even":
                        if (value > trifonsArray.Length)
                        {
                            Console.WriteLine("Invalid count");
                            break;
                        }

                        collection = FirstEvenOddCount(trifonsArray, value);
                        if (collection.Length > 0)
                        {
                            Console.WriteLine("[{0}]", string.Join(", ", collection));
                        }
                        else
                        {
                            Console.WriteLine("[]");
                        }

                        break;

                    case "last even":
                        if (value > trifonsArray.Length)
                        {
                            Console.WriteLine("Invalid count");
                            break;
                        }

                        collection = LastEvenOddCount(trifonsArray, value);
                        if (collection.Length > 0)
                        {
                            Console.WriteLine("[{0}]", string.Join(", ", collection));
                        }
                        else
                        {
                            Console.WriteLine("[]");
                        }

                        break;

                    case "first odd":
                        if (value > trifonsArray.Length)
                        {
                            Console.WriteLine("Invalid count");
                            break;
                        }

                        collection = FirstEvenOddCount(trifonsArray, value, odd: true);
                        if (collection.Length > 0)
                        {
                            Console.WriteLine("[{0}]", string.Join(", ", collection));
                        }
                        else
                        {
                            Console.WriteLine("[]");
                        }

                        break;

                    case "last odd":
                        if (value > trifonsArray.Length)
                        {
                            Console.WriteLine("Invalid count");
                            break;
                        }

                        collection = LastEvenOddCount(trifonsArray, value, odd: true);
                        if (collection.Length > 0)
                        {
                            Console.WriteLine("[{0}]", string.Join(", ", collection));
                        }
                        else
                        {
                            Console.WriteLine("[]");
                        }

                        break;

                    default:
                        break;
                }
            }

            Console.WriteLine("[{0}]", string.Join(", ", trifonsArray));
        }

        static bool Exchange(int[] initial, int index, out int[] result)
        {
            if (0 <= index && index < initial.Length)
            {
                int[] newArray = initial
                .Skip(index + 1)
                .Take(initial.Length - index - 1)
                .Concat(initial.Take(index + 1))
                .ToArray();

                result = newArray;

                return true;
            }
            else
            {
                result = initial;

                return false;
            }
        }

        static int IndexOfMaxEvenOdd(int[] arr, bool odd = false)
        {
            int remainder = 0;

            if (odd)
            {
                remainder = 1;
            }

            var oddEven = arr.Where(x => x % 2 == remainder).ToArray();

            if (oddEven.Count() == 0)
            {
                return -1;
            }

            int index = Array.LastIndexOf(arr, oddEven.Max());

            return index;
        }

        static int IndexOfMinEvenOdd(int[] arr, bool odd = false)
        {
            int remainder = 0;

            if (odd)
            {
                remainder = 1;
            }

            var oddEven = arr.Where(x => x % 2 == remainder).ToArray();

            if (oddEven.Count() == 0)
            {
                return -1;
            }

            int index = Array.LastIndexOf(arr, oddEven.Min());

            return index;
        }

        static int[] FirstEvenOddCount(int[] arr, int count, bool odd = false)
        {
            int remainder = 0;

            if (odd)
            {
                remainder = 1;
            }

            int[] selected = arr
                .Where(x => x % 2 == remainder)
                .Take(count)
                .ToArray();

            return selected;
        }

        static int[] LastEvenOddCount(int[] arr, int count, bool odd = false)
        {
            int remainder = 0;

            if (odd)
            {
                remainder = 1;
            }

            int[] selected = arr
                .Reverse()
                .Where(x => x % 2 == remainder)
                .Take(count)
                .Reverse()
                .ToArray();

            return selected;
        }
    }   

}
