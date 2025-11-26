using System.Collections.Generic;

namespace Logic
{
    public class Play
    {
        // -----------------------------------------------------------------------------------------
        //public static void CalculateGoalProgress(Data.Player player)
        //{
        //    // todo
        //}

        // -----------------------------------------------------------------------------------------
        public static void RemoveTile(Data.HexPos hexPos)
        {
            Data.Map.RemoveTiles(hexPos);
        }

        // -----------------------------------------------------------------------------------------
        public static bool CheckPlayable(Data.Player player, Data.Tile tile, Data.HexPos coords)
        {
            if (coords == Data.HexPos.Invalid)
                return false;

            Data.Tile oldTile = Data.Map.GetTile(coords);
            for (int idx = 0; idx < tile.Def.Conditions.Count; idx++)
            {
                Def.Var condition = tile.Def.Conditions[idx];
                string conditionID = condition.GetString(0);
                if (conditionID == "On")
                {
                    if (oldTile.Def.Tags.Contains(condition.GetString(1)) == false)
                    {
                        return false;
                    }
                }
                else if (conditionID == "Margin")
                {
                    Data.HexPos center = new Data.HexPos(0, 0);
                    if (center.DistanceTo(coords) != 3)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        // -----------------------------------------------------------------------------------------
        public static void GainBenefits(Data.Player player, List<Data.Benefit> benefits)
        {
            foreach (Data.Benefit benefit in benefits)
            {
                if (benefit.BenefitTiming == Def.Timing.PerTurn)
                {
                    Stockpile.AddResToStockpile(player.Income, benefit.Res);
                }
                else
                {
                    Stockpile.AddResToStockpile(player.Stockpile, benefit.Res);
                }
            }
        }

        // -----------------------------------------------------------------------------------------
        public static void SetTileIOnMap(Data.Player player, Data.Tile tile, Data.HexPos coords)
        {
            player.NextTiles.Remove(tile);
            tile.Status = Data.Tile.State.IN_PLAY;
            Data.Map.AddTile(tile, coords);
        }

        // -----------------------------------------------------------------------------------------
        public static void RefreshPlayerIncome(Data.Player player)
        {
            // TO DO
            //player.Income.Clear();
            //foreach (Data.Tile tile in map.TilesInPlay)
            //{
            //    foreach (Data.Benefit benefit in tile.Benefits)
            //    {
            //        if (benefit.BenefitTiming == Def.Timing.PerTurn)
            //        {
            //            Stockpile.AddResToStockpile(player.Income, benefit.Res);
            //        }
            //    }
            //}
        }
    }
}
