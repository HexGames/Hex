using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Hex.Logic
{
    public struct Production
    {
        public BonusStepListRef BonusList;
        public Data.Res Total;

        public Production()
        {
            BonusList = new BonusStepListRef();
            Total = Data.Res.INVALID;
        }

        public void Clear()
        {
            BonusList.Clear();
            Total = Data.Res.INVALID;
        }
    }
}
