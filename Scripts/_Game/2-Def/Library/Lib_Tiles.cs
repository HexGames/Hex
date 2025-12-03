using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Hex.Def
{
    public static partial class Lib
    {
        private static Save.Block _tileRawData = null;
        private static List<Save.Block> _tilesRaw = new List<Save.Block>();
        private static List<Tile> _tiles = new List<Tile>();
        public static TileData[] _tileData = null;
        public static ReadOnlyCollection<Tile> Tiles => _tiles.AsReadOnly();
        public static ReadOnlyCollection<TileData> TileData => _tileData.AsReadOnly();

        private static void InitTileDefs()
        {
            _tiles.Clear();
            for (int idx = 0; idx < _tilesRaw.Count; idx++)
            {
                Tile tile = new Tile(_tilesRaw[idx]);
                tile.ID = idx;
                _tiles.Add(tile);
            }
        }

        private static void InitTileData()
        {
            _tileData = new TileData[_tiles.Count];
            for (int idx = 0; idx < _tiles.Count; idx++)
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

        public static Tile GetTile(string ID)
        {
            foreach (Tile tile in _tiles)
            {
                if (tile.Name == ID)
                {
                    return tile;
                }
            }
            return null;
        }

        public static Tile GetTile(int ID)
        {
            return _tiles[ID];
        }

        public static TileData GetTileData(int ID)
        {
            return _tileData[ID];
        }

        public static Tile GetTile(ReadOnlySpan<char> id)
        {
            foreach (Tile tile in _tiles)
            {
                if (id.SequenceEqual(tile.Name.AsSpan()) == true)
                {
                    return tile;
                }
            }
            return null;
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