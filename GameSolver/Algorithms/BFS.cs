using GameSolver.Abstract;
using GameSolver.DataStructures;
using GameSolver.SearchTree;

namespace GameSolver.Algorithms
{
    public class BFS<S, A> : QueueBasedSearchBase<S, A> where S : class where A : class
    {
        public BFS(SearchBase<S, A> impl) : base(impl, new InOutCollection<Node<S, A>>(false))
        {
        }
    }
}