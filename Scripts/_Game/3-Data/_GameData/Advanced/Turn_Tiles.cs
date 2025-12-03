using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Hex.Data
{
    internal partial struct Turn
    {
        internal TileArray Tiles; // an inline array of MAX_DECK + MAX_MAP

        internal void InitDeckTiles(List<Def.Tile> tileDefs)
        {
            if (tileDefs.Count > Tile.MAX_DECK)
            {
                Debug.LogError("[Data.Game]: count exceeds MAX_DECK");
                return;
            }

            for (int idx = 0; idx < int.Min(tileDefs.Count, Tile.MAX_DECK); idx++)
            {
                Tiles.Array[idx] = new Tile(tileDefs[idx]);
                Tiles.Array[idx].IsDeckTile = true;
                Tiles.Array[idx].State = TileState.InMeta;
            }
            Tiles.DeckTileCount = tileDefs.Count;
        }

        internal void InitMapTiles(List<Def.Tile> tileDefs)
        {
            if (tileDefs.Count > Tile.MAX_MAP)
            {
                Debug.LogError("[Data.Game]: count exceeds MAX_MAP");
                return;
            }

            for (int idx = Tile.MAX_DECK; idx < tileDefs.Count; idx++)
            {
                Tiles.Array[idx] = new Tile(tileDefs[idx]);
                Tiles.Array[idx].IsDeckTile = false; // just to be explicit
                Tiles.Array[idx].State = TileState.OnMap; // also set in Map to make sure
            }
            Tiles.MapTileCount = tileDefs.Count;
        }

        public void AddDeckTile(Def.Tile tileDef)
        {
            Tiles.Array[Tiles.DeckTileCount] = new Tile(tileDef);
            Tiles.Array[Tiles.DeckTileCount].IsDeckTile = true;
            Tiles.Array[Tiles.DeckTileCount].State = TileState.InMeta;
            Tiles.DeckTileCount++;
        }

        public List<int> MakeListWithDeckTileIDs()
        {
            List<int> tileIDs = new List<int>();
            for (int idx = 0; idx < Tiles.DeckTileCount; idx++)
            {
                tileIDs.Add(idx);
            }
            return tileIDs;
        }

        public List<int> MakeListWithMapTileIDs()
        {
            List<int> tileIDs = new List<int>();
            for (int idx = Tile.MAX_DECK; idx < Tiles.MapTileCount; idx++)
            {
                tileIDs.Add(idx);
            }
            return tileIDs;
        }

        // ---------------------------------------------------------------------------------- TileArray
        internal struct TileArray
        {
            [InlineArray(Tile.MAX_DECK + Tile.MAX_MAP)] public struct TileArrayInlineArray { private Tile _element0; }
            internal TileArrayInlineArray Array;
            internal int DeckTileCount = 0;
            internal int MapTileCount = 0;

            public TileArray()
            {
                //_array = default; // is this necessary?
            }
        }
    }
}
