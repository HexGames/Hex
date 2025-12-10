using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Hex.Def
{
    [StructLayout(LayoutKind.Sequential)]
    public struct TileData
    {
        public const int MAX_TERRAIN_TAGS = 4;
        public const int MAX_BUILDING_TAGS = 8;
        public const int MAX_CONDITIONS = 4;
        public const int MAX_EFFECTS = 8;

        [InlineArray(MAX_CONDITIONS)] public struct ConditionArray { private Var _element; }
        [InlineArray(MAX_EFFECTS)] public struct EffectArray { private Var _element; }

        public readonly int Starting;
        public readonly int Level;
        public readonly int Weight;
        public readonly int Initiative;
        public TerrainTagArray TerrainTags;
        public readonly BuildingTagArray BuildingTags;
        public readonly ConditionArray Conditions;
        public readonly EffectArray Effects;

        public TileData(Tile tile)
        {
            Starting = tile.Starting;
            Level = tile.Level;
            Weight = tile.Weight;
            Initiative = tile.Initiative;

            TerrainTags = new TerrainTagArray(tile.TerrainTags);

            BuildingTags = new BuildingTagArray(tile.TerrainTags);

            for (int idx = 0; idx < MAX_CONDITIONS; idx++)
            {
                if (idx < tile.Effects.Count)
                {
                    Effects[idx] = new Var(tile.Effects[idx]);
                }
            }
        }

        // ------------------- Accessors & Arrays -----------------
        public struct TerrainTagArray
        {
            [InlineArray(MAX_TERRAIN_TAGS)] public struct TerrainTagInlineArray { private int _element0; }
            private TerrainTagInlineArray _array;

            public TerrainTagArray(List<string> TerrainTags)
            {
                //_array = default; // is this necessary?

                for (int idx = 0; idx < MAX_TERRAIN_TAGS; idx++)
                {
                    if (idx < TerrainTags.Count)
                    {
                        _array[idx] = Lib.GetTag(TerrainTags[idx]).ID;
                    }
                    else
                    {
                        _array[idx] = -1;
                    }
                }
            }

            public Tag this[int i]
            {
                get
                {
                    return Lib.GetTag(_array[i]);
                }
            }

            public void AddTag(int tagID)
            {
                for (int idx = 0; idx < MAX_TERRAIN_TAGS; idx++)
                {
                    if (_array[idx] == -1)
                    {
                        _array[idx] = tagID;
                        return;
                    }
                }
            }
        }

        public struct BuildingTagArray
        {
            [InlineArray(MAX_BUILDING_TAGS)] public struct BuildingTagInlineArray { private int _element0; }
            private BuildingTagInlineArray _array;

            public BuildingTagArray(List<string> BuildingTags)
            {
                //_array = default; // is this necessary?

                for (int idx = 0; idx < MAX_BUILDING_TAGS; idx++)
                {
                    if (idx < BuildingTags.Count)
                    {
                        _array[idx] = Lib.GetTag(BuildingTags[idx]).ID;
                    }
                    else
                    {
                        _array[idx] = -1;
                    }
                }
            }

            public Tag this[int i]
            {
                get
                {
                    return Lib.GetTag(_array[i]);
                }
            }
        }
    }
}
