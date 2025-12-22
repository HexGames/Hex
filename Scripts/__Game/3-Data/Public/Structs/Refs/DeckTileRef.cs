namespace Hex.Data
{
    public readonly struct DeckTileRef
    {
        private readonly int _id = -1;

        private DeckTileRef(int id)
        {
            _id = id;
        }

        public static DeckTileRef FromID(int id) => new DeckTileRef(id);
        public ref DeckTile Value => ref GameData.Data.Turns.Array[GameData.Data.CurrentTurn].DeckTiles.Array[_id];
        public ref DeckTile GetValue(int turn) => ref GameData.Data.Turns.Array[turn].DeckTiles.Array[_id];

        public override bool Equals(object obj) => obj is DeckTileRef other && Equals(other);
        public bool Equals(DeckTileRef other) => _id == other._id && _id == other._id;
        public static bool operator ==(DeckTileRef a, DeckTileRef b) => a.Equals(b);
        public static bool operator !=(DeckTileRef a, DeckTileRef b) => !a.Equals(b);

        public static DeckTileRef INVALID = new DeckTileRef(-1);
    }
}
