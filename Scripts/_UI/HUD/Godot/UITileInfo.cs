using Godot;
using Godot.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Hex.GodotUI
{
    public partial class UITileInfo : AnimControl
    {
        [Export]
        private UIText _Title;
        [Export]
        private UIText _Type;
        [Export]
        private UIInfoSection _InfoSection;

        public void Refresh(Hex.Data.DeckTile tile, List<UIInfoSection.Texts> titlesAndDescriptions)
        {
            _Title.SetText("$name", tile.Def.UI_Title);
            _Type.SetText("$type", string.Join(", ", tile.Def.BuildingTags));

            _InfoSection.SetTexts(titlesAndDescriptions);
        }

        //public void RefreshEffects(Data.Tile tile)
        //{
        //    _Title.SetText("$name", tile.Def.UI_Title);
        //    _Type.SetText("$type", string.Join(", ", tile.Def.Tags));
        //
        //    // TO DO
        //    //GetTextsForEffects(tile.Effects, _TitlesAndDescriptions);
        //
        //    _InfoSection.SetTexts(_TitlesAndDescriptions);
        //
        //    Refresh(true);
        //}

        // -------------------------------------------------------------------------------------------- static helpers

        /// toInfoTexts will be cleared

    }
}