using CommandSystem;
using System.Collections.Generic;
using Hex;
using System;
using System.Runtime.InteropServices;

namespace Hex.Logic
{
    public static partial class Actions
    {
        public static bool PlayTile(Data.DeckTileRef deckTileRef, Data.HexPos atHexPos, out Data.MapTileRef mapTile, out ReadOnlySpan<Production> production)
        {
            mapTile = Data.MapTileRef.INVALID;
            ProductionPools.ClearOnPlaceProduction();

            if (CanPlaceTile(deckTileRef, atHexPos) == false)
            {
                production = null;
                return false;
            }

            mapTile = Data.Game.MapTiles.CreateMapTileAtHexPos(deckTileRef, atHexPos);

            // calculate bonusLink for tile
            BonusLinkPool.ClearBonusLinks();
            for (int mapTileIdx = 0; mapTileIdx < Data.MapTile.MAP_SIZE; mapTileIdx++)
            {
                Data.MapTileRef currentMapTile = Data.MapTileRef.FromID(mapTileIdx);
                CalculateBonusLink(currentMapTile);
            }

            // calculate onPlaceBonusTree and production
            for (int idx = 0; idx < mapTile.Value.DefData.Effects.Count; idx++) // for each effect
            {
                ref readonly Def.Effect effect = ref mapTile.Value.DefData.Effects[idx];
                if (effect.GetTiming() == Def.Timing.OnPlace && effect.GetEffectType() == Def.EffectType.Production) // is OnPlace production
                {
                    while (ProductionPools.OnPlaceProductionCount >= ProductionPools.OnPlaceProduction.Length) ProductionPools.GrowOnPlaceProduction(); // grow the pool

                    int currentProductionIdx = ProductionPools.OnPlaceProductionCount;
                    ProductionPools.OnPlaceProductionCount++;

                    ref Production productionItem = ref ProductionPools.OnPlaceProduction[currentProductionIdx];


                    // get tile production with condition
                    Def.ResRef res = effect.GetRes(2);
                    productionItem.Reset(mapTile, res); // clear previous data
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
                                            productionItem.LocalList.Add(new LocalStep(adjacentMapTile, baseValue));
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            productionItem.LocalList.Add(new LocalStep(mapTile, baseValue));
                        }
                    }

                    // calculate bonuses
                    ProductionPools.ClearBonusQueue();
                    ProductionPools.BonusEnqueue(mapTile);
                    int depth = 1;
                    while (ProductionPools.BonusQueueEnd - ProductionPools.BonusQueueStart > 0)
                    {
                        ref Data.MapTileRef currentMapTile = ref ProductionPools.BonusDequeue();

                        //process here
                        for (int linkIdx = 0; linkIdx < BonusLinkPool.BonusLinks[currentMapTile.ID].Links.Count; linkIdx++)
                        {
                            ref readonly BonusGiver bonusGiver = ref BonusLinkPool.BonusLinks[currentMapTile.ID].Links[linkIdx];

                            ref readonly Def.Effect bonusEffect = ref bonusGiver.MapTile.Value.DefData.Effects[bonusGiver.EffectIdx];
                            Def.EffectType effectType = bonusEffect.GetEffectType();
                            Def.TagRef bonusTag = bonusEffect.GetTag(2);
                            int bonusValue = bonusEffect.GetInt(3);
                            int bonusTotalValue = 0;
                            Def.Condition bonusCondition = Def.Condition.None;
                            bool bonusConditionIsTrue = false;
                            Def.TagRef bonusConditionTag = Def.TagRef.INVALID;
                            CheckEffectCondition(mapTile, effect, ref bonusCondition, ref bonusConditionIsTrue, ref bonusConditionTag);

                            if (bonusConditionIsTrue == true)
                            {
                                if (bonusCondition == Def.Condition.PerAdjacent || bonusCondition == Def.Condition.PerAdjacentLevel)
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
                                                if (bonusCondition == Def.Condition.PerAdjacentLevel)
                                                {
                                                    factor = adjacentMapTile.Value.DefData.Level;
                                                }

                                                for (int i = 0; i < factor; i++)
                                                {
                                                    ProductionPools.BonusLocalSteps.Add(new LocalStep(adjacentMapTile, bonusValue));
                                                    bonusTotalValue += bonusValue;
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    ProductionPools.BonusLocalSteps.Add(new LocalStep(bonusGiver.MapTile, bonusValue));
                                    bonusTotalValue += bonusValue;
                                }
                            }

                            if (bonusTotalValue > 0)
                            {
                                productionItem.BonusList.Add(new BonusStep(mapTile, bonusGiver.MapTile, depth, bonusEffect.GetEffectType()));
                                ProductionPools.BonusEnqueue(bonusGiver.MapTile);
                            }
                            ProductionPools.BonusLocalSteps.Clear();
                        }

                        depth++;
                    }
                }
            }

