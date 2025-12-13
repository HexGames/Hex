using Godot;
using System.Collections.Generic;

namespace Hex.GodotMap
{
    public partial class MapBindings : Node
    {
        public static MapBindings X = null;

        [Export]
        public Node3D MapTilesNode;
        [Export]
        public CursorTileNode TileCursor;

        public Viewport MainViewport;
        
        public override void _Ready()
        {
            if (X != null)
                Debug.LogError("[MapMain] MapMain.X is already set!");

            X = this;

            MainViewport = GetViewport();
        }
    }
}
