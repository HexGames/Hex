namespace Hex.UI
{
    public static class BenefitAnimationLoop
    {
        private const float TIME_STEP = 0.5f;
        private const int MAX_ENTRIES = 64;

        private struct HexPosBenefit
        {
            public Data.HexPos HexPos;
            public int BenefitID;
        }

        private static HexPosBenefit[] _hexPosBenefits = new HexPosBenefit[MAX_ENTRIES];
        private static int _hexPosBenefitCount = 0;

        public static void PlayBenefitLoop(Logic.Production[] onPlaceProduction, out float delay)
        {
            float timeOffset = 0f;
            float timeStep = TIME_STEP;

            int steps = 0;
            for (int productionIdx = 0; productionIdx < onPlaceProduction.Length; productionIdx++)
            {
                ref Logic.Production production = ref onPlaceProduction[productionIdx];
                steps += production.LocalList.Count;
                for (int bonusIdx = 0; bonusIdx < production.BonusList.Count; bonusIdx++)
                {
                    ref Logic.BonusStep bonusStep = ref production.BonusList[bonusIdx];
                    steps += bonusStep.LocalList.Count;
                }
            }

            timeStep = TIME_STEP * (0.1f + 0.9f * float.Pow(float.Clamp(5.0f / (3 + steps), 0.0f, 1.0f), 0.5f));

            for (int productionIdx = 0; productionIdx < onPlaceProduction.Length; productionIdx++)
            {
                _hexPosBenefitCount = 0;
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
                        Benefit3D.Create(hexPos, Def.Timing.OnPlace, productionIdx, onPlaceProduction.Length, out benefitID);
                        Main.DelayedCall(() => Benefit3D.Show(in benefitID, text), timeOffset);
                        AddHexPosBenefit(hexPos, benefitID);
                    }
                    else
                    {
                        Data.HexPos hexPos = Data.MapHelper.MapTileIDToHexPos(production.Tile.ID);
                        ApplyChangeValueToAllAtHexPos(hexPos, text, timeOffset);
                    }
                    timeOffset += timeStep;
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
                            Benefit3D.Create(hesPos, Def.Timing.OnPlace, productionIdx, onPlaceProduction.Length, out bonusBenefitID);
                            Main.DelayedCall(() => Benefit3D.Show(in bonusBenefitID, text), timeOffset);
                            AddHexPosBenefit(hesPos, bonusBenefitID);
                        }
                        else
                        {
                            Data.HexPos hesPos = Data.MapHelper.MapTileIDToHexPos(bonusStep.SourceTile.ID);
                            ApplyChangeValueToAllAtHexPos(hesPos, text, timeOffset);
                        }
                        timeOffset += timeStep;
                    }
                }

                // extra time
                timeOffset += timeStep;

                for (int bonusIdx = production.BonusList.Count - 1; bonusIdx >= 0; bonusIdx--)
                {
                    ref Logic.BonusStep bonusStep = ref production.BonusList[bonusIdx];
                    Data.HexPos sourceHexPos = Data.MapHelper.MapTileIDToHexPos(bonusStep.SourceTile.ID);
                    Data.HexPos targetHexPos = Data.MapHelper.MapTileIDToHexPos(bonusStep.TargetTile.ID);
                    if (HasBenefitAtHexPos(sourceHexPos) && HasBenefitAtHexPos(targetHexPos))
                    {
                        string text = $"{bonusStep.TargetValue.ToString()}";
                        int benefitToFadeOut = PopBenefitIDAtHexPos(sourceHexPos);
                        Main.DelayedCall(() => Benefit3D.FadeOut(in benefitToFadeOut), timeOffset);
                        ApplyChangeValueToAllAtHexPos(targetHexPos, text, timeOffset);
                        timeOffset += timeStep;
                    }
                }

                // extra time
                timeOffset += 2 * timeStep;

                Data.HexPos productionHexPos = Data.MapHelper.MapTileIDToHexPos(production.Tile.ID);
                int benefitToPop = PopBenefitIDAtHexPos(productionHexPos);
                Main.DelayedCall(() => Benefit3D.Pop(in benefitToPop), timeOffset);

                // extra time
                timeOffset += timeStep;
            }

            delay = timeOffset;
        }

        private static void AddHexPosBenefit(Data.HexPos hexPos, int benefitID)
        {
            if (_hexPosBenefitCount < MAX_ENTRIES)
            {
                _hexPosBenefits[_hexPosBenefitCount].HexPos = hexPos;
                _hexPosBenefits[_hexPosBenefitCount].BenefitID = benefitID;
                _hexPosBenefitCount++;
            }
        }

        private static int PopBenefitIDAtHexPos(Data.HexPos hexPos)
        {
            for (int i = 0; i < _hexPosBenefitCount; i++)
            {
                if (_hexPosBenefits[i].HexPos == hexPos)
                {
                    int benefitID = _hexPosBenefits[i].BenefitID;
                    // remove it from the array
                    _hexPosBenefitCount--;
                    for (int j = i; j < _hexPosBenefitCount; j++)
                    {
                        _hexPosBenefits[j] = _hexPosBenefits[j + 1];
                    }

                    return benefitID;
                }
            }
            return -1;
        }

        private static void ApplyChangeValueToAllAtHexPos(Data.HexPos hexPos, string text, float timeOffset)
        {
            for (int i = 0; i < _hexPosBenefitCount; i++)
            {
                if (_hexPosBenefits[i].HexPos == hexPos)
                {
                    int id = _hexPosBenefits[i].BenefitID;
                    Main.DelayedCall(() => Benefit3D.ChangeValue(in id, text), timeOffset);
                }
            }
        }

        private static bool HasBenefitAtHexPos(Data.HexPos hexPos)
        {
            for (int i = 0; i < _hexPosBenefitCount; i++)
            {
                if (_hexPosBenefits[i].HexPos == hexPos)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
