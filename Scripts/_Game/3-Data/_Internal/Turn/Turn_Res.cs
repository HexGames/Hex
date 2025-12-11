using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Hex.Data
{
    internal partial struct Turn
    {
        internal const int MAX_RES = 16;

        internal ResArray Stockpile;
        internal ResArray Income;

        public void InitRes()
        {
            for (int idx = 0; idx < Def.Lib.Res.Count; idx++)
            {
                Stockpile.Array[idx] = new Res(Def.Lib.Res[idx], Def.Lib.Res[idx].Default);
                Income.Array[idx] = new Res(Def.Lib.Res[idx], 0);
            }
        }

        // ---------------------------------------------------------------------------------- ResArray
        internal struct ResArray
        {
            [InlineArray(MAX_RES)] public struct ResArrayInlineArray { private Res _element0; }
            internal ResArrayInlineArray Array;
            internal int ResCount = 0;

            public ResArray()
            {
                //_array = default; // is this necessary?
            }
        }
    }
}
