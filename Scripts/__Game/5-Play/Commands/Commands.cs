using CommandSystem;

namespace Hex.PlayInternal
{
    public static class Commands
    {
        internal static void Init()
        {
            CommandSys.RegisterCommandHandler(Phase.Build, CommandType.GetTileFromQueue, GetTileFromQueueCommand.HandleCommand);
            CommandSys.RegisterCommandHandler(Phase.Build, CommandType.PlayTile, PlayTileCommand.HandleCommand);

            CommandSys.SetPhase(Phase.Build);
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
