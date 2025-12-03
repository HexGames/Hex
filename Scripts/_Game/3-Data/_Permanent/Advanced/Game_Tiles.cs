using System.Collections.Generic;

namespace Hex.Data
{
    public static partial class Game
    {
        public const int MAX_DECK = 100;
        public const int MAX_MAP = 37; // map size is 1 + 6 * (1 + 2 + 3) = 37 tiles

        private static Tile[] _tiles = null;
        private static int _deckTileCount = 0;
        private static int _mapTileCount = 0;
        public static TileList Tiles = new TileList(_tiles);

        public static void InitDeckTiles(List<Def.Tile> tileDefs)
        {
            _tiles = new Tile[MAX_DECK + MAX_MAP];

            if (tileDefs.Count > MAX_DECK)
            {
                Debug.LogError("[Data.Game]: count exceeds MAX_DECK");
                return;
            }

            for (int idx = 0; idx < int.Min(tileDefs.Count, MAX_DECK); idx++)
            {
                _tiles[idx] = new Tile(tileDefs[idx]);
                _tiles[idx].IsDeckTile = true;
                _tiles[idx].State = TileState.InMeta;
            }
            _deckTileCount = tileDefs.Count;
        }

        public static void InitMapTiles(List<Def.Tile> tileDefs)
        {
            if (_tiles == null)
            {
                Debug.LogError("[Data.Game]: deck tiles must be initialized first");
                return;
            }

            if (tileDefs.Count > MAX_MAP)
            {
                Debug.LogError("[Data.Game]: count exceeds MAX_MAP");
                return;
            }

            for (int idx = MAX_DECK; idx < tileDefs.Count; idx++)
            {
                _tiles[idx] = new Tile(tileDefs[idx]);
                _tiles[idx].IsDeckTile = false; // just to be explicit
                _tiles[idx].State = TileState.OnMap; // also set in Map to make sure
            }
            _mapTileCount = tileDefs.Count;
        }

        public static void AddDeckTile(Def.Tile tileDef)
        {
            _tiles[_deckTileCount] = new Tile(tileDef);
            _tiles[_deckTileCount].IsDeckTile = true;
            _tiles[_deckTileCount].State = TileState.InMeta;
            _deckTileCount++;
        }

        public static ref Tile GetTile(int id)
        {
            return ref _tiles[id];
        }

        public static List<int> MakeListWithDeckTileIDs()
        {
            List<int> tileIDs = new List<int>();
            for (int idx = 0; idx < _deckTileCount; idx++)
            {
                tileIDs.Add(idx);
            }
            return tileIDs;
        }

        public static List<int> MakeListWithMapTileIDs()
        {
            List<int> tileIDs = new List<int>();
            for (int idx = MAX_DECK; idx < _mapTileCount; idx++)
            {
                tileIDs.Add(idx);
            }
            return tileIDs;
        }

        // -------------------------------------------------------------------------------------------------------------- struct for tiles access
        public readonly struct TileList
        {
            private readonly Tile[] _tiles = null;
            public TileList(Tile[] tiles) => _tiles = tiles;
            public ref Tile this[int id] => ref _tiles[id];
        }
    }
}
