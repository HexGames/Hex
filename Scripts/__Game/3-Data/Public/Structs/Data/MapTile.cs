using System.Collections.Generic;

namespace Hex.Data
{
    public struct MapTile
    {
        internal const int MAP_SIZE = 37; // map size is 1 + 6 * (1 + 2 + 3) = 37 tiles

        private readonly int _defID = -1;

        public readonly Def.Tile Def { get => Hex.Def.Lib.GetTile(_defID); }
        public readonly DeckTileRef FromDeckTile; // the index of the DeckTile it came from
        public Def.TileData DefData;

        internal MapTile(Def.Tile def)
        {
            _defID = def.ID;
            DefData = Hex.Def.Lib.GetTileData(_defID);
        }

        internal MapTile(DeckTileRef deckTile)
        {
            ref DeckTile deckTileValue = ref deckTile.Value;
            _defID = deckTileValue.Def.ID;
            DefData = deckTileValue.DefData;
            FromDeckTile = deckTile;
        }

        public bool IsValid()
        {
            return _defID != -1;
        }
    }
}

