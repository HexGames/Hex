namespace Hex.Def
{
    public readonly struct TileRef
    {
        internal readonly int _id = -1;

        private TileRef(int id)
        {
            _id = id;
        }

        public static TileRef FromID(int id) => new TileRef(id);
        public ref Tile Value => ref Lib.GetTile(_id);

        public override bool Equals(object obj) => obj is TileRef other && Equals(other);
        public bool Equals(TileRef other) => _id == other._id && _id == other._id;
        public static bool operator ==(TileRef a, TileRef b) => a.Equals(b);
        public static bool operator !=(TileRef a, TileRef b) => !a.Equals(b);

        public static TileRef INVALID = new TileRef(-1);
    }
}
