namespace Task02Better
{
    using System;

    class StartUp
    {
        public static void Main()
        {
            // 1,2,5,3,4,0,6,7,8
            var arr = new int[3, 3] {
                { 6, 5, 3 },
                { 2, 4, 8 },
                { 7, 0, 1 }
            };

            var board = new Board(arr);
            var startingState = new State(board, null, null, 0);

            var idaStar = new AStarSolver();
            idaStar.Solve(startingState);
        }
    }
}
