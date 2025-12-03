using System;

namespace Hex.Data
{
    public static partial class Game
    {
        public static DeckArrayWrapper DeckTiles = new DeckArrayWrapper();
        public static MapArrayWrapper MapTiles = new MapArrayWrapper();

        public struct DeckArrayWrapper
        {
            public ref Tile this[int id]
            {
                get
                {
                    return ref _data.Turns.Array[_data.CurrentTurn].Tiles.Array[id];
                }
            }

            public Span<Tile> span
            {
                get
                {
                    return ((Span<Tile>)_data.Turns.Array[_data.CurrentTurn].Tiles.Array).Slice(0, _data.Turns.Array[_data.CurrentTurn].Tiles.DeckTileCount);
                }
            }

            public ref Tile GetTileFromHistory(int id, int turn)
            {
                return ref _data.Turns.Array[turn].Tiles.Array[id];
            }

            public Span<Tile> GetTileCollectionFromHistory(int id, int turn)
            {
                return ((Span<Tile>)_data.Turns.Array[turn].Tiles.Array).Slice(0, _data.Turns.Array[turn].Tiles.DeckTileCount);
            }
        }

        public struct MapArrayWrapper
        {
            public ref Tile this[int id]
            {
                get
                {
                    return ref _data.Turns.Array[_data.CurrentTurn].Tiles.Array[id];
                }
            }

            public Span<Tile> span
            {
                get
                {
                    return ((Span<Tile>)_data.Turns.Array[_data.CurrentTurn].Tiles.Array).Slice(Tile.MAX_DECK, _data.Turns.Array[_data.CurrentTurn].Tiles.MapTileCount);
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
        }
    }
}
