using System.Collections.Generic;
using GameSolver.DataStructures;
using GameSolver.Interfaces;
using GameSolver.SearchTree;
using GameSolver.Utils;

namespace GameSolver.Abstract
{
    public abstract class QueueBasedSearchBase<S, A> : ISearchForActions<S, A>, ISearchForState<S, A> where A : class where S : class
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
            _frontier.Clear();
            var node = _impl.FindNode(problem, _frontier);
            return SearchUtils.ToActions(node);
        }

        public S FindState(ISearchProblem<S, A> problem)
        {
            _frontier.Clear();
            var node = _impl.FindNode(problem, _frontier);
            return SearchUtils.ToState(node);
        }
    }
}