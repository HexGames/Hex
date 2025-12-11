using System.Runtime.CompilerServices;

namespace Hex.Data
{
    internal struct MapTileArray
    {
        [InlineArray(MapTile.MAX_MAP)] public struct TileArrayInlineArray { private MapTile _element0; }
        internal TileArrayInlineArray Array;

        public MapTileArray()
        {
        }
    }
}
