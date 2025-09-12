using Godot;
using Godot.Collections;

namespace GodotUI
{
    public partial class UINextTiles : AnimControl
    {
        [Export]
        private Array<UINextTileItem> NextTiles;

        public void Refresh(bool show)
        {
            if (show)
            {
                Data.Player player = Game.Player;

                for (int idx = 0; idx < NextTiles.Count; idx++)
                {
                    if (idx < player.NextTiles.Count)
                    {
                        NextTiles[idx].Visible = true;
                        NextTiles[idx].Refresh(player.NextTiles[NextTiles.Count - 1 - idx]);
                    }
                    else
                    {
                        NextTiles[idx].Visible = false;
                    }
                }

                ShowAnim();
            }
            else
            {
                HideAnim();
            }
        }

        public void OnSkip()
        {
            //Data.Player player = Game.Player;
            //Actions.OnDraft(player.DraftTiles[idx]);
        }

        public void OnViewDeck()
        {
        }
    }
}
