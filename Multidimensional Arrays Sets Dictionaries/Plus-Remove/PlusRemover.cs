namespace SoftUni.Homeworks.AdvancedCSharp.Multidimetional
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// This problem is originally from the JavaScript Basics Exam (24 November 2014). 
    /// You may check your solution here. https://judge.softuni.bg/Contests/Practice/Index/84
    /// 
    /// You are given a sequence of text lines, holding symbols, small and capital Latin
    /// letters.Your task is to remove all "plus shapes" in the text.They may consist of
    /// small and capital letters at the same time, or of any symbol. Example: 
    ///                          a       T
    ///                         aaa     TtT
    ///                          a       T
    /// Every "plus shape" is 3 by 3 symbols crossing each other on 3 lines.Single
    /// "plus shape" can be part of multiple "plus shapes". If new "plus shapes" are
    /// formed after the first removal you don't have to remove them.
    /// </summary>
    class PlusRemover
    {
        static void Main()
        {
            string textStream = string.Empty;

            var renderedStuff = new List<char[]>();

            var jaggedMask = new List<bool[]>();

            while (textStream != "END")
            {
                textStream = Console.ReadLine();

                renderedStuff.Add(textStream.ToCharArray());
                jaggedMask.Add(new bool[textStream.Length]);
            }

            renderedStuff.RemoveAt(renderedStuff.Count - 1);
            jaggedMask.RemoveAt(jaggedMask.Count - 1);

            MarkPluses(renderedStuff, jaggedMask);

            var resultBuilder = new StringBuilder();

            for (int i = 0; i < renderedStuff.Count; i++)
            {
                for (int j = 0; j < renderedStuff[i].Length; j++)
                {
                    if (jaggedMask[i][j])
                    {
                        resultBuilder.Append("");
                    }
                    else
                    {
                        resultBuilder.Append(renderedStuff[i][j]);
                    }
                }

                resultBuilder.Append("\n");
            }

            Console.WriteLine(resultBuilder.ToString());
        }

        private static void MarkPluses(List<char[]> renderedStuff, List<bool[]> jaggedMask)
        {
            for (int i = 1; i < renderedStuff.Count - 1; i++)
            {
                for (int j = 1; j < renderedStuff[i].Length - 1; j++)
                {
                    if (CheckPosition(renderedStuff, i, j))
                    {
                        SetMask(jaggedMask, i, j);
                    }
                }
            }
        }

        private static void SetMask(List<bool[]> jaggedMask, int i, int j)
        {
            jaggedMask[i][j] = true;

            jaggedMask[i + 1][j] = true;
            jaggedMask[i - 1][j] = true;

            jaggedMask[i][j + 1] = true;
            jaggedMask[i][j - 1] = true;
        }

        private static bool CheckPosition(List<char[]> renderedStuff, int i, int j)
        {
            char current = Char.ToLower(renderedStuff[i][j]);

            if (renderedStuff[i + 1].Length <= j || 
                renderedStuff[i - 1].Length <= j)
            {
                return false;
            }

            if (Char.ToLower(renderedStuff[i + 1][j]) == current &&
                Char.ToLower(renderedStuff[i - 1][j]) == current &&
                Char.ToLower(renderedStuff[i][j + 1]) == current &&
                Char.ToLower(renderedStuff[i][j - 1]) == current)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
