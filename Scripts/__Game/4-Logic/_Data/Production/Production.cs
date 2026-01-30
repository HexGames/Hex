using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Hex.Logic
{
    public struct Production
    {
        public Data.MapTileRef Tile;
        public Data.Res Total;
        public BonusStepListRef BonusList;
        public LocalStepListRef LocalList;

        public Production()
        {
            Tile = Data.MapTileRef.INVALID;
            Total = new Data.Res(Def.ResRef.INVALID);
            BonusList = new BonusStepListRef();
            LocalList = new LocalStepListRef();
        }

        public void Reset(Data.MapTileRef tile, Def.ResRef resDef)
        {
            Tile = tile;
            Total = new Data.Res(resDef);
            BonusList.Clear();
            LocalList.Clear();
        }

        public void Clear()
        {
            Tile = Data.MapTileRef.INVALID;
            Total = new Data.Res(Def.ResRef.INVALID);
            BonusList.Clear();
            LocalList.Clear();
        }
    }
}
