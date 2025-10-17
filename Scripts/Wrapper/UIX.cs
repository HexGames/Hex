using System.Collections.Generic;

public static class UIX
{
    private static GodotUI.UIMain Instance => Main.x.UIInstance;

    // Example wrappers for methods
    public static void Turn_Refresh(bool show) => Instance?.Turn.Refresh(show);
    public static void NextTiles_Refresh(bool show) => Instance?.NextTiles.Refresh(show);
    //public static void WDraft_Refresh(bool show) => Instance?.Draft.Refresh(show);
    // Add more as needed...

    //public static GodotUI.IUIWTileInfos TileInfos => Instance?.InWorld.TileInfosPool;
}
