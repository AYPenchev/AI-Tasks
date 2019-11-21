using System.Collections.Generic;

namespace Task02Better
{
    using System;

    class StartUp
    {
        public static void Main()
        {
            var startState = new int[3, 3] {
                { 8, 0, 6 },
                { 5, 4, 7 },
                { 2, 3, 1 }
            };

            var board = new Board(startState);
            var startingState = new State(board, null, null, 0);

            var idaStar = new AStarSolver();
            idaStar.Solve(startingState);
            //List<Tuple<int, string>> costGoingDirections = new List<Tuple<int, string>>();

            //costGoingDirections.Add(new Tuple<int, string>(1, "right"));

            //costGoingDirections.Add(new Tuple<int, string>(0, "left"));

            //costGoingDirections.Add(new Tuple<int, string>(1, "down"));

            //costGoingDirections.Add(new Tuple<int, string>(-1, "up"));

            //costGoingDirections.Sort();

            //foreach (var direction in costGoingDirections)
            //{
            //    Console.WriteLine($"{direction.Item1} {direction.Item2}");
            //}
        }
    }
}
