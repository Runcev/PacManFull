using System;

namespace PacMan.PacmanSearchProblem
{
    public enum TileState
    {
        Filled,
        Empty,
        Edge,
        Candy
    }

    public struct Tile
    {
        public TileState State { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
    
    public class NearbyTiles : IEquatable<NearbyTiles>
    {
        public Tile Up { get; set; }
        public Tile Down { get; set; }
        public Tile Right { get; set; }
        public Tile Left { get; set; }
        public Tile Center { get; set; }

        public bool Equals(NearbyTiles other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Center.Equals(other.Center);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(NearbyTiles)) return false;
            return Center.X == (obj as NearbyTiles).Center.X && Center.Y == (obj as NearbyTiles).Center.Y ;

        }

        public override int GetHashCode()
        {
            return Center.GetHashCode();
        }

        public static bool operator ==(NearbyTiles left, NearbyTiles right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(NearbyTiles left, NearbyTiles right)
        {
            return !Equals(left, right);
        }
    }
}