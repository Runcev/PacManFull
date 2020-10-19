using System.Collections.Generic;
using GameSolver.DataStructures;
using GameSolver.Interfaces;
using GameSolver.SearchTree;

namespace GameSolver.SearchStrategies
{
    public class GraphSearchBFS<S, A> : TreeSearch<S, A> where A : class where S : class
    {
        private readonly HashSet<S> _explored = new HashSet<S>();
        private readonly HashSet<S> _frontierStates = new HashSet<S>();

        public override Node<S, A> FindNode(ISearchProblem<S, A> problem, InOutCollection<Node<S, A>> frontier)
        {
            _explored.Clear();
            _frontierStates.Clear();
            return base.FindNode(problem, frontier);
        }

        protected override void AddToFrontier(Node<S, A> node)
        {
            if (!_explored.Contains(node.State) && !_frontierStates.Contains(node.State))
            {
                Frontier.Add(node);
                _frontierStates.Add(node.State);
            }
        }

        protected override Node<S, A> RemoveFromFrontier()
        {
            var res = Frontier.Remove();
            _explored.Add(res.State);
            _frontierStates.Remove(res.State);
            return res;
        }

        protected override bool IsFrontierEmpty()
        {
            return Frontier.Empty();
        }
    }
}