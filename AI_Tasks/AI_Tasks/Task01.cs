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
            int[] dr = { 0, 1, 0, -1 };
            int[] dc = { 1, 0, -1, 0 };

            while (rowQueue.Count > 0)
            {
                int row = rowQueue.Dequeue();
                int col = colQueue.Dequeue();

                if (row == 4 && col == 4)
                {
                    isReachedEnd = true;
                    break;
                }

                //exploreNeighbours(row, col);
                int rr, cc;

                for (int i = 0; i < 4; i++)
                {
                    rr = row + dr[i];
                    cc = col + dc[i];

                    if (rr < 0 || cc < 0 || rr >= rowLength || cc >= columnLength)
                    {
                        continue;
                    }

                    if (visited[rr, cc] || matrix[rr, cc] == 0)
                    {
                        continue;
                    }

                    if (matrix[rr, cc] == 2)
                    {
                        rr = 2;
                        cc = 4;
                    }

                    rowQueue.Enqueue(rr);
                    colQueue.Enqueue(cc);
                    visited[rr, cc] = true;
                    //Console.Write(matrix[rr, cc] + " ");
                    nodesInNextLayer++;
                }

               // Console.WriteLine();

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
