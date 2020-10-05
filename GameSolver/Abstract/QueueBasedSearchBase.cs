using System.Collections.Generic;
using GameSolver.DataStructures;
using GameSolver.Interfaces;
using GameSolver.SearchTree;

namespace GameSolver.Abstract
{
    public abstract class QueueBasedSearchBase<S, A> : ISearchForActions<S, A>, ISearchForState<S, A> where A : class
    {
        private readonly SearchBase<S, A> _impl;
        private readonly InOutCollection<Node<S, A>> _frontier;

        protected QueueBasedSearchBase(SearchBase<S, A> impl, InOutCollection<Node<S, A>> list)
        {
            _frontier = list;
            _impl = impl;
        }
        
        public IEnumerable<A> FindActions(ISearchProblem<S, A> problem)
        {
            throw new System.NotImplementedException();
        }

        public S FindState(ISearchProblem<S, A> problem)
        {
            throw new System.NotImplementedException();
        }
    }
}