using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Hex.Def
{
    public static partial class Lib
    {
        private static Save.Block _resRawData = null;
        private static List<Save.Block> _resRaw = new List<Save.Block>();
        private static List<Res> _res = new List<Res>();
        public static ReadOnlyCollection<Res> Res => _res.AsReadOnly();

        private static void InitResDefs()
        {
            _res.Clear();
            for (int idx = 0; idx < _resRaw.Count; idx++)
            {
                Res res = new(_resRaw[idx]);
                res.ID = idx;
                _res.Add(res);
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

        public static Res GetRes(string id)
        {
            foreach (Res res in _res)
            {
                if (res.Name == id)
                {
                    return res;
                }
            }
            return null;
        }

        public static Res GetRes(int ID)
        {
            return _res[ID];
        }

        public static Res GetRes(ReadOnlySpan<char> id)
        {
            foreach (Res res in _res)
            {
                if (id.SequenceEqual(res.Name.AsSpan()) == true)
                {
                    return res;
                }
            }
            return null;
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