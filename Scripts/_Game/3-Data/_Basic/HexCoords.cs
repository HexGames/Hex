using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public struct HexCoords : IEquatable<HexCoords>
    {
        public int X { get; }
        public int Y { get; }

        public HexCoords(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object obj) => obj is HexCoords other && Equals(other);
        public bool Equals(HexCoords other) => X == other.X && Y == other.Y;
        public override int GetHashCode() => HashCode.Combine(X, Y);
        public static bool operator ==(HexCoords a, HexCoords b) => a.Equals(b);
        public static bool operator !=(HexCoords a, HexCoords b) => !a.Equals(b);
        public static HexCoords operator +(HexCoords a, HexCoords b) => new HexCoords(a.X + b.X, a.Y + b.Y);

        public int DistanceTo(HexCoords other)
        {
            // Cube coordinates
            // x + y + z = 0
            int dx = X - other.X;
            int dy = Y - other.Y;
            int dz = (-X - Y) - (-other.X - other.Y);
            return (Math.Abs(dx) + Math.Abs(dy) + Math.Abs(dz)) / 2;
        }

        public static int Distance(HexCoords a, HexCoords b) => a.DistanceTo(b);

        public static readonly HexCoords[] Directions = new[]
            {
                new HexCoords(1, 0), new HexCoords(1, -1), new HexCoords(0, -1),
                new HexCoords(-1, 0), new HexCoords(-1, 1), new HexCoords(0, 1)
            };

        public static HexCoords Invalid => new HexCoords(-5, -5);

        public override string ToString() => $"({X}, {Y})";


    }
}
