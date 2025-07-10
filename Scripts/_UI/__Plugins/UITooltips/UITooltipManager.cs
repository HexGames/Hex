using Godot;
using Godot.Collections;

namespace GodotUI
{
    public partial class UITooltipManager : Control
    {
        private static UITooltipManager _instance;
        public static UITooltipManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    GD.PushError("UITooltipManager instance not found. Make sure it is added to the scene.");
                }
                return _instance;
            }
        }

        [Export]
        public Array<UITooltip> Tooltips;

        public override void _Ready()
        {
            if (_instance != null && _instance != this)
            {
                GD.PushWarning("Multiple UITooltipManager instances detected. Only one should exist.");
                QueueFree();
                return;
            }
            _instance = this;
        }

        public UITooltip GetTooltip()
        {
            return Tooltips[0];
        }
    }
}