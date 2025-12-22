namespace Hex.Def
{
    public readonly struct TagRef
    {
        internal readonly int _id = -1;

        private TagRef(int id)
        {
            _id = id;
        }

        public static TagRef FromID(int id) => new TagRef(id);
        public ref Tag Value => ref Lib.GetTag(_id);

        public override bool Equals(object obj) => obj is TagRef other && Equals(other);
        public bool Equals(TagRef other) => _id == other._id && _id == other._id;
        public static bool operator ==(TagRef a, TagRef b) => a.Equals(b);
        public static bool operator !=(TagRef a, TagRef b) => !a.Equals(b);

        public static TagRef INVALID = new TagRef(-1);
    }
}
