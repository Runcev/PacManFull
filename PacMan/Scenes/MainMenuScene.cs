using System;
using GameSolver.Algorithms;
using GameSolver.SearchStrategies;
using Otter.Core;
using Otter.Graphics.Drawables;
using PacMan.PacmanSearchProblem;
using PacMan.Utils;

namespace PacMan.Scenes
{
    public class MainMenuScene : Scene
    {
        private readonly PacmanProblem _problem = new PacmanProblem(GameUtils.Map);
        private readonly GraphSearch<NearbyTiles, MoveAction> _graphSearch = new GraphSearch<NearbyTiles, MoveAction>();

        public MainMenuScene() : base(1280, 960)
        {
            AddGraphic(new Image("menu.jpg"));
        }

        public override void Update()
        {
            if (Input.KeyPressed(Key.D))
            {
                var search = new DFS<NearbyTiles, MoveAction>(_graphSearch);
                Game.SwitchScene(new LabyrinthScene<NearbyTiles, MoveAction>(_problem, search, _graphSearch));
            }
            else if (Input.KeyPressed(Key.B))
            {
                var graphSearchBFS = new GraphSearchBFS<NearbyTiles, MoveAction>();
                var search = new BFS<NearbyTiles, MoveAction>(graphSearchBFS);
                Game.SwitchScene(new LabyrinthScene<NearbyTiles, MoveAction>(_problem, search, graphSearchBFS));
            }
            else if (Input.KeyPressed(Key.G))
            {
                var search = new GreedySearch<NearbyTiles, MoveAction>(_graphSearch,
                    node => Math.Abs(21 - node.State.Center.X) + Math.Abs(20 - node.State.Center.Y));
                Game.SwitchScene(new LabyrinthScene<NearbyTiles, MoveAction>(_problem, search, _graphSearch));
            }
            else if (Input.KeyPressed(Key.A))
            {
                var search = new AStarSearch<NearbyTiles, MoveAction>(_graphSearch,
                    node => Math.Abs(21 - node.State.Center.X) + Math.Abs(20 - node.State.Center.Y));
                Game.SwitchScene(new LabyrinthScene<NearbyTiles, MoveAction>(_problem, search, _graphSearch));
            }
        }
    }
}