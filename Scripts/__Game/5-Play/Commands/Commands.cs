using CommandSystem;

namespace Hex.PlayInternal
{
    public static class Commands
    {
        internal static void Init()
        {
            CommandSys.RegisterCommandHandler(Phase.Build, CommandType.GetTileFromQueue, GetTileFromQueueCommand.HandleCommand);
            CommandSys.RegisterCommandHandler(Phase.Build, CommandType.PlaceTile, PlayTileCommand.HandleCommand);
            CommandSys.RegisterCommandHandler(Phase.Build, CommandType.EndTurn, EndTurnCommand.HandleCommand);
            CommandSys.RegisterCommandHandler(Phase.Build, CommandType.StartTurn, StartTurnCommand.HandleCommand);

            CommandSys.SetPhase(Phase.Build);
        }

        // --------------------------------------------------------------------------------------------------- type check
        internal static T GetInfo<T>(ICommandInfo result) where T : class, ICommandInfo
        {
            if (result is not T typedResult)
            {
                Debug.LogError($"[Commands] Result is not {typeof(T).Name}!");
                return null;
            }
            return typedResult;
        }
    }

    public static class CommandsInit
    {
        public static void Init()
        {
            Commands.Init();
        }
    }
}
