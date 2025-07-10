using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Def
{
    public static partial class Lib
    {
        private static Save.Block _TileRawData = null;
        private static List<Save.Block> _TilesRaw = new List<Save.Block>();
        private static List<Tile> _Tiles = new List<Tile>();
        public static ReadOnlyCollection<Tile> Tiles => _Tiles.AsReadOnly();

        private static void InitTileDefs()
        {
            _Tiles.Clear();
            for (int idx = 0; idx < _TilesRaw.Count; idx++)
            {
                Tile tile = new Tile(_TilesRaw[idx]);
                tile.IDX = idx;
                _Tiles.Add(tile);
            }
        }

        private static Save.Block GetRawTile(string ID)
        {
            foreach (Save.Block tileData in _TilesRaw)
            {
                if (tileData.ValueS == ID)
                {
                    return tileData;
                }
            }
            return null;
        }

        public static Tile GetTile(string ID)
        {
            foreach (Tile tile in _Tiles)
            {
                if (tile.ID == ID)
                {
                    return tile;
                }
            }
            return null;
        }

        private static void SaveTilesDef()
        {
            Save.Data.SaveToFile(_TileRawData, "Defs/Tiles.mod");
        }

        private static void LoadTilesDef()
        {
            _TileRawData = Save.Data.LoadCSV("Defs/Tiles.table");

            _TilesRaw.Clear();
            _TilesRaw = _TileRawData.GetSubs("Tile");
        }
    }
}