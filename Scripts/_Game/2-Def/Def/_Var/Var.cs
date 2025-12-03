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
            Bool,
            Int,
            Timing,
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
            public Value(Res value) { Type = ValueType.Res; _id = value.ID; }
            public Value(Tag value) { Type = ValueType.Tag; _id = value.ID; }
            public Value(Tile value) { Type = ValueType.Tile; _id = value.ID; }

            public bool BoolValue => _id != 0;
            public int IntValue => _id;
            public Timing TimingValue => (Timing)_id;
            public Res ResRef => Lib.GetRes(_id);
            public Tag TagRef => Lib.GetTag(_id);
            public Tile TileRef => Lib.GetTile(_id);
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
            if (Enum.TryParse<Timing>(v, out Timing t)) return new Value(t);

            Res res = Lib.GetRes(v);
            if (res != null) return new Value(res);
            Tag tag = Lib.GetTag(v);
            if (tag != null) return new Value(tag);
            Tile tile = Lib.GetTile(v);
            if (tile != null) return new Value(tile);

            throw new Exception($"Unknown value: {v.ToString()}");
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

        public bool IsRes(int index = 0, int subIndex = 0) => _values[index][subIndex].Type == ValueType.Res;
        public Res GetRes(int index = 0, int subIndex = 0) => _values[index][subIndex].ResRef;

        public bool IsTag(int index = 0, int subIndex = 0) => _values[index][subIndex].Type == ValueType.Tag;
        public Tag GetTag(int index = 0, int subIndex = 0) => _values[index][subIndex].TagRef;

        public bool IsTile(int index = 0, int subIndex = 0) => _values[index][subIndex].Type == ValueType.Tile;
        public Tile GetTile(int index = 0, int subIndex = 0) => _values[index][subIndex].TileRef;

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
                        case ValueType.Res: stringBuilder.Append(_values[idx][subIdx].ResRef.Name); break;
                        case ValueType.Tag: stringBuilder.Append(_values[idx][subIdx].TagRef.Name); break;
                        case ValueType.Tile: stringBuilder.Append(_values[idx][subIdx].TileRef.Name); break;
                    }
                }
            }
            return stringBuilder.ToString();
        }
    }
}