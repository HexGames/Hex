using System.Collections.Generic;

namespace Hex.Data
{
    public partial struct DeckTile
    {
        public enum TileState
        {
            InMeta,
            InDrawDeck,
            InNextQueue,
            Played,
            Skipped,
        }
    }
}
