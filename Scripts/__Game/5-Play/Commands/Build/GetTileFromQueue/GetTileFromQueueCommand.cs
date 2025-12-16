using CommandSystem;
using System.Collections.Generic;
using Hex;

namespace Hex.Play
{
    public class GetTileFromQueueCommandResult : ICommandResult
    {
        public readonly Data.DeckTileRef DeckTile;
        public readonly bool Success = false;

        public GetTileFromQueueCommandResult(Data.DeckTileRef deckTile)
        {
            Success = true;
            DeckTile = deckTile;
        }
    }
}

namespace Hex.PlayInternal
{
    public class GetTileFromQueueCommandInfo : ICommandInfo
    {
        public GetTileFromQueueCommandInfo()
        {
        }
    }

    public class GetTileFromQueueCommand
    {
        public static ICommandResult HandleCommand(ICommandInfo commandInfo)
        {
            var placeTileInfo = commandInfo as GetTileFromQueueCommandInfo;
            if (placeTileInfo == null)
            {
                Debug.LogError("[GetTileFromQueueCommand] HandleCommand: commandInfo is not GetTileFromQueueCommandInfo!");
                return null;
            }

            bool success = Logic.Actions.GetTileFromQueue(out Data.DeckTileRef deckTile);

            if (success == false)
                return default;

            Play.GetTileFromQueueCommandResult result = new Play.GetTileFromQueueCommandResult(deckTile);

            return result;
        }
    }
}
