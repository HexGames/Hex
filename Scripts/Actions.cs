using System.Collections.Generic;

public static partial class Actions
{
    public static void OnStartRun()
    {
        UI.Menu_Hide();

        Main.DelayedAction(OnStartRunDelayed, 0.5f);
    }

    private static void OnStartRunDelayed()
    {
        Game.StartGame();

        Map.Refresh();
        UI.Resources_Refresh(Game.Player.Stockpile, Game.Player.Income);

        OnStartTurn();
    }

    public static void OnEndRun()
    {
        UI.Resources_Refresh(false);
        UI.Turn_Refresh(false);

        Game.EndGame();

        UI.Menu_Show();
    }

    // ---------------------------------------------------------------------------------------------------
    //public static bool CanPlayTile(Data.Tile tile, Data.HexCoord hexCoord)
    //{
    //    return Game.CheckPlayable(tile, hexCoord);
    //}

    // --------------------------------------------------------------------------------------------------- DRAFT
    public static void OnDraft(Data.Tile tile)
    {
        HideDraftWindow();

        Game.SetDraftedTile(tile);

        Map.InitCursorTile(Game.Player.DraftedTile);

        UI.Resources_Refresh(Game.Player.Stockpile, Game.Player.Income);
    }

    // --------------------------------------------------------------------------------------------------- Hover
    public static void OnHoverDraftedTile(Data.HexCoord coord)
    {
        Data.Tile tile = Game.Player.DraftedTile; 

        bool playable = Game.CheckPlayable(tile, coord);
        if (playable == true)
        {
           Game.SimulatePlayTile(tile, coord);

            UI.TileInfo_RefreshBenefits(tile);
        }
        else
        {
            UI.TileInfo_RefreshEffects(tile);
        }
    }

    public static void OnHoverOffMap()
    {
        Data.Tile tile = Game.Player.DraftedTile;

        UI.TileInfo_RefreshEffects(tile);
    }

    // --------------------------------------------------------------------------------------------------- Play
    public static void OnPlayDraftedTile(Data.HexCoord coord)
    {
        Data.Tile tile = Game.Player.DraftedTile;

        bool playable = Game.CheckPlayable(tile, coord);
        if (playable == true)
        {
            List<Data.Res> initialStockpile = new List<Data.Res>(Game.Player.Stockpile);
            List<Data.Res> initialIncome = new List<Data.Res>(Game.Player.Income);

            Game.PlayTile(tile, coord);
            Map.PlayTile(tile, coord);

            UI.TileInfo_Refresh(false);

            for (int benefitIdx = 0; benefitIdx < tile.Benefits.Count; benefitIdx++)
            {
                Data.Benefit benefit = tile.Benefits[benefitIdx];
                Main.DelayedAction(() => OnPlayDraftedTile_GainBenefit(initialStockpile, initialIncome, benefit), benefitIdx * 0.5f);
            }

            Main.DelayedAction(OnEndTurn, tile.Benefits.Count * 0.5f + 0.5f);
        }
    }

    public static void OnPlayDraftedTile_GainBenefit(List<Data.Res> stockpile, List<Data.Res> income, Data.Benefit benefit)
    {
        if (benefit.BenefitTiming == Def.Timing.PerTurn)
        {
            Logic.Stockpile.AddResToStockpile(income, benefit.Res);
        }
        else
        {
            Logic.Stockpile.AddResToStockpile(stockpile, benefit.Res);
        }
        UI.Resources_Refresh(stockpile, income);
        Map.PopUpBenefit(benefit.HexCoord, GodotUI.UIHelper.ResToString(benefit.Res, alwaysShowSign: true, redNegativeValues: true));
    }

    // ---------------------------------------------------------------------------------------------------
    public static void OnEndTurn()
    {
        List<Data.Res> initialStockpile = new List<Data.Res>(Game.Player.Stockpile);

        Game.EndTurn();

        int resWithIncomeCount = 0;
        foreach (Data.Res resIncome in Game.Player.Income)
        {
            if (resIncome.Value != 0)
            {
                Main.DelayedAction(() => OnEndTurn_GainIncome(initialStockpile, resIncome), resWithIncomeCount * 0.5f);
                resWithIncomeCount++;
            }
        }

        Main.DelayedAction(OnStartTurn, resWithIncomeCount * 0.5f);
    }
    public static void OnEndTurn_GainIncome(List<Data.Res> stockpile, Data.Res resIncome)
    {
        Logic.Stockpile.AddResToStockpile(stockpile, resIncome);
        UI.Resources_Refresh(stockpile, Game.Player.Income);
    }

    public static void OnStartTurn()
    {
        UI.Resources_Refresh(Game.Player.Stockpile, Game.Player.Income);

        ShowDraftWindow();
    }

    // ---------------------------------------------------------------------------------------------------
    //public static bool IsDraftWindowShown = false;
    public static void ShowDraftWindow()
    {
        UI.Turn_Refresh(false);

        UI.WDraft_Refresh(true);

        //IsDraftWindowShown = false;
    }

    public static void HideDraftWindow()
    {
        UI.WDraft_Refresh(false);

        UI.Turn_Refresh(true);
    }
}
