using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Def
{
    public static partial class Lib
    {
        private static Save.Block _ResRawData = null;
        private static List<Save.Block> _ResRaw = new List<Save.Block>();
        private static List<Res> _Res = new List<Res>();
        public static ReadOnlyCollection<Res> Res => _Res.AsReadOnly();

        private static void InitResDefs()
        {
            _Res.Clear();
            for (int idx = 0; idx < _ResRaw.Count; idx++)
            {
                Res res = new(_ResRaw[idx]);
                res.IDX = idx;
                _Res.Add(res);
            }
        }

        private static Save.Block GetRawRes(string id)
        {
            foreach (Save.Block resData in _ResRaw)
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
            foreach (Res res in _Res)
            {
                if (res.ID == id)
                {
                    return res;
                }
            }
            return null;
        }

        private static void SaveResDef()
        {
            Save.Data.SaveToFile(_ResRawData, "Defs/Res.mod");
        }

        private static void LoadResDef()
        {
            _ResRawData = Save.Data.LoadCSV("Defs/Res.table");

            _ResRaw.Clear();
            _ResRaw = _ResRawData.GetSubs("Res");
        }
    }
}