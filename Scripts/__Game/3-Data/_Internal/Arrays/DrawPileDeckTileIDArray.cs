using System.Runtime.CompilerServices;

namespace Hex.Data
{
    internal struct DrawPileDeckTileIDArray
    {
        [InlineArray(DeckTile.MAX_DECK)] public struct DrawPileDeckTileIDInlineArray { private int _element0; }
        internal DrawPileDeckTileIDInlineArray Array;
        internal int DrawPileCount = 0;

        public DrawPileDeckTileIDArray()
        {
        }
    }
}
