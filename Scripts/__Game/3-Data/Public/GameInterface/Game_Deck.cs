using System;
using System.Collections.Generic;

namespace Hex.Data
{
    public static partial class Game
    {
        public static DeckArrayInterface DeckTiles = new DeckArrayInterface();
    
        public sealed class DeckArrayInterface
        {
            //public ref DeckTile this[int id]
            //{
            //    get
            //    {
            //        return ref GameData.Data.Turns.Array[GameData.Data.CurrentTurn].DeckTiles.Array[id];
            //    }
            //}

            //public Span<DeckTile> span
            //{
            //    get
            //    {
            //        ref DeckTileArray deckTiles = ref GameData.Data.Turns.Array[GameData.Data.CurrentTurn].DeckTiles;
            //        return ((Span<DeckTile>)deckTiles.Array).Slice(0, deckTiles.DeckTileCount);
            //    }
            //}
    
            //public ref DeckTile GetTileFromHistory(int id, int turn)
            //{
            //    return ref GameData.Data.Turns.Array[turn].DeckTiles.Array[id];
            //}
    
            //public Span<DeckTile> GetTileCollectionFromHistory(int id, int turn)
            //{
            //    ref DeckTileArray deckTiles = ref GameData.Data.Turns.Array[turn].DeckTiles;
            //    return ((Span<DeckTile>)deckTiles.Array).Slice(0, deckTiles.DeckTileCount);
            //}
    
            public void InitDeckTiles(List<Def.Tile> tiles)
            {
                if (GameData.Data.CurrentTurn != 0)
                {
                    Debug.LogError("[DeckArrayInterface] InitDeckTiles can only be called at turn 0.");
                    return;
                }

                GameData.Data.Turns.Array[GameData.Data.CurrentTurn].InitDeckTiles(tiles);
            }
    
            public void CreateDrawPile()
            {
                if (GameData.Data.CurrentTurn != 0)
                {
                    Debug.LogError("[DeckArrayInterface] CreateDrawPile can only be called at turn 0.");
                    return;
                }
    
                GameData.Data.Turns.Array[GameData.Data.CurrentTurn].CreateDrawPileFromDeck();
                GameData.Data.Turns.Array[GameData.Data.CurrentTurn].ShuffleDrawPile();
            }
    
            public void CreateQueue()
            {
                if (GameData.Data.CurrentTurn != 0)
                {
                    Debug.LogError("[DeckArrayInterface] CreateQueue can only be called at turn 0.");
                    return;
                }

                GameData.Data.Turns.Array[GameData.Data.CurrentTurn].CreateQueueFromDrawPile();
            }
    
            public DeckTileRef GetNextDeckTileFromQueue()
            {
                return GameData.Data.Turns.Array[GameData.Data.CurrentTurn].GetNextDeckTileFromQueue();
            }
        }
    }
}
