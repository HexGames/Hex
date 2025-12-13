using System.Runtime.CompilerServices;

namespace Hex.Data
{
    internal struct QueueDeckTileIDArray
    {
        [InlineArray(DeckTile.QUEUE_SIZE)] public struct QueueDeckTileIDInlineArray { private int _element0; }
        internal QueueDeckTileIDInlineArray Array;

        public QueueDeckTileIDArray()
        {
        }
    }
}
