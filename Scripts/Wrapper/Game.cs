using System.Collections.Generic;

public static class Game
{
    private static Logic.Game Instance => Main.x.GameInstance;

    // Exposed members
    public static Data.Decks Decks => Instance?.Decks;
    public static Data.Player Player => Instance?.Player;
    public static Data.Map Map => Instance?.Map;
    public static int TurnNo => Instance != null ? Instance.TurnNo : -1;

    // Exposed methods
    public static void SetDraftedTile(Data.Tile tile) => Instance?.SetDraftedTile(tile);
    public static bool CheckPlayable(in Data.Tile tile, in Data.HexCoord coord) => Instance?.CheckPlayable(tile, coord) ?? false;
    public static void SimulatePlayTile(in Data.Tile tile, in Data.HexCoord coord) => Instance?.SimulatePlayTile(tile, coord);
    public static void PlayTile(in Data.Tile tile, in Data.HexCoord coord) => Instance?.PlayTile(tile, coord);
    public static void EndTurn() => Instance?.EndTurn();
    public static void EndGame() => Instance?.EndGame();

    // Start Game
    public static void StartGame()
    {
        if (Instance == null)
        {
            Main.x.GameInstance = new Logic.Game();
        }
        else
        {
            Debug.LogError("Game instance already exists. Cannot start a new game.");
        }
    }
}