using Godot;

namespace GodotUI
{
    public partial class UIVictory : Control
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

        public void OnBackToMenu()
        {
            Actions.OnEndRun();
        }
    }
}