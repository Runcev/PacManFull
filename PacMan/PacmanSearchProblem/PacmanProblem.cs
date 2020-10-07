using System;
using System.Collections.Generic;
using GameSolver.Interfaces;

namespace PacMan.PacmanSearchProblem
{
    public class PacmanProblem : ISearchProblem<NearbyTiles, MoveAction>
    {
        public NearbyTiles InitState { get; }

        // Pacman - 2
        // Candy - 3
        // Empty - 0
        // Filled - 1
        private readonly int[][] _map;

        public PacmanProblem(int[][] map)
        {
            _map = map;
            InitState = FindPacman();
        }

        public IEnumerable<MoveAction> Actions(NearbyTiles state)
        {
            var actions = new List<MoveAction>();
            if (state.Down.State != TileState.Filled)
            {
                actions.Add(new MoveAction(MoveActionEnum.Down));
            }

            if (state.Up.State != TileState.Filled)
            {
                actions.Add(new MoveAction(MoveActionEnum.Up));
            }

            if (state.Left.State != TileState.Filled)
            {
                actions.Add(new MoveAction(MoveActionEnum.Left));
            }

            if (state.Right.State != TileState.Filled)
            {
                actions.Add(new MoveAction(MoveActionEnum.Right));
            }

            return actions;
        }

        public bool GoalTest(NearbyTiles state)
        {
            return state.Center.State == TileState.Candy;
        }

        public NearbyTiles Result(NearbyTiles state, MoveAction action)
        {
            int x = state.Center.X;
            int y = state.Center.Y;

            return action.Action switch
            {
                MoveActionEnum.Up => GetNearbyTiles(x - 1, y),
                MoveActionEnum.Down => GetNearbyTiles(x + 1, y),
                MoveActionEnum.Left => GetNearbyTiles(x, y - 1),
                MoveActionEnum.Right => GetNearbyTiles(x, y + 1),
                _ => null
            };
        }

        public double StepCost(NearbyTiles stateFrom, MoveAction action, NearbyTiles stateTo)
        {
            return 1;
        }

        private TileState GetState(int i, int j)
        {
            try
            {
                return _map[i][j] switch
                {
                    0 => TileState.Empty,
                    2 => TileState.Empty,
                    3 => TileState.Candy,
                    _ => TileState.Filled
                };
            }
            catch (IndexOutOfRangeException)
            {
                return TileState.Edge;
            }
        }

        private NearbyTiles GetNearbyTiles(int i, int j)
        {
            return new NearbyTiles
            {
                Center = new Tile {State = GetState(i, j), X = i, Y = j},
                Down = new Tile {State = GetState(i + 1, j), X = i + 1, Y = j},
                Up = new Tile {State = GetState(i - 1, j), X = i - 1, Y = j},
                Left = new Tile {State = GetState(i, j - 1), X = i, Y = j - 1},
                Right = new Tile {State = GetState(i, j + 1), X = i, Y = j + 1},
            };
        }

        private NearbyTiles FindPacman()
        {
            for (int i = 0; i < _map.Length; i++)
            {
                for (int j = 0; j < _map[i].Length; j++)
                {
                    if (_map[i][j] == 2)
                    {
                        return GetNearbyTiles(i, j);
                    }
                }
            }

            return null;
        }
    }
}