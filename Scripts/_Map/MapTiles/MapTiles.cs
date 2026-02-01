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

        public static void SetAvailableToPlaceAtHexPos(Data.HexPos coord)
        {
            MapTilesSubsystem.SetAvailableToPlaceAtHexPos(coord);
        }

        public static void ClearAllAvailableForPlace()
        {
            MapTilesSubsystem.ClearAllAvailableForPlace();
        }


        public static void TriggerBenefitHighlightAtHexPos(Data.HexPos coord)
        {
            MapTilesSubsystem.TriggerBenefitHighlightAtHexPos(coord);
        }
    }
}
