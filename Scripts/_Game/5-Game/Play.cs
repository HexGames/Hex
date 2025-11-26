using System.Collections.Generic;

namespace Game
{
    public static class Play
    {
        public static bool CheckPlayable(Data.Tile tile, Data.HexPos coord)
        {
            bool playable = Logic.Play.CheckPlayable(Board.Player, tile, coord);
            return playable;
        }

        public static void SimulatePlayTile(Data.Tile tile, Data.HexPos coord, out List<Data.Benefit> benefitsExtra, out List<Data.Benefit> benefitsExtraAtCoords)
        {
            Logic.Effects.ReapplyAllEffectsWithOverwriteTile(tile, coord);
            Logic.Effects.CalculateAllBenefitsWithOverwriteTile(tile, coord, out benefitsExtra, out benefitsExtraAtCoords);
            Logic.Effects.RemoveBenefitsRange(benefitsExtra, Board.Turn.BenefitsTotal);
            Logic.Effects.RemoveBenefitsRange(benefitsExtraAtCoords, Board.Turn.BenefitsAtCoords);
        }

        public static void PlayTile(Data.Tile tile, Data.HexPos coord, out List<Data.Benefit> benefitsTotal, out List<Data.Benefit> benefitsAtCoords)
        {
            Logic.Play.RemoveTile(coord);
            Logic.Play.SetTileIOnMap(Logic.GameMain.X.Player, tile, coord);
            Logic.Effects.ReapplyAllEffects();
            Logic.Effects.CalculateAllBenefits(out benefitsTotal, out benefitsAtCoords);
        }
    }
}