            production = new ReadOnlySpan<Production>(ProductionPools.OnPlaceProduction, 0, ProductionPools.OnPlaceProductionCount);

            return true;
        }

        private static void CheckEffectCondition(Data.MapTileRef mapTile, Def.Effect effect, ref Def.Condition condition, ref bool conditionIsTrue, ref Def.TagRef tag)
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
                conditionIsTrue = true; // no codition
            }
        }

        private static void CalculateBonusLink(Data.MapTileRef mapTile)
        {
            for (int mapTileIdx = 0; mapTileIdx < Data.MapTile.MAP_SIZE; mapTileIdx++)
            {
                Data.MapTileRef otherMapTile = Data.MapTileRef.FromID(mapTileIdx);
                if (otherMapTile != mapTile)
                {
                    for (int idx = 0; idx < otherMapTile.Value.DefData.Effects.Count; idx++) // for each effect
                    {
                        ref readonly Def.Effect effect = ref otherMapTile.Value.DefData.Effects[idx];
                        Def.EffectType effectType = effect.GetEffectType();
                        bool adjacentBonus = effectType == Def.EffectType.MultiplyAdjacent
                            || effectType == Def.EffectType.AddAdjacent
                            || effectType == Def.EffectType.ReactivateAdjacent;
                        // no other cases for the moment
                        if (effect.GetTiming() == Def.Timing.Always && adjacentBonus && effect.IsTag(2)) // that is Always bonus
                        {
                            bool isBonusForTheMapTile = false;
                            Def.TagRef tag = effect.GetTag(2);
                            if (mapTile.Value.DefData.BuildingTags.HasTag(tag) == true) // matches tag
                            {
                                if (adjacentBonus && otherMapTile.HexPos.DistanceTo(mapTile.HexPos) == 1)
                                {
                                    isBonusForTheMapTile = true;
                                }
                                // no other cases for the moment
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

        public static bool CanPlaceTile(Data.DeckTileRef deckTile, Data.HexPos atHexPos)
        {
            if (atHexPos.DistanceTo(Data.HexPos.CENTER) > 3)
            {
                return false;
            }

            ref readonly Data.DeckTile                  tileData        = ref deckTile.Value;
            ref readonly Def.TileData.ConditionArray    tileConditions  = ref tileData.DefData.Conditions;
            ref readonly Def.TileData                   mapTileDefData  = ref Data.Game.MapTiles[atHexPos].DefData;

            for (int idx = 0; idx < tileConditions.Count; idx++)
            {
                Def.Condition condition = tileConditions[idx].GetCondition(0);
                if (condition == Def.Condition.IfTag)
                {
                    bool hasTag = false;
                    for (int tagIdx = 1; tagIdx < tileConditions[idx].GetCount(); tagIdx++)
                    {
                        if (tileConditions[idx].IsTag(tagIdx)
                            && mapTileDefData.BuildingTags.HasTag(tileConditions[idx].GetTag(tagIdx)) == true)
                        {
                            hasTag = true;
                            break;
                        }
                    }
                    if (hasTag == false)
                    {
                        return false;
                    }
                }
                else if (condition == Def.Condition.IfTerrain)
                {
                    bool hasTag = false;
                    for (int tagIdx = 1; tagIdx < tileConditions[idx].GetCount(); tagIdx++)
                    {
                        if (tileConditions[idx].IsTag(tagIdx)
                            && mapTileDefData.TerrainTags.HasTag(tileConditions[idx].GetTag(tagIdx)) == true)
                        {
                            hasTag = true;
                            break;
                        }
                    }
                    if (hasTag == false)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
