using Godot;
using System;

namespace Hex.Data
{
    public struct HexPos : IEquatable<HexPos>
    {
        private int _x;
        private int _y;
        public int X { get => _x; }
        public int Y { get => _y; }

        public HexPos(int x, int y)
        {
            _x = x;
            _y = y;
        }

        public override bool Equals(object obj) => obj is HexPos other && Equals(other);
        public bool Equals(HexPos other) => _x == other._x && _y == other._y;
        public override int GetHashCode() => HashCode.Combine(_x, _y);
        public static bool operator ==(HexPos a, HexPos b) => a.Equals(b);
        public static bool operator !=(HexPos a, HexPos b) => !a.Equals(b);
        public static HexPos operator +(HexPos a, HexPos b) => new HexPos(a._x + b._x, a._y + b._y);

        public int DistanceTo(HexPos other)
        {
            // Cube coordinates
            // x + y + z = 0
            int dx = _x - other._x;
            int dy = _y - other._y;
            int dz = (-_x - _y) - (-other._x - other._y);
            return (Math.Abs(dx) + Math.Abs(dy) + Math.Abs(dz)) / 2;
        }

        public static int Distance(HexPos a, HexPos b) => a.DistanceTo(b);

        public static readonly HexPos[] Directions = new[]
            {
                new HexPos(1, 0), new HexPos(1, -1), new HexPos(0, -1),
                new HexPos(-1, 0), new HexPos(-1, 1), new HexPos(0, 1)
            };

        public static HexPos CENTER => new HexPos(0, 0);
        public static HexPos INVALID => new HexPos(-5, -5);

        public override string ToString() => $"({_x}, {_y})";


    }
}
