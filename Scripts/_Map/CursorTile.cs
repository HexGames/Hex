using Godot;
using System;

namespace Godot3D
{
    public partial class CursorTile : Node3D
    {
        private Viewport _viewport;
        private Camera3D _camera;

        private Data.HexCoord _lastHexCoord = Data.HexCoord.Invalid;
        private Node3D _instance = null;

        public override void _Ready()
        {
            _viewport = GetViewport();
            _camera = _viewport.GetCamera3D();
        }

        public void Activate(Data.Tile tile)
        {
            Cleanup();

            var prefab = Main.x.Assets.GetPrefab_Tiles(tile.Def.Map_TilePrefab);
            if (prefab != null)
            {
                _instance = prefab.Instantiate<Node3D>();
                _instance.Position = Vector3.Up;
                AddChild(_instance);
            }
        }

        public void Clear()
        {
            Cleanup();

            Visible = false;
        }

        private void Cleanup()
        {
            if (_instance == null)
                return;

            _instance.QueueFree();
            _instance = null;
        }

        public override void _Process(double delta)
        {
            Visible = false;

            if (_instance == null)
                return;

            Visible = true;

            // Get the mouse position in the viewport
            Vector2 mousePos = _viewport.GetMousePosition();

            // Cast a ray from the camera through the mouse position into the xoz plane (y = 0)
            Vector3 from = _camera.ProjectRayOrigin(mousePos);
            Vector3 dir = _camera.ProjectRayNormal(mousePos);
            if (dir.Y == 0)
                return; // Parallel to the plane, no intersection

            // Calculate intersection with y = 0 plane
            float t = -from.Y / dir.Y;
            Vector3 intersection = from + dir * t;

            Data.HexCoord hexCoord = Convert.WorldToHexCoord(intersection);

            if (Game.Map.IsHexCoordOnMap(hexCoord))
            {
                Visible = true;
                Position = Convert.HexCoordToWorld(hexCoord);

                if (_lastHexCoord != hexCoord)
                {
                    Actions.OnHoverCurrentTile(hexCoord);
                    _lastHexCoord = hexCoord;
                }
                if (Actions.IsHoverValid(hexCoord))
                {
                    _instance.Visible = true;
                }
                else
                {
                    _instance.Visible = false;
                }

                Main.x.UIInstance.DebugText.SetText("$", $"{hexCoord}");
            }
            else
            {
                Visible = false;
                if (_lastHexCoord != Data.HexCoord.Invalid)
                {
                    Actions.OnHoverInvalid();
                }

                _lastHexCoord = Data.HexCoord.Invalid;
            }
        }

        public override void _Input(InputEvent @event)
        {
            if (_instance == null)
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
            Actions.OnPlayCurrentTile(_lastHexCoord);
        }
    }
}
