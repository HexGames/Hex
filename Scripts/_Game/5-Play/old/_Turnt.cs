namespace Game
{
    public static class Turn
    {
        public static void StartTurn()
        {
            Logic.Turn.GainAllBenefits(Board.Player);
            Logic.Effects.ReapplyAllEffects();
            Logic.Effects.CalculateAllBenefits(out Board.Turn.BenefitsTotal, out Board.Turn.BenefitsAtCoords);
            Logic.Turn.PutNewTilesIntoNextQueue(Board.Decks, Board.Player);
            Logic.Turn.SetNextTileAsCurrentTile(Board.Player);

            Debug.Log($"OnStartTurn {Logic.GameMain.X.TurnNo}");
        }
        public static void EndTurn()
        {
            Logic.GameMain.X.TurnNo++;
        }
    }
}
