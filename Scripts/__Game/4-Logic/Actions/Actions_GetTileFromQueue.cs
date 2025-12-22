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
        public static void GetTileFromQueue(out Data.DeckTileRef deckTileRef)
        {
            deckTileRef = Data.Game.DeckTiles.GetNextDeckTileFromQueue();
        }
    }
}
