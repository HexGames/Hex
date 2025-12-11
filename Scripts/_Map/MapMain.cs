using Godot;
using System.Collections.Generic;

namespace Godot3D
{
    public partial class MapMain : Node3D
    {
        [Export]
        public WorldUI WorldUI;
        [Export]
        private CursorTile _TileCursor;
        [Export]
        private BenefitPopUp benefitPopUpPrototype;
        private List<BenefitPopUp> BenefitPopUpPool = new List<BenefitPopUp>();

        private readonly System.Collections.Generic.Dictionary<Hex.Data.HexPos, Node3D> _PlacedTiles = new();


        private const string PREFAB_ROOT = "res://Assets/Map/Prefabs/"; // Path to prefabs
        
        public override void _Ready()
        {
            BenefitPopUpPool.Add(benefitPopUpPrototype);
        }

        public void Refresh()
        {
            for (int mapIdx = 0; mapIdx < Hex.Data.Game.MapTiles.span.Length; mapIdx++)
            {
                ref Hex.Data.MapTile tile = ref Hex.Data.Game.MapTiles.span[mapIdx];
                int mapTileId = tile.MapTileID;
                Hex.Data.HexPos hexPos = Hex.Data.MapHelper.MapTileIDToHexPos(mapTileId);
                SetPrefabAtHexPos(tile.Def.Map_TilePrefab, hexPos);
            }
        }

        public void InitCursorTile(Hex.Data.DeckTile deckTile)
        {
            _TileCursor.Activate(deckTile);
        }

        public void OnPlayTile(Hex.Data.DeckTile tile, Hex.Data.HexPos hexPos)
        {
            SetPrefabAtHexPos(tile.Def.Map_TilePrefab, hexPos);
            _TileCursor.Clear();
        }

        public void PopUpBenefit(Hex.Data.HexPos coord, string text)
        {
            // Step 1: Convert HexPos to 3D world position
            Vector3 worldPos = Convert.HexPosToWorld(coord);

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

        private void SetPrefabAtHexPos(string prefabName, Hex.Data.HexPos coord)
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
                instance.Position = Convert.HexPosToWorld(coord);
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
