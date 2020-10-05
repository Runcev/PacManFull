using System.Collections.Generic;

namespace GameSolver.Interfaces
{
    public interface SearchProblem<State, Action>
    {
        State InitState { get; }

        IEnumerable<Action> Actions(State state);

        bool GoalTest(State state);

        double StepCost(State stateFrom, Action action, State stateTo);
    }
}