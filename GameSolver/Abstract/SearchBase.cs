using System;
using System.Collections.Generic;
using GameSolver.DataStructures;
using GameSolver.Interfaces;
using GameSolver.SearchTree;

namespace GameSolver.Abstract
{
    public abstract class SearchBase<S, A> : IMetrics where A : class
    {
        public abstract Node<S, A> FindNode(ISearchProblem<S, A> problem, InOutCollection<Node<S, A>> frontier);
        public abstract long GetMemory();
        public abstract TimeSpan GetTime();
        public abstract int GetSteps();
    }
}