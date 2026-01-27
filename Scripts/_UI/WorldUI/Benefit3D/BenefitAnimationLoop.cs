namespace Hex.UI
{
    public static class BenefitAnimationLoop
    {
        private const float TIME_STEP = 0.5f;

        public static void PlayBenefitLoop(Logic.Production[] onPlaceProduction)
        {
            float timeOffset = 0f;

            for (int productionIdx = 0; productionIdx < onPlaceProduction.Length; productionIdx++)
            {
                ref Logic.Production production = ref onPlaceProduction[productionIdx];
                for (int bonusIdx = 0; bonusIdx < production.BonusList.Count; bonusIdx++)
                {
                    ref Logic.BonusStep bonusStep = ref production.BonusList[bonusIdx];
                    Data.HexPos hesPos = Data.MapHelper.MapTileIDToHexPos(bonusStep.Tile.ID);
                    string text = bonusStep.Value.ToString();
                    Main.DelayedCall(() => Benefit3D.Add(hesPos, Def.Timing.OnPlace, text), timeOffset);
                    timeOffset++;
                }
            }

            for (int productionIdx = onPlaceProduction.Length - 1; productionIdx >= 0; productionIdx--)
            {
                ref Logic.Production production = ref onPlaceProduction[productionIdx];
                for (int bonusIdx = production.BonusList.Count - 1; bonusIdx >= 0; bonusIdx--)
                {
                    ref Logic.BonusStep bonusStep = ref production.BonusList[bonusIdx];
                    Data.HexPos hesPos = Data.MapHelper.MapTileIDToHexPos(bonusStep.Tile.ID);
                    Main.DelayedCall(() => Benefit3D.Pop(hesPos, Def.Timing.OnPlace), timeOffset);
                    timeOffset++;
                }
            }
        }
    }
}
