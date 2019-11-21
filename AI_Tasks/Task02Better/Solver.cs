namespace Task02Better
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// This class is used to extract some functionality which can possibly be used to solve same task with different algorithms
    /// and also to reduce code length of derived class
    /// </summary>
    public abstract class Solver
    {
        /// <summary>
        /// Represents matrix which is the goal state
        /// </summary>
        protected int[,] GoalState { get; set; }

        /// <summary>
        /// Count of the set of all nodes at the end of all visited paths
        /// </summary>
        protected int MaxFringeSize { get; set; }

        /// <summary>
        /// Shows what is the max depth that we have reached while searching for goal state
        /// </summary>
        protected int MaxSearchDepth { get; set; }

        /// <summary>
        /// Count of nodes which possible next states (children) have been explored
        /// </summary>
        protected int NodesExpanded { get; set; }

        /// <summary>
        /// Initialize goal state
        /// </summary>
        public Solver()
        {
            this.GoalState = new int[3, 3]
            {
                { 0, 1, 2 },
                { 3, 4, 5 },
                { 6, 7, 8 }
            };
        }

        /// <summary>
        /// This is the main abstract method called to find the solution of the game
        /// </summary>
        /// <param name="state">This state is the start state from which the algorithm should find solution</param>
        public abstract void Solve(State state);

        protected List<State> GenerateChildrenStates(State currentState, int x, int y)
        {
            var children = new List<State>();
            List<Tuple<int, string>> costGoingDirections = new List<Tuple<int, string>>();

            var rightState = currentState.MoveZeroToTheRight(x, y);
            if (rightState != null)
            {
                costGoingDirections.Add(new Tuple<int, string>(currentState.CompareTo(rightState), "right"));
            }

            var leftState = currentState.MoveZeroToTheLeft(x, y);
            if (leftState != null)
            {
                costGoingDirections.Add(new Tuple<int, string>(currentState.CompareTo(leftState), "left"));
            }

            var downState = currentState.MoveZeroDown(x, y);
            if (downState != null)
            {
                costGoingDirections.Add(new Tuple<int, string>(currentState.CompareTo(downState), "down"));
            }

            var upState = currentState.MoveZeroUp(x, y);
            if (upState != null)
            {
                costGoingDirections.Add(new Tuple<int, string>(currentState.CompareTo(upState), "up"));
            }

            costGoingDirections.Sort();

            for (int i = 0; i < costGoingDirections.Count; i++)
            {
                switch (costGoingDirections[i].Item2)
                {
                    case "right":
                            rightState.LastMove = "right";
                            rightState.Parent = currentState;
                            rightState.SearchDepth++;
                            children.Add(rightState);
                        break;
                    case "left":

                            leftState.LastMove = "left";
                            leftState.Parent = currentState;
                            leftState.SearchDepth++;
                            children.Add(leftState);
                        break;
                    case "down":
                            downState.LastMove = "down";
                            downState.Parent = currentState;
                            downState.SearchDepth++;
                            children.Add(downState);
                        break;
                    case "up":
                            upState.LastMove = "up";
                            upState.Parent = currentState;
                            upState.SearchDepth++;
                            children.Add(upState);
                        break;
                }
            }
            children.Reverse();
            return children;
        }

        /// <summary>
        /// Shows the results of the found solution
        /// </summary>
        /// <param name="finalState">Takes the final state in which the game was</param>
        /// <param name="fringeSize">Count of the set of all nodes at the end of all visited paths</param>
        public void PrintResults(State finalState, int fringeSize)
        {
            var searchDepth = finalState.SearchDepth;
            var path = FindPath(finalState);
            var pathToGoal = this.GetPathAsString(path);
            var costOfPath = path.Count;

            Console.WriteLine($"Path to goal: {pathToGoal}");
            Console.WriteLine($"Cost of path: {costOfPath}");
            Console.WriteLine($"Nodes expanded: {this.NodesExpanded}");
            Console.WriteLine($"Fringe size: {fringeSize}");
            Console.WriteLine($"Max fringe size: {this.MaxFringeSize}");
            Console.WriteLine($"Search depth: {searchDepth}");
            Console.WriteLine($"Max search depth: {this.MaxSearchDepth}");
        }

        /// <summary>
        /// Backtrack the path of the solution found
        /// </summary>
        /// <param name="state">This should be the final state from which we backtrack the path</param>
        /// <returns>Returns the path from goal state to start state</returns>
        private List<string> FindPath(State state)
        {
            var path = new List<string>();
            while (state.Parent != null)
            {
                path.Add(state.LastMove);
                state = state.Parent;
            }

            return path;
        }

        /// <summary>
        /// This function reverse path which was from the goal state to the start state so the real path can be print
        /// </summary>
        /// <param name="path">Backtracked path</param>
        /// <returns>The path from start state to goal state as string</returns>
        private string GetPathAsString(List<string> path)
        {
            //Used stringBuilder because it is fast with concatenation
            var sb = new StringBuilder();
            sb.Append("[\n");

            for (int i = path.Count - 1; i >= 0; i--)
            {
                if (i % 10 == 0)
                {
                    sb.Append("\n");
                }
                sb.Append("'");
                sb.Append(path[i]);
                sb.Append("'");
                sb.Append(", ");
            }

            var pathToGoal = sb.ToString().TrimEnd(new[] { ',', ' ' });
            pathToGoal += "\n]";

            return pathToGoal;
        }
    }
}
