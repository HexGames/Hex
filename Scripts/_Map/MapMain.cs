using Godot;
using System.Collections.Generic;

namespace Godot3D
{
    public partial class MapMain : Node3D
    {
        [Export]
        private CursorTile _TileCursor;
        [Export]
        private BenefitPopUp benefitPopUpPrototype;
        private List<BenefitPopUp> BenefitPopUpPool;

        private readonly System.Collections.Generic.Dictionary<Data.HexCoord, Node3D> _PlacedTiles = new();


        private const string PREFAB_ROOT = "res://Assets/Map/Prefabs/"; // Path to prefabs
        
        public override void _Ready()
        {
            BenefitPopUpPool.Add(benefitPopUpPrototype);
        }

        public void Refresh()
        {
            Data.Map map = Game.Map;

            foreach (Data.Tile tile in map.TilesInPlay)
            {
                Data.HexCoord coord = map.GetCoord(tile);

                SetPrefabAtHexCoord(tile.Def.Map_TilePrefab, coord);
            }
        }

        public void InitCursorTile(Data.Tile tile)
        {
            _TileCursor.Activate(tile);
        }

        public void OnPlayTile(Data.Tile tile, Data.HexCoord hexCoord)
        {
            SetPrefabAtHexCoord(tile.Def.Map_TilePrefab, hexCoord);
            _TileCursor.Clear();
        }

        public void PopUpBenefit(Data.HexCoord coord, string text)
        {
            // Step 1: Convert HexCoord to 3D world position
            Vector3 worldPos = Convert.HexCoordToWorld(coord);

            // Step 2: Convert 3D world position to 2D screen position
            var viewport = GetViewport();
            var camera = viewport.GetCamera3D();
            if (camera == null)
            {
                GD.PrintErr("No Camera3D found in viewport for BenefitPopUp.");
                return;
            }
            Vector2 screenPos = camera.UnprojectPosition(worldPos);

            // Step 3: Find a non-animating BenefitPopUp or create a new one
            BenefitPopUp popup = null;
            for (int i = 0; i < BenefitPopUpPool.Count; i++)
            {
                if (BenefitPopUpPool[i].IsAnimating() == false)
                {
                    popup = BenefitPopUpPool[i];
                    break;
                }
            }

            if (popup == null)
            {
                popup = BenefitPopUpPool[0].Duplicate(7) as BenefitPopUp;
                BenefitPopUpPool[0].GetParent().AddChild(popup);
                BenefitPopUpPool.Add(popup);
            }

            // Step 4: Show the popup at the screen position with the given text
            popup.ShowPopUp(screenPos, text);
        }

        private void SetPrefabAtHexCoord(string prefabName, Data.HexCoord coord)
        {
            // Check if a tile already exists at this coordinate
            if (_PlacedTiles.TryGetValue(coord, out var existingNode))
            {
                // If the prefab is the same, do nothing
                if (existingNode.Name == prefabName)
                    return;

                // Otherwise, remove the old node
                existingNode.QueueFree();
                _PlacedTiles.Remove(coord);
            }

            string prefabPath = PREFAB_ROOT + prefabName + ".tscn";
            var prefab = GD.Load<PackedScene>(prefabPath);
            if (prefab != null)
            {
                var instance = prefab.Instantiate<Node3D>();
                // Use prefabName as the node's name for comparison
                instance.Name = prefabName;
                instance.Position = Convert.HexCoordToWorld(coord);
                AddChild(instance);
                _PlacedTiles[coord] = instance;
            }
            else
            {
                GD.PrintErr($"Prefab not found: {prefabPath}");
            }
        }
    }
}
