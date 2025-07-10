using Godot;

namespace GodotUI
{
    public partial class UIMain : Control
    {
        [ExportCategory("UIs")]
        [Export]
        public UIMenu Menu;
        //[Export]
        //public UIBeforeRound BeforeRound;
        //[Export]
        //public UIViewDeckBtn ViewDeckBtn;
        //[Export]
        //public UIEvent WEvent;
        [Export]
        public UITileInfo TileInfo;
        [Export]
        public UIDraft Draft;
        [Export]
        public UIResources Resoruces;
        [Export]
        public UITurn Turn;
        [Export]
        public UIVictory WVictory;
        [Export]
        public UIText DebugText;

        [ExportCategory("Overlays")]
        [Export]
        public UIDeckViewer DeckViewer;

        public override void _Ready()
        {
            Menu.Visible = false;
            //ViewDeckBtn.Visible = false;
            Resoruces.Visible = false;
            Turn.Visible = false;
            TileInfo.Visible = false;
            Draft.Visible = false;
            //WEvent.Visible = false;
            WVictory.Visible = false;

            SceneTreeTimer timer = GetTree().CreateTimer(0.5f);
            timer.Timeout += StartDelayed;
        }

        public void StartDelayed()
        {
            Menu.ShowAnim();
        }

        //public void ShowDeckOverlay()
        //{
        //    if (Logic.Run.X != null)
        //    {
        //        DeckViewer.Refresh();
        //    }
        //}
    }
}