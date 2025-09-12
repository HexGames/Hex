using System.Collections.Generic;

public static class UI
{
    private static GodotUI.UIMain Instance => Main.x.UIInstance;

    // Example wrappers for methods
    public static void Menu_Show() => Instance?.Menu.ShowAnim();
    public static void Menu_Hide() => Instance?.Menu.HideAnim();
    public static void Resources_Refresh(List<Data.Res> stockpile, List<Data.Res> income) => Instance?.Resoruces.Refresh(stockpile, income);
    public static void Resources_Refresh(bool show) => Instance?.Resoruces.Refresh(show);
    public static void TileInfo_RefreshBenefits(Data.Tile tile) => Instance?.TileInfo.RefreshBenefits(tile);
    public static void TileInfo_RefreshEffects(Data.Tile tile) => Instance?.TileInfo.RefreshEffects(tile);
    public static void TileInfo_Refresh(bool show) => Instance?.TileInfo.Refresh(show);
    public static void Turn_Refresh(bool show) => Instance?.Turn.Refresh(show);
    public static void NextTiles_Refresh(bool show) => Instance?.NextTiles.Refresh(show);
    //public static void WDraft_Refresh(bool show) => Instance?.Draft.Refresh(show);
    // Add more as needed...

    public static GodotUI.IUIWTileInfos TileInfos => Instance?.InWorld.TileInfosPool;
}
