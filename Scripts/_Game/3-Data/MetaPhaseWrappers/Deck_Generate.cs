
using System.Collections.Generic;

namespace Hex.Data
{
    public static partial class Deck
    {
        private static List<Def.Tile> GenerateDeckTiles()
        {
            List<Def.Tile> tileDefs = new List<Def.Tile>();
            foreach (Def.Tile tileDef in Def.Lib.Tiles)
            {
                for (int n = 0; n < tileDef.Starting; n++)
                {
                    tileDefs.Add(tileDef);
                }
            }
            return tileDefs;
        }
    }
}
