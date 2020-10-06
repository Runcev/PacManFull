using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using Microsoft.VisualBasic.CompilerServices;
using Otter.Components;
using Otter.Components.Movement;
using Otter.Core;
using Otter.Graphics;
using Otter.Graphics.Drawables;
using Otter.Utility;

namespace PacMan
{
    public class Class1
    {
        public static void Main(string[] args)
        {
            var game = new Game("Scenes and Entities");
            game.Color = Color.White;
            
            var testEntity = new Entity(350, 240);
            testEntity.AddGraphic(Image.CreateCircle(100, Color.Red));
            
            var scene = new Scene();

            var musicPath = Path.GetFullPath("music.ogg");
            var levelPath = Path.GetFullPath("level.oel");
            var projPath = Path.GetFullPath("project.oep");
            
            var music = new Music(musicPath);
            var ogmoProject = new OgmoProject(projPath);
            ogmoProject.RegisterTag(Tags.Solid, "Solid");
            
            ogmoProject.LoadLevelFromFile(levelPath, scene);

            var levelMatrix = XmlReader(levelPath);
            
             music.Play();

            
            game.Start(scene);
        }
        
        
        private static int[][] XmlReader(string path){
            var xmldoc = new XmlDataDocument();
            XmlNodeList xmlnode ;
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
            
            for ( int i = 0; i < res.Length; i++)
            {
                char2dArray[i] = res[i].ToCharArray();
            }

            int[][] levelMatrix = new int[char2dArray.Length][];
            
            for (int i = 0; i < char2dArray.Length; i++)
            {
                levelMatrix[i] = Array.ConvertAll(res[i].ToCharArray(), c => (int) Char.GetNumericValue(c));
            }
            
            /*
            foreach ( var temp in levelMatrix)
            {
               Console.WriteLine($"{string.Join(' ', temp)}");
            }*/

            return levelMatrix;
        }
    }
    
    enum Tags
    {
        Solid,
        Pacman,
        Food
    }
    
    class Pacman : Entity {
        public Pacman(float x, float y) : base(x, y) {
            var img = Image.CreateRectangle(20, 20, Color.Orange);
            AddGraphic(img);
            
            SetHitbox(20, 20, Tags.Pacman);

            var axis = Axis.CreateWASD();

            var platformingMovement = new BasicMovement(300, 300, 50)
            {
                Axis = axis
            };
            platformingMovement.AddCollision(Tags.Solid);
            platformingMovement.Collider = Hitbox;

            AddComponents(axis, platformingMovement);
            
        }
    }
  
    // A Coin Entity to match the Coin in the Ogmo Project.
    class Food : Entity {
        public Food(float x, float y) : base(x, y) {
            var img = Image.CreateRectangle(16, 16, Color.Red);
            AddGraphic(img);
            SetHitbox(16, 16, Tags.Food);

            // Adjust the position here because of the Origin in the Ogmo Project.
            var random = new Random();

            X += 3;
            Y += 3;
            
            
        }

    }
}