using CommandSystem;

namespace PlayInternal
{
    public class PlayTileCommandResult : ICommandResult
    {
        public bool Success;
        public PlayTileCommandResult(bool success)
        {
            Success = success;
        }
    }

    public class PlayTileCommandInfo : ICommandInfo
    {
        public HexData.Tile Tile;
        public HexData.HexPos AtHexPos;

        public PlayTileCommandInfo(HexData.Tile tile, HexData.HexPos atHexPos)
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

            bool success = Logic.Actions.PlayTile(placeTileInfo.Tile, placeTileInfo.AtHexPos);

            PlayTileCommandResult result = new PlayTileCommandResult(success);

            return result;
        }
    }
}
