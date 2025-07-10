using Godot;
using System;

namespace Godot3D
{
    public partial class CursorTile : Node3D
    {
        private Data.HexCoord _LastHexCoord = Data.HexCoord.Invalid;
        private bool _Active = false;

        public void Activate(Data.Tile tile)
        {
            Cleanup();

            var prefab = Main.x.Assets.GetPrefab_Tiles(tile.Def.Map_TilePrefab);
            if (prefab != null)
            {
                Node3D instance = prefab.Instantiate<Node3D>();
                AddChild(instance);
            }

            _Active = true;
        }

        public void Clear()
        {
            Cleanup();

            Visible = false;
            _Active = false;
        }

        private void Cleanup()
        {
            foreach (Node node in GetChildren())
            {
                node.QueueFree();
            }
        }

        public override void _Process(double delta)
        {
            if (_Active == false)
                return;

            // Get the mouse position in the viewport
            Viewport viewport = GetViewport();
            Vector2 mousePos = viewport.GetMousePosition();

            // Get the camera (assumes there is only one Camera3D in the scene)
            Camera3D camera = GetViewport().GetCamera3D();
            if (camera == null)
                return;

            // Cast a ray from the camera through the mouse position into the xoz plane (y = 0)
            Vector3 from = camera.ProjectRayOrigin(mousePos);
            Vector3 dir = camera.ProjectRayNormal(mousePos);
            if (dir.Y == 0)
                return; // Parallel to the plane, no intersection

            // Calculate intersection with y = 0 plane
            float t = -from.Y / dir.Y;
            Vector3 intersection = from + dir * t;

            // Convert intersection point to HexCoord (assuming you have a method for this)
            // Replace HexCoord.FromWorldXZ with your actual conversion method
            Data.HexCoord hexCoord = Convert.FromWorldXZ(intersection);

            if (Game.Map.IsHexCoordOnMap(hexCoord))
            {
                // Use hexCoord as needed (e.g., update cursor position, highlight tile, etc.)
                Visible = true;
                Position = Convert.HexCoordToWorld(hexCoord) + Vector3.Up;

                if (_LastHexCoord != hexCoord)
                {
                    Actions.OnHoverDraftedTile(hexCoord);
                    _LastHexCoord = hexCoord;
                }
            }
            else
            {
                Visible = false;
                if (_LastHexCoord != Data.HexCoord.Invalid)
                {
                    Actions.OnHoverOffMap();
                }

                _LastHexCoord = Data.HexCoord.Invalid;
            }
        }

        public override void _Input(InputEvent @event)
        {
            if (_Active == false)
                return;

            if (@event is InputEventMouseButton mouseEvent)
            {
                if (mouseEvent.ButtonIndex == MouseButton.Left && mouseEvent.Pressed)
                {
                    OnLeftClick();
                }
            }
        }

        private void OnLeftClick()
        {
            Actions.OnPlayDraftedTile(_LastHexCoord);
        }
    }
}
