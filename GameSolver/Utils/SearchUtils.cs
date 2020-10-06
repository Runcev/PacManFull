using System.Collections.Generic;
using System.Linq;
using GameSolver.SearchTree;

namespace GameSolver.Utils
{
    public class SearchUtils
    {
        public static IEnumerable<Node<S, A>> GetPathFromRoot<S, A>(Node<S, A> node) where A : class
        {
            var path = new LinkedList<Node<S, A>>();

            while (!node.IsRootNode())
            {
                path.AddFirst(node);
                node = node.Parent;
            }

            path.AddFirst(node);
            return path.ToArray();
        }

        public static IEnumerable<A> GetSequenceOfActions<S, A>(Node<S, A> node) where A : class
        {
            var actions = new LinkedList<A>();
            while (!node.IsRootNode())
            {
                actions.AddFirst(node.Action);
                node = node.Parent;
            }

            return actions.ToArray();
        }

        public static IEnumerable<A> ToActions<S, A>(Node<S, A> node) where A : class
        {
            return node != null ? GetSequenceOfActions(node) : Enumerable.Empty<A>();
        }

        public static S ToState<S, A>(Node<S, A> node) where A : class where S : class
        {
            return node?.State;
        }
    }
}