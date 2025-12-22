using System;
using Hex;

namespace Hex.Data
{
    public struct Res
    {
        public readonly Hex.Def.ResRef Def;
        public int Value;

        public Res(Hex.Def.ResRef resDef)
        {
            Def = resDef;
            Value = 0;
        }

        public Res(Hex.Def.ResRef resDef, int value)
        {
            Def = resDef;
            Value = value;
        }

        public override bool Equals(object obj) => obj is Res other && Equals(other);
        public bool Equals(Res other) => Def == other.Def && Value == other.Value;
        public override int GetHashCode() => HashCode.Combine(Def, Value);
        public static bool operator ==(Res a, Res b) => a.Equals(b);
        public static bool operator !=(Res a, Res b) => !a.Equals(b);

        public static Res INVALID => new Res(Hex.Def.ResRef.INVALID);
    }
}
