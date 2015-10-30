namespace SoftUni.CSharp.ExamPreparation
{
    using System;
    using System.Collections.Generic;
    using System.Security;
    using System.Text;

    /// <summary>
    /// This assignment is from the Advanced C# Exam Preparation 2015 September.
    /// You can check out the description and submit your solution here
    /// https://judge.softuni.bg/Contests/Practice/Index/84
    /// </summary>
    class TextGravity
    {
        static void Main()
        {
            int lineLength = int.Parse(Console.ReadLine());

            string text = Console.ReadLine();

            List<char[]> characterTable = ConvertToTable(text, lineLength);

            ApplyGravity(characterTable, lineLength);

            string html = ConvertToHTMLTable(characterTable, lineLength);

            Console.WriteLine(html);
        }

        private static string ConvertToHTMLTable(List<char[]> characterTable, int lineLength)
        {
            var htmlBuilder = new StringBuilder();

            htmlBuilder.Append("<table>");

            foreach (var row in characterTable)
            {
                htmlBuilder.Append("<tr>");

                foreach (var symbol in row)
                {
                    htmlBuilder
                        .Append("<td>")
                        .Append(SecurityElement.Escape(symbol.ToString()))
                        .Append("</td>");
                }

                htmlBuilder.Append("</tr>");
            }

            htmlBuilder.Append("</table>");

            return htmlBuilder.ToString();
        }

        private static void ApplyGravity(List<char[]> characterTable, int tableWidth)
        {
            int tableHeight = characterTable.Count;

            for (int i = 0; i < tableWidth; i++)
            {
                var currentColumn = new Stack<char>();

                for (int j = 0; j < tableHeight; j++)
                {
                    if (characterTable[j][i] != ' ')
                    {
                        currentColumn.Push(characterTable[j][i]);
                    }
                }

                for (int j = tableHeight - 1; j >= 0; j--)
                {
                    if (currentColumn.Count > 0)
                    {
                        characterTable[j][i] = currentColumn.Pop();
                    }
                    else
                    {
                        characterTable[j][i] = ' ';
                    }
                }
            }
        }

        private static List<char[]> ConvertToTable(string text, int lineLength)
        {
            var table = new List<char[]>();

            int startIndex = 0;

            while (startIndex < text.Length)
            {
                int maxLength = lineLength;

                if (lineLength + startIndex >= text.Length)
                {
                    maxLength = text.Length - startIndex;
                }

                string currentLine = text.Substring(startIndex, maxLength);

                table.Add(FormatLine(currentLine, lineLength));

                startIndex += maxLength;
            }

            return table;
        }

        private static char[] FormatLine(string currentLine, int lineLength)
        {
            int emptySpaceToAdd = lineLength - currentLine.Length;

            if (emptySpaceToAdd != 0)
            {
                currentLine = string.Format(
                    "{0}{1}"
                    , currentLine
                    , new string(' ', emptySpaceToAdd));
            }

            return currentLine.ToCharArray();
        }
    }
}
