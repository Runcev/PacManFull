using System.Collections.Generic;
using GameSolver.Interfaces;

namespace GameSolver.SearchTree
{
    public static class NodeFactory<S, A> where A : class
    {
        public static Node<S, A> CreateNode(S state) {
            return new Node<S, A>(state);
        }
        
        public static Node<S, A> CreateNode(S state, Node<S, A> parent, A action, double stepCost) {
            return new Node<S, A>(state, parent, action, parent.PathCost + stepCost);
        }
        
        public static IEnumerable<Node<S, A>> GetSuccessors(Node<S, A> node, ISearchProblem<S, A> problem) {
            var successors = new List<Node<S, A>>();
            
            foreach (var action in problem.Actions(node.State)) {
                S successorState = problem.Result(node.State, action);
            
                double stepCost = problem.StepCost(node.State, action, successorState);
                successors.Add(CreateNode(successorState, node, action, stepCost));
            }
            
            return successors;
        }
    }
}