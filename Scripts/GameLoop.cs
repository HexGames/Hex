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

            Main.DelayedCall(OnStartRunDelayed, 0.5f);
        }

        private static void OnStartRunDelayed()
        {
            Play.Game.NewGame();

            Map.MapTiles.Refresh();

            // add 0, 0 - production
            // add ---- - add reactivate
            // add 0, 1 - add multiply <- 
            // add ---- - add additive <- 

            // appear +2 at 0,0
            // appear *2 at 0,1 arrow to 0,0
            // appear +3 at 1,0 arrow to 0,0
            // appear *2 at 2,0 arrow to 1,0

            // process *2 at 2,0 -> make +6 at 1,0
            // process +3 at 1,0 -> make +8 at 0,0
            // process *2 at 0,1 -> make +16 at 0,0
            // process +2 at 0,0 -> pop +16 at 0,0

            // test example - TO DO - remove
            //Map.WorldUI.ArrowsPool.AddArrow(new Data.HexPos(0, 0), new Data.HexPos(0, 1));
            //Map.WorldUI.ArrowsPool.AddArrow(new Data.HexPos(2, 0), new Data.HexPos(2, -1));
            //Map.WorldUI.ArrowsPool.AddArrow(new Data.HexPos(2, 0), new Data.HexPos(1, 0));

            //UI.TileInfo.Add(new Data.HexPos(0, 0), "+2");
            //UI.TileInfo.Add(new Data.HexPos(2, 0), "+2");
            //UI.TileInfo.Add(new Data.HexPos(3, -1), "x2");

            UI.Benefit3D.Add(new Hex.Data.HexPos(1, 0), Hex.Def.Timing.OnPlace, "+3");

            UI.Benefit3D.Add(new Hex.Data.HexPos(1, 0), Hex.Def.Timing.OnPlace, "+3");

            UI.Benefit3D.Add(new Hex.Data.HexPos(1, 0), Hex.Def.Timing.PerTurn, "+2");

            Main.DelayedCall(() => UI.Benefit3D.Pop(new Hex.Data.HexPos(1, 0), Hex.Def.Timing.OnPlace), 1.5f);

            Main.DelayedCall(() => UI.Benefit3D.Pop(new Hex.Data.HexPos(1, 0), Hex.Def.Timing.PerTurn), 3.0f);

            Main.DelayedCall(() => UI.Benefit3D.Pop(new Hex.Data.HexPos(1, 0), Hex.Def.Timing.OnPlace), 4.5f);

            StartTurn();
        }

        public static void StartTurn()
        {
            Hex.Play.Game.AutoInputGetTileFromQueue();
        }

        public static void Update(double delta)
        {
            Hex.Play.Game.Update();
        }
    }
}

