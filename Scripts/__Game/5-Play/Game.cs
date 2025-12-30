using CommandSystem;
using System;
using Hex;
using Hex.PlayInternal;

namespace Hex.Play
{
    public static class Game
    {
        public static bool LockInput = false; 
        private static Data.DeckTileRef _currentDeckTile = Data.DeckTileRef.INVALID;

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

            Data.Game.NewTurn();
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

        private static void AutoInputGetTileFromQueueResultHandler(ICommandResult result)
        {
            var getTileFromQueueCommandResult = result as GetTileFromQueueCommandResult;
            if (getTileFromQueueCommandResult == null)
            {
                Debug.LogError("[Game] AutoInputGetTileFromQueueResultHandler: result is not GetTileFromQueueCommandResult!");
                return;
            }
            _currentDeckTile = getTileFromQueueCommandResult.DeckTile;
        }

        public static void AutoInputGetTileFromQueue()
        {
            CommandSys.AddCommand(new Command(CommandType.GetTileFromQueue, new GetTileFromQueueCommandInfo()));
        }

        public static void AutoInputStartTurn()
        {
            CommandSys.AddCommand(new Command(CommandType.StartTurn, new StartTurnCommandInfo()));
        }

        public static void InputPlayTile(Data.HexPos atHexPos)
        {
            if (LockInput == true) return;

            LockInput = true;
            CommandSys.AddCommand(new Command(CommandType.PlaceTile, new PlayTileCommandInfo(_currentDeckTile, atHexPos)));
        }

        public static void AutoInputEndTurn()
        {
            CommandSys.AddCommand(new Command(CommandType.EndTurn, new EndTurnCommandInfo()));
        }

        public static void Update()
        {
            CommandSys.ProcessAllCommands();
        }
    }
}
