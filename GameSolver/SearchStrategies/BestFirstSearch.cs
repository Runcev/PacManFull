using System;
using GameSolver.Abstract;
using GameSolver.DataStructures;
using GameSolver.Interfaces;
using GameSolver.SearchTree;

namespace GameSolver.SearchStrategies
{
    public class BestFirstSearch<S, A> : QueueBasedSearchBase<S, A>, IInformedSearch<S, A>
        where A : class where S : class
    {
        public BestFirstSearch(SearchBase<S, A> impl, Func<Node<S, A>, double> h) : base(impl, new PriorityQueue<Node<S, A>>(h))
        {
            Heuristic = h;
        }

        public Func<Node<S, A>, double> Heuristic { get; }
    }
}