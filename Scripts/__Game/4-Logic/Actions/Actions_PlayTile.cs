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

            // calculate bonusLink for all tiles
            ProductionCalculator.CalculateAllBonusLinks();

            // calculate OnPlace production for the placed tile
            ProductionCalculator.CalculateTileProduction(
                mapTile,
                Def.Timing.OnPlace,
                ProductionPools.OnPlaceProduction,
                ref ProductionPools.OnPlaceProductionCount,
                ProductionPools.GrowOnPlaceProduction);

            // calculate bonuses
            ProductionCalculator.CalculateBonuses(
                mapTile,
                ProductionPools.OnPlaceProduction,
                ProductionPools.OnPlaceProductionCount);

            // calculate final total and target values
            ProductionCalculator.CalculateFinalValues(
                ProductionPools.OnPlaceProduction,
                ProductionPools.OnPlaceProductionCount);

            production = new ReadOnlySpan<Production>(ProductionPools.OnPlaceProduction, 0, ProductionPools.OnPlaceProductionCount);

            return true;
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
