using Godot;

namespace GodotUI
{
    public partial class UITooltipTileTrigger : Control
    {
        public enum Orientation
        {
            UP,
            DOWN,
        };

        [ExportCategory("Setup")]
        [Export]
        private bool CanBeHovered = true;

        // Data
        private Data.Tile _Tile = null;

        [ExportCategory("Runtime Hover")]
        [Export]
        private bool _Disabled = false;
        [Export]
        private UITooltipTile _Tooltip = null;

        public void OnHoverEnter()
        {
            if (_Disabled || _Tile == null)
                return;

            // ---
            //_Tooltip = Main.x.Tooltips.GetTooltipCard();
            //_Tooltip.Refresh(_Tile);
            //_Tooltip.CanBeHovered = CanBeHovered;
            //
            //_Tooltip.Position = new Vector2(GetGlobalRect().Position.X + (Size.X / 2) - (_Tooltip.Size.X / 2), GetGlobalRect().Position.Y > 800.0f ? 416.0f : GetGlobalRect().Position.Y + _Tooltip.Size.Y - 96.0f);
            //
            //_Tooltip.TargetHoverEnter();
        }

        public void OnHoverExit()
        {
            if (_Tooltip != null)
            {
                _Tooltip.TargetHoverExit();
                _Tooltip = null;
            }
        }

        // ------------------------------------------------------------------
        public void SetCard(Data.Tile tile)
        {
            _Tile = tile;
        }
    }
}