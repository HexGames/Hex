using Godot;

namespace Hex.GodotUI
{
    public partial class UIMain : Control
    {
        public static UIMain X = null;

        [ExportCategory("UIs")]
        [Export]
        public UIMainMenu Menu;
        [Export]
        public UITileInfo TileInfo;
        [Export]
        public UINextTile NextTiles;
        [Export]
        public UIResources Resoruces;
        [Export]
        public UITurn Turn;
        [Export]
        public UIText DebugText;

        [ExportCategory("Overlays")]
        [Export]
        public UIDeckViewer DeckViewer;

        [ExportCategory("Prototypes")]
        [Export]
        public UITileInfo3D TileInfoPrototype;
        [Export]
        public Benefit3DControl Benefit3DPrototype;

        public override void _Ready()
        {
            if (X != null)
                Debug.LogError("[UIMain] UIMain.X is already set!");

            X = this;

            Menu.Visible = false;
            Resoruces.Visible = false;
            Turn.Visible = false;
            TileInfo.Visible = false;
            NextTiles.Visible = false;

            SceneTreeTimer timer = GetTree().CreateTimer(0.5f);
            timer.Timeout += StartDelayed;
        }

        public void StartDelayed()
        {
            Menu.ShowAnim();
        }
    }
}