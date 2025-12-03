using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Hex.Data
{
    public static class DrawPile
    {
        private static List<int> _tileIDs = null;
        public static ReadOnlyCollection<int> TileIDs => _tileIDs.AsReadOnly();

        public static void InitFromAllDeckTiles()
        {
            _tileIDs = GameData.MakeListWithDeckTileIDs();
        }
    }
}
