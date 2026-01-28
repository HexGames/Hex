using System;
using System.Collections.Generic;

namespace Hex.UI
{
    public static class TileInfo
    {
        private static List<GodotUI.UIInfoSection.Texts> _TitlesAndDescriptions = new List<GodotUI.UIInfoSection.Texts>();

        // ---------------------------------------------------------------------------------------------------
        public static void Show()
        {
            GodotUI.UIMain.X.TileInfo.ShowAnim();
        }

        public static void Hide()
        {
            GodotUI.UIMain.X.TileInfo.HideAnim();
        }

        public static void RefrehsForBenefits(Data.DeckTileRef tile, ReadOnlySpan<Data.Benefit> benefits)
        {
            _TitlesAndDescriptions.Clear();

            AddSectionForBenefits(benefits, Def.Timing.OnPlace, "Instant");

            AddSectionForBenefits(benefits, Def.Timing.PerTurn, "PerTurn");

            AddSectionForBenefits(benefits, Def.Timing.Always, "Always");

            GodotUI.UIMain.X.TileInfo.Refresh(tile, _TitlesAndDescriptions);
        }

        private static void AddSectionForBenefits(ReadOnlySpan<Data.Benefit> benefits, Def.Timing timingFilter, string title)
        {
            GodotUI.UIInfoSection.Texts text = new GodotUI.UIInfoSection.Texts();
            text.Title += title;
            foreach (Data.Benefit benefit in benefits)
            {
                if (benefit.BenefitTiming == timingFilter)
                {
                    text.Description += GodotUI.UIHelper.ResToString(benefit.Res, alwaysShowSign: true, redNegativeValues: true) + "\n";
                }
            }
            if (text.Description.Length > 0) _TitlesAndDescriptions.Add(text);
        }

        public static void RefrehsForEffects(Data.DeckTileRef tile)
        {
            _TitlesAndDescriptions.Clear();

            AddSectionForEffects(tile, Def.Timing.OnPlace, "Instant");

            AddSectionForEffects(tile, Def.Timing.PerTurn, "PerTurn");

            AddSectionForEffects(tile, Def.Timing.Always, "Always");

            GodotUI.UIMain.X.TileInfo.Refresh(tile, _TitlesAndDescriptions);
        }

        private static void AddSectionForEffects(Data.DeckTileRef tile, Def.Timing timingFilter, string title)
        {
            GodotUI.UIInfoSection.Texts text = new GodotUI.UIInfoSection.Texts();
            
            text.Title = title;
            
            for (int effectIdx = 0; effectIdx < tile.Value.DefData.Effects.Count; effectIdx++)
            {
                ref Def.Var effect = ref tile.Value.DefData.Effects[effectIdx];
                if (effect.IsTiming(0) == true && effect.GetTiming(0) == timingFilter && effect.GetCount() > 1)
                {
                    if (effect.IsRes(1) == true)
                    {
                        Def.ResRef res = effect.GetRes(1);
                        for (int idx = 2; idx < effect.GetCount(); idx++)
                        {
                            if (effect.IsInt(idx, 0) == true)
                            {
                                if (effect.Is)
                            }
                        }
                    }
                }
            }

            if (text.Description.Length > 0) _TitlesAndDescriptions.Add(text);
        }
    }
}
