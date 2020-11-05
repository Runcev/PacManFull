using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace GameSolver.Full
{
    public class IterativeAlphaBetaSearch<S,A,P> : IAdversialSearch<S,A>
    {
        private readonly IGame<S, A, P> _game;
        private readonly double _utilMin;
        private readonly double _utilMax;
        private int _currDepthLimit;
        private bool _heuristicEvaluationUsed;
        private readonly Timer _timer;
        private bool _logEnabled;


        public IterativeAlphaBetaSearch(IGame<S, A, P> game, double utilMin, double utilMax, int time)
        {
            _game = game;
            _utilMin = utilMin;
            _utilMax = utilMax;
            _timer =  new Timer(time);
        }

        public void SetLogEnabled(bool b)
        {
            _logEnabled = b;
        }

        public A MakeDecision(S state)
        {
            var player = _game.GetPlayer(state);
            var results = OrderActions(state, _game.GetActions(state), player, 0);
            _timer.Start();
            _currDepthLimit = 0;

            do
            {
                IncrementDepthLimit();
                if (_logEnabled)
                {

                }

                _heuristicEvaluationUsed = false;
                var newResults = new ActionStore<A>();
                foreach (var action in results)
                {
                    var value = MinValue(_game.GetResult(state, action), player,
                        double.NegativeInfinity, double.PositiveInfinity, 1);

                    if (!_timer.TimeOutOccurred())
                    {
                        break;
                    }

                    newResults.Add(action, value);
                    if (_logEnabled)
                    {
                        //TODO
                    }
                }

                if (_logEnabled)
                {
                    Console.WriteLine();
                }

                if (newResults.Size() > 0)
                {
                    results = newResults.Actions;
                    if (!_timer.TimeOutOccurred())
                    {
                        if (HasSafeWinner(newResults.UtilValues.ElementAt(0)))
                        {
                            break;
                        }
                        else if (newResults.Size() > 1 && IsSignificantlyBetter(newResults.UtilValues.ElementAt(0), newResults.UtilValues.ElementAt(1)))
                        {
                            break;
                        }
                    }
                }
            } while (!_timer.TimeOutOccurred() && _heuristicEvaluationUsed);

            return results.ElementAt(0);
        }
        
        public double MaxValue(S state, P player, double alpha, double beta, int depth) 
        {
            if (_game.IsTerminal(state) || depth >= _currDepthLimit || _timer.TimeOutOccurred()) 
            {
                return Eval(state, player);
            } 
            else {
                var value = double.NegativeInfinity;
                foreach (var action in OrderActions(state, _game.GetActions(state), player, depth))
                {
                    value = Math.Max(value, MinValue(_game.GetResult(state, action), player, alpha, beta, depth + 1));
                    if (value >= beta)
                    {
                        return value;
                    }

                    alpha = Math.Max(alpha, value);
                }
                return value;
            }
        }
        
        public double MinValue(S state, P player, double alpha, double beta, int depth) 
        {
            if (_game.IsTerminal(state) || depth >= _currDepthLimit || _timer.TimeOutOccurred()) 
            {
                return Eval(state, player);
            } 
            else 
            {
                var value = double.PositiveInfinity;
                foreach (var action in OrderActions(state, _game.GetActions(state), player, depth))
                {
                    value = Math.Min(value, MaxValue(_game.GetResult(state, action), player, alpha, beta, depth + 1));
                    if (value <= alpha)
                    {
                        return value;
                    }

                    beta = Math.Min(beta, value);
                }
                return value;
            }
        }

        private void IncrementDepthLimit() {
            _currDepthLimit++;
        }

        private static bool IsSignificantlyBetter(double newUtility, double utility) {
            return false;
        }

        private bool HasSafeWinner(double resultUtility) {
            return resultUtility <= _utilMin || resultUtility >= _utilMax;
        }

        private double Eval(S state, P player) {
            if (_game.IsTerminal(state)) 
            {
                return _game.GetUtility(state, player);
            }
            else {
                _heuristicEvaluationUsed = true;
                return (_utilMin + _utilMax) / 2;
            }
        }

        private static List<A> OrderActions(S state, List<A> actions, P player, int depth) {
            return actions;
        }
        
    }
    
    
    public sealed class Timer {
        private static long duration;
        private static long startTime;

        public Timer(int maxSeconds) => duration = 1000 * maxSeconds;

        public void Start() {
            startTime = DateTime.Now.Millisecond;
        }

        public bool TimeOutOccurred() {
            return DateTime.Now.Millisecond > startTime + duration;
        }
    }
    
    public sealed class ActionStore<A> {
        public readonly List<A> Actions = new List<A>();
        public readonly List<double> UtilValues = new List<double>();

        public void Add(A action, double utilValue) {
            var idx = 0;
            while (idx < Actions.Count && utilValue <= UtilValues.ElementAt(idx))
            {
                idx++;
            }

            Actions.Add(idx, action);
            UtilValues.Add(idx, utilValue);
        }

        public int Size()
        {
            return Actions.Count;
        }
    }
}