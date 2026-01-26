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
            for (int mapTileIdx = 0; mapTileIdx < Data.MapTile.MAP_SIZE; mapTileIdx++)
            {
                Data.MapTileRef otherMapTile = Data.MapTileRef.FromID(mapTileIdx);
                if (otherMapTile != mapTile)
                {
                    for (int idx = 0; idx < otherMapTile.Value.DefData.Effects.Count; idx++) // for each effect
                    {
                        ref readonly Def.Var effect = ref otherMapTile.Value.DefData.Effects[idx];
                        if (effect.IsTiming(0, 0) == true && effect.IsBonus(1, 0) == true && effect.GetTiming(0, 0) == Def.Timing.Always && effect.IsTag(2, 0)) // that is Always bonus
                        {
                            bool isBonusForTheMapTile = false;
                            if (mapTile.Value.DefData.BuildingTags.HasTag(effect.GetTag(2, 0)) == true) // matches tag
                            {
                                Def.Bonus bonus = effect.GetBonus(1, 0);
                                if (bonus == Def.Bonus.MultiplyAdjacent || bonus == Def.Bonus.AddAdjacent || bonus == Def.Bonus.ReactivateAdjacent)
                                {
                                    if (otherMapTile.HexPos.DistanceTo(mapTile.HexPos) == 1)
                                    {
                                        isBonusForTheMapTile = true;
                                    }
                                }
                                else if (bonus == Def.Bonus.Add)
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

            // calculate onPlaceBonusTree and production
            for (int idx = 0; idx < mapTile.Value.DefData.Effects.Count; idx++) // for each effect
            {
                ref readonly Def.Var effect = ref mapTile.Value.DefData.Effects[idx];
                if (effect.IsTiming(0, 0) == true && effect.IsRes(1, 0) == true && effect.GetTiming(0, 0) == Def.Timing.OnPlace) // is OnPlace production
                {
                    while (ProductionPools.OnPlaceProductionCount >= ProductionPools.OnPlaceProduction.Length) ProductionPools.GrowOnPlaceProduction(); // grow the pool

                    ref Production productionItem = ref ProductionPools.OnPlaceProduction[ProductionPools.OnPlaceProductionCount];
                    ProductionPools.OnPlaceProductionCount++;

                    productionItem.Clear(); // clear previous data
                    int value = 0;

                    // calculate tile production
                    for (int subIdx = 2; subIdx < effect.GetCount(); subIdx++) // for each production value
                    {
                        ProcessEffectValues(mapTile, idx, subIdx, ref value);
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
                            int bonusValue = 0;

                            ref readonly Def.Var bonusEffect = ref bonusGiver.MapTile.Value.DefData.Effects[bonusGiver.EffectIdx];
                            for (int subIdx = 3; subIdx < bonusEffect.GetCount(); subIdx++) // for each bonus effect value
                            {
                                ProcessEffectValues(bonusGiver.MapTile, bonusGiver.EffectIdx, subIdx, ref bonusValue);
                            }

                            ProductionPools.OnPlaceProduction[ProductionPools.OnPlaceProductionCount].BonusList.Add(new BonusStep(mapTile, bonusGiver.MapTile, depth, bonusEffect.GetBonus(1, 0), bonusValue));
                            ProductionPools.BonusEnqueue(bonusGiver.MapTile);
                        }

                        depth++;
                    }

                    productionItem.Total = new Data.Res(effect.GetRes(1, 0), value);
                }
            }

            production = new ReadOnlySpan<Production>(ProductionPools.OnPlaceProduction, 0, ProductionPools.OnPlaceProductionCount);

            return true;
        }

        private static void ProcessEffectValues(in Data.MapTileRef mapTile,int effectIdx, int subIdx, ref int value)
        {
            ref Def.Var effect = ref mapTile.Value.DefData.Effects[effectIdx];
            if (effect.IsInt(subIdx, 0) == true)
            {
                if (effect.GetSubCount(subIdx) == 1)
                {
                    // unconditional production
                    int localValue = effect.GetInt(subIdx, 0);
                    value += localValue;
                    ProductionPools.OnPlaceProduction[ProductionPools.OnPlaceProductionCount].BonusList.Add(new BonusStep(mapTile, Def.Bonus.Add, localValue));
                }
                else if (effect.GetSubCount(subIdx) == 3 && effect.IsCondition(subIdx, 1) == true)
                {
                    if (effect.GetCondition(subIdx, 1) == Def.Condition.IfTerrain && effect.IsTag(subIdx, 2) == true)
                    {
                        if (mapTile.Value.DefData.TerrainTags.HasTag(effect.GetTag(subIdx, 2)) == true)
                        {
                            int localValue = effect.GetInt(subIdx, 0);
                            value += localValue;
                            ProductionPools.OnPlaceProduction[ProductionPools.OnPlaceProductionCount].BonusList.Add(new BonusStep(mapTile, Def.Bonus.Add, localValue));
                        }
                    }
                    else if (effect.GetCondition(subIdx, 1) == Def.Condition.IfTag && effect.IsTag(subIdx, 2) == true)
                    {
                        if (mapTile.Value.DefData.BuildingTags.HasTag(effect.GetTag(subIdx, 2)) == true)
                        {
                            int localValue = effect.GetInt(subIdx, 0);
                            value += localValue;
                            ProductionPools.OnPlaceProduction[ProductionPools.OnPlaceProductionCount].BonusList.Add(new BonusStep(mapTile, Def.Bonus.Add, localValue));
                        }
                    }
                    else if (effect.GetCondition(subIdx, 1) == Def.Condition.IfAdjacent && effect.IsTag(subIdx, 2) == true)
                    {
                        bool found = false;
                        for (int dirIdx = 0; dirIdx < Data.HexPos.Directions.Length; dirIdx++)
                        {
                            ref readonly Data.MapTile adjacentMapTile = ref Data.Game.MapTiles[mapTile.HexPos + Data.HexPos.Directions[dirIdx]];
                            if (adjacentMapTile.IsValid() == true
                                && adjacentMapTile.DefData.TerrainTags.HasTag(effect.GetTag(subIdx, 2)) == true)
                            {
                                found = true;
                                break;
                            }
                        }
                        if (found == true)
                        {
                            int localValue = effect.GetInt(subIdx, 0);
                            value += localValue;
                            ProductionPools.OnPlaceProduction[ProductionPools.OnPlaceProductionCount].BonusList.Add(new BonusStep(mapTile, Def.Bonus.Add, localValue));
                        }
                    }
                    else if (effect.GetCondition(subIdx, 1) == Def.Condition.PerAdjacent && effect.IsTag(subIdx, 2) == true)
                    {
                        for (int dirIdx = 0; dirIdx < Data.HexPos.Directions.Length; dirIdx++)
                        {
                            ref readonly Data.MapTile adjacentMapTile = ref Data.Game.MapTiles[mapTile.HexPos + Data.HexPos.Directions[dirIdx]];
                            if (adjacentMapTile.IsValid() == true
                                && adjacentMapTile.DefData.TerrainTags.HasTag(effect.GetTag(subIdx, 2)) == true)
                            {
                                int localValue = effect.GetInt(subIdx, 0);
                                value += localValue;
                                ProductionPools.OnPlaceProduction[ProductionPools.OnPlaceProductionCount].BonusList.Add(new BonusStep(mapTile, Def.Bonus.Add, localValue));
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
