using System;

namespace GameSolver.Interfaces
{
    public interface IMetrics
    {
        long GetMemory();
        TimeSpan GetTime();
        int GetSteps();
    }
}