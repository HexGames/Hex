using System.Collections.Generic;

namespace Hex.Logic
{
    public static partial class Map
    {
        public static List<Def.Tile> GenerateMapTiles()
        {
            List<Def.Tile> tiles = new List<Hex.Def.Tile>();

            tiles.Add(Def.Lib.GetTile("Water"));
            tiles.Add(Def.Lib.GetTile("Water"));
            tiles.Add(Def.Lib.GetTile("Water"));
            tiles.Add(Def.Lib.GetTile("Water"));

            tiles.Add(Def.Lib.GetTile("Water"));
            tiles.Add(Def.Lib.GetTile("Grass"));
            tiles.Add(Def.Lib.GetTile("Forest"));
            tiles.Add(Def.Lib.GetTile("Grass"));
            tiles.Add(Def.Lib.GetTile("Water"));

            tiles.Add(Def.Lib.GetTile("Water"));
            tiles.Add(Def.Lib.GetTile("Grass"));
            tiles.Add(Def.Lib.GetTile("Grass"));
            tiles.Add(Def.Lib.GetTile("Grass"));
            tiles.Add(Def.Lib.GetTile("Grass"));
            tiles.Add(Def.Lib.GetTile("Grass"));

            tiles.Add(Def.Lib.GetTile("Water"));
            tiles.Add(Def.Lib.GetTile("Grass"));
            tiles.Add(Def.Lib.GetTile("Mountain"));
            tiles.Add(Def.Lib.GetTile("Mountain"));
            tiles.Add(Def.Lib.GetTile("Forest"));
            tiles.Add(Def.Lib.GetTile("Grass"));
            tiles.Add(Def.Lib.GetTile("Grass"));

            tiles.Add(Def.Lib.GetTile("Forest"));
            tiles.Add(Def.Lib.GetTile("Grass"));
            tiles.Add(Def.Lib.GetTile("Mountain"));
            tiles.Add(Def.Lib.GetTile("Forest"));
            tiles.Add(Def.Lib.GetTile("Grass"));
            tiles.Add(Def.Lib.GetTile("Grass"));

            tiles.Add(Def.Lib.GetTile("Grass"));
            tiles.Add(Def.Lib.GetTile("Forest"));
            tiles.Add(Def.Lib.GetTile("Grass"));
            tiles.Add(Def.Lib.GetTile("Forest"));
            tiles.Add(Def.Lib.GetTile("Grass"));

            tiles.Add(Def.Lib.GetTile("Grass"));
            tiles.Add(Def.Lib.GetTile("Grass"));
            tiles.Add(Def.Lib.GetTile("Forest"));
            tiles.Add(Def.Lib.GetTile("Grass"));

            return tiles;
        }
    }
}
