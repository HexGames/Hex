using System;
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

        public int Starting;
        public int Level;
        public int Weight;
        public int Initiative;
        public TerrainTagArray TerrainTags;
        public BuildingTagArray BuildingTags;
        public ConditionArray Conditions;
        public EffectArray Effects;

        public TileData(Tile tile)
        {
            Starting = tile.Starting;
            Level = tile.Level;
            Weight = tile.Weight;
            Initiative = tile.Initiative;

            TerrainTags = new TerrainTagArray(tile.TerrainTags);

            BuildingTags = new BuildingTagArray(tile.BuildingTags);

            Conditions = new ConditionArray(tile.Conditions);

            Effects = new EffectArray(tile.Effects);
        }

        // ------------------- Accessors & Arrays -----------------
        public struct TerrainTagArray
        {
            [InlineArray(MAX_TERRAIN_TAGS)] public struct TerrainTagInlineArray { private int _element0; }
            private TerrainTagInlineArray _array;
            public int _count;

            public TerrainTagArray(IReadOnlyList<string> terrainTags)
            {
                _count = terrainTags.Count;
                for (int idx = 0; idx < MAX_TERRAIN_TAGS; idx++)
                {
                    if (idx < terrainTags.Count)
                    {
                        _array[idx] = Lib.GetTagRef(terrainTags[idx])._id;
                    }
                    else
                    {
                        _array[idx] = -1;
                    }
                }
            }

            public int Count => _count;

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

            public bool HasTag(in TagRef tag)
            {
                for (int idx = 0; idx < MAX_TERRAIN_TAGS; idx++)
                {
                    if (_array[idx] == tag._id)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public struct BuildingTagArray
        {
            [InlineArray(MAX_BUILDING_TAGS)] public struct BuildingTagInlineArray { private int _element0; }
            private BuildingTagInlineArray _array;
            public int _count;

            public BuildingTagArray(IReadOnlyList<string> buildingTags)
            {
                _count = buildingTags.Count;
                for (int idx = 0; idx < MAX_BUILDING_TAGS; idx++)
                {
                    if (idx < buildingTags.Count)
                    {
                        _array[idx] = Lib.GetTagRef(buildingTags[idx])._id;
                    }
                    else
                    {
                        _array[idx] = -1;
                    }
                }
            }

            public int Count => _count;

            public Tag this[int i]
            {
                get
                {
                    return Lib.GetTag(_array[i]);
                }
            }

            public bool HasTag(in TagRef tag)
            {
                for (int idx = 0; idx < MAX_BUILDING_TAGS; idx++)
                {
                    if (_array[idx] == tag._id)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public struct ConditionArray
        {
            [InlineArray(MAX_CONDITIONS)] public struct ConditionInlineArray { private Var _element0; }
            private ConditionInlineArray _array;
            public int _count;

            public ConditionArray(IReadOnlyList<string> effects)
            {
                _count = effects.Count;
                for (int idx = 0; idx < MAX_CONDITIONS; idx++)
                {
                    if (idx < effects.Count)
                    {
                        _array[idx] = new Var(effects[idx]);
                    }
                    else
                    {
                        _array[idx] = Var.INVALID;
                    }
                }
            }

            public int Count => _count;

            public ref Var this[int i]
            {
                get => ref AsSpan()[i];
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private Span<Var> AsSpan()
            {
                return MemoryMarshal.CreateSpan(
                    ref Unsafe.As<ConditionInlineArray, Var>(ref _array),
                    MAX_CONDITIONS);
            }
        }

        public struct EffectArray
        {
            [InlineArray(MAX_EFFECTS)] public struct EffectInlineArray { private Var _element0; }
            private EffectInlineArray _array;
            public int _count;

            public EffectArray(IReadOnlyList<string> conditions)
            {
                _count = conditions.Count;
                for (int idx = 0; idx < MAX_EFFECTS; idx++)
                {
                    if (idx < conditions.Count)
                    {
                        _array[idx] = new Var(conditions[idx]);
                    }
                    else
                    {
                        _array[idx] = Var.INVALID;
                    }
                }
            }

            public int Count => _count;

            public ref Var this[int i]
            {
                get => ref AsSpan()[i];
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private Span<Var> AsSpan()
            {
                return MemoryMarshal.CreateSpan(
                    ref Unsafe.As<EffectInlineArray, Var>(ref _array),
                    MAX_EFFECTS);
            }
        }
    }
}
