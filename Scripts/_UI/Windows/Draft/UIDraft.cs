using Godot;
using Godot.Collections;

namespace GodotUI
{
    public partial class UIDraft : AnimControl
    {
        [Export]
        private Array<UIDraftChoice> DraftChoices;

        public void Refresh(bool show)
        {
            if (show)
            {
                Data.Player player = Game.Player;
                
                for (int idx = 0; idx < DraftChoices.Count; idx++)
                {
                    if (idx < player.DraftTiles.Count)
                    {
                        DraftChoices[idx].Visible = true;
                        DraftChoices[idx].Refresh(player.DraftTiles[idx]);
                    }
                    else
                    {
                        DraftChoices[idx].Visible = false;
                    }
                }

                ShowAnim();
            }
            else
            {
                HideAnim();
            }
        }

        public void OnDraft(int idx)
        {
            Data.Player player = Game.Player;
            Actions.OnDraft(player.DraftTiles[idx]);
        }

        public void OnClose()
        {
            Actions.HideDraftWindow();
        }
    }
}