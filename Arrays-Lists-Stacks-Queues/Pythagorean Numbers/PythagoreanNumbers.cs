namespace SoftUni.Homeworks.AdvancedCSharp.Arrays
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// This problem is from the Java Basics Exam (26 May 2014). You may check your solution here.
    /// https://judge.softuni.bg/Contests/Practice/Index/12
    /// 
    /// George likes math. Recently he discovered an interesting property of the Pythagorean Theorem:
    /// there are infinite number of triplets of integers a, b and c (a ≤ b), such that a2 + b2 = c2.
    /// Write a program to help George find all such triplets (called Pythagorean numbers) among
    /// a set of integer numbers.
    /// </summary>
    class PythagoreanNumbers
    {
        static void Main()
        {
            int count = int.Parse(Console.ReadLine());

            int[] numbers = new int[count];

            for (int i = 0; i < count; i++)
            {
                numbers[i] = int.Parse(Console.ReadLine());
            }

            List<string> pythaNums = new List<string>();
            HashSet<int> setA = new HashSet<int>();
            HashSet<int> setB = new HashSet<int>();

            for (int i = 0; i < numbers.Length; i++)
            {
                for (int j = 0; j < numbers.Length; j++)
                {
                    for (int k = 0; k < numbers.Length; k++)
                    {
                        int a = numbers[i];
                        int b = numbers[j];
                        int c = numbers[k];

                        bool isPythagor = 
                            Math.Pow(a, 2) +
                            Math.Pow(b, 2) ==
                            Math.Pow(c, 2);

                        if (isPythagor)
                        {
                            if (setA.Contains(b) && setB.Contains(a))
                            {
                                continue;
                            }

                            if (a < b)
                            {
                                pythaNums.Add(string.Format("{0}*{0} + {1}*{1} = {2}*{2}", a, b, c));
                            }
                            else
                            {
                                pythaNums.Add(string.Format("{0}*{0} + {1}*{1} = {2}*{2}", b, a, c));
                            }

                            setA.Add(a);
                            setB.Add(b);
                        }
                    }
                }
            }

            if (pythaNums.Count > 0)
            {
                foreach (var item in pythaNums)
                {
                    Console.WriteLine(item);
                }
            }
            else
            {
                Console.WriteLine("No");
            }
            
        }
    }
}
