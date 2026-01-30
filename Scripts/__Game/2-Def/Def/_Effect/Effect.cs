using System;
using System.Runtime.CompilerServices;
using System.Text;

namespace Hex.Def
{
    public struct Effect
    {
        enum ValueType
        {
            Invalid,
            Bool,
            Int,
            Timing,
            EffectType,
            Condition,
            Tag, // ref
            Res, // ref
            Tile // ref
        };

        private struct Value
        {
            public ValueType Type;

            private int _id;

            public Value(bool value) { Type = ValueType.Bool; _id = value ? 1 : 0; }
            public Value(int value) { Type = ValueType.Int; _id = value; }
            public Value(Timing value) { Type = ValueType.Timing; _id = (int)value; }
            public Value(EffectType value) { Type = ValueType.EffectType; _id = (int)value; }
            public Value(Condition value) { Type = ValueType.Condition; _id = (int)value; }
            public Value(ResRef resRef) { Type = ValueType.Res; _id = resRef._id; }
            public Value(TagRef tagRef) { Type = ValueType.Tag; _id = tagRef._id; }
            public Value(TileRef tileRef) { Type = ValueType.Tile; _id = tileRef._id; }

            public bool BoolValue => _id != 0;
            public int IntValue => _id;
            public Timing TimingValue => (Timing)_id;
            public EffectType BonusValue => (EffectType)_id;
            public Condition ConditionValue => (Condition)_id;
            public ResRef ResRef => ResRef.FromID(_id);
            public TagRef TagRef => TagRef.FromID(_id);
            public TileRef TileRef => TileRef.FromID(_id);
        }

        private const int MAX_VALUE_COUNT = 8;

        [InlineArray(MAX_VALUE_COUNT)] public struct ValueArray { private Value _element; }

        private ValueArray _values;
        private int _count;

        // ----------------- Constructor -----------------
        public Effect(string def)
        {
            Parse(def);
        }

        // ----------------- Parse -----------------
        private Value ParseValue(ReadOnlySpan<char> v)
        {
            if (bool.TryParse(v, out bool b)) return new Value(b);
            if (int.TryParse(v, out int i)) return new Value(i);
            if (Enum.TryParse<Timing>(v, out Timing timing)) return new Value(timing);
            if (Enum.TryParse<EffectType>(v, out EffectType bonus)) return new Value(bonus);
            if (Enum.TryParse<Condition>(v, out Condition condition)) return new Value(condition);

            ResRef resRef = Lib.GetResRef(v);
            if (resRef != ResRef.INVALID) return new Value(resRef);
            TagRef tagRef = Lib.GetTagRef(v);
            if (tagRef != TagRef.INVALID) return new Value(tagRef);
            TileRef tileRef = Lib.GetTileRef(v);
            if (tileRef != TileRef.INVALID) return new Value(tileRef);

            Debug.LogError($"[VAR] Unknown value - {v.ToString()}");
            return new Value(0);
        }

        private void Parse(string defString)
        {
            _count = 0;
            for (int idx = 0; idx < MAX_VALUE_COUNT; idx++)
            {
                _values[idx] = default;
            }

            int start = 0;
            for (int i = 0; i < defString.Length; i++)
            {
                char c = defString[i];
                if (c == ':')
                {
                    _values[_count] = ParseValue(defString.AsSpan(start, i - start));
                    _count++;

                    start = i + 1;
                }
            }

            if (start < defString.Length)
            {
                _values[_count] = ParseValue(defString.AsSpan(start, defString.Length - start));
                _count++;
            }
        }

        // ----------------- Specific Getter -----------------
        public Timing GetTiming() => _values[0].TimingValue;
        public EffectType GetEffectType() => _values[1].BonusValue;

        // ----------------- Getters -----------------
        public bool IsBool(int idx) => _values[idx].Type == ValueType.Bool;
        public bool GetBool(int idx) => _values[idx].BoolValue;

        public bool IsInt(int idx) => _values[idx].Type == ValueType.Int;
        public int GetInt(int idx) => _values[idx].IntValue;

        public bool IsTiming(int idx) => _values[idx].Type == ValueType.Timing;
        public Timing GetTiming(int idx) => _values[idx].TimingValue;

        public bool IsBonus(int idx) => _values[idx].Type == ValueType.EffectType;
        public EffectType GetBonus(int idx) => _values[idx].BonusValue;

        public bool IsCondition(int idx) => _values[idx].Type == ValueType.Condition;
        public Condition GetCondition(int idx) => _values[idx].ConditionValue;

        public bool IsRes(int idx) => _values[idx].Type == ValueType.Res;
        public ResRef GetRes(int idx) => _values[idx].ResRef;

        public bool IsTag(int idx) => _values[idx].Type == ValueType.Tag;
        public TagRef GetTag(int idx) => _values[idx].TagRef;

        public bool IsTile(int idx) => _values[idx].Type == ValueType.Tile;
        public TileRef GetTile(int idx) => _values[idx].TileRef;

        public int GetCount() => _count;

        // ----------------- ToString -----------------
        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            for (int idx = 0; idx < _count; idx++)
            {
                if (idx > 0) stringBuilder.Append(':');
                switch (_values[idx].Type)
                {
                    case ValueType.Bool: stringBuilder.Append(_values[idx].BoolValue); break;
                    case ValueType.Int: stringBuilder.Append(_values[idx].IntValue); break;
                    case ValueType.Timing: stringBuilder.Append(_values[idx].TimingValue.ToString()); break;
                    case ValueType.EffectType: stringBuilder.Append(_values[idx].BonusValue.ToString()); break;
                    case ValueType.Condition: stringBuilder.Append(_values[idx].ConditionValue.ToString()); break;
                    case ValueType.Res: stringBuilder.Append(_values[idx].ResRef.Value.Name); break;
                    case ValueType.Tag: stringBuilder.Append(_values[idx].TagRef.Value.Name); break;
                    case ValueType.Tile: stringBuilder.Append(_values[idx].TileRef.Value.Name); break;
                }
            }
            return stringBuilder.ToString();
        }

        public string GetString(int idx = 0)
        {
            switch (_values[idx].Type)
            {
                case ValueType.Bool: return _values[idx].BoolValue.ToString();
                case ValueType.Int: return _values[idx].IntValue.ToString();
                case ValueType.Timing: return _values[idx].TimingValue.ToString();
                case ValueType.EffectType: return _values[idx].BonusValue.ToString();
                case ValueType.Condition: return _values[idx].ConditionValue.ToString();
                case ValueType.Res: return _values[idx].ResRef.Value.Name;
                case ValueType.Tag: return _values[idx].TagRef.Value.Name;
                case ValueType.Tile: return _values[idx].TileRef.Value.Name;
                default: throw new Exception("Unknown Var type");
            }
        }

        public static Effect INVALID = default;
    }
}