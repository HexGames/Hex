using CommandSystem;

namespace Hex
{
    public static class CommandResultHandlers
    {
        public static void Init()
        {
            CommandSys.RegisterResultHandler(Phase.Build, CommandType.GetTileFromQueue, GetTileFromQueueResultHandler);
        }

        public static void GetTileFromQueueResultHandler(ICommandResult result)
        {
            var getTileFromQueueCommandResult = result as Play.GetTileFromQueueCommandResult;
            if (getTileFromQueueCommandResult == null)
            {
                Debug.LogError("[CommandResultHandlers] GetTileFromQueueResultHandler: result is not GetTileFromQueueCommandResult!");
                return;
            }
            Map.CursorTile.InitCursorTile(getTileFromQueueCommandResult.DeckTile);
        }

        public static void PlaceTileResultHandler(ICommandResult result)
        {
            var playTileCommandResult = result as Play.PlayTileCommandResult;
            if (playTileCommandResult == null)
            {
                Debug.LogError("[CommandResultHandlers] GetTileFromQueueResultHandler: result is not GetTileFromQueueCommandResult!");
                return;
            }
            Map.MapTiles.PlaceTile(playTileCommandResult.MapTile);
        }
    }
}
