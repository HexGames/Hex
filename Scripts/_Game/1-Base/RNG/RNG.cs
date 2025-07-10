
using System;

namespace RNG
{
    public static class RNG
    {
        private static int TileSeed = 1;
        private static Random TileRNG = null;

        public static void xInit()
        {
            TileRNG = new Random(TileSeed);
        }

        public static int Tile(int maxTilesIdx)
        {
            return TileRNG.Next(0, maxTilesIdx);
        }
    }
}
