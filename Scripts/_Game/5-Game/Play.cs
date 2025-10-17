using System.Collections.Generic;

namespace Game
{
    public static class Play
    {
        public static bool CheckPlayable(Data.Tile tile, Data.HexCoords coord)
        {
            bool playable = Logic.Play.CheckPlayable(Board.Player, Board.Map, tile, coord);
            return playable;
        }

        public static void SimulatePlayTile(Data.Tile tile, Data.HexCoords coord, out List<Data.Benefit> benefitsExtra, out List<Data.Benefit> benefitsExtraAtCoords)
        {
            Logic.Play.ReapplyAllEffectsWithOverwriteTile(Logic.GameMain.X.Map, tile, coord);
            Logic.Play.CalculateAllBenefitsWithOverwriteTile(Logic.GameMain.X.Map, tile, coord, out benefitsExtra, out benefitsExtraAtCoords);
            Logic.Play.RemoveBenefitsRange(benefitsExtra, Board.Turn.BenefitsTotal);
            Logic.Play.RemoveBenefitsRange(benefitsExtraAtCoords, Board.Turn.BenefitsAtCoords);
        }

        public static void PlayTile(Data.Tile tile, Data.HexCoords coord, out List<Data.Benefit> benefitsTotal, out List<Data.Benefit> benefitsAtCoords)
        {
            Logic.Play.RemoveTile(Logic.GameMain.X.Map, coord);
            Logic.Play.SetTileIOnMap(Logic.GameMain.X.Player, Logic.GameMain.X.Map, tile, coord);
            Logic.Play.ReapplyAllEffects(Logic.GameMain.X.Map);
            Logic.Play.CalculateAllBenefits(Logic.GameMain.X.Map, out benefitsTotal, out benefitsAtCoords);
        }
    }
}
