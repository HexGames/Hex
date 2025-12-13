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

        public static void RefrehsForBenefits(Hex.Data.DeckTile tile, List<Hex.Data.Benefit> benefits)
        {
            _TitlesAndDescriptions.Clear();

            AddSectionForBenefits(benefits, Hex.Def.Timing.OnPlace, "Instant");

            AddSectionForBenefits(benefits, Hex.Def.Timing.PerTurn, "PerTurn");

            AddSectionForBenefits(benefits, Hex.Def.Timing.Always, "Always");

            GodotUI.UIMain.X.TileInfo.Refresh(tile, _TitlesAndDescriptions);
        }

        private static void AddSectionForBenefits(List<Hex.Data.Benefit> benefits, Hex.Def.Timing timingFilter, string title)
        {
            GodotUI.UIInfoSection.Texts text = new GodotUI.UIInfoSection.Texts();
            text.Title += title;
            foreach (Hex.Data.Benefit benefit in benefits)
            {
                if (benefit.BenefitTiming == timingFilter)
                {
                    text.Description += GodotUI.UIHelper.ResToString(benefit.Res, alwaysShowSign: true, redNegativeValues: true) + "\n";
                }
            }
            if (text.Description.Length > 0) _TitlesAndDescriptions.Add(text);
        }

        public static void RefrehsForEffects(Hex.Data.DeckTile tile)
        {
            _TitlesAndDescriptions.Clear();

            AddSectionForEffects(tile, Hex.Def.Timing.OnPlace, "Instant");

            AddSectionForEffects(tile, Hex.Def.Timing.PerTurn, "PerTurn");

            AddSectionForEffects(tile, Hex.Def.Timing.Always, "Always");

            GodotUI.UIMain.X.TileInfo.Refresh(tile, _TitlesAndDescriptions);
        }

        private static void AddSectionForEffects(Hex.Data.DeckTile tile, Hex.Def.Timing timingFilter, string title)
        {
            GodotUI.UIInfoSection.Texts text = new GodotUI.UIInfoSection.Texts();
            
            text.Title = title;
            text.Description = ""; //Logic.Effects.GetTileEddectsDescription(tile, timingFilter);

            if (text.Description.Length > 0) _TitlesAndDescriptions.Add(text);
        }
    }
}
