using System;
using System.Collections.Generic;

namespace Def
{
    public class Var
    {
        enum ValueType
        {
            Bool,
            Int,
            String,
            Res,
        };

        private struct Value
        {
            private object _value;

            public Value(bool b)
            {
                _value = b;
            }
            public Value(int i)
            {
                _value = i;
            }
            public Value(string s)
            {
                _value = s;
            }
            public Value(Timing timing)
            {
                _value = timing;
            }
            public Value(Res res)
            {
                _value = res;
            }

            public bool Is<T>() => _value is T;
            public T Get<T>() => (T)_value!;
        }

        private List<List<Value>> _values = new List<List<Value>>();

        public Var(string defString)
        {
            int lastMarkerIdx = 0;
            _values.Add(new List<Value>());
            int listIndex = 0;
            for (int charIdx = 0; charIdx < defString.Length; charIdx++)
            {
                if (defString[charIdx] == ':')
                {
                    lastMarkerIdx = AddValue(listIndex, defString, lastMarkerIdx, charIdx);

                    _values.Add(new List<Value>());
                    listIndex++;
                }
                else if (defString[charIdx] == '*')
                {
                    lastMarkerIdx = AddValue(listIndex, defString, lastMarkerIdx, charIdx);
                }
            }
            if (lastMarkerIdx < defString.Length)
            {
                lastMarkerIdx = AddValue(listIndex, defString, lastMarkerIdx, defString.Length);
            }
        }

        private int AddValue(int listIndex, string defString, int lastMarkerIdx, int charIdx)
        {
            ReadOnlySpan<char> value = defString.AsSpan(lastMarkerIdx, charIdx - lastMarkerIdx);
            _values[listIndex].Add(ParseValue(value));
            lastMarkerIdx = charIdx + 1;
            return lastMarkerIdx;
        }

        private Value ParseValue(ReadOnlySpan<char> value)
        {
            if (bool.TryParse(value, out bool boolResult))
            {
                return new Value(boolResult);
            }
            else if (int.TryParse(value, out int intResult))
            {
                return new Value(intResult);
            }
            else if (Enum.TryParse<Timing>(value, out Timing timingResult))
            {
                return new Value(timingResult);
            }
            else
            {
                Res res = Lib.GetRes(value);
                if (res != null)
                {
                    return new Value(res);
                }
                else
                {
                    return new Value(value.ToString());
                }
            }
        }

        public Var Clone()
        {
            Var clone = new Var("");
            clone._values.Clear();
            foreach (List<Value> subList in _values)
            {
                List<Value> newSubList = new List<Value>();
                foreach (Value val in subList)
                {
                    newSubList.Add(val);
                }
                clone._values.Add(newSubList);
            }
            return clone;
        }

        public bool IsBool(int index = 0, int subIndex = 0)
        {
            return _values[index][subIndex].Is<bool>();
        }
        public bool GetBool(int index = 0, int subIndex = 0)
        {
            if (_values[index][subIndex].Is<bool>() == false)
                Debug.LogError($"[Var] [{index}][{subIndex}] value is not bool! ({ToString()})");
            return _values[index][subIndex].Get<bool>();
        }

        public bool IsInt(int index = 0, int subIndex = 0)
        {
            return _values[index][subIndex].Is<int>();
        }
        public int GetInt(int index = 0, int subIndex = 0)
        {
            if (_values[index][subIndex].Is<int>() == false)
                Debug.LogError($"[Var] [{index}][{subIndex}] value is not int! ({ToString()})");
            return _values[index][subIndex].Get<int>();
        }

        public bool IsString(int index = 0, int subIndex = 0)
        {
            return _values[index][subIndex].Is<string>();
        }
        public string GetString(int index = 0, int subIndex = 0)
        {
            if (_values[index][subIndex].Is<string>() == false)
                Debug.LogError($"[Var] [{index}][{subIndex}] value is not string! ({ToString()})");
            return _values[index][subIndex].Get<string>();
        }

        public bool IsTiming(int index = 0, int subIndex = 0)
        {
            return _values[index][subIndex].Is<Timing>();
        }
        public Timing GetTiming(int index = 0, int subIndex = 0)
        {
            if (_values[index][subIndex].Is<Timing>() == false)
                Debug.LogError($"[Var] [{index}][{subIndex}] value is not Timing! ({ToString()})");
            return _values[index][subIndex].Get<Timing>();
        }

        public bool IsRes(int index = 0, int subIndex = 0)
        {
            return _values[index][subIndex].Is<Res>();
        }
        public Res GetRes(int index = 0, int subIndex = 0)
        {
            if (_values[index][subIndex].Is<Res>() == false)
                Debug.LogError($"[Var] [{index}][{subIndex}] value is not Res! ({ToString()})");
            return _values[index][subIndex].Get<Res>();
        }

        public int GetCount()
        {
            return _values.Count;
        }

        public int GetSubCount(int index)
        {
            return _values[index].Count;
        }

        public override string ToString()
        {
            string result = "";
            for (int idx = 0; idx < _values.Count; idx++)
            {
                if (idx > 0)
                    result += ":";
                for (int subIdx = 0; subIdx < _values[idx].Count; subIdx++)
                {
                    if (subIdx > 0)
                        result += "*";
                    Value val = _values[idx][subIdx];

                    if (val.Is<bool>() == true)
                    {
                        result += val.Get<bool>().ToString();
                    }
                    else if (val.Is<int>() == true)
                    {
                        result += val.Get<int>().ToString();
                    }
                    else if (val.Is<string>() == true)
                    {
                        result += val.Get<string>();
                    }
                    else if (val.Is<Res>() == true)
                    {
                        result += val.Get<Res>().ID;
                    }
                }
            }
            return result;
        }
    }
}