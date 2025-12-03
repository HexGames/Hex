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
        public static void RemoveTile(Hex.Data.HexPos hexPos)
        {
            Hex.Data.Map.RemoveTiles(hexPos);
        }

        // -----------------------------------------------------------------------------------------
        public static bool CheckPlayable(Hex.Data.Player player, Hex.Data.Tile tile, Hex.Data.HexPos coords)
        {
            if (coords == Hex.Data.HexPos.Invalid)
                return false;

            Hex.Data.Tile oldTile = Hex.Data.Map.GetTile(coords);
            for (int idx = 0; idx < tile.Def.Conditions.Count; idx++)
            {
                Hex.Def.Var condition = tile.Def.Conditions[idx];
                string conditionID = condition.GetString(0);
                if (conditionID == "On")
                {
                    if (oldTile.Def.BuildingTags.Contains(condition.GetString(1)) == false)
                    {
                        return false;
                    }
                }
                else if (conditionID == "Margin")
                {
                    Hex.Data.HexPos center = new Hex.Data.HexPos(0, 0);
                    if (center.DistanceTo(coords) != 3)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        // -----------------------------------------------------------------------------------------
        public static void GainBenefits(Hex.Data.Player player, List<Hex.Data.Benefit> benefits)
        {
            foreach (Hex.Data.Benefit benefit in benefits)
            {
                if (benefit.BenefitTiming == Hex.Def.Timing.PerTurn)
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
        public static void SetTileIOnMap(Hex.Data.Player player, Hex.Data.Tile tile, Hex.Data.HexPos coords)
        {
            player.NextTiles.Remove(tile);
            tile.Status = Hex.Data.Tile.State.IN_PLAY;
            Hex.Data.Map.AddTile(tile, coords);
        }

        // -----------------------------------------------------------------------------------------
        public static void RefreshPlayerIncome(Hex.Data.Player player)
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
