using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Data
{
    public class Decks
    {
        private List<Def.Tile> _DeckTiles = new List<Def.Tile>();

        public Decks()
        {
        }

        public void AddTile(Def.Tile tileDef)
        {
            _DeckTiles.Add(tileDef);
        }

        public Def.Tile GetRandomTile()
        {
            int tileIdx = RNG.RNG.Tile(_DeckTiles.Count);
            return _DeckTiles[tileIdx];
        }
    }
}
