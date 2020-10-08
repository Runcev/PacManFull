using System;
using System.Collections.Generic;
using GameSolver.Abstract;
using GameSolver.DataStructures;
using GameSolver.Interfaces;
using GameSolver.SearchTree;

namespace GameSolver.SearchStrategies
{
    public class TreeSearch<S, A> : SearchBase<S, A> where A : class
    {
        private int count = 0;
        private long bytes = 0;

        private TimeSpan times = new TimeSpan(0, 0, 0);

        protected InOutCollection<Node<S, A>> _frontier;

        public override Node<S, A> FindNode(ISearchProblem<S, A> problem, InOutCollection<Node<S, A>> frontier)
        {
            bytes = GC.GetTotalMemory(true);

            var startTime = System.Diagnostics.Stopwatch.StartNew();
            startTime.Start();

            _frontier = frontier;

            var root = NodeFactory.CreateNode<S, A>(problem.InitState);

            AddToFrontier(root);

            while (!IsFrontierEmpty())
            {
                count++;
                var node = RemoveFromFrontier();

                if (problem.GoalTest(node.State))
                {
                    startTime.Stop();
                    times = startTime.Elapsed;
                    return node;
                }

                foreach (var successor in NodeFactory.GetSuccessors(node, problem))
                {
                    AddToFrontier(successor);
                }
            }

            startTime.Stop();
            times = startTime.Elapsed;

            return null;
        }

        public override long GetMemory()
        {
            bytes -= GC.GetTotalMemory(true);

            return -bytes;
        }

        public override TimeSpan GetTime()
        {
            return times;
        }

        public override int GetSteps()
        {
            return count;
        }

        protected virtual void AddToFrontier(Node<S, A> node)
        {
            _frontier.Add(node);
        }

        protected virtual Node<S, A> RemoveFromFrontier()
        {
            count++;
            return _frontier.Remove();
        }

        protected virtual bool IsFrontierEmpty()
        {
            return _frontier.Empty();
        }
    }
}
