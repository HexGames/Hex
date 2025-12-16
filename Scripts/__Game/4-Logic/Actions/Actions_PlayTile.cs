using CommandSystem;
using System.Collections.Generic;
using Hex;

namespace Hex.Logic
{
    public static partial class Actions
    {
        public static bool PlayTile(Data.DeckTileRef deckTile, Data.HexPos atHexPos, out Data.MapTileRef mapTile, out List<Data.TileToTile> onPlaceBonusTree, out Data.Res production)
        {
            // transfer terrain tags - before setTileAtHexPos

            mapTile = Data.Game.MapTiles.CreateMapTileAtHexPos(deckTile, atHexPos);

            // calculate onPlaceBonusTree and production

            onPlaceBonusTree = null;
            production = default;
            return false;
        }
    }
}
