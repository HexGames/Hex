using System;
using System.Collections.Generic;

namespace Logic
{
    public partial class Game
    {
        private static class Start
        {
            // -----------------------------------------------------------------------------------------
            public static List<Def.Tile> GenerateMap()
            {
                List<Def.Tile> tiles = new List<Def.Tile>();

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
            
            // -----------------------------------------------------------------------------------------
            public static void AddStartingRes(Data.Player player)
            {
                foreach (Def.Res resDef in Def.Lib.Res)
                { 
                    Stockpile.AddResToStockpile(player.Stockpile, new Data.Res(resDef, resDef.Default));
                }
            }

            // -----------------------------------------------------------------------------------------
            public static void AddStartingDeckTiles(Data.Decks decks)
            {
                foreach (Def.Tile tileDef in Def.Lib.Tiles)
                {
                    for (int n = 0; n < tileDef.Data_Starting; n++)
                    {
                        decks.AddTile(tileDef);
                    }
                }
            }
        }
    }
}
