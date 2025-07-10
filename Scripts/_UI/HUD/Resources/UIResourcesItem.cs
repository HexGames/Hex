using Godot;

namespace GodotUI
{
    public partial class UIResourcesItem : Control
    {
        private AnimPulse PulseContainer;
        private TextureRect Icon;
        private UIText Value;
        private UIText PerTurn;
        private UITooltipTrigger ToolTip;

        private int OldValue = -1;
        private int OldPerTurn = -1;

        public override void _Ready()
        {
            PulseContainer = GetNode<AnimPulse>("PulseContainer");
            Icon = GetNode<TextureRect>("PulseContainer/Icon");
            Value = GetNode<UIText>("PulseContainer/Value");
            PerTurn = GetNode<UIText>("PulseContainer/PerTurn");
        }

        public void SetRes(Def.Res resDef, int value, int perTurn)
        {
            int pulseMagnitude = Mathf.Abs(OldValue - value) + Mathf.Abs(OldPerTurn - perTurn);
            OldValue = value;
            OldPerTurn = perTurn;

            Icon.Texture = Main.x.Assets.GetTexture2D_Icons(resDef.Image);
            Value.SetText("$", UIHelper.ResValueToString(value, 1, false, true));
            if (perTurn != 0)
            {
                PerTurn.SetText("$", UIHelper.ResValueToString(perTurn, 1, true, true));
            }
            else
            {
                PerTurn.SetText("$", "");
            }

            if (pulseMagnitude > 0)
            {
                PulseContainer.PulseAnim(pulseMagnitude);
            }
        }
    }
}