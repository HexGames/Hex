using System.Collections.Generic;

namespace Game
{
    public static class Play
    {
        public static bool CheckPlayable(HexData.Tile tile, HexData.HexPos coord)
        {
            bool playable = Logic.Play.CheckPlayable(Board.Player, tile, coord);
            return playable;
        }

        public static void SimulatePlayTile(HexData.Tile tile, HexData.HexPos coord, out List<HexData.Benefit> benefitsExtra, out List<HexData.Benefit> benefitsExtraAtCoords)
        {
            Logic.Effects.ReapplyAllEffectsWithOverwriteTile(tile, coord);
            Logic.Effects.CalculateAllBenefitsWithOverwriteTile(tile, coord, out benefitsExtra, out benefitsExtraAtCoords);
            Logic.Effects.RemoveBenefitsRange(benefitsExtra, Board.Turn.BenefitsTotal);
            Logic.Effects.RemoveBenefitsRange(benefitsExtraAtCoords, Board.Turn.BenefitsAtCoords);
        }

        public static void PlayTile(HexData.Tile tile, HexData.HexPos coord, out List<HexData.Benefit> benefitsTotal, out List<HexData.Benefit> benefitsAtCoords)
        {
            Logic.Play.RemoveTile(coord);
            Logic.Play.SetTileIOnMap(Logic.GameMain.X.Player, tile, coord);
            Logic.Effects.ReapplyAllEffects();
            Logic.Effects.CalculateAllBenefits(out benefitsTotal, out benefitsAtCoords);
        }
    }
}
