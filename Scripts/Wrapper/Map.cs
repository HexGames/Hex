public static class Map
{
    private static Godot3D.MapMain Instance => Main.x.MapInstance;

    // Example wrappers for methods
    public static void Refresh() => Instance?.Refresh();
    public static void InitCursorTile(Data.Tile tile) => Instance?.InitCursorTile(tile);
    public static void PopUpBenefit(Data.HexCoord coord, string text) => Instance?.PopUpBenefit(coord, text);
    public static void PlayTile(Data.Tile tile, Data.HexCoord hexCoord) => Instance?.OnPlayTile(tile, hexCoord);

    public static Godot3D.WorldUI WorldUI => Instance?.WorldUI;
}
