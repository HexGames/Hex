using Godot;

namespace GodotUI
{
    public partial class UIWTileInfo : Control
    {
        private UIText _text;

        public override void _Ready()
        {
            _text = GetNode<UIText>("Bg/Text");
        }

        public void Refresh(Data.HexCoord atHexCoord, string text)
        {
            Vector3 worldPos = Godot3D.Convert.HexCoordToWorld(atHexCoord) + 0.3f * Vector3.Up;
            Vector2 screenPos = Godot3D.Convert.WorldToScreen(worldPos);
            Position = screenPos;
            _text.SetText("$", text);
        }
    }
}
