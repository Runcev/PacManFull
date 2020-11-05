using System;
using System.Linq;

namespace GameSolver.Full
{
    public class MiniMaxSearch<S, A, P> : IAdversarialSearch<S, A> where A : class
    {
        private readonly IGame<S, A, P> _game;

        public static MiniMaxSearch<S, A, P> CreateFor(IGame<S, A, P> game)
        {
            return new MiniMaxSearch<S, A, P>(game);
        }

        public MiniMaxSearch(IGame<S, A, P> game)
        {
            _game = game;
        }

        public A MakeDecision(S state)
        {
            A result = null;
            var resultValue = double.NegativeInfinity;
            var player = _game.GetPlayer(state);
            foreach (var action in _game.GetActions(state))
            {
                var value = MinValue(_game.GetResult(state, action), player);
                if (value > resultValue)
                {
                    result = action;
                    resultValue = value;
                }
            }

            return result;
        }

        private double MinValue(S state, P player)
        {
            if (_game.IsTerminal(state))
            {
                return _game.GetUtility(state, player);
            }

            return _game.GetActions(state)
                .Select(a => MaxValue(_game.GetResult(state, a), player))
                .DefaultIfEmpty(double.PositiveInfinity)
                .Min();
        }

        private double MaxValue(S state, P player)
        {
            if (_game.IsTerminal(state))
            {
                return _game.GetUtility(state, player);
            }

            return _game.GetActions(state)
                .Select(a => MinValue(_game.GetResult(state, a), player))
                .DefaultIfEmpty(double.NegativeInfinity)
                .Max();
        }
    }
}