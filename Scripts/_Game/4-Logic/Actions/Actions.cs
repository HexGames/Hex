namespace Logic
{
    public static class Actions
    {
        public static bool PlayTile(Hex.Data.Tile tile, Hex.Data.HexPos atHexPos)
        {
            Play.RemoveTile(atHexPos);
            Play.SetTileIOnMap(GameMain.X.Player, tile, atHexPos);
            Effects.ReapplyAllEffects();
            Effects.CalculateAllBenefits(out benefitsTotal, out benefitsAtCoords);

            return true;
        }
    }
}
