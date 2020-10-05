using System.Collections.Generic;

namespace GameSolver.Interfaces
{
    public interface ISearchForState<S, A>
    {
        S FindState(ISearchProblem<S, A> problem);
    }
}