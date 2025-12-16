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
            CommandsInit.Init();

            CommandSys.RegisterResultHandler(Phase.Build, CommandType.GetTileFromQueue, AutoInputGetTileFromQueueResultHandler);
        }

        public static void NewGame()
        {
            Data.Game.DeckTiles.InitDeckTiles(Logic.Deck.GenerateDeckTiles());
            Data.Game.MapTiles.InitMapTiles(Logic.Map.GenerateMapTiles());
            Data.Game.DeckTiles.CreateDrawPile();
            Data.Game.DeckTiles.CreateQueue();
        }

        //public static void RegisterPlayTileOutputHandlers(Action<ICommandResult> handler)
        //{
        //    CommandSys.RegisterResultHandler(Phase.Build, CommandType.PlayTile, handler);
        //}
        //
        //public static void RegisterGetTileFromQueueOutputHandlers(Action<ICommandResult> handler)
        //{
        //    CommandSys.RegisterResultHandler(Phase.Build, CommandType.GetTileFromQueue, handler);
        //}

        public static void InputPlayTile(Data.HexPos atHexPos)
        {
            CommandSys.AddCommand(new Command(CommandType.PlayTile, new PlayTileCommandInfo(PlayData.CurrentDeckTile, atHexPos)));
        }

        public static void AutoInputGetTileFromQueue()
        {
            CommandSys.AddCommand(new Command(CommandType.GetTileFromQueue, new GetTileFromQueueCommandInfo()));
        }

        private static void AutoInputGetTileFromQueueResultHandler(ICommandResult result)
        {
            var getTileFromQueueCommandResult = result as GetTileFromQueueCommandResult;
            if (getTileFromQueueCommandResult == null)
            {
                Debug.LogError("[Game] AutoInputGetTileFromQueueResultHandler: result is not GetTileFromQueueCommandResult!");
                return;
            }
            PlayData.CurrentDeckTile = getTileFromQueueCommandResult.DeckTile;
        }

        public static void Update()
        {
            CommandSys.ProcessAllCommands();
        }
    }
}
