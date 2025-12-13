using System.Runtime.CompilerServices;

namespace Hex.Data
{
    internal struct MapTileArray
    {
        [InlineArray(MapTile.MAP_SIZE)] public struct TileArrayInlineArray { private MapTile _element0; }
        internal TileArrayInlineArray Array;

        public MapTileArray()
        {
        }
    }
}
