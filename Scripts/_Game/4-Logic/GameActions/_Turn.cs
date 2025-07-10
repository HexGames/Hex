using System.Collections.Generic;

namespace Logic
{
    public partial class Game
    {
        private static class Turn
        {
            // -----------------------------------------------------------------------------------------
            public static void GainAllBenefits(Data.Player player, Data.Map map)
            {
                foreach (Data.Tile tile in map.TilesInPlay)
                {
                    Stockpile.AddBenefitsToStockpile(player.Stockpile, tile.Benefits);
                }
            }

            // -----------------------------------------------------------------------------------------
            //public static void RefreshDeckCards(Data.Decks decks, Data.Player player)
            //{
            //    // todo
            //}

            //private static bool CheckPlayableAnywhere(Data.Player player, Data.Map map, Data.Tile tile)
            //{
            //    // todo
            //    return true;
            //}

            // -----------------------------------------------------------------------------------------
            public static void RefreshDraftTiles(Data.Decks decks, Data.Player player)
            {
                player.DraftTiles.Clear();
                for (int n = 0; n < player.DraftTilesCount; n++)
                {
                    Def.Tile tileDef = decks.GetRandomTile();
                    player.DraftTiles.Add(new Data.Tile(tileDef, Data.Tile.State.IN_DRAFT));
                }
            }
        }
    }
}
