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
    }
}
