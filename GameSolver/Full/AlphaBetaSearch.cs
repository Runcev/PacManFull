﻿using System;

namespace GameSolver.Full
{
    public class AlphaBetaSearch<S, A, P> : IAdversarialSearch<S, A> where A : class
    {
        private readonly IGame<S, A, P> _game;

        public static AlphaBetaSearch<S, A, P> CreateFor(IGame<S, A, P> game)
        {
            return new AlphaBetaSearch<S, A, P>(game);
        }

        public AlphaBetaSearch(IGame<S, A, P> game)
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
                var value = MinValue(_game.GetResult(state, action), player, double.NegativeInfinity,
                    double.PositiveInfinity);
                if (value > resultValue)
                {
                    result = action;
                    resultValue = value;
                }
            }

            return result;
        }


        private double MaxValue(S state, P player, double alpha, double beta)
        {
            if (_game.IsTerminal(state))
            {
                return _game.GetUtility(state, player);
            }

            var value = double.NegativeInfinity;
            foreach (var action in _game.GetActions(state))
            {
                value = Math.Max(value, MinValue(_game.GetResult(state, action), player, alpha, beta));
                if (value >= beta)
                {
                    return value;
                }

                alpha = Math.Max(alpha, value);
            }

            return value;
        }

        private double MinValue(S state, P player, double alpha, double beta)
        {
            if (_game.IsTerminal(state))
            {
                return _game.GetUtility(state, player);
            }

            var value = double.PositiveInfinity;
            foreach (var action in _game.GetActions(state))
            {
                value = Math.Min(value, MaxValue(_game.GetResult(state, action), player, alpha, beta));
                if (value <= alpha)
                {
                    return value;
                }

                beta = Math.Min(beta, value);
            }

            return value;
        }
    }
}