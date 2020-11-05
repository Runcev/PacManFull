using System;
using System.Linq;

namespace GameSolver.Full
{
    public class MiniMaxSearch<S, A, P> : IAdversialSearch<S, A>
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
            var result = default(A);
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

        public double MinValue(S state, P player)
        {
            if (_game.IsTerminal(state))
            {
                return _game.GetUtility(state, player);
            }

            return _game.GetActions(state).Where()
        }

        public double MaxValue(S state, P player)
        {
            if (_game.IsTerminal(state))
            {
                return _game.GetUtility(state, player);
            }

            return _game.GetActions(state).Where(action => MinValue(_game.GetResult(state, action), player)).Min() ?? double.PositiveInfinity;
        }
    }
}