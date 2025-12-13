using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandSystem;
using System.Collections.Generic;
using Hex;

namespace Hex.Logic
{
    public static partial class Actions
    {
        public static bool GetTileFromQueue(out int deckTileID)
        {
            ref Data.DeckTile deckTile = ref Data.Game.DeckTiles.GetNextDeckTileIDFromQueue();
            deckTileID = deckTile.DeckTileID;
            return true;
        }
    }
}
