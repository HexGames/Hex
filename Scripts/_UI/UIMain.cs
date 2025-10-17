using Godot;

namespace GodotUI
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
        public UINextTiles NextTiles;
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
        public UIBenefitPop3D BenefitPopPrototype;

        public override void _Ready()
        {
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