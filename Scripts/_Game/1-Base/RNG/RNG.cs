
using System;

namespace RNG
{
    public static class RNG
    {
        internal static Random TileRNG = null;
        private static int TileSeed = 1;

        internal static void Init()
        {
            TileRNG = new Random(TileSeed);
        }

        public static int Tile(int maxTilesIdx)
        {
            return TileRNG.Next(0, maxTilesIdx);
        }
    }

    public static class RNGInit
    {
        public static void Init()
        {
            RNG.Init();
        }
    }
}
