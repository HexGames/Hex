using CommandSystem;
using System.Collections.Generic;
using Hex;

namespace Hex.Logic
{
    public static partial class Actions
    {
        public static bool PlayTile(Data.Tile tile, Data.HexPos atHexPos, out List<TileToTile> onPlaceBonusTree, out Data.Res production)
        {
            // transfer terrain tags - before setTileAtHexPos
            tile._defData.TerrainTags = Data.Game.MapTiles[atHexPos]._defData.TerrainTags;

            Data.Game.MapTiles.SetTileAtHexPos(tile, atHexPos);

            // calculate onPlaceBonusTree and production

            onPlaceBonusTree = null;
            production = default;
            return false;
        }
    }
}
