namespace BunkerBuster
{
    using System;
    using System.Linq;

    /// <summary>
    /// Exam Link: https://judge.softuni.bg/Contests/Practice/Index/92
    /// </summary>
    class Program
    {
        static int totalRows;

        static int totalCols;

        static long[][] battlefield;

        static void Main()
        {
            ReadInput();
            DoCommands();

            int destroyedCells = battlefield
                .SelectMany(item => item)
                .Count(cell => cell <= 0);

            int totalCells = totalRows * totalCols;

            Console.WriteLine("Destroyed bunkers: {0}", destroyedCells);
            Console.WriteLine("Damage done: {0:F1} %", (destroyedCells / (double)totalCells) * 100);
        }

        private static void DoCommands()
        {
            string command;
            while ((command = Console.ReadLine()) != "cease fire!")
            {
                string[] CurrentCommand = command.Split(' ');

                DoCurrentCommand(
                    int.Parse(CurrentCommand[0])
                    , int.Parse(CurrentCommand[1])
                    , CurrentCommand[2][0]);
            }
        }

        private static void DoCurrentCommand(int row, int column, int power)
        {
            int halfDamage = (int)Math.Ceiling(power / 2f);

            DamageAdjacent(row, column, halfDamage);

            if (battlefield[row][column] > 0)
            {
                battlefield[row][column] -= power;
            }
        }

        private static void DamageAdjacent(int row, int column, int halfDamage)
        {
            for (int i = row - 1; i < row + 2; i++)
            {
                for (int j = column - 1; j < column + 2; j++)
                {
                    if (i == row && j == column)
                    {
                        continue;
                    }

                    if (IsInRange(i, j) && battlefield[i][j] > 0)
                    {
                        battlefield[i][j] -= halfDamage;
                    }
                }
            }
        }

        private static bool IsInRange(int row, int col)
        {
            bool inRange = (0 <= row && row < totalRows) &&
                           (0 <= col && col < totalCols);

            return inRange;
        }
       
        static void ReadInput()
        {
            int[] rowsCols = Console
                .ReadLine()
                .Split(' ')
                .Select(int.Parse)
                .ToArray();

            totalRows = rowsCols[0];
            totalCols = rowsCols[1];

            battlefield = new long[totalRows][];

            for (int i = 0; i < totalRows; i++)
            {
                long[] rowData = Console
                    .ReadLine()
                    .Split(' ')
                    .Select(long.Parse)
                    .ToArray();

                battlefield[i] = rowData;
            }       
        }
    }
}
