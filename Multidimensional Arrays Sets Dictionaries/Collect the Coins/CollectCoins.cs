namespace SoftUni.Homeworks.AdvancedCSharp.Multidimetional
{
    using System;
    using System.Collections.Generic;
    using HomeworkHelpers;

    /// <summary>
    /// Working with multidimensional arrays can be (and should be) fun. Let's make a game out of it.
    /// 
    /// You receive the layout of a board from the console. Assume it will always have 4 rows which
    /// you'll get as strings, each on a separate line. Each character in the strings will represent
    /// a cell on the board. Note that the strings may be of different length.
    /// 
    /// You are the player and start at the top-left corner (that would be position [0, 0] on the
    /// board). On the fifth line of input you'll receive a string with movement commands which tell
    /// you where to go next, it will contain only these four characters – '>' (move right),
    /// '&lt;' (moveleft),'^' (moveup) and 'v' (move down). 
    /// 
    /// You need to keep track of two types of events – collecting coins (represented by the symbol
    /// '$', of course) and hitting the walls of the board (when the player tries to move off the
    /// board to invalid coordinates). When all moves are over, print the amount of money collected
    /// and the number of walls hit.
    /// </summary>
    class CollectCoins
    {
        static TextHelper Helper = new TextHelper();

        static void Main()
        {
            Helper.SetupConsole();

            Helper.PrintColorText("Collect the Coins\n\n", "cyan");

            int rows = 4;

            char[][] board = new char[rows][];

            for (int i = 0; i < rows; i++)
            {
                Helper.PrintColorText(string.Format("Enter data for row {0}: ", i), "white");

                board[i] = Console
                    .ReadLine()
                    .ToCharArray();
            }

            Helper.PrintColorText("Input directions line: ", "green");

            // Starting with a predefined turn in case the [0,0] contains a coin
            string commands = "V" + Console.ReadLine();//  "V>>^^>>>VVV<<";
            Dictionary<string, int> gameResults = ExecuteCommands(board, commands, top: -1, left: 0);

            Console.WriteLine();
            Console.WriteLine("Result: ");
            foreach (var pair in gameResults)
            {
                Helper.PrintColorText(string.Format("{0}: {1}\n", pair.Key, pair.Value), "green");
            }

            Helper.Restart(Main);
        }

        static Dictionary<string, int> ExecuteCommands(char[][] board, string commands
            , int top = 0, int left = 0)
        {
            Dictionary<string, int> gameStats = new Dictionary<string, int>()
            {
                { "Collected Coins" , 0 },
                { "Wall Hits", 0 }
            };

            bool validMove = false;

            foreach (var direction in commands)
            {
                switch (direction)
                {
                    case '>':
                        validMove = ValidateTurn(board, top, left + 1);

                        if (MakeTurn(board, top, left + 1, validMove, gameStats))
                        {
                            left++;
                        }

                        break;
                    case 'V':
                        validMove = ValidateTurn(board, top + 1, left);

                        if (MakeTurn(board, top + 1, left, validMove, gameStats))
                        {
                            top++;
                        }

                        break;
                    case '<':
                        validMove = ValidateTurn(board, top, left - 1);

                        if (MakeTurn(board, top, left - 1, validMove, gameStats))
                        {
                            left--;
                        }

                        break;
                    case '^':
                        validMove = ValidateTurn(board, top - 1, left);

                        if (MakeTurn(board, top - 1, left, validMove, gameStats))
                        {
                            top--;
                        }

                        break;
                    default:
                        throw new ArgumentException("Ivalid Command: " + direction);
                }

               // Uncomment to see the turns indexes
               // Console.WriteLine("[{0}][{1}]", top, left);
            }

            
            return gameStats;
        }

        static bool MakeTurn(char[][] board, int top, int left, bool validMove
            , Dictionary<string, int> game)
        {
            if (validMove)
            {
                if (board[top][left] == '$')
                {
                    game["Collected Coins"]++;

                    // Remove the coin from the board after collection
                    // if board was string[] it is not possible to change board[top][left] = '0'
                    // because string chars are read-only
                    board[top][left] = '0';  
                } 
            }
            else
            {
                game["Wall Hits"]++;
            }

            return validMove;
        }

        private static bool ValidateTurn(char[][] board, int top, int left)
        {
            int rows = board.Length;

            if (0 <= top && top < rows)
            {
                int cols = board[top].Length;

                if (0 <= left && left < cols)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
