using System;
using System.Diagnostics;
using GameSolver.Abstract;
using GameSolver.DataStructures;
using GameSolver.Interfaces;
using GameSolver.SearchTree;

namespace GameSolver.SearchStrategies
{
    public class TreeSearch<S, A> : SearchBase<S, A> where A : class
    {
        private int _count;
        private long _bytes;

        private TimeSpan _times = new TimeSpan(0, 0, 0);

        protected InOutCollection<Node<S, A>> Frontier;

        public override Node<S, A> FindNode(ISearchProblem<S, A> problem, InOutCollection<Node<S, A>> frontier)
        {
            _bytes = GC.GetTotalMemory(true);

            var startTime = Stopwatch.StartNew();
            startTime.Start();

            Frontier = frontier;

            var root = NodeFactory.CreateNode<S, A>(problem.InitState);

            AddToFrontier(root);

            long maxMemory = _bytes;

            while (!IsFrontierEmpty())
            {
                maxMemory = Math.Max(maxMemory, GC.GetTotalMemory(false));
                
                _count++;
                var node = RemoveFromFrontier();

                if (problem.GoalTest(node.State))
                {
                    startTime.Stop();
                    _times = startTime.Elapsed;
                    _bytes -= maxMemory;
                    return node;
                }

                foreach (var successor in NodeFactory.GetSuccessors(node, problem))
                {
                    AddToFrontier(successor);
                }
            }

            _bytes -= maxMemory;
            startTime.Stop();
            _times = startTime.Elapsed;

            return null;
        }

        public override long GetMemory()
        {
            return -_bytes;
        }

        public override TimeSpan GetTime()
        {
            return _times;
        }

        public override int GetSteps()
        {
            return _count;
        }

        protected virtual void AddToFrontier(Node<S, A> node)
        {
            Frontier.Add(node);
        }

        protected virtual Node<S, A> RemoveFromFrontier()
        {
            _count++;
            return Frontier.Remove();
        }

        protected virtual bool IsFrontierEmpty()
        {
            return Frontier.Empty();
        }
    }
}
