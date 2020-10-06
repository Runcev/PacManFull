namespace PacMan.PacmanSearchProblem
{
    public enum TileState
    {
        Filled,
        Empty,
        Edge = Filled,
        Candy
    }

    public class Tile
    {
        public TileState State { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
    
    public class NearbyTiles
    {
        public Tile Up { get; set; }
        public Tile Down { get; set; }
        public Tile Right { get; set; }
        public Tile Left { get; set; }
        public Tile Center { get; set; }
    }
}