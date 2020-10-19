using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using GameSolver.Interfaces;
using Otter.Core;
using Otter.Graphics.Drawables;
using PacMan.PacmanSearchProblem;

namespace PacMan.Scenes
{
    public class LabyrinthScene<S, A>: Scene where A : class
    {
        [SuppressMessage("ReSharper.DPA", "DPA0001: Memory allocation issues")]
        public LabyrinthScene(ISearchProblem<S, A> problem, ISearchForActions<S, A> search, IMetrics metrics)
        {
            Utils.GameUtils.OgmoProject.LoadLevelFromFile("level.oel", this);
            
            var skovoroda = new Skovoroda(40 * 17, 40 * 14);
            var world = new World(40, 40);
            Add(skovoroda);
            Add(world);
            
            var actions = search.FindActions(problem);

            var actionsTask = Task.Run(async () =>
            {
                foreach (A a in actions)
                {
                    var action = a as MoveAction;
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
            
                    await Task.Delay(20);
                }
            });
            
            actionsTask.ContinueWith(t => Game.SwitchScene(new MetricsScene(actions.Count(), metrics)));
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