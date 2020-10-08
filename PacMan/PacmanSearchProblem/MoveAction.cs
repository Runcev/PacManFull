namespace PacMan.PacmanSearchProblem
{
    public enum MoveActionEnum
    {
        Up,
        Down,
        Left,
        Right
    }

    public class MoveAction
    {
        public MoveAction(MoveActionEnum action)
        {
            Action = action;
        }

        public MoveActionEnum Action { get; }
    }
}