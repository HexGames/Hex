namespace Game
{
    public static class Board
    {
        public static Data.Decks Decks => Logic.GameMain.X.Decks;
        public static Data.Player Player => Logic.GameMain.X.Player;
        public static Data.Turn Turn => Logic.GameMain.X.Turn;
        public static int TurnNo => Logic.GameMain.X.TurnNo;
    }
}
