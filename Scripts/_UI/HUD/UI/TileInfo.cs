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
                ref Def.Effect effect = ref tile.Value.DefData.Effects[effectIdx];
                if (effect.GetTiming() == timingFilter)
                {
                    if (effect.GetEffectType() == Def.EffectType.Production)
                    {
                        Def.ResRef res = effect.GetRes(2);
                        int value = effect.GetInt(3);

                        text.Description += $"+{value}{GodotUI.UIHelper.ResToString(res, value)}{res.Value.Title}";
                        text = AddConditionText(text, effect);

                        text.Description += "\n";
                    }
                    else if (effect.GetEffectType() == Def.EffectType.MultiplyAdjacent)
                    {
                        Def.TagRef bonusTag = effect.GetTag(2);
                        int value = effect.GetInt(3);

                        text.Description += $"*{value} to all {bonusTag}s";
                        text = AddConditionText(text, effect);

                        text.Description += "\n";
                    }
                }
            }

            if (text.Description.Length > 0) _TitlesAndDescriptions.Add(text);
        }

        private static GodotUI.UIInfoSection.Texts AddConditionText(GodotUI.UIInfoSection.Texts text, Def.Effect effect)
        {
            if (effect.GetCount() == 6)
            {
                Def.TagRef tag = effect.GetTag(5);
                if (effect.GetCondition(4) == Def.Condition.IfTerrain) text.Description += $" if on {tag.Value.Name}";
                else if (effect.GetCondition(4) == Def.Condition.IfTag) text.Description += $" over an {tag.Value.Name}";
                else if (effect.GetCondition(4) == Def.Condition.IfAdjacent) text.Description += $" if near {tag.Value.Name}";
                else if (effect.GetCondition(4) == Def.Condition.PerAdjacent) text.Description += $" for each adjacent {tag.Value.Name}";
                else if (effect.GetCondition(4) == Def.Condition.PerAdjacentLevel) text.Description += $" for each adjacent {tag.Value.Name} levels";
            }

            return text;
        }
    }
}
