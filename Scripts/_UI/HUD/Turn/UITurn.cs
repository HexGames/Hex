using Godot;

namespace GodotUI
{
    public partial class UITurn : Control
    {
        [Export]
        private UIText Text;
        [Export]
        private Button EventBtn;
        [Export]
        private Button DraftBtn;
        [Export]
        private Button EndTurnBtn;

        public void Refresh(bool show)
        {
            if (show)
            {
                Data.Player player = Game.Player;

                //DraftBtn.ThemeTypeVariation = player.DraftPicksCurrent > 0 ? "ButtonRound" : "ButtonRoundGrey";

                //Text.SetText("$", timeline.Turn.ToString());
                //EndTurnBtn.Disabled = timeline.Event || player.DraftPicksCurrent > 0;

                Visible = true;
            }
            else
            {
                Visible = false;
            }
        }

        public void OnEndTurn()
        {
            Actions.OnEndTurn();
        }

        public void OnDraftWindow()
        {
            Actions.ShowDraftWindow();
        }
    }
}