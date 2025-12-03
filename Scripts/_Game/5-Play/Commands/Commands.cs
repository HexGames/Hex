using CommandSystem;

namespace PlayInternal
{
    public static class Commands
    {
        public static void Initialize()
        {
            CommandSys.RegisterCommandHandler(Phase.Build, CommandType.PlayTile, PlayTileCommand.HandleCommand);

            CommandSys.SetPhase(Phase.Build);
        }
    }
}
