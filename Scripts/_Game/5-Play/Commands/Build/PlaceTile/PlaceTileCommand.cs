using CommandSystem;
using System.Collections.Generic;
using Hex;

namespace Hex.PlayInternal
{
    public class PlayTileCommandResult : ICommandResult
    {
        public readonly List<TileToTile> OnPlaceBonusTree = new List<TileToTile>();
        public readonly Data.Res Production;
        public readonly bool Success = false;

        public PlayTileCommandResult(List<TileToTile> onPlaceBonusTree, Data.Res production)
        {
            Success = true;
            OnPlaceBonusTree = onPlaceBonusTree;
            Production = production;
        }
    }

    public class PlayTileCommandInfo : ICommandInfo
    {
        public Data.Tile Tile;
        public Data.HexPos AtHexPos;

        public PlayTileCommandInfo(Data.Tile tile, Data.HexPos atHexPos)
        {
            Tile = tile;
            AtHexPos = atHexPos;
        }
    }

    public class PlayTileCommand
    {
        public static ICommandResult HandleCommand(ICommandInfo commandInfo)
        {
            var placeTileInfo = commandInfo as PlayTileCommandInfo;
            if (placeTileInfo == null)
                return null;

            bool success = Logic.Actions.PlayTile(placeTileInfo.Tile, placeTileInfo.AtHexPos, out List<TileToTile> onPlaceBonusTree, out Data.Res production);

            if (success == false)
                return default;

            PlayTileCommandResult result = new PlayTileCommandResult(onPlaceBonusTree, production);

            return result;
        }
    }
}
