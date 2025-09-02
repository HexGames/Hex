using Godot;
using Godot.Collections;
using System.Collections.Generic;

namespace GodotUI
{
    public partial class UINextTileItem : Control
    {
        private Camera3D _Camera;
        private Node3D _PrefabParent;

        static private int _InstanceNumber = 0;

        public override void _Ready()
        {
            _Camera = GetNode<Camera3D>("Button/BtnOffset/Content/SubViewportContainer/SubViewport/Node3D/Camera3D");
            _PrefabParent = GetNode<Node3D>("Button/BtnOffset/Content/SubViewportContainer/SubViewport/Node3D/HexParent");

            // clear 3d
            foreach (Node node in _PrefabParent.GetChildren())
            {
                node.QueueFree();
            }

            // set the masks
            _Camera.CullMask = (uint)1 << (_InstanceNumber + 1); // offset by 1 to avoid normal geometry
            _InstanceNumber++;

            // set the mask to what is left - mostly the Light
            foreach (VisualInstance3D visual in GetAllVisualInstances(this))
            {
                visual.Layers = _Camera.CullMask;
                visual.Visible = true;
            }
        }

        public void Refresh(Data.Tile tile)
        {

            // clean 3d tiles
            foreach (Node node in _PrefabParent.GetChildren())
            {
                node.QueueFree();
            }

            // add new 3d tile
            var prefab = Main.x.Assets.GetPrefab_Tiles(tile.Def.Map_TilePrefab);
            if (prefab != null)
            {
                Node3D instance = prefab.Instantiate<Node3D>();
                foreach (VisualInstance3D visual in GetAllVisualInstances(instance))
                {
                    visual.Layers = _Camera.CullMask;
                }
                _PrefabParent.AddChild(instance);
            }
        }

        private List<VisualInstance3D> GetAllVisualInstances(Node node)
        {
            var result = new List<VisualInstance3D>();
            if (node is VisualInstance3D vi)
                result.Add(vi);

            foreach (Node child in node.GetChildren())
                result.AddRange(GetAllVisualInstances(child));

            return result;
        }

        private void OnLeftClick()
        {
            EmitSignal("LeftClick");
        }
    }
}
