using GameSolver.Interfaces;
using Otter.Core;
using Otter.Graphics.Text;

namespace PacMan.Scenes
{
    public class MetricsScene : Scene
    {
        public MetricsScene(int steps, IMetrics metrics)
        {
            var metricsText = new Text
            {
                String = "Metrics",
                FontSize = 40,
                X =  32 * 40 / 2 - 25,
                Y = 5
            };
            AddGraphic(metricsText);

            var t = metrics.GetTime();
            var time = new Text
            {
                String = $"Time: {t.Seconds}.{t.Milliseconds} s",
                FontSize = 40,
                X = 30,
                Y = 80
            };
            AddGraphic(time);

            var algoSteps = new Text
            {
                String = $"Steps of algorithm: {metrics.GetSteps()}",
                FontSize = 40,
                X = 30,
                Y = 150
            };
            AddGraphic(algoSteps);

            var pacmanSteps = new Text
            {
                String = $"Steps of Pacman: {steps}",
                FontSize = 40,
                X = 30,
                Y = 220
            };
            AddGraphic(pacmanSteps);

            var memory = new Text
            {
                String = $"Memory used: {metrics.GetMemory() / 1024f / 1024} mb",
                FontSize = 40,
                X = 30,
                Y = 290
            };
            AddGraphic(memory);
            
            var space = new Text
            {
                String = "Press space to return\nto main menu",
                FontSize = 70,
                X = 30,
                Y = 600
            };
            AddGraphic(space);
        }

        public override void Update()
        {
            if (Input.KeyPressed(Key.Space))
            {
                Game.SwitchScene(new MainMenuScene());
            }
        }
    }
}