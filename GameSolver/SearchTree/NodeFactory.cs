using System.Collections.Generic;
using GameSolver.Interfaces;

namespace GameSolver.SearchTree
{
    public static class NodeFactory
    {
        public static Node<S, A> CreateNode<S, A>(S state) where A : class
        {
            return new Node<S, A>(state);
        }

        public static Node<S, A> CreateNode<S, A>(S state, Node<S, A> parent, A action, double stepCost) where A : class
        {
            return new Node<S, A>(state, parent, action, parent.PathCost + stepCost);
        }

        public static IEnumerable<Node<S, A>> GetSuccessors<S, A>(Node<S, A> node, ISearchProblem<S, A> problem)
            where A : class
        {
            var successors = new List<Node<S, A>>();

            foreach (var action in problem.Actions(node.State))
            {
                S successorState = problem.Result(node.State, action);

                double stepCost = problem.StepCost(node.State, action, successorState);
                successors.Add(CreateNode(successorState, node, action, stepCost));
            }

            return successors;
        }
    }
}