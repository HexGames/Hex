using System.Runtime.CompilerServices;

namespace Hex.Data
{
    internal struct DeckTileArray
    {
        [InlineArray(DeckTile.MAX_DECK)] public struct TileArrayInlineArray { private DeckTile _element0; }
        internal TileArrayInlineArray Array;
        internal int DeckTileCount = 0;

        public DeckTileArray()
        {
        }
    }
}
