using System.Collections.Generic;

namespace GameSolver.Full
{
    public interface IGame<S,A,P>
    {
        S GetInitialState();
        P[] GetPlayers();
        P GetPlayer(S state);
        List<A> GetActions(S state);
        S GetResult(S state, A action);
        bool IsTerminal(S state);
        double GetUtility(S state, P player);
    }
}