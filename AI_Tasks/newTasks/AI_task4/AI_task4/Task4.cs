using System;
using System.Collections.Generic;

namespace AI_task4
{
    public class Point
    {
        public int x, y;

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public override string ToString() 
        {
            return "[" + x + ", " + y + "]";
        }
    }

    public class PointsAndScores
    {
        public int score;
        public Point point;

        public PointsAndScores(int score, Point point)
        {
            this.score = score;
            this.point = point;
        }
    }

    class Board
    {
        public List<Point> availablePoints = new List<Point>();
        public int[,] board = new int[3,3];

        public List<PointsAndScores> rootsChildrenScore = new List<PointsAndScores>();

        public int evaluateBoard()
        {
            int score = 0;
            int empty;
            int X;
            int O;
            for (int row = 0; row < 3; ++row)
            {
                empty = 0;
                X = 0;
                O = 0;
                for (int col = 0; col < 3; ++col)
                {
                    if (board[row, col] == 0)
                    {
                        empty++;
                    }
                    else if (board[row, col] == 1)
                    {
                        X++;
                    }
                    else
                    {
                        O++;
                    }
                }
                score += changeInScore(X, O);
            }

            for (int col = 0; col < 3; ++col)
            {
                empty = 0;
                X = 0;
                O = 0;
                for (int row = 0; row < 3; ++row)
                {
                    if (board[row, col] == 0)
                    {
                        empty++;
                    }
                    else if (board[row, col] == 1)
                    {
                        X++;
                    }
                    else
                    {
                        O++;
                    }
                }
                score += changeInScore(X, O);
            }

            empty = 0;
            X = 0;
            O = 0;

            for (int row = 0, col = 0; row < 3; ++row, ++col)
            {
                if (board[row, col] == 1)
                {
                    X++;
                }
                else if (board[row, col] == 2)
                {
                    O++;
                }
                else
                {
                    empty++;
                }
            }

            score += changeInScore(X, O);

            empty = 0;
            X = 0;
            O = 0;

            for (int row = 2, col = 0; row > -1; --row, ++col)
            {
                if (board[row, col] == 1)
                {
                    X++;
                }
                else if (board[row, col] == 2)
                {
                    O++;
                }
                else
                {
                    empty++;
                }
            }

            score += changeInScore(X, O);

            return score;
        }

        private int changeInScore(int X, int O)
        {
            int change;
            if (X == 3)
            {
                change = 100;
            }
            else if (X == 2 && O == 0)
            {
                change = 10;
            }
            else if (X == 1 && O == 0)
            {
                change = 1;
            }
            else if (O == 3)
            {
                change = -100;
            }
            else if (O == 2 && X == 0)
            {
                change = -10;
            }
            else if (O == 1 && X == 0)
            {
                change = -1;
            }
            else
            {
                change = 0;
            }
            return change;
        }

        int uptoDepth = -1;

        public int alphaBetaMinMax(int alpha, int beta, int depth, int turn)
        {
            if (beta <= alpha)
            {
                if (turn == 1)
                {
                    return int.MaxValue;
                }
                else
                {
                    return int.MinValue;
                }
            }

            if (depth == uptoDepth || isGameOver())
            {
                return evaluateBoard();
            }

            List<Point> pointsAvailable = getAvailableStates();

            if (pointsAvailable.Count == 0)
            {
                return 0;
            }

            if (depth == 0)
            {
                rootsChildrenScore.Clear();
            }

            int maxValue = int.MinValue;
            int minValue = int.MaxValue;

            for (int i = 0; i < pointsAvailable.Count; ++i)
            {
                Point point = pointsAvailable[i];

                int currentScore = 0;
                if (turn == 1)
                {
                    placeAMove(point, 1);
                    currentScore = alphaBetaMinMax(alpha, beta, depth + 1, 2);
                    maxValue = Math.Max(maxValue, currentScore);

                    alpha = Math.Max(currentScore, alpha);
                    if (depth == 0)
                    {
                        rootsChildrenScore.Add(new PointsAndScores(currentScore, point));
                    }
                }
                else if (turn == 2)
                {
                    placeAMove(point, 2);
                    currentScore = alphaBetaMinMax(alpha, beta, depth + 1, 1);
                    minValue = Math.Min(minValue, currentScore);

                    beta = Math.Min(currentScore, beta);
                }

                board[point.x, point.y] = 0;

                if (currentScore == int.MaxValue || currentScore == int.MinValue)
                {
                    break;
                }
            }
            return turn == 1 ? maxValue : minValue;
        }

