
using System.Linq;

namespace Task02
{
    using System;
    using System.Collections.Generic;
    using System.Collections;

    class AStar
    {
        private static int[,] grid =
        {
            {6, 5, 4},
            {2, 4, 8},
            {7, 0, 1}
        };
        private static int xLen = grid.GetLength(0);
        private static int yLen = grid.GetLength(1);

        public struct Cell
        {
            // Row and column of parent
            int parent_i, parent_j;

            public int Parent_i
            {
                get { return parent_i; }
                set { parent_i = value; }
            }

            public int Parent_j
            {
                get { return parent_j; }
                set { parent_j = value; }
            }

            // f(n) = g(n) + h(n) - heuristic
            double f, g, h;

            public double F
            {
                get { return f; }
                set { f = value; }
            }

            public double G
            {
                get { return g; }
                set { g = value; }
            }

            public double H
            {
                get { return h; }
                set { h = value; }
            }
        }

        public static bool IsValid(int row, int col)
        {
            return (row >= 0) && (row < xLen) && (col >= 0) && (col < yLen);
        }

        //public static bool IsUnblocked(int[,] grid, int row, int col)
        //{
        //    return grid[row, col] == 0;
        //}

        public static bool IsDestination(int row, int col, Tuple<int, int> dest)
        {
            return row == dest.Item1 && col == dest.Item2;
        }

        public static int CalculateHValue(int row, int col, Tuple<int, int> dest)
        {
            return Math.Abs(row - dest.Item1) + Math.Abs(col - dest.Item2);
        }

        public static void TracePath(Cell[,] cellDetails, Tuple<int,int> dest )
        {
            Console.WriteLine("The path is: ");
            int row = dest.Item1;
            int col = dest.Item2;

            Stack<Tuple<int, int>> path = new Stack<Tuple<int, int>>();

            while (!(cellDetails[row, col].Parent_i == row && cellDetails[row, col].Parent_j == col))
            {
                path.Push(new Tuple<int, int>(row, col));
                int tempRow = cellDetails[row, col].Parent_i;
                int tempCol = cellDetails[row, col].Parent_j;
                row = tempRow;
                col = tempCol;
            }

            path.Push(new Tuple<int, int>(row, col));

            while (path.Count != 0)
            {
                Tuple<int, int> node = path.Peek();
                path.Pop();
                Console.Write($" -> ({node.Item1}, {node.Item2})");
            }
        }

