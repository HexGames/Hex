using Godot;

namespace GodotUI
{
    public partial class UIViewDeckBtn : Control
    {
        public void Refresh(bool show)
        {
            if (show)
            {
                Visible = true;
            }
            else
            {
                Visible = false;
            }
        }

        public void OnShowDeckViewer()
        {
            //Game.x.UI.ShowDeckOverlay();
        }
    }
}