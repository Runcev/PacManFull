using System.Collections.Generic;
using GameSolver.DataStructures;
using GameSolver.Interfaces;
using GameSolver.SearchTree;

namespace GameSolver.SearchStrategies
{
    public class GraphSearch<S, A> : TreeSearch<S, A> where A : class
    {
        private readonly HashSet<S> _explored = new HashSet<S>();

        public override Node<S, A> FindNode(ISearchProblem<S, A> problem, InOutCollection<Node<S, A>> frontier)
        {
            _explored.Clear();
            return base.FindNode(problem, frontier);
        }

        protected new void AddToFrontier(Node<S, A> node)
        {
            if (!_explored.Contains(node.State))
            {
                _frontier.Add(node);
            }
        }

        protected new Node<S, A> RemoveFromFrontier()
        {
            CleanUpFrontier();
            var result = _frontier.Remove();
            _explored.Add(result.State);
            return result;
        }

        protected new bool IsFrontierEmpty()
        {
            CleanUpFrontier();
            return _frontier.Empty();
        }

        private void CleanUpFrontier()
        {
            while (!_frontier.Empty() && _explored.Contains(_frontier.Peek().State))
            {
                _frontier.Remove();
            }
        }
    }
}