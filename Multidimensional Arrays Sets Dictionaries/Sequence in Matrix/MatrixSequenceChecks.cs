namespace SoftUni.Homeworks.AdvancedCSharp.Multidimetional
{
    using System;

    public class MatrixSequenceChecks
    {
        public int CheckVerticalSequence<T>(int row, int col, T pattern, T[,] matrix
            , bool[,] mask) where T : IConvertible
        {
            int count = 0;

            for (; row < matrix.GetLength(0); row++)
            {
                if (matrix[row, col].Equals(pattern))
                {
                    mask[row, col] = true;
                    count++;
                }
                else
                {
                    break;
                }
            }

            return count;
        }

        public int CheckHorizontalSequence<T>(int row, int col, T pattern, T[,] matrix
            , bool[,] mask) where T : IConvertible
        {
            int count = 0;

            for (; col < matrix.GetLength(1); col++)
            {
                if (matrix[row, col].Equals(pattern))
                {
                    mask[row, col] = true;
                    count++;
                }
                else
                {
                    break;
                }
            }

            return count;
        }

        public int CheckRightDiagonalSequence<T>(int row, int col, T pattern, T[,] matrix
            , bool[,] mask) where T : IConvertible
        {
            int count = CheckDiagonalSequence(row, col, pattern, matrix, mask, x => x + 1);

            return count;
        }

        public int CheckLeftDiagonalSequence<T>(int row, int col, T pattern, T[,] matrix
            , bool[,] mask) where T : IConvertible
        {
            int count = CheckDiagonalSequence(row, col, pattern, matrix, mask, x => x - 1);

            return count;
        }

        private int CheckDiagonalSequence<T>(int row, int col, T pattern, T[,] matrix
    , bool[,] mask, Func<int, int> caseOperator) where T : IConvertible
        {
            int count = 0;

            while (row < matrix.GetLength(0) &&
                0 <= col && col < matrix.GetLength(1))
            {
                if (matrix[row, col].Equals(pattern))
                {
                    mask[row, col] = true;
                    count++;
                }
                else
                {
                    break;
                }

                // Checked diagonals are always to the bottom
                row++;
                // caseOperator should increment left in case of left diagonal pattern or decrement it
                // in the other case
                col = caseOperator(col);
            }

            return count;
        }
    }
}
