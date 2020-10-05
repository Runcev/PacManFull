using System.IO;
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

            var levelPath = Path.Combine("Assets", "Ogmo", "level.oel");
            var projPath = Path.Combine("Assets", "Ogmo", "project.oep");
            
            var ogmoProject = new OgmoProject(projPath);
            ogmoProject.RegisterTag(Tags.Solid, "Solid");
            
            ogmoProject.LoadLevelFromFile(levelPath, scene);

            game.Start(scene);
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
            var img = Image.CreateRectangle(20, 20, Color.Yellow);
            AddGraphic(img);
            SetHitbox(20, 20, Tags.Pacman);

            var axis = Axis.CreateWASD();

            var platformingMovement = new BasicMovement(300, 300, 10)
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
        public Food(float x, float y) : base(x, y)
        {
            var img = Image.CreateRectangle(16, 16, Color.Red);
            AddGraphic(img);
            SetHitbox(16, 16, Tags.Food);

            // Adjust the position here because of the Origin in the Ogmo Project.
            X += 3;
            Y += 3;
        }
    }
}