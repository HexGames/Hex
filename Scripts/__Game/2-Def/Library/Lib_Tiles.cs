using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Hex.Def
{
    public static partial class Lib
    {
        private static Save.Block _tileRawData = null;
        private static List<Save.Block> _tilesRaw = new List<Save.Block>();
        private static Tile[] _tiles = null;
        public static TileData[] _tileData = null;
        public static ReadOnlyCollection<Tile> Tiles => _tiles.AsReadOnly();
        public static ReadOnlyCollection<TileData> TileData => _tileData.AsReadOnly();

        private static void InitTileDefs()
        {
            _tiles = new Tile[_tilesRaw.Count];
            for (int idx = 0; idx < _tilesRaw.Count; idx++)
            {
                _tiles[idx] = new Tile(idx, _tilesRaw[idx]);
            }
        }
        private static void InitTileData()
        {
            _tileData = new TileData[_tiles.Length];
            for (int idx = 0; idx < _tiles.Length; idx++)
            {
                _tileData[idx] = new TileData(_tiles[idx]);
            }
        }

        private static Save.Block GetRawTile(string ID)
        {
            foreach (Save.Block tileData in _tilesRaw)
            {
                if (tileData.ValueS == ID)
                {
                    return tileData;
                }
            }
            return null;
        }

        public static ref Tile GetTile(int ID)
        {
            return ref _tiles[ID];
        }

        public static TileData GetTileData(int ID)
        {
            return _tileData[ID];
        }

        public static TileRef GetTileRef(string name)
        {
            return GetTileRef(name.AsSpan());
        }

        internal static TileRef GetTileRef(ReadOnlySpan<char> name)
        {
            for (int idx = 0; idx < _tiles.Length; idx++)
            {
                if (name.SequenceEqual(_tiles[idx].Name))
                {
                    return TileRef.FromID(idx);
                }
            }
            return TileRef.INVALID;
        }

        private static void SaveTilesDef()
        {
            Save.Data.SaveToFile(_tileRawData, "Defs/Tiles.mod");
        }

        private static void LoadTilesDef()
        {
            _tileRawData = Save.Data.LoadCSV("Defs/Tiles.table");

            _tilesRaw.Clear();
            _tilesRaw = _tileRawData.GetSubs("Tile");
        }
    }
}