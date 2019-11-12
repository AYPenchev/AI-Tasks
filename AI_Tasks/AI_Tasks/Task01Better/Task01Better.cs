namespace Task01Better
{
    using System;
    using System.Collections.Generic;

    class Task01Better
    {
        private static int[,] matrix =
        {
            {1, 1, 0, 1, 1, 1},
            {1, 2, 0, 0, 1, 1},
            {1, 1, 1, 1, 2, 1},
            {1, 1, 1, 1, 1, 1},
            {1, 0, 0, 1, 1, 1},
            {1, 1, 1, 1, 1, 1}
        };

        public static int GetRowLength => matrix.GetLength(0);

        public static int GetColumnLength => matrix.GetLength(1);

        public static List<Tuple<int, int>> Bfs(Tuple<int, int> startNode, Tuple<int, int> endNode)
        {
            Tuple<int, int>[,] prev = Solve(startNode);

            return ReconstructPath(startNode, endNode, prev);
        }

        public static Tuple<int, int>[,] Solve(Tuple<int, int> startNode)
        {
            Queue<Tuple<int, int>> nodesQueue = new Queue<Tuple<int, int>>();
            nodesQueue.Enqueue(startNode);

            bool[,] visited = new bool[GetRowLength, GetColumnLength];
            visited[startNode.Item1, startNode.Item2] = true;

            Tuple<int, int>[,] prev = new Tuple<int, int>[GetRowLength, GetColumnLength];

            while (nodesQueue.Count > 0)
            {
                Tuple<int, int> node = nodesQueue.Dequeue();
                List<Tuple<int, int>> neighbours = new List<Tuple<int, int>>();
                int row = node.Item1;
                int col = node.Item2;

                int[] directionRow = { 0, 1, 0, -1 };
                int[] directionCol = { 1, 0, -1, 0 };

                int newRowPosition, newColPosition;

                for (int i = 0; i < 4; i++)
                {
                    newRowPosition = row + directionRow[i];
                    newColPosition = col + directionCol[i];

                    if (newRowPosition < 0 || newColPosition < 0 || newRowPosition >= GetRowLength || newColPosition >= GetColumnLength)
                    {
                        continue;
                    }

                    if (visited[newRowPosition, newColPosition] || matrix[newRowPosition, newColPosition] == 0)
                    {
                        continue;
                    }

                    if (matrix[newRowPosition, newColPosition] == 2)
                    {
                        newRowPosition = 2;
                        newColPosition = 4;
                    }

                    neighbours.Add(new Tuple<int, int>(newRowPosition, newColPosition));
                }

                foreach (var neighbour in neighbours)
                {
                    if (!visited[neighbour.Item1, neighbour.Item2])
                    {
                        nodesQueue.Enqueue(neighbour);
                        visited[neighbour.Item1, neighbour.Item2] = true;
                        prev[neighbour.Item1, neighbour.Item2] = node;
                    }
                }
            }
            return prev;
        }

        public static List<Tuple<int, int>> ReconstructPath(Tuple<int, int> startNode, Tuple<int, int> endNode, Tuple<int, int>[,] prev)
        {
            List<Tuple<int, int>> path = new List<Tuple<int, int>>();

            Tuple<int, int> at = endNode;
            while (prev[at.Item1, at.Item2] != null)
            {
                at = prev[at.Item1, at.Item2];
                path.Add(at);
            }

            path.Reverse();

            if (path.Count != 0 && path[0].Equals(startNode))
            {
                return path;
            }

            return new List<Tuple<int, int>>();
        }
        public static void Main()
        {
            Tuple<int, int> startNode = new Tuple<int, int>(0, 0);
            Tuple<int, int> endNode = new Tuple<int, int>(4, 4);

            List<Tuple<int, int>> path = Bfs(startNode, endNode);
            if (path.Count != 0)
            {
                Console.WriteLine($"{path.Count} steps to end node!");

                for (int i = 0; i < path.Count; i++)
                {
                    Console.WriteLine($"Node {i} coordinates- x: {path[i].Item1}, y: {path[i].Item2}");
                }
                Console.WriteLine($"End node coordinates- x: {endNode.Item1}, y: {endNode.Item2}");
            }
            else
            {
                Console.WriteLine("There is no path to the end node");
            }
        }
    }
}
