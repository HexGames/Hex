using System.Collections.Generic;

namespace Logic
{
    public partial class Game
    {
        private static class Play
        {
            // -----------------------------------------------------------------------------------------
            //public static void CalculateGoalProgress(Data.Player player)
            //{
            //    // todo
            //}

            // -----------------------------------------------------------------------------------------
            public static void RemoveTile(Data.Map map, Data.HexCoord hexCoord)
            {
                map.RemoveTiles(hexCoord);

                // todo add benefits from OnDestroySelf
            }

            // -----------------------------------------------------------------------------------------
            public static bool CheckPlayable(Data.Player player, Data.Map map, Data.Tile tile, Data.HexCoord coords)
            {
                if (coords == Data.HexCoord.Invalid)
                    return false;

                Data.Tile oldTile = map.GetTile(coords);
                for (int idx = 0; idx < tile.Def.Data_Conditions.Count; idx++)
                {
                    Def.Var condition = tile.Def.Data_Conditions[idx];
                    string conditionID = condition.GetString(0);
                    if (conditionID == "On")
                    {
                        if (oldTile.Def.Data_Tags.Contains(condition.GetString(1)) == false)
                        {
                            return false;
                        }
                    }
                    else if (conditionID == "Margin")
                    {
                        Data.HexCoord center = new Data.HexCoord(0, 0);
                        if (center.DistanceTo(coords) != 3)
                        {
                            return false;
                        }
                    }
                }

                return true;
            }

            // -----------------------------------------------------------------------------------------
            public static void CalculateOnPlayBenefits(Game game, Data.Tile tile, Data.HexCoord coords)
            {
                tile.Benefits.Clear();

                if (coords != Data.HexCoord.Invalid)
                {
                    foreach (Def.Var effect in tile.Effects)
                    {
                        Def.Timing timing = Effects.GetTiming(effect);
                        if (timing  == Def.Timing.OnPlace) // <--------------------------------- OnPlaceSelf
                        {
                            tile.Benefits.AddRange(Effects.Execute(game, coords, effect));
                        }
                        else if (timing == Def.Timing.PerTurn) // <-------------------------------- PerTurn - self
                        {
                            tile.Benefits.AddRange(Effects.Execute(game, coords, effect));
                        }
                    }

                    //Data.Tile oldTile = game.Map.GetTile(coords);
                    //if (oldTile != null)
                    //{
                    //    foreach (Data.Effect effect in oldTile.Effects)
                    //    {
                    //        if (effect.EffectTiming == Def.Timing.OnDestroySelf) // <--------------------------- OnDestroySelf
                    //        {
                    //            tile.Benefits.AddRange(Effects.Execute(game, coords, effect));
                    //        }
                    //    }
                    //}

                    foreach (Data.Tile otherTile in game.Map.TilesInPlay)
                    {
                        Data.HexCoord otherTileCoord = game.Map.GetCoord(otherTile);
                        if (otherTileCoord != coords)
                        {
                            foreach (Def.Var effect in otherTile.Effects)
                            {
                                Def.Timing timing = Effects.GetTiming(effect);
                                //if (effect.EffectTiming == Def.Timing.OnPlaceOther) // <----------------------- OnPlaceOther
                                //{
                                //    tile.Benefits.AddRange(Effects.Execute(game, otherTileCoord, effect, tile));
                                //}
                                //else if (oldTile != null && effect.EffectTiming == Def.Timing.OnDestroyOther) // <------------------ OnDestroyOther
                                //{
                                //    tile.Benefits.AddRange(Effects.Execute(game, otherTileCoord, effect, oldTile));
                                //}
                                //else 
                                if (timing == Def.Timing.PerTurn) // <-------------------------------- PerTurn - other
                                {
                                    List<Data.Benefit> newBenefits = Effects.Execute(game, otherTileCoord, effect, null);
                                    foreach (Data.Benefit benefit in newBenefits)
                                    {
                                        bool found = false;
                                        foreach (Data.Benefit existingBenefit in otherTile.Benefits)
                                        {
                                            if (benefit.Res.Def == existingBenefit.Res.Def)
                                            {
                                                if (benefit.Res.Value != existingBenefit.Res.Value)
                                                {
                                                    tile.Benefits.Add(new Data.Benefit(benefit.Res.Def, benefit.Res.Value - existingBenefit.Res.Value, otherTileCoord, Def.Timing.PerTurn));
                                                }
                                                found = true;
                                                break;
                                            }
                                        }
                                        if (found == false)
                                        {
                                            tile.Benefits.Add(new Data.Benefit(benefit.Res.Def, benefit.Res.Value, otherTileCoord, Def.Timing.PerTurn));
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
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
            public static void MoveTileInPlay(Data.Player player, Data.Map map, Data.Tile tile, Data.HexCoord coords)
            {
                player.NextTiles.Remove(tile);
                tile.Status = Data.Tile.State.IN_PLAY;

                for (int idx = 0; idx < tile.Effects.Count; idx++)
                {
                    Def.Var effect = tile.Effects[idx];
                    if (Effects.GetTiming(effect) == Def.Timing.OnPlace)
                    {
                        // remove from tile effects
                        tile.Effects.RemoveAt(idx);
                        idx--;
                    }
                }

                map.AddTile(tile, coords);
            }

            // -----------------------------------------------------------------------------------------
            public static void CalculateAllTilesPerTurnBenefits(Game game)
            {
                foreach (Data.Tile tile in game.Map.TilesInPlay)
                {
                    Data.HexCoord coord = game.Map.GetCoord(tile); 
                    // caluclate just on end turn benefits
                    tile.Benefits.Clear();
                    foreach (Def.Var effect in tile.Effects)
                    {
                        Def.Timing timing = Effects.GetTiming(effect);
                        if (timing == Def.Timing.PerTurn)
                        {
                            tile.Benefits.AddRange( Effects.Execute(game, coord, effect));
                        }
                    }
                }
            }

            // -----------------------------------------------------------------------------------------
            public static void RefreshPlayerIncome(Data.Player player, Data.Map map)
            {
                player.Income.Clear();
                foreach (Data.Tile tile in map.TilesInPlay)
                {
                    foreach (Data.Benefit benefit in tile.Benefits)
                    {
                        if (benefit.BenefitTiming == Def.Timing.PerTurn)
                        {
                            Stockpile.AddResToStockpile(player.Income, benefit.Res);
                        }
                    }
                }
            }
        }
    }
}
