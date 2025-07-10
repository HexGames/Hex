using System;

namespace Data
{
    public struct Res //: IEquatable<Res>
    {
        public Def.Res Def;
        public int Value;

        public Res(Def.Res def)
        {
            Def = def;
            Value = 0;
        }

        public Res(Def.Res def, int value)
        {
            Def = def;
            Value = value;
        }

        public override bool Equals(object obj) => obj is Res other && Equals(other);
        public bool Equals(Res other) => Def == other.Def && Value == other.Value;
        public override int GetHashCode() => HashCode.Combine(Def, Value);
        public static bool operator ==(Res a, Res b) => a.Equals(b);
        public static bool operator !=(Res a, Res b) => !a.Equals(b);

        public static Res Null => new Res(null, 0);

        public bool IsNull()
        {
            return Def == null;
        }
        public bool IsNotNull()
        {
            return Def != null;
        }
    }
}
