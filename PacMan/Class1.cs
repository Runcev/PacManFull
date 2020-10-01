using Otter.Core;
using Otter.Graphics;
using Otter.Graphics.Drawables;

namespace PacMan
{
    public class Class1
    {
        public static void Main(string[] args)
        {
            var game = new Game("Scenes and Entities");
            
            var testEntity = new Entity(350, 240);
            testEntity.AddGraphic(Image.CreateCircle(100, Color.Red));
            
            var scene = new Scene();
            scene.Add(testEntity);
            
            game.Start(scene);
        }
    }
}