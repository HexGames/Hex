using Godot;
using System;

namespace Hex.Data
{
    public struct HexPos : IEquatable<HexPos>
    {
        public int X { get; }
        public int Y { get; }

        public HexPos(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object obj) => obj is HexPos other && Equals(other);
        public bool Equals(HexPos other) => X == other.X && Y == other.Y;
        public override int GetHashCode() => HashCode.Combine(X, Y);
        public static bool operator ==(HexPos a, HexPos b) => a.Equals(b);
        public static bool operator !=(HexPos a, HexPos b) => !a.Equals(b);
        public static HexPos operator +(HexPos a, HexPos b) => new HexPos(a.X + b.X, a.Y + b.Y);

        public int DistanceTo(HexPos other)
        {
            // Cube coordinates
            // x + y + z = 0
            int dx = X - other.X;
            int dy = Y - other.Y;
            int dz = (-X - Y) - (-other.X - other.Y);
            return (Math.Abs(dx) + Math.Abs(dy) + Math.Abs(dz)) / 2;
        }

        public static int Distance(HexPos a, HexPos b) => a.DistanceTo(b);

        public static readonly HexPos[] Directions = new[]
            {
                new HexPos(1, 0), new HexPos(1, -1), new HexPos(0, -1),
                new HexPos(-1, 0), new HexPos(-1, 1), new HexPos(0, 1)
            };

        public static HexPos Invalid => new HexPos(-5, -5);

        public override string ToString() => $"({X}, {Y})";


    }
}
