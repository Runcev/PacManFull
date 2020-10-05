namespace GameSolver.SearchTree
{
    public class Node<S, A> where A : class
    {
        public S State { get; }
        public Node<S, A> Parent { get; }
        public A Action { get; }
        public double PathCost { get; }

        public Node(S state, Node<S, A> parent = null, A action = null, double pathCost = 0)
        {
            State = state;
            Parent = parent;
            Action = action;
            PathCost = pathCost;
        }

        public override string ToString()
        {
            return $"[parent={Parent}, action={Action}, state={State}, pathCost={PathCost}]";
        }
    }
}