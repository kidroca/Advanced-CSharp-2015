namespace SoftUni.Homeworks.AdvancedCSharp.Multidimetional
{
    using System;

    public class MatrixPattern<T> where T : IConvertible
    {
        private int length;

        public MatrixPattern(T pattern, int startRow, int startCol)
        {
            this.Footprint = pattern;
            this.StartRow = startRow;
            this.StartCol = startCol;
            this.Length = 0;
        }

        public T Footprint { get; private set; }      

        public int Length
        {
            get
            {
                return this.length;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Sequence length cannot be less than 0");
                }

                this.length = value;
            }
        }

        public int StartRow { get; private set; }

        public int StartCol { get; private set; }

        public bool[,] PatternMask { get; set; }
    }
}
