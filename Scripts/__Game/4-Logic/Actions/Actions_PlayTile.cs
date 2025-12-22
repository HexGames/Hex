using CommandSystem;
using System.Collections.Generic;
using Hex;

namespace Hex.Logic
{
    public static partial class Actions
    {
        public static bool PlayTile(Data.DeckTileRef deckTile, Data.HexPos atHexPos, out Data.MapTileRef mapTile, out List<Data.TileToTile> onPlaceBonusTree, out Data.Res production)
        {
            onPlaceBonusTree = null;
            production = Data.Res.INVALID;
            mapTile = Data.MapTileRef.INVALID;

            if (CanPlaceTile(deckTile, atHexPos) == false)
            {
                return false;
            }

            mapTile = Data.Game.MapTiles.CreateMapTileAtHexPos(deckTile, atHexPos);

            // calculate onPlaceBonusTree and production

            return true;
        }

        public static bool CanPlaceTile(Data.DeckTileRef deckTile, Data.HexPos atHexPos)
        {
            if (atHexPos.DistanceTo(Data.HexPos.CENTER) > 3)
            {
                return false;
            }

            ref Data.DeckTile tileData = ref deckTile.Value;
            for (int idx = 0; idx < tileData.DefData.Conditions.Count; idx++)
            {
                if (tileData.DefData.Conditions[idx].GetCondition(0) == Def.Condition.IfTerrain)
                {
                    bool hasTag = false;
                    for (int tagIdx = 1; tagIdx < tileData.DefData.Conditions[idx].GetCount(); tagIdx++)
                    {
                        if (tileData.DefData.Conditions[idx].IsTag(tagIdx)
                            && Data.Game.MapTiles[atHexPos].DefData.TerrainTags.HasTag(tileData.DefData.Conditions[idx].GetTag(tagIdx)) == true)
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
