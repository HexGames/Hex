using Godot;
using Godot.Collections;

namespace Hex.GodotUI
{
    public partial class UIDeckViewer : Control
    {
        //[Export]
        //private Array<UICardFace> Cards;

        //private Data.Deck _Deck = null;

        public void Refresh(/*Data.Deck deck*/)
        {
            Visible = true;
        }

        public void OnClose()
        {
            Visible = false;
        }
    }
}