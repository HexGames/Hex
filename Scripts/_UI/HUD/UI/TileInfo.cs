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

            AddSectionForEffects(tile, Def.Timing.OnPlace, "Instant");

            AddSectionForEffects(tile, Def.Timing.PerTurn, "PerTurn");

            AddSectionForEffects(tile, Def.Timing.Always, "Always");

            GodotUI.UIMain.X.TileInfo.Refresh(tile, _TitlesAndDescriptions);
        }

        private static void AddSectionForEffects(Data.Tile tile, Def.Timing timingFilter, string title)
        {
            GodotUI.UIInfoSection.Texts text = new GodotUI.UIInfoSection.Texts();
            
            text.Title = title;
            text.Description = Logic.Effects.GetTileEddectsDescription(tile, timingFilter);

            if (text.Description.Length > 0) _TitlesAndDescriptions.Add(text);
        }
    }
}
