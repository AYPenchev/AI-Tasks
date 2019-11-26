namespace Task02Better
{
    using System;

    public class State : IComparable<State>
    {
        private int manhatanDistance;

        /// <summary>
        /// Initialize current state of the game
        /// </summary>
        /// <param name="currentBoard">Current state of matrix(Board)</param>
        /// <param name="parent">Previous state of the game</param>
        /// <param name="lastMove">Shows in what direction was the last move of the element with value 0</param>
        /// <param name="searchDepth">Shows how deep we have searched so far to find the goal state</param>
        public State(Board currentBoard, State parent, string lastMove, int searchDepth)
        {
            this.CurrentBoard = currentBoard;
            this.Parent = parent;
            this.LastMove = lastMove;
            this.SearchDepth = searchDepth;
        }
        /// <summary>
        /// Represents the current state of matrix(Board)
        /// </summary>
        public Board CurrentBoard { get; set; }

        /// <summary>
        /// Represents the previous state of the game
        /// </summary>
        public State Parent { get; set; }

        /// <summary>
        /// Shows in what direction was the last move of the element with value 0
        /// </summary>
        public string LastMove { get; set; }

        /// <summary>
        /// Shows how deep we have searched so far to find the goal state
        /// </summary>
        public int SearchDepth { get; set; }

        /// <summary>
        /// Caculates approximation Heuristics
        /// Manhattan Distance -
        /// Sum of absolute values of differences in the goal’s x and y coordinates and the current cell’s x and y coordinates respectively
        /// We use Manhattan Distance when we are allowed to move only in four directions only (right, left, top, bottom)
        /// </summary>
        /// <returns></returns>
        public int ManhatanDistance()
        {
            var matrix = this.CurrentBoard.Matrix;
            this.manhatanDistance = 0;

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    var num = matrix[i, j];

                    switch (num)
                    {
                        case 1: this.manhatanDistance += Math.Abs(0 - i) + Math.Abs(1 - j); break;
                        case 2: this.manhatanDistance += Math.Abs(0 - i) + Math.Abs(2 - j); break;
                        case 3: this.manhatanDistance += Math.Abs(1 - i) + Math.Abs(0 - j); break;
                        case 4: this.manhatanDistance += Math.Abs(1 - i) + Math.Abs(1 - j); break;
                        case 5: this.manhatanDistance += Math.Abs(1 - i) + Math.Abs(2 - j); break;
                        case 6: this.manhatanDistance += Math.Abs(2 - i) + Math.Abs(0 - j); break;
                        case 7: this.manhatanDistance += Math.Abs(2 - i) + Math.Abs(1 - j); break;
                        case 8: this.manhatanDistance += Math.Abs(2 - i) + Math.Abs(2 - j); break;
                                                                   
                        default:
                            break;
                    }
                }
            }
            return this.manhatanDistance;
        }

        /// <summary>
        /// Creates new state where the element with value 0 is moved left
        /// </summary>
        /// <param name="x"> x-row coordinate of element with value 0 before being moved left</param>
        /// <param name="y"> y-column coordinate of element with value 0 before being moved left</param>
        /// <returns>New state where element with value 0 is moved left</returns>
        public State MoveZeroToTheLeft(int x, int y)
        {
            if (y == 0)
            {
                return null;
            }

            var clonedState = this.Clone();

            var temp = clonedState.CurrentBoard.Matrix[x, y - 1];
            clonedState.CurrentBoard.Matrix[x, y - 1] = 0;
            clonedState.CurrentBoard.Matrix[x, y] = temp;
            return clonedState;
        }

        /// <summary>
        /// Creates new state where the element with value 0 is moved right
        /// </summary>
        /// <param name="x"> x-row coordinate of element with value 0 before being moved right</param>
        /// <param name="y"> y-column coordinate of element with value 0 before being moved right</param>
        /// <returns>New state where element with value 0 is moved right</returns>
        public State MoveZeroToTheRight(int x, int y)
        {
            if (y == (this.CurrentBoard.Matrix.Length / 3) - 1)
            {
                return null;
            }

            var clonedState = this.Clone();

            var temp = clonedState.CurrentBoard.Matrix[x, y + 1];
            clonedState.CurrentBoard.Matrix[x, y + 1] = 0;
            clonedState.CurrentBoard.Matrix[x, y] = temp;
            return clonedState;
        }

        /// <summary>
        /// Creates new state where the element with value 0 is moved up
        /// </summary>
        /// <param name="x"> x-row coordinate of element with value 0 before being moved up</param>
        /// <param name="y"> y-column coordinate of element with value 0 before being moved up</param>
        /// <returns>New state where element with value 0 is moved up</returns>
        public State MoveZeroUp(int x, int y)
        {
            if (x == 0)
            {
                return null;
            }

            var clonedState = this.Clone();

            var temp = clonedState.CurrentBoard.Matrix[x - 1, y];
            clonedState.CurrentBoard.Matrix[x - 1, y] = 0;
            clonedState.CurrentBoard.Matrix[x, y] = temp;
            return clonedState;
        }

        /// <summary>
        /// Creates new state where the element with value 0 is moved down
        /// </summary>
        /// <param name="x"> x-row coordinate of element with value 0 before being moved down</param>
        /// <param name="y"> y-column coordinate of element with value 0 before being moved down</param>
        /// <returns>New state where element with value 0 is moved down</returns>
        public State MoveZeroDown(int x, int y)
        {
            if (x == (this.CurrentBoard.Matrix.Length / 3) - 1)
            {
                return null;
            }

            var clonedState = this.Clone();

            var temp = clonedState.CurrentBoard.Matrix[x + 1, y];
            clonedState.CurrentBoard.Matrix[x + 1, y] = 0;
            clonedState.CurrentBoard.Matrix[x, y] = temp;
            return clonedState;
        }

        /// <summary>
        /// Clones the current state
        /// </summary>
        /// <returns>Return the new cloned state</returns>
        public State Clone()
        {
            var newMatrix = new int[3, 3];

            for (int i = 0; i < this.CurrentBoard.Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < this.CurrentBoard.Matrix.GetLength(1); j++)
                {
                    newMatrix[i, j] = this.CurrentBoard.Matrix[i, j];
                }
            }

            var clonedBoard = new Board(newMatrix);

            return new State(clonedBoard, this.Parent, this.LastMove, this.SearchDepth);
        }

        /// <summary>
        /// In C# when overriding Equals method we are obligeded to override GetHashCode method too
        /// </summary>
        /// <returns>A hash code that is used to insert and identify an object in a hash-based collection</returns>
        public override int GetHashCode()
        {
            return this.CurrentBoard.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            var otherState = (State)obj;

            return this.CurrentBoard.Equals(otherState.CurrentBoard);
        }

        /// <summary>
        /// Compares new state Heuristic with current state Heuristic to check which way to go next
        /// </summary>
        /// <param name="other">Any possible next state</param>
        /// <returns>
        /// Returns 1 if current state is closer to goal state than the checked next state,
        /// returns -1 if checked state is closer to goal state than the current next state,
        /// returns 0 if two states are at even distance to goal state
        /// </returns>
        public int CompareTo(State other)
        {
            var thisValue = this.ManhatanDistance();
            var otherValue = other.ManhatanDistance();

            if (thisValue < otherValue)
            {
                return 1;
            }
            else if (thisValue > otherValue)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }
}
