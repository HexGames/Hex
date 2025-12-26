using CommandSystem;

namespace Hex
{
    public static class CommandResultHandlers
    {
        public static void Init()
        {
            CommandSys.RegisterResultHandler(Phase.Build, CommandType.GetTileFromQueue, GetTileFromQueueResultHandler);
            CommandSys.RegisterResultHandler(Phase.Build, CommandType.PlaceTile, PlaceTileResultHandler);
            CommandSys.RegisterResultHandler(Phase.Build, CommandType.EndTurn, EndTurnResultHandler);
        }

        public static void GetTileFromQueueResultHandler(ICommandResult commandResult)
        {
            var result = GetResult<Play.GetTileFromQueueCommandResult>(commandResult);
            if (result == null) return;

            Map.CursorTile.InitCursorTile(result.DeckTile);
            Play.Game.LockInput = false;
        }

        public static void PlaceTileResultHandler(ICommandResult commandResult)
        {
            var result = GetResult<Play.PlayTileCommandResult>(commandResult);
            if (result == null) return;

            if (result.Success == false)
            {
                Play.Game.LockInput = false;
                return;
            }

            Map.CursorTile.ClearCursorTile();
            Map.MapTiles.PlaceTile(result.MapTile);

            // play result.OnPlaceBonusTree animations

            Main.DelayedCall(GameLoop.EndTurn, 0.5f);
            // Main.DelayedCall(Play.Game.AutoInputEndTurn, 0.5f);
        }

        public static void EndTurnResultHandler(ICommandResult commandResult)
        {
            var result = GetResult<Play.EndTurnCommandResult>(commandResult);
            if (result == null) return;

            Map.MapTiles.PlaceTile(result.MapTile);

            // play result.OnEndTurnBonusTree animations

            Main.DelayedCall(GameLoop.EndTurn, 0.5f);
        }

        // --------------------------------------------------------------------------------------------------- type check
        private static T GetResult<T>(ICommandResult result) where T : class, ICommandResult
        {
            if (result is not T typedResult)
            {
                Debug.LogError($"[CommandResultHandlers] Result is not {typeof(T).Name}!");
                return null;
            }
            return typedResult;
        }
    }
}
