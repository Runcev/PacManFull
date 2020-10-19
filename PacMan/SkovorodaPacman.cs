using Otter.Core;
using PacMan.Scenes;

namespace PacMan
{
    public class SkovorodaPacman
    {
        public static void Main(string[] args)
        {
            var game = new Game("Skovoroda Pacman", 32 * 40, 24 * 40);
            var music = new Music("music.ogg");
            music.Volume = 0.1f;
            music.Play();
            game.Start(new MainMenuScene());
        }
    }
}