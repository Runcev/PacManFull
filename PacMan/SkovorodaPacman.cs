using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using GameSolver.Algorithms;
using GameSolver.DataStructures;
using GameSolver.Interfaces;
using GameSolver.SearchStrategies;
using GameSolver.SearchTree;
using Microsoft.VisualBasic.CompilerServices;
using Otter.Components;
using Otter.Components.Movement;
using Otter.Core;
using Otter.Graphics;
using Otter.Graphics.Drawables;
using Otter.Utility;
using PacMan.PacmanSearchProblem;
using PacMan.Scenes;
using PacMan.Utils;

namespace PacMan
{
    public class SkovorodaPacman
    {
        public static async Task Main(string[] args)
        {
            var game = new Game("Skovoroda Pacman", 32 * 40, 24 * 40);

            var scene = new Scene();

            var levelPath = Path.Combine("level.oel");
            var projPath = Path.Combine("project.oep");

            var ogmoProject = new OgmoProject(projPath);
            ogmoProject.LoadLevelFromFile(levelPath, scene);

            var map = MapUtils.Map;
            map[1][1] = 2;
            map[14][14] = 3;

            var problem = new PacmanProblem(map);

            var graphSearch = new GraphSearch<NearbyTiles, MoveAction>();

            double GetMan(Node<NearbyTiles, MoveAction> node)
            {
                // return Math.Abs(14 - node.State.Center.X) + Math.Abs(14 - node.State.Center.Y);
                return 0;
            }

            var search = new GreedySearch<NearbyTiles, MoveAction>(graphSearch, GetMan);
            var actions = search.FindActions(problem);

            var skovoroda = new Skovoroda(40 * 14, 40 * 14);
            var world = new World(40, 40);
            scene.Add(skovoroda);
            scene.Add(world);

            var gameRun = Task.Run(() => game.Start(scene));
            var doActions = Task.Run(async () =>
            {
                foreach (MoveAction action in actions)
                {
                    world.X += action.Action switch
                    {
                        MoveActionEnum.Up => 0,
                        MoveActionEnum.Down => 0,
                        MoveActionEnum.Left => -40,
                        MoveActionEnum.Right => 40,
                        _ => 0
                    };

                    world.Y += action.Action switch
                    {
                        MoveActionEnum.Up => -40,
                        MoveActionEnum.Down => 40,
                        MoveActionEnum.Left => 0,
                        MoveActionEnum.Right => 0,
                        _ => 0
                    };

                    await Task.Delay(200);
                }
            });

            Task.WaitAll(gameRun, doActions);

            Console.WriteLine($"Time of algorithm : {graphSearch.GetTime()}");
            Console.WriteLine($"Steps of algorithm: {graphSearch.GetSteps()}");
            Console.WriteLine($"Steps of Pacman: {actions.Count()}");
            Console.WriteLine($"Memory: {graphSearch.GetMemory() / 1024f / 1024f}");
        }
    }

    class World : Entity
    {
        public World(float x, float y) : base(x, y)
        {
            AddGraphic(new Image("world.png"));
        }
    }

    class Skovoroda : Entity
    {
        public Skovoroda(float x, float y) : base(x, y)
        {
            AddGraphic(new Image("skovoroda.png"));
        }
    }
}