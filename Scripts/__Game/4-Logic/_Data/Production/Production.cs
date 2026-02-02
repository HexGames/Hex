using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System;

namespace Hex.Logic
{
    public struct Production
    {
        public Data.MapTileRef Tile;
        public Def.ResRef ResDef;
        public int LocalValue;
        public int TotalValue;
        public BonusStepListRef BonusList;
        public LocalStepListRef LocalList;

        public Production()
        {
            Tile = Data.MapTileRef.INVALID;
            ResDef = Def.ResRef.INVALID;
            LocalValue = 0;
            TotalValue = 0;
            BonusList = new BonusStepListRef();
            LocalList = new LocalStepListRef();
        }

        public void Reset(Data.MapTileRef tile, Def.ResRef resDef)
        {
            Tile = tile;
            ResDef = resDef;
            LocalValue = 0;
            TotalValue = 0;
            BonusList.Clear();
            LocalList.Clear();
        }

        public void Clear()
        {
            Tile = Data.MapTileRef.INVALID;
            ResDef = Def.ResRef.INVALID;
            LocalValue = 0;
            TotalValue = 0;
            BonusList.Clear();
            LocalList.Clear();
        }
    }

    /// <summary>
    /// Static methods for calculating production from tiles.
    /// </summary>
    public static class ProductionCalculator
    {
        /// <summary>
        /// Calculates production for a single tile based on the specified timing (OnPlace or PerTurn).
        /// </summary>
        public static void CalculateTileProduction(
            Data.MapTileRef mapTile,
            Def.Timing timing,
            Production[] productionArray,
            ref int productionCount,
            Action growProductionArray)
        {
            int totalValue = 0;

            for (int idx = 0; idx < mapTile.Value.DefData.Effects.Count; idx++)
            {
                ref readonly Def.Effect effect = ref mapTile.Value.DefData.Effects[idx];
                if (effect.GetTiming() == timing && effect.GetEffectType() == Def.EffectType.Production)
                {
                    int currentProductionIdx = -1;
                    int lastProductionIdx = productionCount - 1;
                    if (lastProductionIdx >= 0)
                    {
                        ref Production lastProduction = ref productionArray[lastProductionIdx];
                        if (lastProduction.ResDef == effect.GetRes(2) && lastProduction.Tile == mapTile)
                        {
                            currentProductionIdx = lastProductionIdx;
                        }
                    }
                    if (currentProductionIdx < 0)
                    {
                        // new production
                        totalValue = 0;
                        while (productionCount >= productionArray.Length) growProductionArray();
                        currentProductionIdx = productionCount;
                        productionCount++;
                    }

                    ref Production productionItem = ref productionArray[currentProductionIdx];

                    // get tile production with condition
                    Def.ResRef res = effect.GetRes(2);
                    if (currentProductionIdx != lastProductionIdx) productionItem.Reset(mapTile, res);
                    int baseValue = effect.GetInt(3);
                    Def.Condition condition = Def.Condition.None;
                    bool conditionIsTrue = false;
                    Def.TagRef tag = Def.TagRef.INVALID;
                    CheckEffectCondition(mapTile, effect, ref condition, ref conditionIsTrue, ref tag);

                    if (conditionIsTrue == true)
                    {
                        if (condition == Def.Condition.PerAdjacent || condition == Def.Condition.PerAdjacentLevel)
                        {
                            for (int dirIdx = 0; dirIdx < Data.HexPos.Directions.Length; dirIdx++)
                            {
                                Data.HexPos adjacentHexCoord = mapTile.HexPos + Data.HexPos.Directions[dirIdx];
                                if (Data.MapHelper.IsHexPosOnMap(adjacentHexCoord) == true)
                                {
                                    Data.MapTileRef adjacentMapTile = Data.MapTileRef.FromID(Data.MapHelper.HexPosToMapTileID(adjacentHexCoord));
                                    if (adjacentMapTile.Value.IsValid() == true
                                        && adjacentMapTile.Value.DefData.TerrainTags.HasTag(tag) == true)
                                    {
                                        int factor = 1;
                                        if (condition == Def.Condition.PerAdjacentLevel)
                                        {
                                            factor = adjacentMapTile.Value.DefData.Level;
                                        }

                                        for (int i = 0; i < factor; i++)
                                        {
                                            totalValue += baseValue;
                                            productionItem.LocalList.Add(new LocalStep(adjacentMapTile, totalValue));
                                            productionItem.LocalValue = totalValue;
                                            productionItem.TotalValue = totalValue;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            totalValue += baseValue;
                            productionItem.LocalList.Add(new LocalStep(mapTile, totalValue));
                            productionItem.LocalValue = totalValue;
                            productionItem.TotalValue = totalValue;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Calculates bonuses for the production items starting from the specified index.
        /// </summary>
        public static void CalculateBonuses(
            Data.MapTileRef mapTile,
            Production[] productionArray,
            int productionCount,
            int startProductionIdx = 0)
        {
            for (int productionIdx = startProductionIdx; productionIdx < productionCount; productionIdx++)
            {
                ref Production productionItem = ref productionArray[productionIdx];

                ProductionPools.ClearBonusQueue();
                ProductionPools.BonusEnqueue(mapTile);
                int depth = 1;
                while (ProductionPools.BonusQueueEnd - ProductionPools.BonusQueueStart > 0)
                {
                    ref Data.MapTileRef currentMapTile = ref ProductionPools.BonusDequeue();

                    for (int linkIdx = 0; linkIdx < BonusLinkPool.BonusLinks[currentMapTile.ID].Links.Count; linkIdx++)
                    {
                        ref readonly BonusGiver bonusGiver = ref BonusLinkPool.BonusLinks[currentMapTile.ID].Links[linkIdx];

                        ref readonly Def.Effect bonusEffect = ref bonusGiver.MapTile.Value.DefData.Effects[bonusGiver.EffectIdx];
                        Def.EffectType effectType = bonusEffect.GetEffectType();
                        Def.TagRef bonusTag = bonusEffect.GetTag(2);
                        int bonusLocalValue = 0;
                        int bonusValue = bonusEffect.GetInt(3);
                        Def.Condition bonusCondition = Def.Condition.None;
                        bool bonusConditionIsTrue = false;
                        Def.TagRef bonusConditionTag = Def.TagRef.INVALID;
                        CheckEffectCondition(currentMapTile, bonusEffect, ref bonusCondition, ref bonusConditionIsTrue, ref bonusConditionTag);

                        if (bonusConditionIsTrue == true)
                        {
                            if (bonusCondition == Def.Condition.PerAdjacent || bonusCondition == Def.Condition.PerAdjacentLevel)
                            {
                                for (int dirIdx = 0; dirIdx < Data.HexPos.Directions.Length; dirIdx++)
                                {
                                    Data.HexPos adjacentHexCoord = currentMapTile.HexPos + Data.HexPos.Directions[dirIdx];
                                    if (Data.MapHelper.IsHexPosOnMap(adjacentHexCoord) == true)
                                    {
                                        Data.MapTileRef adjacentMapTile = Data.MapTileRef.FromID(Data.MapHelper.HexPosToMapTileID(adjacentHexCoord));
                                        if (adjacentMapTile.Value.IsValid() == true
                                            && adjacentMapTile.Value.DefData.TerrainTags.HasTag(bonusConditionTag) == true)
                                        {
                                            int factor = 1;
                                            if (bonusCondition == Def.Condition.PerAdjacentLevel)
                                            {
                                                factor = adjacentMapTile.Value.DefData.Level;
                                            }

                                            for (int i = 0; i < factor; i++)
                                            {
                                                bonusLocalValue += bonusValue;
                                                ProductionPools.BonusLocalSteps.Add(new LocalStep(adjacentMapTile, bonusLocalValue));
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                bonusLocalValue += bonusValue;
                                ProductionPools.BonusLocalSteps.Add(new LocalStep(bonusGiver.MapTile, bonusLocalValue));
                            }
                        }

                        if (bonusLocalValue > 0)
                        {
                            productionItem.BonusList.Add(new BonusStep(currentMapTile, bonusGiver.MapTile, depth, bonusEffect.GetEffectType()));
                            ref BonusStep bonusStep = ref productionItem.BonusList[productionItem.BonusList.Count - 1];
                            for (int localStepIdx = 0; localStepIdx < ProductionPools.BonusLocalSteps.Count; localStepIdx++)
                            {
                                bonusStep.LocalList.Add(ProductionPools.BonusLocalSteps[localStepIdx]);
                            }
                            bonusStep.LocalValue = bonusLocalValue;
                            bonusStep.TotalValue = bonusLocalValue;
                            ProductionPools.BonusEnqueue(bonusGiver.MapTile);
                        }
                        ProductionPools.BonusLocalSteps.Clear();
                    }

                    depth++;
                }
            }
        }

        /// <summary>
        /// Calculates final total and target values after bonuses have been applied.
        /// </summary>
        public static void CalculateFinalValues(
            Production[] productionArray,
            int productionCount,
            int startProductionIdx = 0)
        {
            for (int productionIdx = startProductionIdx; productionIdx < productionCount; productionIdx++)
            {
                ref Production productionItem = ref productionArray[productionIdx];
                for (int bonusIdx = productionItem.BonusList.Count - 1; bonusIdx >= 0; bonusIdx--)
                {
                    ref BonusStep bonusStep = ref productionItem.BonusList[bonusIdx];
                    if (bonusStep.TargetTile == productionItem.Tile)
                    {
                        if (bonusStep.EffectType == Def.EffectType.AddAdjacent)
                        {
                            productionItem.TotalValue += bonusStep.TotalValue;
                            bonusStep.TargetValue = productionItem.TotalValue;
                        }
                        else if (bonusStep.EffectType == Def.EffectType.MultiplyAdjacent)
                        {
                            productionItem.TotalValue += (bonusStep.TotalValue - 1) * productionItem.LocalValue;
                            bonusStep.TargetValue = productionItem.TotalValue;
                        }
                        else if (bonusStep.EffectType == Def.EffectType.ReactivateAdjacent)
                        {
                            productionItem.TotalValue *= (1 + bonusStep.TotalValue);
                            bonusStep.TargetValue = productionItem.TotalValue;
                        }
                    }
                    else
                    {
                        ref BonusStep targetBonusStep = ref GetTargetBonusStep(ref productionItem, ref bonusStep);
                        if (bonusStep.EffectType == Def.EffectType.AddAdjacent)
                        {
                            targetBonusStep.TotalValue += bonusStep.TotalValue;
                            bonusStep.TargetValue = targetBonusStep.TotalValue;
                        }
                        else if (bonusStep.EffectType == Def.EffectType.MultiplyAdjacent)
                        {
                            targetBonusStep.TotalValue += (bonusStep.TotalValue - 1) * targetBonusStep.LocalValue;
                            bonusStep.TargetValue = targetBonusStep.TotalValue;
                        }
                        else if (bonusStep.EffectType == Def.EffectType.ReactivateAdjacent)
                        {
                            targetBonusStep.TotalValue *= (1 + bonusStep.TotalValue);
                            bonusStep.TargetValue = targetBonusStep.TotalValue;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Calculates bonus links for all tiles on the map.
        /// </summary>
        public static void CalculateAllBonusLinks()
        {
            BonusLinkPool.ClearBonusLinks();
            for (int mapTileIdx = 0; mapTileIdx < Data.MapTile.MAP_SIZE; mapTileIdx++)
            {
                Data.MapTileRef currentMapTile = Data.MapTileRef.FromID(mapTileIdx);
                CalculateBonusLink(currentMapTile);
            }
        }

        /// <summary>
        /// Calculates bonus links for a single tile.
        /// </summary>
        public static void CalculateBonusLink(Data.MapTileRef mapTile)
        {
            for (int mapTileIdx = 0; mapTileIdx < Data.MapTile.MAP_SIZE; mapTileIdx++)
            {
                Data.MapTileRef otherMapTile = Data.MapTileRef.FromID(mapTileIdx);
                if (otherMapTile != mapTile)
                {
                    for (int idx = 0; idx < otherMapTile.Value.DefData.Effects.Count; idx++)
                    {
                        ref readonly Def.Effect effect = ref otherMapTile.Value.DefData.Effects[idx];
                        Def.EffectType effectType = effect.GetEffectType();
                        bool adjacentBonus = effectType == Def.EffectType.MultiplyAdjacent
                            || effectType == Def.EffectType.AddAdjacent
                            || effectType == Def.EffectType.ReactivateAdjacent;
                        if (effect.GetTiming() == Def.Timing.Always && adjacentBonus && effect.IsTag(2))
                        {
                            bool isBonusForTheMapTile = false;
                            Def.TagRef tag = effect.GetTag(2);
                            if (mapTile.Value.DefData.BuildingTags.HasTag(tag) == true)
                            {
                                if (adjacentBonus && otherMapTile.HexPos.DistanceTo(mapTile.HexPos) == 1)
                                {
                                    isBonusForTheMapTile = true;
                                }
                            }
                            if (isBonusForTheMapTile == true)
                            {
                                BonusLinkPool.BonusLinks[mapTile.ID].Links.Add(new BonusGiver(in otherMapTile, idx));
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Checks if an effect's condition is satisfied.
        /// </summary>
        public static void CheckEffectCondition(Data.MapTileRef mapTile, Def.Effect effect, ref Def.Condition condition, ref bool conditionIsTrue, ref Def.TagRef tag)
        {
            if (effect.GetCount() == 6)
            {
                condition = effect.GetCondition(4);
                tag = effect.GetTag(5);
                if (condition == Def.Condition.IfTerrain)
                {
                    conditionIsTrue = mapTile.Value.DefData.TerrainTags.HasTag(tag);
                }
                else if (condition == Def.Condition.IfTag)
                {
                    conditionIsTrue = mapTile.Value.DefData.BuildingTags.HasTag(tag);
                }
                else if (condition == Def.Condition.IfAdjacent)
                {
                    for (int dirIdx = 0; dirIdx < Data.HexPos.Directions.Length; dirIdx++)
                    {
                        Data.HexPos adjacentHexCoord = mapTile.HexPos + Data.HexPos.Directions[dirIdx];
                        if (Data.MapHelper.IsHexPosOnMap(adjacentHexCoord) == true)
                        {
                            Data.MapTileRef adjacentMapTile = Data.MapTileRef.FromID(Data.MapHelper.HexPosToMapTileID(adjacentHexCoord));
                            if (adjacentMapTile.Value.IsValid() == true
                                && adjacentMapTile.Value.DefData.TerrainTags.HasTag(tag) == true)
                            {
                                conditionIsTrue = true;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    conditionIsTrue = true; // per adjacent conditions
                }
            }
            else
            {
                conditionIsTrue = true; // no condition
            }
        }

        private static ref BonusStep GetTargetBonusStep(ref Production production, ref BonusStep bonusStep)
        {
            for (int idx = 0; idx < production.BonusList.Count; idx++)
            {
                ref BonusStep targetBonusStep = ref production.BonusList[idx];
                if (bonusStep.TargetTile == targetBonusStep.SourceTile)
                {
                    return ref targetBonusStep;
                }
            }
            return ref bonusStep;
        }
    }
}
