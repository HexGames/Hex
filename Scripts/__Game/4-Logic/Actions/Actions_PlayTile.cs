using CommandSystem;
using System.Collections.Generic;
using Hex;
using System;
using System.Runtime.InteropServices;

namespace Hex.Logic
{
    public static partial class Actions
    {
        public static bool PlayTile(Data.DeckTileRef deckTile, Data.HexPos atHexPos, out Data.MapTileRef mapTile, out ReadOnlySpan<Production> production)
        {
            mapTile = Data.MapTileRef.INVALID;
            foreach (Production income in Production.OnPlaceProduction) income.Clear();
            Production.OnPlaceProductionCount = 0;

            if (CanPlaceTile(deckTile, atHexPos) == false)
            {
                production = CollectionsMarshal.AsSpan(Production.OnPlaceProduction).Slice(0, Production.OnPlaceProductionCount);
                return false;
            }

            mapTile = Data.Game.MapTiles.CreateMapTileAtHexPos(deckTile, atHexPos);

            // calculate onPlaceBonusTree and production
            if (Production.OnPlaceProduction.Count <= 1) Production.OnPlaceProduction.Add(new Production()); // hack
            Production.OnPlaceProduction[0].Total = new Data.Res(Def.Lib.GetResRef("Population"), 7); // hack
            Production.OnPlaceProductionCount++; // hack

            production = CollectionsMarshal.AsSpan(Production.OnPlaceProduction).Slice(0, Production.OnPlaceProductionCount);
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
