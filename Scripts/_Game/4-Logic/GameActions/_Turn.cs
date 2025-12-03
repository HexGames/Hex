using System.Collections.Generic;

namespace Logic
{
    public static class Turn
    {
        // -----------------------------------------------------------------------------------------
        public static void GainAllBenefits(Hex.Data.Player player)
        {
            // TO DO
            //foreach (Data.Tile tile in map.TilesInPlay)
            //{
            //    Stockpile.AddBenefitsToStockpile(player.Stockpile, tile.Benefits);
            //}
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
        //public static void RefreshDraftTiles(Data.Decks decks, Data.Player player)
        //{
        //    player.DraftTiles.Clear();
        //    for (int n = 0; n < player.DraftTilesCount; n++)
        //    {
        //        Def.Tile tileDef = decks.GetRandomTile();
        //        player.DraftTiles.Add(new Data.Tile(tileDef, Data.Tile.State.IN_DRAFT));
        //    }
        //}

        // -----------------------------------------------------------------------------------------
        public static void SetNextTileAsCurrentTile(Hex.Data.Player player)
        {
            player.CurrentTile = player.NextTiles[0];
        }

        // -----------------------------------------------------------------------------------------
        public static void PutNewTilesIntoNextQueue(Hex.Data.TilePool decks, Hex.Data.Player player)
        {
            while (player.NextTiles.Count < player.NextTilesCount)
            {
                Hex.Def.Tile tileDef = decks.GetRandomTile();
                player.NextTiles.Add(new Hex.Data.Tile(tileDef, Hex.Data.Tile.State.IN_NEXT));
            }
        }
    }
}
