using Hex;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Hex.Data
{
    public static partial class Map
    {
        private static List<int> _tileIDs = null;
        private static readonly Dictionary<HexPos, int> _hexPosToTileID = new();
        private static readonly Dictionary<int, HexPos> _tileIDToHexPos = new();

        public static ReadOnlyCollection<int> TileIDs => _tileIDs.AsReadOnly();

        public static void GenerateMap()
        {
            GameData.InitMapTiles(GenerateMapTiles());
        }

        public static void Init()
        {
            _tileIDs = GameData.MakeListWithMapTileIDs();
            int tileIdx = 0;

            for (int x = -3; x <= 3; x++)
            {
                for (int y = Math.Max(-3, -x - 3); y <= Math.Min(3, -x + 3); y++)
                {
                    var coords = new HexPos(x, y);
                    if (HexPos.Distance(coords, new HexPos(0, 0)) <= 3)
                    {
                        _hexPosToTileID[coords] = _tileIDs[tileIdx];
                        _tileIDToHexPos[_tileIDs[tileIdx]] = coords;
                        //Game.Tiles[_tileIDs[tileIdx]].HexPos = coords; // maybe replace dictionary with this?

                        tileIdx++;
                    }
                }
            }
        }
        
        public static bool IsHexPosOnMap(HexPos coord)
        {
            return HexPos.Distance(coord, new HexPos(0, 0)) <= 3;
        }


        public static int GetTileID(HexPos coord)
        {
            if (_hexPosToTileID.TryGetValue(coord, out int tileIdx) == true) return tileIdx;
            
            return -1;
        }


        private static HexPos GetHexPos(int tileID)
        {
            if (_tileIDToHexPos.TryGetValue(tileID, out HexPos hexPos) == true) return hexPos;

            return HexPos.Invalid;
        }


        // --------------------------------------------------------------------------------------------------------------
        public static void ReplaceTile(int tileID, HexPos atHexPos)
        {
            _hexPosToTileID[atHexPos] = tileID;
            _tileIDToHexPos[tileID] = atHexPos;
        }
    }
}