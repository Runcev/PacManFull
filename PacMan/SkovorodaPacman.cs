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

            var map = LevelReader(levelPath);
            map[1][1] = 2;
            map[13][18] = 3;

            var problem = new PacmanProblem(map);

            var graphSearch = new GraphSearch<NearbyTiles, MoveAction>();

            var search = new DFS<NearbyTiles, MoveAction>(graphSearch);
            var actions = search.FindActions(problem);
            
            var skovoroda = new Skovoroda(40 * 17, 40 * 14);
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
                    
                    await Task.Delay(500);
                }
            });

            Task.WaitAll(gameRun, doActions);
        }

        public static int[][] LevelReader(string path)
        {
            var xmldoc = new XmlDataDocument();
            XmlNodeList xmlnode;
            string str = null;
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            xmldoc.Load(fs);

            xmlnode = xmldoc.GetElementsByTagName("level");

            for (int i = 0; i <= xmlnode.Count - 1; i++)
            {
                xmlnode[i].ChildNodes.Item(0);
                str = xmlnode[i].ChildNodes.Item(0).InnerText.Trim();
            }

            var res = Regex.Split(str, "\n");


            char[][] char2dArray = new char[res.Length][];

            for (int i = 0; i < res.Length; i++)
            {
                char2dArray[i] = res[i].Where(ch => ch != ',').ToArray();
            }

            int[][] levelMatrix = new int[char2dArray.Length][];

            for (int i = 0; i < char2dArray.Length; i++)
            {
                levelMatrix[i] = Array.ConvertAll(char2dArray[i], c => (int) Char.GetNumericValue(c) == 3 ? 1 : 0);
            }

            return levelMatrix;
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