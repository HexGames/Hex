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

                return true;
            }

            // -----------------------------------------------------------------------------------------
            public static void CalculateOnPlayBenefits(Game game, Data.Tile tile, Data.HexCoord coords)
            {
                tile.Benefits.Clear();

                if (coords != Data.HexCoord.Invalid)
                {
                    foreach (Data.Effect effect in tile.Effects)
                    {
                        if (effect.EffectTiming == Def.Timing.OnPlaceSelf) // <--------------------------------- OnPlaceSelf
                        {
                            tile.Benefits.AddRange(Effects.Execute(game, coords, effect));
                        }
                        else if (effect.EffectTiming == Def.Timing.PerTurn) // <-------------------------------- PerTurn - self
                        {
                            tile.Benefits.AddRange(Effects.Execute(game, coords, effect));
                        }
                    }

                    Data.Tile oldTile = game.Map.GetTile(coords);
                    if (oldTile != null)
                    {
                        foreach (Data.Effect effect in oldTile.Effects)
                        {
                            if (effect.EffectTiming == Def.Timing.OnDestroySelf) // <--------------------------- OnDestroySelf
                            {
                                tile.Benefits.AddRange(Effects.Execute(game, coords, effect));
                            }
                        }
                    }

                    foreach (Data.Tile otherTile in game.Map.TilesInPlay)
                    {
                        Data.HexCoord otherTileCoord = game.Map.GetCoord(otherTile);
                        if (otherTileCoord != coords)
                        {
                            foreach (Data.Effect effect in otherTile.Effects)
                            {
                                if (effect.EffectTiming == Def.Timing.OnPlaceOther) // <----------------------- OnPlaceOther
                                {
                                    tile.Benefits.AddRange(Effects.Execute(game, otherTileCoord, effect, tile));
                                }
                                else if (oldTile != null && effect.EffectTiming == Def.Timing.OnDestroyOther) // <------------------ OnDestroyOther
                                {
                                    tile.Benefits.AddRange(Effects.Execute(game, otherTileCoord, effect, oldTile));
                                }
                                else if (effect.EffectTiming == Def.Timing.PerTurn) // <-------------------------------- PerTurn - other
                                {
                                    List<Data.Benefit> newBenefits = Effects.Execute(game, otherTileCoord, effect);
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
            public static void MoveTileToInPlay(Data.Player player, Data.Map map, Data.Tile tile, Data.HexCoord coords)
            {
                player.DraftTiles.Remove(tile);
                tile.Status = Data.Tile.State.IN_PLAY;

                for (int idx = 0; idx < tile.Effects.Count; idx++)
                {
                    Data.Effect effect = tile.Effects[idx];
                    if (effect.EffectTiming == Def.Timing.OnPlaceSelf)
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
                    foreach (Data.Effect effect in tile.Effects)
                    {
                        if (effect.EffectTiming == Def.Timing.PerTurn)
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
