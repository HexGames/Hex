using System;

namespace Hex.Data
{
    public struct Res
    {
        private readonly int _defID = -1;
        public readonly Hex.Def.Res Def { get => Hex.Def.Lib.GetRes(_defID); }
        public int Value;

        public Res(Hex.Def.Res def)
        {
            _defID = def.ID;
            Value = 0;
        }

        public Res(Hex.Def.Res def, int value)
        {
            _defID = def.ID;
            Value = value;
        }

        public override bool Equals(object obj) => obj is Res other && Equals(other);
        public bool Equals(Res other) => _defID == other._defID && Value == other.Value;
        public override int GetHashCode() => HashCode.Combine(_defID, Value);
        public static bool operator ==(Res a, Res b) => a.Equals(b);
        public static bool operator !=(Res a, Res b) => !a.Equals(b);

        public static Res Null => new Res(null, 0);

        public bool IsNull()
        {
            return _defID < 0;
        }
        public bool IsNotNull()
        {
            return _defID >= 0;
        }
    }
}
