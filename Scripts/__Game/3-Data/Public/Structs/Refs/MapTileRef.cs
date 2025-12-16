namespace Hex.Data
{
    public readonly struct MapTileRef
    {
        private readonly int _id = -1;

        private MapTileRef(int id)
        {
            _id = id;
        }

        public static MapTileRef FromID(int id) => new MapTileRef(id);
        public ref MapTile Value => ref GameData.Data.Turns.Array[GameData.Data.CurrentTurn].MapTiles.Array[_id];
        public ref MapTile GetValue(int turn) => ref GameData.Data.Turns.Array[turn].MapTiles.Array[_id];
        public HexPos HexPos => MapHelper.MapTileIDToHexPos(_id);
    }
}
