using System.Collections.Generic;

namespace Hex.Data
{
    public struct MapTile
    {
        internal const int MAX_MAP = 37; // map size is 1 + 6 * (1 + 2 + 3) = 37 tiles

        private readonly int _defID = -1;

        public Def.Tile Def { get => Hex.Def.Lib.GetTile(_defID); }
        public Def.TileData DefData;
        public int FromDeckTileID = -1; // the index of the DeckTile it came from
        public int MapTileID = -1; // the intex from MapTileArray

        internal MapTile(Def.Tile def, int id)
        {
            _defID = def.ID;
            DefData = Hex.Def.Lib.GetTileData(_defID);
            MapTileID = id;
        }

        internal MapTile(DeckTile deckTile, int id)
        {
            _defID = deckTile.Def.ID;
            DefData = deckTile.DefData;
            FromDeckTileID = deckTile.DeckTileID;
            MapTileID = id;
        }

        public bool IsValid()
        {
            return _defID != -1;
        }
    }
}

