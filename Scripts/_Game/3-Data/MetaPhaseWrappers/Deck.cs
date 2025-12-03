using Hex;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Hex.Data
{
    public static partial class Deck
    {
        private static List<int> _tileIDs = null;
        public static ReadOnlyCollection<int> TileIDs => _tileIDs.AsReadOnly();

        public static void GenerateMap()
        {
            GameData.InitMapTiles(GenerateDeckTiles());
        }
    }
}
