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
            var values = (ID[])Enum.GetValues(typeof(ID));
            _randoms = new Random[values.Length];

            foreach (var id in values)
            {
                _randoms[(int)id] = new Random(HashCode.Combine(12345, id));
            }
        }

        public static int Get(ID id, int max)
        {
            return _randoms[(int)id].Next(0, max);
        }
    }
}
