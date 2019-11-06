namespace AI_Tasks
{
    using System;
    using System.Collections.Generic;

    class Task01
    {
        static void Main()
        {
            //int n = int.Parse(Console.ReadLine());
            //int[,] matrixNxN = new int[n,n];
            // start destination matrix[0,0] endRowDest endColDest
            
            int[,] matrix =
            {
                {1, 1, 0, 1, 1, 1},
                {1, 2, 0, 0, 1, 1},
                {1, 1, 1, 1, 2, 1},
                {1, 1, 1, 1, 1, 1},
                {1, 0, 0, 1, 1, 1},
                {1, 1, 1, 1, 1, 1}
            };

            int rowLength = matrix.GetLength(0);
            int columnLength = matrix.GetLength(1);
            int starRow = 0, startCol = 0;
            Queue<int> rowQueue = new Queue<int>();
            Queue<int> colQueue = new Queue<int>();

            int moveCount = 0;
            int nodesLeftInLayer = 1;
            int nodesInNextLayer = 0;
            bool isReachedEnd = false;
            bool[,] visited = new bool[rowLength, columnLength];

            rowQueue.Enqueue(starRow);
            colQueue.Enqueue(startCol);
            visited[starRow, startCol] = true;
            int[] directionRow = { 0, 1, 0, -1 };
            int[] directionCol = { 1, 0, -1, 0 };

            while (rowQueue.Count > 0)
            {
                int row = rowQueue.Dequeue();
                int col = colQueue.Dequeue();

                if (row == 4 && col == 4)
                {
                    isReachedEnd = true;
                    break;
                }

                int newRowPosition, newColPosition;

                for (int i = 0; i < 4; i++)
                {
                    newRowPosition = row + directionRow[i];
                    newColPosition = col + directionCol[i];

                    if (newRowPosition < 0 || newColPosition < 0 || newRowPosition >= rowLength || newColPosition >= columnLength)
                    {
                        continue;
                    }

                    if (visited[newRowPosition, newColPosition] || matrix[newRowPosition, newColPosition] == 0)
                    {
                        continue;
                    }

                    visited[newRowPosition, newColPosition] = true;

                    if (matrix[newRowPosition, newColPosition] == 2)
                    {
                        newRowPosition = 2;
                        newColPosition = 4;
                        visited[newRowPosition, newColPosition] = true;

                    }

                    rowQueue.Enqueue(newRowPosition);
                    colQueue.Enqueue(newColPosition);
                    nodesInNextLayer++;
                }

                nodesLeftInLayer--;

                if (nodesLeftInLayer == 0)
                {
                    nodesLeftInLayer = nodesInNextLayer;
                    nodesInNextLayer = 0;
                    moveCount++;
                }
            }

            if(isReachedEnd)
            {
                Console.WriteLine(moveCount);
            } 
            else
            {
                Console.WriteLine(-1);
            }
        }
    }
}
