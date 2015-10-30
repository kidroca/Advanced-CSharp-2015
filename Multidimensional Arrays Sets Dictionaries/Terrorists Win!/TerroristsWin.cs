namespace SoftUni.Homeworks.AdvancedCSharp.Multidimetional
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// This problem is from the Java Basics Exam (7 January 2015). You may check your solution 
    /// here https://judge.softuni.bg/Contests/Practice/Index/57
    /// 
    /// On de_dust2 terrorists have planted a bomb (or possibly several of them)! Write a program
    /// that sets those bombs off!
    /// 
    /// A bomb is a string in the format |...|. When set off, the bomb destroys all characters
    /// inside. The bomb should also destroy n characters to the left and right of the bomb. n is
    /// determined by the bomb power (the last digit of the ASCII sum of the characters inside
    /// the bomb). Destroyed characters should be replaced by '.' (dot). For example, we are
    /// given the following text:
    ///     "prepare|yo|dong"
    /// The bomb is |yo|. We get the bomb power by calculating the last digit of the
    /// sum: y (121) + o (111) = 232. The bomb explodes and destroys itself and 2 characters to
    /// the left and 2 characters to the right. The result is:
    ///     "prepa........ng"
    /// </summary>
    class TerroristsWin
    {
        static void Main()
        {
            var foundBombs = new List<Dictionary<string, int>>();

            string textWithBombs = Console.ReadLine(); // "de_dust2 |A| the best |BB|map!"; // "prepare|yo|dong"; 

            bool allBombsFound = false;
            int bombStart = 0,
                bombEnd = -1;

            while(!allBombsFound)
            {
                bombStart = textWithBombs.IndexOf('|', bombEnd + 1);
                bombEnd = textWithBombs.IndexOf('|', bombStart + 1);

                if (bombStart != -1 &&
                    bombEnd != -1)
                {
                    var bomb = new Dictionary<string, int>();
                    bomb["start"] = bombStart;
                    bomb["end"] = bombEnd;

                    foundBombs.Add(bomb);
                }
                else
                {
                    allBombsFound = true;
                }
            }

            var mask = new bool[textWithBombs.Length];

            foreach (var bomb in foundBombs)
            {
                SetOffBomb(bomb, textWithBombs, mask);
            }

            for (int i = 0; i < textWithBombs.Length; i++)
            {
                if (mask[i])
                {
                    Console.Write('.');
                }
                else
                {
                    Console.Write(textWithBombs[i]);
                }
            }
            Console.WriteLine();
        }

        private static void SetOffBomb(Dictionary<string, int> bomb, string textWithBomb
            , bool[] mask)
        {
            int start = bomb["start"],
                end = bomb["end"];

            string bombPattern = textWithBomb.Substring(start + 1, end - start - 1);

            int bombPower = CalculateBombPower(bombPattern);

            int startArea = start - bombPower > 0 ? start - bombPower : 0,
                endArea = end + bombPower < textWithBomb.Length ? end + bombPower : textWithBomb.Length - 1;

            for (int i = startArea; i <= endArea; i++)
            {
                mask[i] = true;
            }
        }

        private static int CalculateBombPower(string bombPattern)
        {
            int asciSum = 0;

            foreach (char symbol in bombPattern)
            {
                asciSum += symbol;
            }

            return asciSum % 10;
        }
    }
}
