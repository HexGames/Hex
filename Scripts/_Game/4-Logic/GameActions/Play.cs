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
        public static void RemoveTile(Data.Map map, Data.HexCoords hexCoord)
        {
            map.RemoveTiles(hexCoord);
        }

        // -----------------------------------------------------------------------------------------
        public static bool CheckPlayable(Data.Player player, Data.Map map, Data.Tile tile, Data.HexCoords coords)
        {
            if (coords == Data.HexCoords.Invalid)
                return false;

            Data.Tile oldTile = map.GetTile(coords);
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
                    Data.HexCoords center = new Data.HexCoords(0, 0);
                    if (center.DistanceTo(coords) != 3)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        // -----------------------------------------------------------------------------------------
        private static Data.Tile _overwriteTile = null;
        private static Data.HexCoords _overwriteCoords = Data.HexCoords.Invalid;
        public static void ReapplyAllEffectsWithOverwriteTile(Data.Map map, Data.Tile overwriteTile, Data.HexCoords overwriteCoords)
        {
            _overwriteTile = overwriteTile;
            _overwriteCoords = overwriteCoords;
            ReapplyAllEffects(map);
            _overwriteTile = null;
            _overwriteCoords = Data.HexCoords.Invalid;
        }

        public static void ReapplyAllEffects(Data.Map map)
        {
            // fitst clear all
            for (int tileIdx = 0; tileIdx < map.TilesInPlay.Count; tileIdx++)
            {
                Data.Tile tile = map.TilesInPlay[tileIdx];
                Data.HexCoords coords = map.GetCoords(tile);
                if (_overwriteTile != null && coords == _overwriteCoords)
                {
                    tile = _overwriteTile;
                }

                foreach (Data.EffectNode effectNode in tile.GetEffectsList(_overwriteTile != null))
                {
                    effectNode.Clear();
                }
            }

            // then reapply
            for (int tileIdx = 0; tileIdx < map.TilesInPlay.Count; tileIdx++)
            {
                Data.Tile tile = map.TilesInPlay[tileIdx];
                Data.HexCoords coords = map.GetCoords(tile);
                if (_overwriteTile != null && coords == _overwriteCoords)
                {
                    tile = _overwriteTile;
                }

                foreach (Data.EffectNode effectNode in tile.GetEffectsList(_overwriteTile != null))
                {
                    Def.Var effectDef = effectNode.EffectDef;
                    int baseValue = 0;

                    if (effectNode.ResDef == null)
                    {
                        // verify condition
                        string keyword = effectDef.GetString(1);
                        if (keyword == "AddAdjacent")
                        {
                            ApplyAdjacentEffectNode(map, effectNode, coords, effectDef, Data.EffectNode.BonusType.Add);
                        }
                        else if (keyword == "MultiplyAdjacent")
                        {
                            ApplyAdjacentEffectNode(map, effectNode, coords, effectDef, Data.EffectNode.BonusType.Multiply);
                        }
                        else if (keyword == "ReactivateAdjacent")
                        {
                            ApplyAdjacentEffectNode(map, effectNode, coords, effectDef, Data.EffectNode.BonusType.Reactivate);
                        }
                    }

                    for (int valueGroupIdx = 2; valueGroupIdx < effectDef.GetCount(); valueGroupIdx++)
                    {
                        if (effectDef.GetSubCount(valueGroupIdx) == 1)
                        {
                            baseValue += effectDef.GetInt(valueGroupIdx);
                        }
                    }
                    effectNode.SetBaseValue(baseValue);
                    for (int valueGroupIdx = 2; valueGroupIdx < effectDef.GetCount(); valueGroupIdx++)
                    {
                        if (effectDef.GetSubCount(valueGroupIdx) == 3)
                        {
                            // verify condition
                            string codition = effectDef.GetString(valueGroupIdx, 1);
                            if (codition == "IfAdjacent")
                            {
                                ApplyAdjacentEffectTile(map, effectNode, coords, effectDef.GetString(valueGroupIdx, 2), effectDef.GetInt(valueGroupIdx, 0), true, false, Data.EffectNode.BonusType.Add);
                            }
                            else if (effectDef.GetString(valueGroupIdx, 1) == "PerAdjacent")
                            {
                                ApplyAdjacentEffectTile(map, effectNode, coords, effectDef.GetString(valueGroupIdx, 2), effectDef.GetInt(valueGroupIdx, 0), false, false, Data.EffectNode.BonusType.Add);
                            }
                            else if (effectDef.GetString(valueGroupIdx, 1) == "PerAdjacentLevel")
                            {
                                ApplyAdjacentEffectTile(map, effectNode, coords, effectDef.GetString(valueGroupIdx, 2), effectDef.GetInt(valueGroupIdx, 0), false, true, Data.EffectNode.BonusType.Add);
                            }
                        }
                    }
                }
            }
        }

        private static void ApplyAdjacentEffectTile(Data.Map map, Data.EffectNode effectNode, Data.HexCoords coords, string targetTag, int value, bool justOnce, bool perLevel, Data.EffectNode.BonusType bonusType)
        {
            foreach (Data.HexCoords direction in Data.HexCoords.Directions)
            {
                Data.HexCoords adjCoords = coords + direction;
                Data.Tile adjTile = map.GetTile(coords + direction); 
                if (_overwriteTile != null && adjCoords == _overwriteCoords)
                {
                    adjTile = _overwriteTile;
                }
                if (adjTile != null && adjTile.Def.Tags.Contains(targetTag))
                {
                    int multiplier = 1;
                    if (perLevel) multiplier = adjTile.Def.Level;
                    effectNode.AddTileBonus(adjTile, multiplier * value, bonusType);
                    if (justOnce == true)
                        return;
                }
            }
        }

        private static void ApplyAdjacentEffectNode(Data.Map map, Data.EffectNode effectNode, Data.HexCoords coords, Def.Var effectDef, Data.EffectNode.BonusType bonusType)
        {
            foreach (Data.HexCoords direction in Data.HexCoords.Directions)
            {
                Data.HexCoords adjCoords = coords + direction;
                Data.Tile adjTile = map.GetTile(coords + direction);
                if (_overwriteTile != null && adjCoords == _overwriteCoords)
                {
                    adjTile = _overwriteTile;
                }
                string targetTag = effectDef.GetString(1, 1);
                if (adjTile != null && adjTile.Def.Tags.Contains(targetTag))
                {
                    foreach (Data.EffectNode adjEffectNode in adjTile.GetEffectsList(_overwriteTile != null))
                    {
                        adjEffectNode.AddNodeBonus(effectNode, bonusType);
                    }
                }
            }
        }

        // -----------------------------------------------------------------------------------------
        private static List<Data.Benefit> _benefitsTotal = new List<Data.Benefit>();
        private static List<Data.Benefit> _benefitsAtCoords = new List<Data.Benefit>();
        private static List<Data.Benefit> _benefitsTotalWithOverwrite = new List<Data.Benefit>();
        private static List<Data.Benefit> _benefitsAtCoordsWithOverwrite = new List<Data.Benefit>();
        //private static List<Data.Benefit> _benefitsSteps = new List<Data.Benefit>(); // TO DO ?

        public static void CalculateAllBenefitsWithOverwriteTile(Data.Map map, Data.Tile overwriteTile, Data.HexCoords overwriteCoords, out List<Data.Benefit> benefitsTotal, out List<Data.Benefit> benefitsAtCoords)
        {
            _overwriteTile = overwriteTile;
            _overwriteCoords = overwriteCoords;
            benefitsTotal = _benefitsTotalWithOverwrite;
            benefitsAtCoords = _benefitsAtCoordsWithOverwrite;
            AddAllBenefits(map, benefitsTotal, benefitsAtCoords);
            _overwriteTile = null;
            _overwriteCoords = Data.HexCoords.Invalid;
        }

        public static void CalculateAllBenefits(Data.Map map, out List<Data.Benefit> benefitsTotal, out List<Data.Benefit> benefitsAtCoords)
        {
            benefitsTotal = _benefitsTotal;
            benefitsAtCoords = _benefitsAtCoords;
            AddAllBenefits(map, benefitsTotal, benefitsAtCoords);
        }

        private static void AddAllBenefits(Data.Map map, List<Data.Benefit> benefitsTotal, List<Data.Benefit> benefitsAtCoords)
        {
            benefitsTotal.Clear();
            benefitsAtCoords.Clear();

            for (int tileIdx = 0; tileIdx < map.TilesInPlay.Count; tileIdx++)
            {
                Data.Tile tile = map.TilesInPlay[tileIdx];
                Data.HexCoords coords = map.GetCoords(tile);
                if (_overwriteTile != null && coords == _overwriteCoords)
                {
                    tile = _overwriteTile;
                }

                foreach (Data.EffectNode effectNode in tile.GetEffectsList(_overwriteTile != null))
                {
                    Def.Timing benefitTiming = effectNode.Timing;
                    if (benefitTiming != Def.Timing.OnPlace)
                        continue;
                    Def.Res def = effectNode.ResDef;
                    Data.HexCoords hexCoords = coords;
                    Data.EffectNode.Values values = effectNode.GetValues();
                    int totalValue = values.value * values.multiply * ( 1 + values.reactivate);

                    AddBenefitToList(benefitsAtCoords, def, totalValue, hexCoords, benefitTiming);
                    AddBenefitToList(benefitsTotal, def, totalValue, Data.HexCoords.Invalid, benefitTiming);
                }
            }
        }

        public static void AddBenefitToList(List<Data.Benefit> benefits, Def.Res resDef, int value, Data.HexCoords coords, Def.Timing timing)
        {
            for (int idx = 0; idx < benefits.Count; idx++)
            {
                Data.Benefit benefit = benefits[idx];
                if (benefit.Res.Def == resDef && benefit.BenefitTiming == timing && benefit.HexCoords == coords)
                {
                    benefit.Res.Value += value;
                    benefits[idx] = benefit;
                    return;
                }
            }
            Data.Benefit newBenefit = new Data.Benefit(resDef, value, coords, timing);
            benefits.Add(newBenefit);
        }

        // -----------------------------------------------------------------------------------------
        public static void RemoveBenefitsRange(List<Data.Benefit> originalBenefits, List<Data.Benefit> toRemoveBenefits)
        {
            foreach (Data.Benefit toRemove in toRemoveBenefits)
            {
                for (int idx = 0; idx < originalBenefits.Count; idx++)
                {
                    Data.Benefit original = originalBenefits[idx];
                    if (original.Res.Def == toRemove.Res.Def && original.BenefitTiming == toRemove.BenefitTiming && original.HexCoords == toRemove.HexCoords)
                    {
                        original.Res.Value -= toRemove.Res.Value;
                        if (original.Res.Value == 0)
                        {
                            originalBenefits.RemoveAt(idx);
                        }
                        else
                        {
                            originalBenefits[idx] = original;
                        }
                        break;
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
        public static void SetTileIOnMap(Data.Player player, Data.Map map, Data.Tile tile, Data.HexCoords coords)
        {
            player.NextTiles.Remove(tile);
            tile.Status = Data.Tile.State.IN_PLAY;
            map.AddTile(tile, coords);
        }

        // -----------------------------------------------------------------------------------------
        public static void RefreshPlayerIncome(Data.Player player, Data.Map map)
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
