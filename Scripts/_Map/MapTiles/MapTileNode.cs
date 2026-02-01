using Godot;
using System.Collections.Generic;

namespace Hex.GodotMap
{
    public partial class MapTileNode : Node3D
    {
        private Node3D _availableToPlace;
        private Node3D _benefitHighlight;
        private GpuParticles3D _benefitHighlightVFX;

        private const string PREFAB_PATH_AVAILABLE_TO_PLACE = "res://Assets/Map/Prefabs/_AvailableToPlace.tscn";
        private const string PREFAB_PATH_BENEFIT_HIGHLIGHT = "res://Assets/Map/Prefabs/_BenefitHighlight.tscn";

        public override void _Ready()
        {
            string prefabPath = PREFAB_PATH_AVAILABLE_TO_PLACE;
            var prefab = GD.Load<PackedScene>(prefabPath);
            if (prefab != null)
            {
                _availableToPlace = prefab.Instantiate<Node3D>();
                AddChild(_availableToPlace);
                _availableToPlace.Visible = false;
            }

            prefabPath = PREFAB_PATH_BENEFIT_HIGHLIGHT;
            prefab = GD.Load<PackedScene>(prefabPath);
            if (prefab != null)
            {
                _benefitHighlight = prefab.Instantiate<Node3D>();
                AddChild(_benefitHighlight);
                _benefitHighlight.Visible = false;
            }

            _benefitHighlightVFX = _benefitHighlight.GetNode<GpuParticles3D>("VFX");
        }

        public void SetAvailableToPlace(bool active)
        {
            _availableToPlace.Visible = active;
        }

        public void TriggerBenefitHighlight()
        {
            _benefitHighlightVFX.Emitting = true;
        }
    }
}