        public bool isGameOver()
        {
            return (hasXWon() || hasOWon() || (getAvailableStates().Count == 0));
        }

        public bool hasXWon()
        {
            if ((board[0, 0] == board[1, 1] && board[0, 0] == board[2, 2] && board[0, 0] == 1) || (board[0, 2] == board[1, 1] && board[0, 2] == board[2, 0] && board[0, 2] == 1))
            {
                return true;
            }

            for (int row = 0; row < 3; ++row)
            {
                if (((board[row, 0] == board[row, 1] && board[row, 0] == board[row, 2] && board[row, 0] == 1)
                        || (board[0, row] == board[1, row] && board[0, row] == board[2, row] && board[0, row] == 1)))
                {
                    return true;
                }
            }
            return false;
        }

        public bool hasOWon()
        {
            if ((board[0, 0] == board[1, 1] && board[0, 0] == board[2, 2] && board[0, 0] == 2) || (board[0, 2] == board[1, 1] && board[0, 2] == board[2, 0] && board[0, 2] == 2))
            {
                return true;
            }
            for (int row = 0; row < 3; ++row)
            {
                if ((board[row, 0] == board[row, 1] && board[row, 0] == board[row, 2] && board[row, 0] == 2)
                        || (board[0, row] == board[1, row] && board[0, row] == board[2, row] && board[0, row] == 2))
                    return true;
            }

            return false;
        }

        public List<Point> getAvailableStates()
        {
            List<Point> availablePoints = new List<Point>();
            for (int row = 0; row < 3; ++row)
            {
                for (int col = 0; col < 3; ++col)
                {
                    if (board[row, col] == 0)
                    {
                        availablePoints.Add(new Point(row, col));
                    }
                }
            }
            return availablePoints;
        }

        public void placeAMove(Point point, int player)
        {
            board[point.x, point.y] = player;
        }

        public Point returnBestMove()
        {
            int MAX = -100000;
            int best = -1;

            for (int i = 0; i < rootsChildrenScore.Count; ++i)
            {
                if (MAX < rootsChildrenScore[i].score)
                {
                    MAX = rootsChildrenScore[i].score;
                    best = i;
                }
            }
            return rootsChildrenScore[best].point;
        }


        public void displayBoard()
        {
            Console.WriteLine();
            for (int row = 0; row < 3; ++row)
            {
                for (int col = 0; col < 3; ++col)
                {
                    Console.Write(board[row, col] + " ");
                }
                Console.WriteLine();
            }
        }
    }

    class Task4
    {
        public static void Main()
        {
            Board board = new Board();
            Random random = new Random();

            board.displayBoard();

            Console.WriteLine("Press 2 if you want to move first, otherwise press 1.");
            int choice = int.Parse(Console.ReadLine());
            if (choice == 1)
            {
                Point p = new Point(random.Next(3), random.Next(3));
                board.placeAMove(p, 1);
                board.displayBoard();
            }

            while (!board.isGameOver())
            {
                Console.WriteLine("Your move: ");
                Point userMove = new Point(int.Parse(Console.ReadLine()), int.Parse(Console.ReadLine()));

                board.placeAMove(userMove, 2);
                board.displayBoard();
                if (board.isGameOver())
                {
                    break;
                }

                board.alphaBetaMinMax(int.MinValue, int.MaxValue, 0, 1);
                foreach (PointsAndScores pas in board.rootsChildrenScore)
                {
                    board.placeAMove(board.returnBestMove(), 1);
                }
                board.displayBoard();
            }
            if (board.hasXWon())
            {
                Console.WriteLine("Try again!");
            }
            else if (board.hasOWon())
            {
                Console.WriteLine("You win!");
            }
            else
            {
                Console.WriteLine("Par!");
            }
        }
    }
}
