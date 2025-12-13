using Godot;
using System.Collections.Generic;
using Hex.GodotMap;

namespace Hex.Map
{
    public static class MapTiles
    {
        public static void Refresh()
        {
            for (int mapIdx = 0; mapIdx < Hex.Data.Game.MapTiles.span.Length; mapIdx++)
            {
                ref Hex.Data.MapTile tile = ref Hex.Data.Game.MapTiles.span[mapIdx];
                int mapTileId = tile.MapTileID;
                Hex.Data.HexPos hexPos = Hex.Data.MapHelper.MapTileIDToHexPos(mapTileId);
                MapTilesSubsystem.SetPrefabAtHexPos(tile.Def.Map_TilePrefab, hexPos);
            }
        }
    }
}
