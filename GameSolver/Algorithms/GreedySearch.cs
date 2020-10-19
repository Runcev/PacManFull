using System;
using GameSolver.Abstract;
using GameSolver.SearchStrategies;
using GameSolver.SearchTree;

namespace GameSolver.Algorithms
{
    public class GreedySearch<S, A> : BestFirstSearch<S, A> where S : class where A : class
    {
        public GreedySearch(SearchBase<S, A> impl, Func<Node<S, A>, double> h) : base(impl, h)
        {
        }
    }
}