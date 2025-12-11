using CommandSystem;
using System.Collections.Generic;
using Hex;

namespace Hex.Logic
{
    public static partial class Actions
    {
        public static bool PlayTile(Data.DeckTile deckTile, Data.HexPos atHexPos, out List<TileToTile> onPlaceBonusTree, out Data.Res production)
        {
            // transfer terrain tags - before setTileAtHexPos

            ref Data.MapTile mapTile = ref Data.Game.MapTiles.CreateMapTileAtHexPos(deckTile, atHexPos);

            // calculate onPlaceBonusTree and production

            onPlaceBonusTree = null;
            production = default;
            return false;
        }
    }
}
