using Godot;
using Hex;

namespace Hex.GodotUI
{
    public partial class UITileInfo3D : Control
    {
        private UIText _text;

        public override void _Ready()
        {
            _text = GetNode<UIText>("Bg/Text");
        }

        public void Show(Hex.Data.HexPos atHexPos, string text)
        {
            Vector3 worldPos = GodotMap.Convert.HexPosToWorld(atHexPos) + 0.3f * Vector3.Up;
            Vector2 screenPos = GodotMap.Convert.WorldToScreen(worldPos);
            Position = screenPos;
            _text.SetText("$", text);
        }

        public void Refresh(string text)
        {
            _text.SetText("$", text);
        }

        public void Hide()
        {
            Visible = false;
        }
    }
}
