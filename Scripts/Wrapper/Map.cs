public static class Map
{
    private static Godot3D.MapMain Instance => Main.x.MapInstance;

    // Example wrappers for methods
    public static void Refresh() => Instance?.Refresh();
    public static void InitCursorTile(Hex.Data.Tile tile) => Instance?.InitCursorTile(tile);
    public static void PopUpBenefit(Hex.Data.HexPos coord, string text) => Instance?.PopUpBenefit(coord, text);
    public static void PlayTile(Hex.Data.Tile tile, Hex.Data.HexPos hexPos) => Instance?.OnPlayTile(tile, hexPos);

    public static Godot3D.WorldUI WorldUI => Instance?.WorldUI;
}
