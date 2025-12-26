using System.Collections.Generic;

namespace Hex.Logic
{
    public static partial class Map
    {
        public static List<Def.TileRef> GenerateMapTiles()
        {
            List<Def.TileRef> tiles = new List<Def.TileRef>();

            tiles.Add(Def.Lib.GetTileRef("Water"));
            tiles.Add(Def.Lib.GetTileRef("Water"));
            tiles.Add(Def.Lib.GetTileRef("Water"));
            tiles.Add(Def.Lib.GetTileRef("Water"));

            tiles.Add(Def.Lib.GetTileRef("Water"));
            tiles.Add(Def.Lib.GetTileRef("Grass"));
            tiles.Add(Def.Lib.GetTileRef("Forest"));
            tiles.Add(Def.Lib.GetTileRef("Grass"));
            tiles.Add(Def.Lib.GetTileRef("Water"));

            tiles.Add(Def.Lib.GetTileRef("Water"));
            tiles.Add(Def.Lib.GetTileRef("Grass"));
            tiles.Add(Def.Lib.GetTileRef("Grass"));
            tiles.Add(Def.Lib.GetTileRef("Grass"));
            tiles.Add(Def.Lib.GetTileRef("Grass"));
            tiles.Add(Def.Lib.GetTileRef("Grass"));

            tiles.Add(Def.Lib.GetTileRef("Water"));
            tiles.Add(Def.Lib.GetTileRef("Grass"));
            tiles.Add(Def.Lib.GetTileRef("Mountain"));
            tiles.Add(Def.Lib.GetTileRef("Mountain"));
            tiles.Add(Def.Lib.GetTileRef("Forest"));
            tiles.Add(Def.Lib.GetTileRef("Grass"));
            tiles.Add(Def.Lib.GetTileRef("Grass"));

            tiles.Add(Def.Lib.GetTileRef("Forest"));
            tiles.Add(Def.Lib.GetTileRef("Grass"));
            tiles.Add(Def.Lib.GetTileRef("Mountain"));
            tiles.Add(Def.Lib.GetTileRef("Forest"));
            tiles.Add(Def.Lib.GetTileRef("Grass"));
            tiles.Add(Def.Lib.GetTileRef("Grass"));

            tiles.Add(Def.Lib.GetTileRef("Grass"));
            tiles.Add(Def.Lib.GetTileRef("Forest"));
            tiles.Add(Def.Lib.GetTileRef("Grass"));
            tiles.Add(Def.Lib.GetTileRef("Forest"));
            tiles.Add(Def.Lib.GetTileRef("Grass"));

            tiles.Add(Def.Lib.GetTileRef("Grass"));
            tiles.Add(Def.Lib.GetTileRef("Grass"));
            tiles.Add(Def.Lib.GetTileRef("Forest"));
            tiles.Add(Def.Lib.GetTileRef("Grass"));

            return tiles;
        }
    }
}
