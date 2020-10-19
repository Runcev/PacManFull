using System.Collections.Generic;

namespace GameSolver.Interfaces
{
    public interface ISearchForActions<S, A>
    {
        IEnumerable<A> FindActions(ISearchProblem<S, A> problem);
    }
}