namespace Hex
{
    public static class GameLoop
    {
        public static void Init()
        {
            Play.Game.Init();

            // after Play.Game.Init
            CommandResultHandlers.Init();

            UI.MainMenu.OnStartGame = OnStartRun;
        }

        // --------------------------------------------------------------------------------------------------- Game
        public static void OnStartRun()
        {
            UI.MainMenu.Hide();

            UI.Resources.Show();
            UI.NextTile.Show();

            Main.DelayedCall(OnStartRunDelayed, 0.5f);
        }

        private static void OnStartRunDelayed()
        {
            Play.Game.NewGame();

            Map.MapTiles.Refresh();

            StartTurn();
        }

        public static void StartTurn()
        {
            Play.Game.AutoInputGetTileFromQueue();
            Play.Game.AutoInputStartTurn();
        }

        public static void EndTurn()
        {
            Play.Game.AutoInputEndTurn();
        }

        public static void Update(double delta)
        {
            Hex.Play.Game.Update();
        }
    }
}

