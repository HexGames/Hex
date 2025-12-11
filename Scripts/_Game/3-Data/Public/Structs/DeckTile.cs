using System.Collections.Generic;

namespace Hex.Data
{
    public partial struct DeckTile
    {
        internal const int MAX_DECK = 91; // so that MAX_DECK + MAX_MAP = 128 (MAX_MAP is 37)

        private readonly int _defID = -1;

        public Def.Tile Def { get => Hex.Def.Lib.GetTile(_defID); }
        public Def.TileData DefData;
        public int DeckTileID = -1; // the intex from DeckTileArray
        public TileState State = TileState.InMeta;

        public DeckTile(Def.Tile def, int id)
        {
            _defID = def.ID;
            DefData = Hex.Def.Lib.GetTileData(_defID);
            DeckTileID = id;
        }

        public bool IsValid()
        {
            return _defID != -1;
        }
    }
}

