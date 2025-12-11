using Godot;
using System;

namespace Godot3D
{
    public partial class CursorTile : Node3D
    {
        private Viewport _viewport;
        private Camera3D _camera;

        private Hex.Data.HexPos _lastHexPos = Hex.Data.HexPos.Invalid;
        private Node3D _instance = null;

        public override void _Ready()
        {
            _viewport = GetViewport();
            _camera = _viewport.GetCamera3D();
        }

        public void Activate(Hex.Data.DeckTile deckTile)
        {
            Cleanup();

            var prefab = Main.x.Assets.GetPrefab_Tiles(deckTile.Def.Map_TilePrefab);
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

            Hex.Data.HexPos hexPos = Convert.WorldToHexPos(intersection);

            if (Hex.Data.MapHelper.IsHexPosOnMap(hexPos))
            {
                Visible = true;
                Position = Convert.HexPosToWorld(hexPos);

                if (_lastHexPos != hexPos)
                {
                    _lastHexPos = hexPos;
                }
                //if (Actions.IsHoverValid(hexPos))
                //{
                //    Actions.OnHoverCurrentTile(hexPos);
                    _instance.Visible = true;
                //}
                //else
                //{
                //    Actions.OnHoverBlocked();
                //    _instance.Visible = false;
                //}

                Main.x.UIInstance.DebugText.SetText("$", $"{hexPos}");
            }
            else
            {
                Visible = false;
                if (_lastHexPos != Hex.Data.HexPos.Invalid)
                {
                    //Actions.OnHoverInvalid();
                }

                _lastHexPos = Hex.Data.HexPos.Invalid;
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
            Hex.Play.Game.InputPlayTile(_lastHexPos);
            //Actions.OnPlayCurrentTile(_lastHexPos);
        }
    }
}
