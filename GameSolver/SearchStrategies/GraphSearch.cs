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

        protected override void AddToFrontier(Node<S, A> node)
        {
            if (!_explored.Contains(node.State))
            {
                Frontier.Add(node);
            }
        }

        protected override Node<S, A> RemoveFromFrontier()
        {
            CleanUpFrontier();
            var result = Frontier.Remove();
            _explored.Add(result.State);
            return result;
        }

        protected override bool IsFrontierEmpty()
        {
            CleanUpFrontier();
            return Frontier.Empty();
        }

        private void CleanUpFrontier()
        {
            while (!Frontier.Empty() && _explored.Contains(Frontier.Peek().State))
            {
                Frontier.Remove();
            }
        }
    }
}