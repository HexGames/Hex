using PlayInternal;
using CommandSystem;
using System;

namespace Play
{
    public static class Game
    {
        public static void Initialize()
        {
            Commands.Initialize();
        }
        public static void NewGame()
        {
            if (Logic.GameMain.X == null)
            {
                Logic.GameMain.X = new Logic.GameMain();
            }
            else
            {
                Debug.LogError("Game instance already exists. Cannot start a new game.");
                return;
            }

            Logic.Start.AddStartingDeckTiles();
            Logic.Start.AddStartingRes();
        }

        public static void RegisterPlayTileOutputHandlers(Action<ICommandResult> handler)
        {
            CommandSys.RegisterResultHandler(Phase.Build, CommandType.PlayTile, handler);
        }

        public static void InputPlayTile(HexData.Tile tile, HexData.HexPos atHexPos)
        {
            CommandSys.AddCommand(new Command(CommandType.PlayTile, new PlayTileCommandInfo(tile, atHexPos)));
        }

        public static void Update()
        {
            CommandSys.ProcessAllCommands();
        }
    }
}
