using System.Collections.Generic;

namespace GameSolver.Interfaces
{
    public interface ISearchProblem<S, A>
    {
        S InitState { get; }

        IEnumerable<A> Actions(S state);

        bool GoalTest(S state);
        
        S Result(S state, A action);

        double StepCost(S stateFrom, A action, S stateTo);
    }
}