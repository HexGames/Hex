using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Hex.Data
{
    public static partial class Map
    {
        private static List<Def.Tile> GenerateMapTiles()
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
