public static class Map
{
    private static Godot3D.MapMain Instance => Main.x.MapInstance;

    // Example wrappers for methods
    public static void Refresh() => Instance?.Refresh();
    public static void InitCursorTile(Hex.Data.DeckTile tile) => Instance?.InitCursorTile(tile);
    public static void PopUpBenefit(Hex.Data.HexPos coord, string text) => Instance?.PopUpBenefit(coord, text);
    public static void PlayTile(Hex.Data.DeckTile deckTile, Hex.Data.HexPos hexPos) => Instance?.OnPlayTile(deckTile, hexPos);

    public static Godot3D.WorldUI WorldUI => Instance?.WorldUI;
}
