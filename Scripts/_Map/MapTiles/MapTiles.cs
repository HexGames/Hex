using Godot;
using System.Collections.Generic;
using Hex.GodotMap;

namespace Hex.Map
{
    public static class MapTiles
    {
        public static void Refresh()
        {
            for (int mapID = 0; mapID < Data.Game.MapTiles.Collection.Length; mapID++)
            {
                // to do - think about changing Data.MapHelper.MapTileIDToHexPos and maps to an Array of MapTileRef
                Data.HexPos hexPos = Data.MapHelper.MapTileIDToHexPos(mapID);
                MapTilesSubsystem.SetPrefabAtHexPos(Data.Game.MapTiles.Collection[mapID].Def.Map_TilePrefab, hexPos);
            }
        }

        public static void PlaceTile(Data.MapTileRef mapTile)
        {
            MapTilesSubsystem.SetPrefabAtHexPos(mapTile.Value.Def.Map_TilePrefab, mapTile.HexPos);
        }
    }
}
