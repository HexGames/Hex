using System;
using System.Collections.Generic;

namespace Hex.Data
{
    public static partial class Game
    {
        public static MapArrayWrapper MapTiles = new MapArrayWrapper();

        public struct MapArrayWrapper
        {

            public ref Tile this[int id]
            {
                get
                {
                    return ref _data.Turns.Array[_data.CurrentTurn].Tiles.Array[id];
                }
            }

            public ref Tile this[HexPos hexPos]
            {
                get
                {
                    return ref _data.Turns.Array[_data.CurrentTurn].Tiles.Array[MapHelper.HexPosToMapTileID(hexPos)];
                }
            }
            public Span<Tile> span
            {
                get
                {
                    ref Turn currentTurn = ref _data.Turns.Array[_data.CurrentTurn];
                    return ((Span<Tile>)currentTurn.Tiles.Array).Slice(Tile.MAX_DECK, currentTurn.Tiles.MapTileCount);
                }
            }

            public ref Tile GetTileFromHistory(int id, int turn)
            {
                return ref _data.Turns.Array[turn].Tiles.Array[id];
            }

            public Span<Tile> GetTileCollectionFromHistory(int id, int turn)
            {
                return ((Span<Tile>)_data.Turns.Array[turn].Tiles.Array).Slice(Tile.MAX_DECK, _data.Turns.Array[turn].Tiles.MapTileCount);
            }

            public void SetMapTiles(List<Def.Tile> tiles)
            {
                _data.Turns.Array[_data.CurrentTurn].InitMapTiles(tiles);
            }

            public void SetTileAtHexPos(Tile tile, HexPos hexPos)
            {
                int mapTileID = MapHelper.HexPosToMapTileID(hexPos);
                tile.MapTileID = mapTileID;
                tile.State = TileState.OnMap;
                _data.Turns.Array[_data.CurrentTurn].Tiles.Array[mapTileID] = tile; // copy
            }
        }
    }
}
