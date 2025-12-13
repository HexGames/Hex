using Godot;
using System.Collections.Generic;

namespace Hex.GodotMap
{
    public static class MapTilesSubsystem
    {
        private static readonly Dictionary<Hex.Data.HexPos, Node3D> _PlacedTiles = new ();

        private const string PREFAB_ROOT = "res://Assets/Map/Prefabs/"; // Path to prefabs

        public static void SetPrefabAtHexPos(string prefabName, Hex.Data.HexPos coord)
        {
            Node3D mapTilesParentNode = MapBindings.X.MapTilesNode;
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
                mapTilesParentNode.AddChild(instance);
                _PlacedTiles[coord] = instance;
            }
            else
            {
                GD.PrintErr($"Prefab not found: {prefabPath}");
            }
        }
    }
}