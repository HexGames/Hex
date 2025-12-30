using Godot;
using System.Collections.Generic;

namespace Hex.GodotMap
{
    public static class MapTilesSubsystem
    {
        private static readonly Dictionary<Data.HexPos, MapTileNode> _PlacedTiles = new ();

        private const string PREFAB_ROOT = "res://Assets/Map/Prefabs/"; // Path to prefabs

        public static void SetPrefabAtHexPos(string prefabName, Data.HexPos coord)
        {
            Node3D mapTilesParentNode = MapBindings.X.MapTilesNode;
            // Check if a tile already exists at this coordinate
            if (_PlacedTiles.TryGetValue(coord, out MapTileNode existingNode))
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
                var instance = prefab.Instantiate<MapTileNode>();
                // Use prefabName as the node's name for comparison
                instance.Name = prefabName;
                instance.Position = Convert.HexPosToWorld(coord);
                mapTilesParentNode.AddChild(instance);
                _PlacedTiles[coord] = instance;
            }
            else
            {
                GD.PrintErr($"Prefab not found: {prefabPath}");
            }
        }

        public static void SetAvailableToPlaceAtHexPos(Data.HexPos coord)
        {
            Node3D mapTilesParentNode = MapBindings.X.MapTilesNode;
            // Check if a tile already exists at this coordinate
            if (_PlacedTiles.TryGetValue(coord, out MapTileNode existingNode))
            {
                existingNode.SetAvailableToPlace(true);
            }
        }

        public static void ClearAllAvailableForPlace()
        {
            foreach (MapTileNode existingNode in _PlacedTiles.Values)
            {
                existingNode.SetAvailableToPlace(false);
            }
        }
    }
}