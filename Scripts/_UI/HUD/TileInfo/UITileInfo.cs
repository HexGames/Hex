using Godot;
using Godot.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GodotUI
{
    public partial class UITileInfo : AnimControl
    {
        [Export]
        private UIText _Title;
        [Export]
        private UIText _Type;
        [Export]
        private UIInfoSection _InfoSection;

        private List<UIInfoSection.Texts> _TitlesAndDescriptions = new List<UIInfoSection.Texts>(); // optimization

        public void RefreshBenefits(Data.Tile tile)
        {
            _Title.SetText("$name", tile.Def.UI_Title);
            _Type.SetText("$type", string.Join(", ", tile.Def.Data_Tags));

            GetTextsForBenefits(tile.Benefits, _TitlesAndDescriptions);

            _InfoSection.SetTexts(_TitlesAndDescriptions);

            Refresh(true);
        }

        public void RefreshEffects(Data.Tile tile)
        {
            _Title.SetText("$name", tile.Def.UI_Title);
            _Type.SetText("$type", string.Join(", ", tile.Def.Data_Tags));

            GetTextsForEffects(tile.Effects, _TitlesAndDescriptions);

            _InfoSection.SetTexts(_TitlesAndDescriptions);

            Refresh(true);
        }

        public void Refresh(bool show)
        {
            if (show)
            {
                ShowAnim();
            }
            else
            {
                HideAnim();
            }
        }

        // -------------------------------------------------------------------------------------------- static helpers

        /// toInfoTexts will be cleared
        public static void GetTextsForBenefits(List<Data.Benefit> benefits, List<UIInfoSection.Texts> toInfoTexts)
        {
            Data.Map map = Game.Map;

            toInfoTexts.Clear();

            UIInfoSection.Texts text_Instant = new UIInfoSection.Texts();
            text_Instant.Title += "Instant";
            foreach (Data.Benefit benefit in benefits)
            {
                if (benefit.BenefitTiming == Def.Timing.OnPlace)
                {
                    text_Instant.Description += UIHelper.ResToString(benefit.Res, alwaysShowSign: true, redNegativeValues: true) + "\n";
                }
            }
            //foreach (Data.Benefit benefit in benefits)
            //{
            //    if (benefit.BenefitTiming == Def.Timing.OnPlaceOther)
            //    {
            //        
            //        text_Instant.Description += UIHelper.ResToString(benefit.Res, alwaysShowSign: true, redNegativeValues: true) + " from " + map.GetTile(benefit.HexCoord).Def.UI_Title + "\n";
            //    }
            //}
            //foreach (Data.Benefit benefit in benefits)
            //{
            //    if (benefit.BenefitTiming == Def.Timing.OnDestroySelf)
            //    {
            //        text_Instant.Description += UIHelper.ResToString(benefit.Res, alwaysShowSign: true, redNegativeValues: true) + "\n";
            //    }
            //}
            //foreach (Data.Benefit benefit in benefits)
            //{
            //    if (benefit.BenefitTiming == Def.Timing.OnDestroyOther)
            //    {
            //
            //        text_Instant.Description += UIHelper.ResToString(benefit.Res, alwaysShowSign: true, redNegativeValues: true) + " from " + map.GetTile(benefit.HexCoord).Def.UI_Title + "\n";
            //    }
            //}
            if (text_Instant.Description.Length > 0) toInfoTexts.Add(text_Instant);

            UIInfoSection.Texts text_EachTurn = new UIInfoSection.Texts();
            text_EachTurn.Title += "Each Turn";
            foreach (Data.Benefit benefit in benefits)
            {
                if (benefit.BenefitTiming == Def.Timing.PerTurn)
                {
                    text_EachTurn.Description += UIHelper.ResToString(benefit.Res, alwaysShowSign: true, redNegativeValues: true) + "\n";
                }
            }
            if (text_EachTurn.Description.Length > 0) toInfoTexts.Add(text_EachTurn);
        }

        /// toInfoTexts will be cleared
        public static void GetTextsForEffects(List<Def.Var> effects, List<UIInfoSection.Texts> toInfoTexts)
        {
            toInfoTexts.Clear();

            UIInfoSection.Texts text_OnPlace = new UIInfoSection.Texts();
            text_OnPlace.Title += "On Place";
            foreach (Def.Var effect in effects)
            {
                Def.Timing timing = Logic.Effects.GetTiming(effect);
                if (timing == Def.Timing.OnPlace)
                {
                    text_OnPlace.Description += UIEffectsHelper.GetDescription(effect);
                }
            }
            if (text_OnPlace.Description.Length > 0) toInfoTexts.Add(text_OnPlace);

            //UIInfoSection.Texts text_OnPlaceOther = new UIInfoSection.Texts();
            //text_OnPlaceOther.Title += "On Place other Tile";
            //foreach (Data.Effect effect in effects)
            //{
            //    if (effect.EffectTiming == Def.Timing.OnPlaceOther)
            //    {
            //        text_OnPlaceOther.Description += UIEffectsHelper.GetDescription(effect);
            //    }
            //}
            //if (text_OnPlaceOther.Description.Length > 0) toInfoTexts.Add(text_OnPlaceOther);

            UIInfoSection.Texts text_PerTurn = new UIInfoSection.Texts();
            text_PerTurn.Title += "Per Turn";
            foreach (Def.Var effect in effects)
            {
                Def.Timing timing = Logic.Effects.GetTiming(effect);
                if (timing == Def.Timing.PerTurn)
                {
                    text_PerTurn.Description += UIEffectsHelper.GetDescription(effect);
                }
            }
            if (text_PerTurn.Description.Length > 0) toInfoTexts.Add(text_PerTurn);

            //UIInfoSection.Texts text_OnDestroy = new UIInfoSection.Texts();
            //text_OnDestroy.Title += "On Destroy";
            //foreach (Data.Effect effect in effects)
            //{
            //    if (effect.EffectTiming == Def.Timing.OnDestroySelf)
            //    {
            //        text_OnDestroy.Description += UIEffectsHelper.GetDescription(effect);
            //    }
            //}
            //if (text_OnDestroy.Description.Length > 0) toInfoTexts.Add(text_OnDestroy);
            //
            //UIInfoSection.Texts text_OnDestroyOther = new UIInfoSection.Texts();
            //text_OnDestroyOther.Title += "On Destroy other Tile";
            //foreach (Data.Effect effect in effects)
            //{
            //    if (effect.EffectTiming == Def.Timing.OnDestroyOther)
            //    {
            //        text_OnDestroyOther.Description += UIEffectsHelper.GetDescription(effect);
            //    }
            //}
            //if (text_OnDestroyOther.Description.Length > 0) toInfoTexts.Add(text_OnDestroyOther);
        }
    }
}