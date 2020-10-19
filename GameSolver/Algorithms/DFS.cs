using GameSolver.Abstract;
using GameSolver.DataStructures;
using GameSolver.SearchTree;

namespace GameSolver.Algorithms
{
    public class DFS<S, A> : QueueBasedSearchBase<S, A> where S : class where A : class
    {
        public DFS(SearchBase<S, A> impl) : base(impl, new InOutCollection<Node<S, A>>(true))
        {
        }
    }
}