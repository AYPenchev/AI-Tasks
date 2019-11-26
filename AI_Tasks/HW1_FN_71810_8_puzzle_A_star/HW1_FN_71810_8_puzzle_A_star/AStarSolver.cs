namespace Task02Better
{
    using System.Collections.Generic;

    public class AStarSolver : Solver
    {
        /// <summary>
        /// This is the main method called to find the solution of the game
        /// </summary>
        /// <param name="state">This state is the start state from which the algorithm should find solution</param>
        public override void Solve(State state)
        {
            var visited = new HashSet<Board>();

            //Priority queue implemented with Heap injected as external library C5
            var queue = new PriorityQueue<int, State>();

            queue.Enqueue(state.SearchDepth, state);
            visited.Add(state.CurrentBoard);

            while (queue.Count > 0)
            {
                if (queue.Count > this.MaxFringeSize)
                {
                    this.MaxFringeSize = queue.Count;
                }

                state = queue.DequeueValue();

                if (state.CurrentBoard.IsEqual(this.GoalState))
                {
                    this.PrintResults(state, queue.Count);
                    break;
                }

                this.NodesExpanded++;

                var zeroXAndY = state.CurrentBoard.IndexOfZero();
                var zeroX = zeroXAndY.Item1;
                var zeroY = zeroXAndY.Item2;
                var children = this.GenerateChildrenStates(state, zeroX, zeroY);

                for (var i = children.Count - 1; i >= 0; i--)
                {
                    var currentChild = children[i];
                    if (!visited.Contains(currentChild.CurrentBoard))
                    {
                        queue.Enqueue(currentChild.SearchDepth, currentChild);
                        visited.Add(currentChild.CurrentBoard);

                        if (currentChild.SearchDepth > this.MaxSearchDepth)
                        {
                            this.MaxSearchDepth = currentChild.SearchDepth;
                        }
                    }
                }
            }
        }
    }
}
