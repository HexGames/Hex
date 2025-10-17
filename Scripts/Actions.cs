using System.Collections.Generic;

public static partial class Actions
{
    public static void Init()
    {
        UI.MainMenu.OnStartGame = OnStartRun;
    }

    // --------------------------------------------------------------------------------------------------- Game
    public static void OnStartRun()
    {
        UI.MainMenu.Hide();

        Main.DelayedAction(OnStartRunDelayed, 0.5f);
    }

    private static void OnStartRunDelayed()
    {
        Game.Start.NewGame();

        Map.Refresh();
        OnStartTurn();

        // test example - TO DO - remove
        //Map.WorldUI.ArrowsPool.AddArrow(new Data.HexCoords(0, 0), new Data.HexCoords(0, 1));
        //Map.WorldUI.ArrowsPool.AddArrow(new Data.HexCoords(2, 0), new Data.HexCoords(2, -1));
        //Map.WorldUI.ArrowsPool.AddArrow(new Data.HexCoords(2, 0), new Data.HexCoords(1, 0));

        //UI.TileInfo.Add(new Data.HexCoords(0, 0), "+2");
        //UI.TileInfo.Add(new Data.HexCoords(2, 0), "+2");
        //UI.TileInfo.Add(new Data.HexCoords(3, -1), "x2");
    }

    public static void OnEndRun()
    {
        UI.Resources.Hide();
        UIX.Turn_Refresh(false);

        Game.Start.EndGame();

        UI.MainMenu.Show();
    }

    // --------------------------------------------------------------------------------------------------- Turn
    public static void OnStartTurn()
    {
        UI.Resources.Refresh(Game.Board.Player.Stockpile, Game.Board.Player.Income);
        UI.Resources.Show();

        Game.Turn.StartTurn();
        Map.InitCursorTile(Game.Board.Player.CurrentTile);
        UIX.NextTiles_Refresh(true);
    }

    public static void OnEndTurn()
    {
        Main.DelayedAction(OnStartTurn, 1.0f);
    }

    //    List<Data.Res> initialStockpile = new List<Data.Res>(Game.Board.Player.Stockpile);
    //
    //    Game.Turn.EndTurn();
    //
    //    int resWithIncomeCount = 0;
    //    foreach (Data.Res resIncome in Game.Board.Player.Income)
    //    {
    //        if (resIncome.Value != 0)
    //        {
    //            Main.DelayedAction(() => OnEndTurn_GainIncome(initialStockpile, resIncome), resWithIncomeCount * 0.5f);
    //            resWithIncomeCount++;
    //        }
    //    }
    //
    //    Main.DelayedAction(OnStartTurn, resWithIncomeCount * 0.5f);
    //}
    //
    //public static void OnEndTurn_GainIncome(List<Data.Res> stockpile, Data.Res resIncome)
    //{
    //    Logic.Stockpile.AddResToStockpile(stockpile, resIncome);
    //    UI.Resources.Refresh(stockpile, Game.Board.Player.Income);
    //}

    // ---------------------------------------------------------------------------------------------------
    // TO DO - add auto skip for unplayable tiles
    //public static bool CanPlayTile(Data.Tile tile, Data.HexCoord hexCoord)
    //{
    //    return Game.CheckPlayable(tile, hexCoord);
    //}

    // --------------------------------------------------------------------------------------------------- Hover
    public static bool IsHoverValid(Data.HexCoords coord)
    {
        Data.Tile tile = Game.Board.Player.CurrentTile;
        return Game.Play.CheckPlayable(tile, coord);
    }

    public static void OnHoverCurrentTile(Data.HexCoords coord) // from Map.Cursor
    {
        Data.Tile tile = Game.Board.Player.CurrentTile;
        List<Data.Benefit> benefitsTotal;
        List<Data.Benefit> benefitAtCoords;
        Game.Play.SimulatePlayTile(tile, coord, out benefitsTotal, out benefitAtCoords);
        UI.TileInfo.RefrehsForBenefits(tile, benefitsTotal);
        UI.TileInfo.Show();
    }

    public static void OnHoverBlocked() // from Map.Cursor
    {
        Data.Tile tile = Game.Board.Player.CurrentTile;
        UI.TileInfo.RefrehsForEffects(tile);
        UI.TileInfo.Show();
    }

    public static void OnHoverInvalid() // from Map.Cursor
    {
        UI.TileInfo.Hide();
    }

    // --------------------------------------------------------------------------------------------------- Play
    public static void OnPlayCurrentTile(Data.HexCoords coord)
    {
        Data.Tile tile = Game.Board.Player.CurrentTile;
        if (Game.Play.CheckPlayable(tile, coord) == false)
            return;

        List<Data.Benefit> benefitsTotal;
        List<Data.Benefit> benefitAtCoords;
        Game.Play.PlayTile(tile, coord, out benefitsTotal, out benefitAtCoords);

        Map.PlayTile(tile, coord); 
        UI.TileInfo.Hide();

        UI.TileInfo3D.

        OnEndTurn();

        //Data.Tile tile = Game.Player.CurrentTile;
        //
        //bool playable = Game.CheckPlayable(tile, coord);
        //if (playable == true)
        //{
        //    List<Data.Res> initialStockpile = new List<Data.Res>(Game.Player.Stockpile);
        //    List<Data.Res> initialIncome = new List<Data.Res>(Game.Player.Income);
        //
        //    Game.PlayTile(tile, coord);
        //    Map.PlayTile(tile, coord);
        //
        //    UIX.TileInfo_Refresh(false);
        //
        //    // TO DO
        //    //for (int benefitIdx = 0; benefitIdx < tile.Benefits.Count; benefitIdx++)
        //    //{
        //    //    Data.Benefit benefit = tile.Benefits[benefitIdx];
        //    //    Main.DelayedAction(() => OnPlayDraftedTile_GainBenefit(initialStockpile, initialIncome, benefit), benefitIdx * 0.5f);
        //    //}
        //    //
        //    //Main.DelayedAction(OnEndTurn, tile.Benefits.Count * 0.5f + 0.5f);
        //}
    }

    //public static void OnPlayDraftedTile_GainBenefit(List<Data.Res> stockpile, List<Data.Res> income, Data.Benefit benefit)
    //{
    //    if (benefit.BenefitTiming == Def.Timing.PerTurn)
    //    {
    //        Logic.Stockpile.AddResToStockpile(income, benefit.Res);
    //    }
    //    else
    //    {
    //        Logic.Stockpile.AddResToStockpile(stockpile, benefit.Res);
    //    }
    //    //UIX.Resources_Refresh(stockpile, income);
    //    Map.PopUpBenefit(benefit.HexCoords, GodotUI.UIHelper.ResToString(benefit.Res, alwaysShowSign: true, redNegativeValues: true));
    //}

    // ---------------------------------------------------------------------------------------------------
    //public static bool IsDraftWindowShown = false;
    //public static void ShowDraftWindow()
    //{
    //    UI.Turn_Refresh(false);
    //
    //    UI.WDraft_Refresh(true);
    //
    //    //IsDraftWindowShown = false;
    //}
    //
    //public static void HideDraftWindow()
    //{
    //    UI.WDraft_Refresh(false);
    //
    //    //UI.Turn_Refresh(true);
    //}
}
