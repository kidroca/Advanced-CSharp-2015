namespace Problem_2
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class Program
    {
        static List<char[]> field = new List<char[]>();

        static int totalRows;

        static int totalCols;

        static int pRow;

        static int pCol;

        static bool gameEnded = false;

        static bool isWinning = true;

        static void Main()
        {
            ReadFieldData();

            SetPlayerPosition();

            DoCommands();

            PrintField(field);
            
            Console.WriteLine(
                "{0}: {1} {2}"
                , isWinning ? "won" : "dead"
                , pRow
                , pCol);
        }

        private static void PrintField(List<char[]> field)
        {
            foreach (var line in field)
            {
                Console.WriteLine(new string(line));
            }
        }

        private static void SetPlayerPosition()
        {
            for (int i = totalRows - 1; i >= 0; i--)
            {
                int index;
                if ((index = Array.IndexOf(field[i], 'P')) != -1)
                {
                    pRow = i;
                    pCol = index;
                    break;
                }
            }
        }

        private static void DoCommands()
        {
            string commands = Console.ReadLine();

            foreach (var c in commands)
            {
                switch (c)
                {
                    case 'R':
                        if (CheckPlayerTurn(pRow, pCol + 1))
                        {
                            DoPlayerTurn(pRow, pCol, pRow, pCol + 1);
                            pCol++;
                        }

                        break;
                    case 'D':
                        if (CheckPlayerTurn(pRow + 1, pCol))
                        {
                            DoPlayerTurn(pRow, pCol, pRow + 1, pCol);
                            pRow++;
                        }

                        break;
                    case 'L':
                        if (CheckPlayerTurn(pRow, pCol - 1))
                        {
                            DoPlayerTurn(pRow, pCol, pRow, pCol - 1);
                            pCol--;
                        }

                        break;
                    case 'U':
                        if (CheckPlayerTurn(pRow - 1, pCol))
                        {
                            DoPlayerTurn(pRow, pCol, pRow - 1, pCol);
                            pRow--;
                        }

                        break;
                    default:
                        break;
                }

                if (gameEnded && isWinning)
                {
                    field[pRow][pCol] = '.';
                }

                DoBunniesTurn();
             
                if (gameEnded)
                {
                    break;
                }
            }
        }

        private static void DoBunniesTurn()
        {
            List<char[]> newField = field.Select(x => x.ToArray()).ToList();

            for (int i = 0; i < totalRows; i++)
            {
                for (int j = 0; j < totalCols; j++)
                {
                    if (field[i][j] == 'B')
                    {
                        SpreadBunnies(i, j, newField);
                    }
                }
            }

            field = newField;
        }

        private static void SpreadBunnies(int i, int j, List<char[]> newField)
        {         
            if (IsWithinField(i, j + 1)) // Right
            {
                SpreadTo(i, j + 1, newField);
            }

            
            if (IsWithinField(i + 1, j)) // Down
            {
                SpreadTo(i + 1, j, newField);
            }

           
            if (IsWithinField(i, j - 1)) // Left
            {
                SpreadTo(i, j - 1, newField);
            }

          
            if (IsWithinField(i - 1, j)) // Up
            {
                SpreadTo(i - 1, j, newField);
            }
        }

        private static void SpreadTo(int i, int j, List<char[]> newField)
        {
            if (newField[i][j] == 'P')
            {
                isWinning = false;
                gameEnded = true;
            }

            newField[i][j] = 'B';
        }

        private static void DoPlayerTurn(int i, int j, int i2, int j2)
        {

            if (field[i2][j2] != 'B')
            {
                field[i2][j2] = 'P';
                field[i][j] = '.';
            }
        }

        private static bool CheckPlayerTurn(int row, int col)
        {
            if (IsWithinField(row, col))
            {
                if (field[row][col] == 'B')
                {
                    isWinning = false;
                    gameEnded = true;
                }

                return true;
            }
            else
            {
                gameEnded = true;
                return false;
            }
        }

        private static bool IsWithinField(int row, int col)
        {
            bool isWithin =
                0 <= row && row < totalRows &&
                0 <= col && col < totalCols;

            return isWithin;
        }

        private static void ReadFieldData()
        {
            int[] rowsCols = Console
                .ReadLine()
                .Split(' ')
                .Select(int.Parse)
                .ToArray();

            totalRows = rowsCols[0];
            totalCols = rowsCols[1];

            for (int i = 0; i < totalRows; i++)
            {
                field.Add(Console
                    .ReadLine()
                    .Trim()
                    .ToCharArray());
            }
        }
    }
}
