using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public struct HexCoord : IEquatable<HexCoord>
    {
        public int X { get; }
        public int Y { get; }

        public HexCoord(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object obj) => obj is HexCoord other && Equals(other);
        public bool Equals(HexCoord other) => X == other.X && Y == other.Y;
        public override int GetHashCode() => HashCode.Combine(X, Y);
        public static bool operator ==(HexCoord a, HexCoord b) => a.Equals(b);
        public static bool operator !=(HexCoord a, HexCoord b) => !a.Equals(b);
        public static HexCoord operator +(HexCoord a, HexCoord b) => new HexCoord(a.X + b.X, a.Y + b.Y);

        public int DistanceTo(HexCoord other)
        {
            // Cube coordinates
            // x + y + z = 0
            int dx = X - other.X;
            int dy = Y - other.Y;
            int dz = (-X - Y) - (-other.X - other.Y);
            return (Math.Abs(dx) + Math.Abs(dy) + Math.Abs(dz)) / 2;
        }

        public static int Distance(HexCoord a, HexCoord b) => a.DistanceTo(b);

        public static readonly HexCoord[] Directions = new[]
            {
                new HexCoord(1, 0), new HexCoord(1, -1), new HexCoord(0, -1),
                new HexCoord(-1, 0), new HexCoord(-1, 1), new HexCoord(0, 1)
            };

        public static HexCoord Invalid => new HexCoord(-5, -5);
    }
}
