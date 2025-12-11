using System;
using System.Collections.Generic;

namespace Hex.Data
{
    public static partial class Game
    {
        public static DeckArrayInterface DeckTiles = new DeckArrayInterface();

        public sealed class DeckArrayInterface
        {
            public ref DeckTile this[int id]
            {
                get
                {
                    return ref _data.Turns.Array[_data.CurrentTurn].DeckTiles.Array[id];
                }
            }

            public Span<DeckTile> span
            {
                get
                {
                    ref DeckTileArray deckTiles = ref _data.Turns.Array[_data.CurrentTurn].DeckTiles;
                    return ((Span<DeckTile>)deckTiles.Array).Slice(0, deckTiles.DeckTileCount);
                }
            }

            public ref DeckTile GetTileFromHistory(int id, int turn)
            {
                return ref _data.Turns.Array[turn].DeckTiles.Array[id];
            }

            public Span<DeckTile> GetTileCollectionFromHistory(int id, int turn)
            {
                ref DeckTileArray deckTiles = ref _data.Turns.Array[turn].DeckTiles;
                return ((Span<DeckTile>)deckTiles.Array).Slice(0, deckTiles.DeckTileCount);
            }

            public void InitDeckTiles(List<Def.Tile> tiles)
            {
                _data.Turns.Array[_data.CurrentTurn].InitDeckTiles(tiles);
            }
        }
    }
}
