using System.Collections.Generic;

namespace Hex.UI
{
    public static class BenefitAnimationLoop
    {
        private const float TIME_STEP = 0.5f;

        private static Dictionary<Data.HexPos, int> _hexPosToBenefitID = new Dictionary<Data.HexPos, int>();

        public static void PlayBenefitLoop(Logic.Production[] onPlaceProduction)
        {
            float timeOffset = 0f;
            for (int productionIdx = 0; productionIdx < onPlaceProduction.Length; productionIdx++)
            {
                _hexPosToBenefitID.Clear();
                ref Logic.Production production = ref onPlaceProduction[productionIdx];
                int benefitID = -1;
                for (int localIdx = 0; localIdx < production.LocalList.Count; localIdx++)
                {
                    ref Logic.LocalStep localStep = ref production.LocalList[localIdx];
                    Data.HexPos sourceHesPos = Data.MapHelper.MapTileIDToHexPos(localStep.SourceTile.ID);
                    Main.DelayedCall(() => Map.MapTiles.TriggerBenefitHighlightAtHexPos(sourceHesPos), timeOffset);
                    string text = $"+{localStep.Value.ToString()}";
                    if (benefitID < 0)
                    {
                        Data.HexPos hexPos = Data.MapHelper.MapTileIDToHexPos(production.Tile.ID);
                        Benefit3D.Create(hexPos, Def.Timing.OnPlace, out benefitID);
                        Benefit3D.Show(in benefitID, text);
                        _hexPosToBenefitID.Add(hexPos, benefitID);
                    }
                    else
                    {
                        Main.DelayedCall(() => Benefit3D.ChangeValue(in benefitID, text), timeOffset);
                    }
                    timeOffset++;
                }

                for (int bonusIdx = 0; bonusIdx < production.BonusList.Count; bonusIdx++)
                {
                    ref Logic.BonusStep bonusStep = ref production.BonusList[bonusIdx];
                    int bonusBenefitID = -1;
                    for (int localIdx = 0; localIdx < bonusStep.LocalList.Count; localIdx++)
                    {
                        ref Logic.LocalStep localStep = ref bonusStep.LocalList[localIdx];
                        Data.HexPos sourceHesPos = Data.MapHelper.MapTileIDToHexPos(localStep.SourceTile.ID);
                        Main.DelayedCall(() => Map.MapTiles.TriggerBenefitHighlightAtHexPos(sourceHesPos), timeOffset);
                        string text = $"*{localStep.Value.ToString()}";
                        if (bonusBenefitID < 0)
                        {
                            Data.HexPos hesPos = Data.MapHelper.MapTileIDToHexPos(bonusStep.SourceTile.ID);
                            Benefit3D.Create(hesPos, Def.Timing.OnPlace, out bonusBenefitID);
                            Main.DelayedCall(() => Benefit3D.Show(in bonusBenefitID, text), timeOffset);
                            _hexPosToBenefitID.Add(hesPos, bonusBenefitID);
                        }
                        else
                        {
                            Main.DelayedCall(() => Benefit3D.ChangeValue(in bonusBenefitID, text), timeOffset);
                        }
                        timeOffset++;
                    }
                }

                for (int bonusIdx = production.BonusList.Count - 1; bonusIdx >= 0; bonusIdx--)
                {
                    ref Logic.BonusStep bonusStep = ref production.BonusList[bonusIdx];
                    Data.HexPos sourceHexPos = Data.MapHelper.MapTileIDToHexPos(bonusStep.SourceTile.ID);
                    Data.HexPos targetHexPos = Data.MapHelper.MapTileIDToHexPos(bonusStep.TargetTile.ID);
                    if (_hexPosToBenefitID.TryGetValue(sourceHexPos, out int sourceBenefitID) == true && _hexPosToBenefitID.TryGetValue(targetHexPos, out int targetBenefitID) == true)
                    {
                        string text = $"{bonusStep.TargetValue.ToString()}";
                        Main.DelayedCall(() => Benefit3D.Pop(in sourceBenefitID), timeOffset);
                        Main.DelayedCall(() => Benefit3D.ChangeValue(in targetBenefitID, text), timeOffset);
                        timeOffset++;
                    }
                }

                Main.DelayedCall(() => Benefit3D.Pop(in benefitID), timeOffset);
            }
        }
    }
}
