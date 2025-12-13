using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Hex.Data
{
    internal partial struct Turn
    {
        internal DeckTileArray DeckTiles;
        internal DrawPileDeckTileIDArray DrawPileDeckTileIDs;
        internal QueueDeckTileIDArray QueueDeckTileIDs;

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

        internal void CreateDrawPileFromDeck()
        {
            // clear
            for (int idx = 0; idx < DrawPileDeckTileIDs.DrawPileCount; idx++)
            {
                DrawPileDeckTileIDs.Array[idx] = -1;
            }
            DrawPileDeckTileIDs.DrawPileCount = 0;

            // add all deck tiles
            for (int idx = 0; idx < DeckTiles.DeckTileCount; idx++)
            {
                DrawPileDeckTileIDs.Array[DrawPileDeckTileIDs.DrawPileCount] = DeckTiles.Array[idx].DeckTileID;
                DrawPileDeckTileIDs.DrawPileCount++;
            }
        }

        internal void ShuffleDrawPile()
        {
            for (int i = DrawPileDeckTileIDs.DrawPileCount - 1; i > 0; i--)
            {
                int j = RNG.RNG.Get(RNG.RNG.ID.Draw, i + 1); // 0 ≤ j ≤ i
                (DrawPileDeckTileIDs.Array[i], DrawPileDeckTileIDs.Array[j]) = (DrawPileDeckTileIDs.Array[j], DrawPileDeckTileIDs.Array[i]); // swap
            }
        }

        internal void CreateQueueFromDrawPile()
        {
            for (int idx = 0; idx < DeckTile.QUEUE_SIZE; idx++)
            {
                if (DrawPileDeckTileIDs.DrawPileCount > 0)
                {
                    QueueDeckTileIDs.Array[idx] = DrawPileDeckTileIDs.Array[DrawPileDeckTileIDs.DrawPileCount - 1];
                    DrawPileDeckTileIDs.DrawPileCount--;
                }
                else
                {
                    // to do treat deck to small case
                    Debug.LogError("[Data.Game]: not enough tiles in draw pile to fill queue");
                }
            }
        }

        internal int GetNextDeckTileIDFromQueue()
        {
            int firstInQueue = QueueDeckTileIDs.Array[0];

            for (int idx = 1; idx < DeckTile.QUEUE_SIZE; idx++)
            {
                QueueDeckTileIDs.Array[idx - 1] = QueueDeckTileIDs.Array[idx];
            }

            if (DrawPileDeckTileIDs.DrawPileCount > 0)
            {
                QueueDeckTileIDs.Array[DeckTile.QUEUE_SIZE - 1] = DrawPileDeckTileIDs.Array[DrawPileDeckTileIDs.DrawPileCount - 1];
                DrawPileDeckTileIDs.DrawPileCount--;
            }
            else
            {
                // to do treat deck to small case
                Debug.LogError("[Data.Game]: not enough tiles in draw pile to re-fill queue");
            }

            return firstInQueue;
        }
    }
}
