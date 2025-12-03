public static class GameLoop
{
    public static void Init()
    {
        Play.Game.Initialize();

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
        Game.Start.NewGame();

        Map.Refresh();
        OnStartTurn();

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
    }

    public static void Update(double delta)
    {
        Play.Game.Update();
    }
}

