using CommandSystem;

namespace Hex.Play
{
    public class GetTileFromQueueCommandResult : ICommandResult
    {
        public readonly Data.DeckTileRef DeckTile;

        public GetTileFromQueueCommandResult(Data.DeckTileRef deckTile)
        {
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
            var info = Commands.GetInfo<GetTileFromQueueCommandInfo>(commandInfo);
            if (info == null) return null;

            Logic.Actions.GetTileFromQueue(out Data.DeckTileRef deckTile);

            Play.GetTileFromQueueCommandResult result = new Play.GetTileFromQueueCommandResult(deckTile);

            return result;
        }
    }
}
