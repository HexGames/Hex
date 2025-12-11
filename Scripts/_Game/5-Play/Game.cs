using CommandSystem;
using System;
using Hex;
using Hex.PlayInternal;

namespace Hex.Play
{
    public static class Game
    {
        public static void Init()
        {
            Data.MapHelperInit.Init();
            CommandsInit.Init();
        }

        public static void NewGame()
        {
            Data.Game.DeckTiles.InitDeckTiles(Logic.Deck.GenerateDeckTiles());
            Data.Game.MapTiles.InitMapTiles(Logic.Map.GenerateMapTiles());
        }

        public static void RegisterPlayTileOutputHandlers(Action<ICommandResult> handler)
        {
            CommandSys.RegisterResultHandler(Phase.Build, CommandType.PlayTile, handler);
        }

        public static void InputPlayTile(Data.HexPos atHexPos)
        {
            CommandSys.AddCommand(new Command(CommandType.PlayTile, new PlayTileCommandInfo(Data.Game.DeckTiles[PlayData.CurrentTileID], atHexPos)));
        }

        public static void Update()
        {
            CommandSys.ProcessAllCommands();
        }
    }
}
