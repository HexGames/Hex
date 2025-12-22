using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace Hex.Def
{
    public struct Var
    {
        enum ValueType
        {
            Invalid,
            Bool,
            Int,
            Timing,
            Bonus,
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
            public Value(Bonus value) { Type = ValueType.Bonus; _id = (int)value; }
            public Value(Condition value) { Type = ValueType.Condition; _id = (int)value; }
            public Value(ResRef resRef) { Type = ValueType.Res; _id = resRef._id; }
            public Value(TagRef tagRef) { Type = ValueType.Tag; _id = tagRef._id; }
            public Value(TileRef tileRef) { Type = ValueType.Tile; _id = tileRef._id; }

            public bool BoolValue => _id != 0;
            public int IntValue => _id;
            public Timing TimingValue => (Timing)_id;
            public Bonus BonusValue => (Bonus)_id;
            public Condition ConditionValue => (Condition)_id;
            public ResRef ResRef => ResRef.FromID(_id);
            public TagRef TagRef => TagRef.FromID(_id);
            public TileRef TileRef => TileRef.FromID(_id);
        }

        private const int MAX_VALUE_COUNT = 8;
        private const int MAX_VALUE_ARRAY_COUNT = 8;

        [InlineArray(MAX_VALUE_COUNT)] public struct ValueArray { private Value _element; }
        [InlineArray(MAX_VALUE_ARRAY_COUNT)] public struct ValueTable { private ValueArray _element; }
        [InlineArray(MAX_VALUE_ARRAY_COUNT)] public struct CountArray { private int _element; }

        private ValueTable _values;
        private int _tableCount;
        private CountArray _listCounts;

        // ----------------- Constructor -----------------
        public Var(string def)
        {
            Parse(def);
        }

        // ----------------- Parse -----------------
        private Value ParseValue(ReadOnlySpan<char> v)
        {
            if (bool.TryParse(v, out bool b)) return new Value(b);
            if (int.TryParse(v, out int i)) return new Value(i);
            if (Enum.TryParse<Timing>(v, out Timing timing)) return new Value(timing);
            if (Enum.TryParse<Bonus>(v, out Bonus bonus)) return new Value(bonus);
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
            _tableCount = 0;
            for (int idx = 0; idx < MAX_VALUE_ARRAY_COUNT; idx++)
            {
                for (int subIdx = 0; subIdx < MAX_VALUE_COUNT; subIdx++)
                {
                    _values[idx][subIdx] = default;
                }
                _listCounts[idx] = 0;
            }

            int start = 0;
            for (int i = 0; i < defString.Length; i++)
            {
                char c = defString[i];
                if (c == ':' || c == '*')
                {
                    int subIdx = _listCounts[_tableCount];
                    _values[_tableCount][subIdx] = ParseValue(defString.AsSpan(start, i - start));
                    _listCounts[_tableCount]++;

                    start = i + 1;
                    if (c == ':') _tableCount++;
                }
            }

            if (start < defString.Length)
            {
                int subIdx = _listCounts[_tableCount];
                _values[_tableCount][subIdx] = ParseValue(defString.AsSpan(start, defString.Length - start));
                _listCounts[_tableCount]++;
            }
            _tableCount++;
        }

        // ----------------- Getters -----------------
        public bool IsBool(int index = 0, int subIndex = 0) => _values[index][subIndex].Type == ValueType.Bool;
        public bool GetBool(int index = 0, int subIndex = 0) => _values[index][subIndex].BoolValue;

        public bool IsInt(int index = 0, int subIndex = 0) => _values[index][subIndex].Type == ValueType.Int;
        public int GetInt(int index = 0, int subIndex = 0) => _values[index][subIndex].IntValue;

        public bool IsTiming(int index = 0, int subIndex = 0) => _values[index][subIndex].Type == ValueType.Timing;
        public Timing GetTiming(int index = 0, int subIndex = 0) => _values[index][subIndex].TimingValue;

        public bool IsBonus(int index = 0, int subIndex = 0) => _values[index][subIndex].Type == ValueType.Bonus;
        public Bonus GetBonus(int index = 0, int subIndex = 0) => _values[index][subIndex].BonusValue;

        public bool IsCondition(int index = 0, int subIndex = 0) => _values[index][subIndex].Type == ValueType.Condition;
        public Condition GetCondition(int index = 0, int subIndex = 0) => _values[index][subIndex].ConditionValue;

        public bool IsRes(int index = 0, int subIndex = 0) => _values[index][subIndex].Type == ValueType.Res;
        public ResRef GetRes(int index = 0, int subIndex = 0) => _values[index][subIndex].ResRef;

        public bool IsTag(int index = 0, int subIndex = 0) => _values[index][subIndex].Type == ValueType.Tag;
        public TagRef GetTag(int index = 0, int subIndex = 0) => _values[index][subIndex].TagRef;

        public bool IsTile(int index = 0, int subIndex = 0) => _values[index][subIndex].Type == ValueType.Tile;
        public TileRef GetTile(int index = 0, int subIndex = 0) => _values[index][subIndex].TileRef;

        public int GetCount() => _tableCount;
        public int GetSubCount(int listIndex) => _listCounts[listIndex];

        // ----------------- ToString -----------------
        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            for (int idx = 0; idx < _tableCount; idx++)
            {
                if (idx > 0) stringBuilder.Append(':');
                for (int subIdx = 0; subIdx < _listCounts[idx]; subIdx++)
                {
                    if (subIdx > 0) stringBuilder.Append('*');
                    switch (_values[idx][subIdx].Type)
                    {
                        case ValueType.Bool: stringBuilder.Append(_values[idx][subIdx].BoolValue); break;
                        case ValueType.Int: stringBuilder.Append(_values[idx][subIdx].IntValue); break;
                        case ValueType.Timing: stringBuilder.Append(_values[idx][subIdx].TimingValue.ToString()); break;
                        case ValueType.Bonus: stringBuilder.Append(_values[idx][subIdx].BonusValue.ToString()); break;
                        case ValueType.Condition: stringBuilder.Append(_values[idx][subIdx].ConditionValue.ToString()); break;
                        case ValueType.Res: stringBuilder.Append(_values[idx][subIdx].ResRef.Value.Name); break;
                        case ValueType.Tag: stringBuilder.Append(_values[idx][subIdx].TagRef.Value.Name); break;
                        case ValueType.Tile: stringBuilder.Append(_values[idx][subIdx].TileRef.Value.Name); break;
                    }
                }
            }
            return stringBuilder.ToString();
        }

        public string GetString(int index = 0, int subIndex = 0)
        {
            switch (_values[index][subIndex].Type)
            {
                case ValueType.Bool: return _values[index][subIndex].BoolValue.ToString();
                case ValueType.Int: return _values[index][subIndex].IntValue.ToString();
                case ValueType.Timing: return _values[index][subIndex].TimingValue.ToString();
                case ValueType.Bonus: return _values[index][subIndex].BonusValue.ToString();
                case ValueType.Condition: return _values[index][subIndex].ConditionValue.ToString();
                case ValueType.Res: return _values[index][subIndex].ResRef.Value.Name;
                case ValueType.Tag: return _values[index][subIndex].TagRef.Value.Name;
                case ValueType.Tile: return _values[index][subIndex].TileRef.Value.Name;
                default: throw new Exception("Unknown Var type");
            }
        }

        public static Var INVALID = default;
    }
}