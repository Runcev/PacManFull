using System.Collections.Generic;
using GameSolver.Abstract;
using GameSolver.DataStructures;
using GameSolver.Interfaces;
using GameSolver.SearchTree;

namespace GameSolver.SearchStrategies
{
    public class TreeSearch<S, A> : SearchBase<S, A> where A : class
    {
        protected InOutCollection<Node<S, A>> _frontier;

        public override Node<S, A> FindNode(ISearchProblem<S, A> problem, InOutCollection<Node<S, A>> frontier)
        {
            _frontier = frontier;

            var root = NodeFactory.CreateNode<S, A>(problem.InitState);

            AddToFrontier(root);

            while (!IsFrontierEmpty())
            {
                var node = RemoveFromFrontier();

                if (problem.GoalTest(node.State))
                {
                    return node;
                } 
                
                foreach (var successor in NodeFactory.GetSuccessors(node, problem))
                {
                    AddToFrontier(successor);
                }
            }

            return null;
        }

        protected virtual void AddToFrontier(Node<S, A> node)
        {
            _frontier.Add(node);
        }

        protected virtual Node<S, A> RemoveFromFrontier()
        {
            return _frontier.Remove();
        }
        
        protected virtual bool IsFrontierEmpty()
        {
            return _frontier.Empty();
        }
    }
}