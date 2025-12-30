using System;
using System.Collections.Generic;

namespace Hex.Data
{
    public static partial class Game
    {
        public static MapTilesInterface MapTiles = new MapTilesInterface();
        
        public sealed class MapTilesInterface
        {
        
            public ref MapTile this[int id]
            {
                get
                {
                    return ref GameData.Data.Turns.Array[GameData.Data.CurrentTurn].MapTiles.Array[id];
                }
            }
        
            public ref MapTile this[HexPos hexPos]
            {
                get
                {
                    return ref GameData.Data.Turns.Array[GameData.Data.CurrentTurn].MapTiles.Array[MapHelper.HexPosToMapTileID(hexPos)];
                }
            }

            public Span<MapTile> Collection
            {
                get
                {
                    return GameData.Data.Turns.Array[GameData.Data.CurrentTurn].MapTiles.Array;
                }
            }
        
            //public ref MapTile GetTileFromHistory(int id, int turn)
            //{
            //    return ref _data.Turns.Array[turn].MapTiles.Array[id];
            //}

            //public Span<MapTile> GetTileCollectionFromHistory(int id, int turn)
            //{
            //    return _data.Turns.Array[turn].MapTiles.Array;
            //}
        
            public void InitMapTiles(List<Def.TileRef> tiles)
            {
                GameData.Data.Turns.Array[GameData.Data.CurrentTurn].InitMapTiles(tiles);
            }
        
            public MapTileRef CreateMapTileAtHexPos(DeckTileRef deckTile, HexPos hexPos)
            {
                ref Turn turn = ref GameData.Data.Turns.Array[GameData.Data.CurrentTurn];
                int mapTileID = MapHelper.HexPosToMapTileID(hexPos);

                Def.TileData.TerrainTagArray terrainTags = turn.MapTiles.Array[mapTileID].DefData.TerrainTags; // copy existing terrain tags

                turn.MapTiles.Array[mapTileID] = new MapTile(deckTile);
                turn.MapTiles.Array[mapTileID].DefData.TerrainTags = terrainTags; // paste existing terrain tags
                return MapTileRef.FromID(mapTileID);
            }
        }
    }
}
