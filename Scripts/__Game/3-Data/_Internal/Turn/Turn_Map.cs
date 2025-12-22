using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Hex.Data
{
    internal partial struct Turn
    {
        internal MapTileArray MapTiles;

        internal void InitMapTiles(List<Def.TileRef> tileDefs) // map initial tiles
        {
            if (tileDefs.Count > MapTile.MAP_SIZE)
            {
                Debug.LogError("[Data.Game]: count exceeds MAX_MAP");
                return;
            }

            for (int idx = 0; idx < tileDefs.Count; idx++)
            {
                int id = idx;
                MapTiles.Array[id] = new MapTile(tileDefs[idx].Value);
            }
        }
    }
}
