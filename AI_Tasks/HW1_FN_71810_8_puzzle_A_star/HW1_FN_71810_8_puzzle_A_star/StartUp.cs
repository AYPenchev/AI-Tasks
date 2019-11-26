namespace Task02Better
{
    class StartUp
    {
        public static void Main()
        {
            var startState = new int[3, 3] {
                //{ 8, 0, 6 },
                //{ 5, 4, 7 },
                //{ 2, 3, 1 }
                { 6, 5, 3 },
                { 2, 4, 8 },
                { 7, 0, 1 }
            };

            var board = new Board(startState);
            var startingState = new State(board, null, null, 0);

            var aStar = new AStarSolver();
            var measurer = new PerformanceMeasurer();
            measurer.StartMeasuring();
            aStar.Solve(startingState);
            measurer.StopMeasuring();
            measurer.PrintResults();
        }
    }
}
