using System;
using GameSolver.SearchTree;

namespace GameSolver.Interfaces
{
    public interface IInformedSearch<S, A> where A : class
    {
        Func<Node<S, A>, double> Heuristic { get; }
    }
}