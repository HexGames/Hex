using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Hex.Def
{
    public static partial class Lib
    {
        private static Save.Block _resRawData = null;
        private static List<Save.Block> _resRaw = new List<Save.Block>();
        private static Res[] _res = null;
        public static ReadOnlyCollection<Res> Res => _res.AsReadOnly();

        private static void InitResDefs()
        {
            _res = new Res[_resRaw.Count];
            for (int idx = 0; idx < _resRaw.Count; idx++)
            {
                _res[idx] = new Res(idx, _resRaw[idx]);
            }
        }

        private static Save.Block GetRawRes(string id)
        {
            foreach (Save.Block resData in _resRaw)
            {
                if (resData.ValueS == id)
                {
                    return resData;
                }
            }
            return null;
        }

        public static ref Res GetRes(int ID)
        {
            return ref _res[ID];
        }

        public static ResRef GetResRef(string name)
        {
            return GetResRef(name.AsSpan());
        }

        internal static ResRef GetResRef(ReadOnlySpan<char> name)
        {
            for (int idx = 0; idx < _res.Length; idx++)
            {
                if (name.SequenceEqual(_res[idx].Name))
                {
                    return ResRef.FromID(idx);
                }
            }
            return ResRef.INVALID;
        }

        private static void SaveResDef()
        {
            Save.Data.SaveToFile(_resRawData, "Defs/Res.mod");
        }

        private static void LoadResDef()
        {
            _resRawData = Save.Data.LoadCSV("Defs/Res.table");

            _resRaw.Clear();
            _resRaw = _resRawData.GetSubs("Res");
        }
    }
}