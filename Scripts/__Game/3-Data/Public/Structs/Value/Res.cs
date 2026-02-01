using System;
using Hex;

namespace Hex.Data
{
    public struct Res
    {
        public readonly Def.ResRef ResDef;
        public int Value;

        public Res(Def.ResRef resDef)
        {
            ResDef = resDef;
            Value = 0;
        }

        public Res(Def.ResRef resDef, int value)
        {
            ResDef = resDef;
            Value = value;
        }

        public override bool Equals(object obj) => obj is Res other && Equals(other);
        public bool Equals(Res other) => ResDef == other.ResDef && Value == other.Value;
        public override int GetHashCode() => HashCode.Combine(ResDef, Value);
        public static bool operator ==(Res a, Res b) => a.Equals(b);
        public static bool operator !=(Res a, Res b) => !a.Equals(b);

        public static Res INVALID => new Res(Hex.Def.ResRef.INVALID);
    }
}
