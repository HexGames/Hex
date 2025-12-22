using System;
using System.Collections.Generic;

namespace Hex.Data
{
    public static class MapHelper
    {
        private static Dictionary<int, HexPos> _mapTileIDToHexPos = new Dictionary<int, HexPos>();
        private static Dictionary<HexPos, int> _hexPosToMapTileID= new Dictionary<HexPos, int>();

        static MapHelper()
        {
            int mapTileID = 0; // just the MapTileArray index
            for (int x = -3; x <= 3; x++)
            {
                for (int y = Math.Max(-3, -x - 3); y <= Math.Min(3, -x + 3); y++)
                {
                    var coords = new HexPos(x, y);
                    if (HexPos.Distance(coords, new HexPos(0, 0)) <= 3)
                    {
                        _mapTileIDToHexPos[mapTileID] = coords;
                        _hexPosToMapTileID[coords] = mapTileID;

                        mapTileID++;
                    }
                }
            }
        }

        internal static HexPos MapTileIDToHexPos(int tileID)
        {
            if (_mapTileIDToHexPos.ContainsKey(tileID))
            {
                return _mapTileIDToHexPos[tileID];
            }
            return HexPos.INVALID;
        }

        internal static int HexPosToMapTileID(HexPos hexPos)
        {
            if (_hexPosToMapTileID.ContainsKey(hexPos))
            {
                return _hexPosToMapTileID[hexPos];
            }
            return -1;
        }

        public static bool IsHexPosOnMap(HexPos coord)
        {
            return HexPos.Distance(coord, new HexPos(0, 0)) <= 3;
        }
    }
}
