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
                    return ref _data.Turns.Array[_data.CurrentTurn].MapTiles.Array[id];
                }
            }

            public ref MapTile this[HexPos hexPos]
            {
                get
                {
                    return ref _data.Turns.Array[_data.CurrentTurn].MapTiles.Array[MapHelper.HexPosToMapTileID(hexPos)];
                }
            }
            public Span<MapTile> span
            {
                get
                {
                    return _data.Turns.Array[_data.CurrentTurn].MapTiles.Array;
                }
            }

            public ref MapTile GetTileFromHistory(int id, int turn)
            {
                return ref _data.Turns.Array[turn].MapTiles.Array[id];
            }

            public Span<MapTile> GetTileCollectionFromHistory(int id, int turn)
            {
                return _data.Turns.Array[turn].MapTiles.Array;
            }

            public void InitMapTiles(List<Def.Tile> tiles)
            {
                _data.Turns.Array[_data.CurrentTurn].InitMapTiles(tiles);
            }

            public ref MapTile CreateMapTileAtHexPos(DeckTile deckTile, HexPos hexPos)
            {
                int mapTileID = MapHelper.HexPosToMapTileID(hexPos);
                ref Turn turn = ref _data.Turns.Array[_data.CurrentTurn];
                Def.TileData.TerrainTagArray terrainTags = turn.MapTiles.Array[mapTileID].DefData.TerrainTags; // copy existing terrain tags
                turn.MapTiles.Array[mapTileID] = new MapTile(deckTile, mapTileID);
                turn.MapTiles.Array[mapTileID].DefData.TerrainTags = terrainTags; // paste existing terrain tags
                return ref turn.MapTiles.Array[mapTileID];
            }
        }
    }
}
