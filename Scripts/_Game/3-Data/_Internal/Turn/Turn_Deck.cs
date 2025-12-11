using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Hex.Data
{
    internal partial struct Turn
    {
        internal DeckTileArray DeckTiles;

        internal void InitDeckTiles(List<Def.Tile> tileDefs)
        {
            if (tileDefs.Count > DeckTile.MAX_DECK)
            {
                Debug.LogError("[Data.Game]: count exceeds MAX_DECK");
                return;
            }

            for (int idx = 0; idx < tileDefs.Count; idx++)
            {
                AddDeckTile(tileDefs[idx]);
            }
        }

        internal void AddDeckTile(Def.Tile tileDef)
        {
            int id = DeckTiles.DeckTileCount; // the index in the array
            DeckTiles.Array[DeckTiles.DeckTileCount] = new DeckTile(tileDef, id);
            DeckTiles.Array[DeckTiles.DeckTileCount].State = DeckTile.TileState.InMeta;
            DeckTiles.DeckTileCount++;
        }
    }
}
