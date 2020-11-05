namespace GameSolver.Full
{
    public interface IAdversialSearch<S,A>
    {
        A MakeDecision(S state);
        
    }
}