        public static void AStarSearch(int[,] grid, Tuple<int, int> src, Tuple<int, int> dest)
        {
            if (!IsValid(src.Item1, src.Item2))
            {
                Console.WriteLine("Source is invalid");
                return;
            }

            if (!IsValid(dest.Item1, dest.Item2))
            {
                Console.WriteLine("Destination is invalid");
                return;
            }

            //if (!IsUnblocked(grid, src.Item1, src.Item2) || !IsUnblocked(grid, dest.Item1, dest.Item2))
            //{
            //    Console.WriteLine("Source or the destination is blocked");
            //    return;
            //}

            if (IsDestination(src.Item1, src.Item2, dest))
            {
                Console.WriteLine("We are already at the destination");
                return;
            }

            bool[,] closedList = new bool[xLen, yLen];
            Cell[,] cellDetails = new Cell[xLen, yLen];

            int i, j;

            for (i = 0; i < xLen; i++)
            {
                for (j = 0; j < yLen; j++)
                {
                    cellDetails[i, j].F = double.MaxValue;
                    cellDetails[i, j].G = double.MaxValue;
                    cellDetails[i, j].H = double.MaxValue;
                    cellDetails[i, j].Parent_i = -1;
                    cellDetails[i, j].Parent_j = -1;
                }
            }

            i = src.Item1;
            j = src.Item2;
            cellDetails[i, j].F = 0.0;
            cellDetails[i, j].G = 0.0;
            cellDetails[i, j].H = 0.0;
            cellDetails[i, j].Parent_i = i;
            cellDetails[i, j].Parent_j = j;

            HashSet<Tuple<double, Tuple<int, int>>> openList = new HashSet<Tuple<double, Tuple<int, int>>>();
            openList.Add(new Tuple<double, Tuple<int, int>>(0.0, new Tuple<int, int>(i, j)));
            bool isFoundDest = false;

            while (openList.Count != 0)
            {
                Tuple<double, Tuple<int, int>> nodePair = openList.First();

                openList.Remove(openList.First());
                i = nodePair.Item2.Item1;
                j = nodePair.Item2.Item2;
                closedList[i, j] = true;

                double gNew, hNew, fNew;

                if (IsValid(i - 1, j))
                {
                    if (IsDestination(i - 1, j, dest))
                    {
                        cellDetails[i - 1, j].Parent_i = i;
                        cellDetails[i - 1, j].Parent_j = j;
                        Console.WriteLine("The destination cell is found");
                        TracePath(cellDetails, dest);
                        isFoundDest = true;
                        return;
                    }
                    else if (closedList[i - 1, j] == false /*&& IsUnblocked(grid, i - 1, j)*/)
                    {
                        gNew = cellDetails[i, j].G + 1.0;
                        hNew = CalculateHValue(i - 1, j, dest);
                        fNew = gNew + hNew;

                        if (cellDetails[i - 1, j].F.Equals(double.MaxValue) || cellDetails[i - 1, j].F > fNew)
                        {
                            openList.Add(new Tuple<double, Tuple<int, int>>(fNew, new Tuple<int, int>(i - 1, j)));

                            cellDetails[i - 1, j].F = fNew;
                            cellDetails[i - 1, j].G = gNew;
                            cellDetails[i - 1, j].H = hNew;
                            cellDetails[i - 1, j].Parent_i = i;
                            cellDetails[i - 1, j].Parent_j = j;
                        }
                    }
                }

                if (IsValid(i + 1, j))
                {
                    if (IsDestination(i + 1, j, dest))
                    {
                        cellDetails[i + 1, j].Parent_i = i;
                        cellDetails[i + 1, j].Parent_j = j;
                        Console.WriteLine("The destination cell is found");
                        TracePath(cellDetails, dest);
                        isFoundDest = true;
                        return;
                    }
                    else if (closedList[i + 1, j] == false /*&& IsUnblocked(grid, i + 1, j)*/)
                    {
                        gNew = cellDetails[i, j].G + 1.0;
                        hNew = CalculateHValue(i + 1, j, dest);
                        fNew = gNew + hNew;

                        if (cellDetails[i + 1, j].F.Equals(double.MaxValue) || cellDetails[i + 1, j].F > fNew)
                        {
                            openList.Add(new Tuple<double, Tuple<int, int>>(fNew, new Tuple<int, int>(i + 1, j)));

                            cellDetails[i + 1, j].F = fNew;
                            cellDetails[i + 1, j].G = gNew;
                            cellDetails[i + 1, j].H = hNew;
                            cellDetails[i + 1, j].Parent_i = i;
                            cellDetails[i + 1, j].Parent_j = j;
                        }
                        
                    }
                }

                if (IsValid(i, j + 1))
                {
                    if (IsDestination(i, j + 1, dest))
                    {
                        cellDetails[i, j + 1].Parent_i = i;
                        cellDetails[i, j + 1].Parent_j = j;
                        Console.WriteLine("The destination cell is found");
                        TracePath(cellDetails, dest);
                        isFoundDest = true;
                        return;
                    }
                    else if (closedList[i, j + 1] == false /*&& IsUnblocked(grid, i, j + 1)*/)
                    {
                        gNew = cellDetails[i, j].G + 1.0;
                        hNew = CalculateHValue(i, j + 1, dest);
                        fNew = gNew + hNew;

                        if (cellDetails[i, j + 1].F.Equals(double.MaxValue) || cellDetails[i, j + 1].F > fNew)
                        {
                            openList.Add(new Tuple<double, Tuple<int, int>>(fNew, new Tuple<int, int>(i, j + 1)));

                            cellDetails[i, j + 1].F = fNew;
                            cellDetails[i, j + 1].G = gNew;
                            cellDetails[i, j + 1].H = hNew;
                            cellDetails[i, j + 1].Parent_i = i;
                            cellDetails[i, j + 1].Parent_j = j;
                        }
                    }
                }

                if (IsValid(i, j - 1))
                {
                    if (IsDestination(i, j - 1, dest))
                    {
                        cellDetails[i, j - 1].Parent_i = i;
                        cellDetails[i, j - 1].Parent_j = j;
                        Console.WriteLine("The destination cell is found");
                        TracePath(cellDetails, dest);
                        isFoundDest = true;
                        return;
                    }
                    else if (closedList[i, j - 1] == false /*&& IsUnblocked(grid, i, j - 1)*/)
                    {
                        gNew = cellDetails[i, j].G + 1.0;
                        hNew = CalculateHValue(i, j - 1, dest);
                        fNew = gNew + hNew;

                        if (cellDetails[i, j - 1].F.Equals(double.MaxValue) || cellDetails[i, j - 1].F > fNew)
                        {
                            openList.Add(new Tuple<double, Tuple<int, int>>(fNew, new Tuple<int, int>(i, j - 1)));

                            cellDetails[i, j - 1].F = fNew;
                            cellDetails[i, j - 1].G = gNew;
                            cellDetails[i, j - 1].H = hNew;
                            cellDetails[i, j - 1].Parent_i = i;
                            cellDetails[i, j - 1].Parent_j = j;
                        }
                    }
                }
            }

            if (!isFoundDest)
            {
                Console.WriteLine("Failed to find the Destination Cell");
            }
        }
            
        static void Main()
        {
            Tuple<int, int> src = new Tuple<int, int>(2, 2);
            Tuple<int, int> dest = new Tuple<int, int>(0, 0);
            
            AStarSearch(grid, src, dest);
        }
    }
}
