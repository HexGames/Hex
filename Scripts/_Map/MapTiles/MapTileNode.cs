using Godot;
using System.Collections.Generic;

namespace Hex.GodotMap
{
    public partial class MapTileNode : Node3D
    {
        private Node3D _availableToPlace;

        private const string PREFAB_PATH = "res://Assets/Map/Prefabs/_AvailableToPlace.tscn";

        public override void _Ready()
        {
            string prefabPath = PREFAB_PATH;
            var prefab = GD.Load<PackedScene>(prefabPath);
            if (prefab != null)
            {
                _availableToPlace = prefab.Instantiate<Node3D>();
                AddChild(_availableToPlace);
                _availableToPlace.Visible = false;
            }
        }

        public void SetAvailableToPlace(bool active)
        {
            _availableToPlace.Visible = active;
        }
    }
}
