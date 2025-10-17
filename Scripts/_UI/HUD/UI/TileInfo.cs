using System.Collections.Generic;

namespace UI
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

        public static void RefrehsForBenefits(Data.Tile tile, List<Data.Benefit> benefits)
        {
            _TitlesAndDescriptions.Clear();

            AddSectionForBenefits(benefits, Def.Timing.OnPlace, "Instant");

            AddSectionForBenefits(benefits, Def.Timing.PerTurn, "PerTurn");

            AddSectionForBenefits(benefits, Def.Timing.Always, "Always");

            GodotUI.UIMain.X.TileInfo.Refresh(tile, _TitlesAndDescriptions);
        }

        private static void AddSectionForBenefits(List<Data.Benefit> benefits, Def.Timing timingFilter, string title)
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

        public static void RefrehsForEffects(Data.Tile tile)
        {
            _TitlesAndDescriptions.Clear();

            AddSectionForEffects(tile.GetEffectsList(false), Def.Timing.OnPlace, "Instant");

            AddSectionForEffects(tile.GetEffectsList(false), Def.Timing.PerTurn, "PerTurn");

            AddSectionForEffects(tile.GetEffectsList(false), Def.Timing.Always, "Always");

            GodotUI.UIMain.X.TileInfo.Refresh(tile, _TitlesAndDescriptions);
        }

        private static void AddSectionForEffects(List<Data.EffectNode> effects, Def.Timing timingFilter, string title)
        {
            GodotUI.UIInfoSection.Texts text = new GodotUI.UIInfoSection.Texts();
            text.Title += title;
            foreach (Data.EffectNode effect in effects)
            {
                if (effect.Timing == timingFilter)
                {
                    text.Description += GodotUI.UIHelper.EffectVarToString(effect.EffectDef) + "\n";
                }
            }
            if (text.Description.Length > 0) _TitlesAndDescriptions.Add(text);
        }
    }
}
