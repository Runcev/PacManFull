using System.Collections.Generic;

namespace GameSolver.Interfaces
{
    public interface SearchProblem<S, A>
    {
        S InitState { get; }

        IEnumerable<A> Actions(S state);

        bool GoalTest(S state);

        double StepCost(S stateFrom, A action, S stateTo);
    }
}