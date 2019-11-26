namespace Task02Better
{
    using System;

    public class Board
    {
        /// <summary>
        /// Two dimensional matrix representig the game board
        /// </summary>
        public int[,] Matrix { get; set; }

        /// <summary>
        /// Initialize game board
        /// </summary>
        /// <param name="matrix">Matrix provided to be used with A* algorithm and be ordered in the same way as Goal state matrix</param>
        public Board(int[,] matrix)
        {
            this.Matrix = matrix;
        }

        /// <summary>
        /// Finds the location indexes of element with value 0 which represents the empty block
        /// </summary>
        /// <returns>Pair of two indexes x-by row and y-by column.</returns>
        public Tuple<int, int> IndexOfZero()
        {
            int width = this.Matrix.GetLength(0);
            int height = this.Matrix.GetLength(1);

            for (int x = 0; x < width; ++x)
            {
                for (int y = 0; y < height; ++y)
                {
                    if (this.Matrix[x, y].Equals(0))
                    {
                        return Tuple.Create(x, y);
                    }
                }
            }
            return Tuple.Create(-1, -1);
        }

        /// <summary>
        /// Checks if two matrices are the same by value not by reference 
        /// </summary>
        /// <param name="matrixToTest">This is the matrix which will be compared with the main one</param>
        /// <returns>True or false according to the result. If matrixes are equal return true and vice versa</returns>
        public bool IsEqual(int[,] matrixToTest)
        {
            for (int i = 0; i < this.Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < this.Matrix.GetLength(1); j++)
                {
                    if (this.Matrix[i, j] != matrixToTest[i, j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// In C# when overriding Equals method we are obligeded to override GetHashCode method too
        /// </summary>
        /// <returns>A hash code that is used to insert and identify an object in a hash-based collection</returns>
        public override int GetHashCode()
        {
            int result = 0;
            int shift = 0;
            for (int i = 0; i < this.Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < this.Matrix.GetLength(1); j++)
                {
                    shift = (shift + 11) % 21;
                    result ^= (this.Matrix[i, j] + 1024) << shift;
                }
            }
            return result;
        }

        /// <summary>
        /// Also check if matrices if two matrices are the same by value not by reference,
        ///  but in this case the matrix should be a property of class Board
        /// </summary>
        /// <param name="obj">Accept any object and cast it to type board afterwards</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            var otherBoard = (Board)obj;
            for (int i = 0; i < this.Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < this.Matrix.GetLength(1); j++)
                {
                    if (this.Matrix[i, j] != otherBoard.Matrix[i, j])
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
