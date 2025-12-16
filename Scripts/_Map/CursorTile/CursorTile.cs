using Hex.GodotMap;

namespace Hex.Map
{
    public static class CursorTile
    {
        public static void InitCursorTile(Data.DeckTileRef deckTile)
        {
            MapBindings.X.TileCursor.Activate(deckTile);
        }

        public static void OnPlayTile(Data.DeckTileRef deckTile, Data.HexPos hexPos)
        {
            MapTilesSubsystem.SetPrefabAtHexPos(deckTile.Value.Def.Map_TilePrefab, hexPos);
            MapBindings.X.TileCursor.Clear();
        }
    }
}
