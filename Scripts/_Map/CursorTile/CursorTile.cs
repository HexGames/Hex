using Hex.GodotMap;

namespace Hex.Map
{
    public static class CursorTile
    {
        public static void InitCursorTile(Hex.Data.DeckTile deckTile)
        {
            MapBindings.X.TileCursor.Activate(deckTile);
        }

        public static void OnPlayTile(Hex.Data.DeckTile tile, Hex.Data.HexPos hexPos)
        {
            MapTilesSubsystem.SetPrefabAtHexPos(tile.Def.Map_TilePrefab, hexPos);
            MapBindings.X.TileCursor.Clear();
        }
    }
}
