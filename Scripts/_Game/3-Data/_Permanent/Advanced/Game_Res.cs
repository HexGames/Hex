namespace Hex.Data
{
    public static partial class Game
    {
        // for res keep the ID == idx and the same in both Data.Game.Stockpile, Data.Game.Income and Def.Lib the same

        private static Res[] _stockpile;
        private static Res[] _income;

        public static ResList Stockpile = new ResList(_stockpile);
        public static ResList Income = new ResList(_income);

        public static void InitRes()
        {
            _stockpile = new Res[Def.Lib.Res.Count];
            _income = new Res[Def.Lib.Res.Count];
            for (int idx = 0; idx < Def.Lib.Res.Count; idx++)
            {
                _stockpile[idx] = new Res(Def.Lib.Res[idx], Def.Lib.Res[idx].Default);
                _income[idx] = new Res(Def.Lib.Res[idx], 0);
            }
        }

        // -------------------------------------------------------------------------------------------------------------- struct for res access
        public readonly struct ResList
        {
            private readonly Res[] _res = null;
            public ResList(Res[] res) => _res = res;
            public ref Res this[int id] => ref _res[id];
            public ref Res this[Def.Res resDef] => ref _res[resDef.ID];
        }
    }
}
