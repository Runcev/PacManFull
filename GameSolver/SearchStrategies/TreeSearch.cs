using System.Collections.Generic;
using GameSolver.Abstract;
using GameSolver.DataStructures;
using GameSolver.Interfaces;
using GameSolver.SearchTree;

namespace GameSolver.SearchStrategies
{
    public class TreeSearch<S, A> : SearchBase<S, A> where A : class
    {
        protected InOutCollection<Node<S, A>> _frontier;

        public override Node<S, A> FindNode(ISearchProblem<S, A> problem, InOutCollection<Node<S, A>> frontier)
        {
            _frontier = frontier;

            var root = NodeFactory.CreateNode<S, A>(problem.InitState);

            return root;
        }

        protected void AddToFrontier(Node<S, A> node)
        {
            _frontier.Add(node);
        }

        protected Node<S, A> RemoveFromFrontier()
        {
            Node<S, A> result = _frontier.Remove();
            return result;
        }
        
        protected bool IsFrontierEmpty()
        {
            return _frontier.Empty();
        }
    }
}