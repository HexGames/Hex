using System;
using System.Collections.Generic;

namespace Hex.RNG
{
    public static class RNG
    {
        public enum ID
        {
            Draw,
        }

        private static readonly Random[] _randoms;

        static RNG() // init
        {
            ID[] values = (ID[])Enum.GetValues(typeof(ID));
            _randoms = new Random[values.Length];

            foreach (var id in values)
            {
                //int seed = HashCode.Combine(12345, id);
                int seed = (int)id * 12345 + 13;
                _randoms[(int)id] = new Random(seed);
            }
        }

        public static int Get(ID id, int max)
        {
            return _randoms[(int)id].Next(0, max);
        }
    }
}
