namespace Hex.Def
{
    public readonly struct ResRef
    {
        internal readonly int _id = -1;

        private ResRef(int id)
        {
            _id = id;
        }

        public static ResRef FromID(int id) => new ResRef(id);
        public ref Res Value => ref Lib.GetRes(_id);

        public override bool Equals(object obj) => obj is ResRef other && Equals(other);
        public bool Equals(ResRef other) => _id == other._id && _id == other._id;
        public static bool operator ==(ResRef a, ResRef b) => a.Equals(b);
        public static bool operator !=(ResRef a, ResRef b) => !a.Equals(b);

        public static ResRef INVALID = new ResRef(-1);
    }
}
