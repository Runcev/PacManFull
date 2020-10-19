using System;
using GameSolver.Abstract;
using GameSolver.SearchStrategies;
using GameSolver.SearchTree;

namespace GameSolver.Algorithms
{
    public class AStarSearch<S, A> : BestFirstSearch<S, A> where S : class where A : class
    {
        public AStarSearch(SearchBase<S, A> impl, Func<Node<S, A>, double> h) : base(impl, CreateFn(h))
        {
        }

        private static Func<Node<S, A>, double> CreateFn(Func<Node<S, A>, double> h)
        {
            return node => node.PathCost + h.Invoke(node);
        }
    }
}