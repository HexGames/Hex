using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Hex.Logic
{
    public struct Production
    {
        public Data.MapTileRef Tile;
        public Def.ResRef ResDef;
        public int LocalValue;
        public int TotalValue;
        public BonusStepListRef BonusList;
        public LocalStepListRef LocalList;

        public Production()
        {
            Tile = Data.MapTileRef.INVALID;
            ResDef = Def.ResRef.INVALID;
            LocalValue = 0;
            TotalValue = 0;
            BonusList = new BonusStepListRef();
            LocalList = new LocalStepListRef();
        }

        public void Reset(Data.MapTileRef tile, Def.ResRef resDef)
        {
            Tile = tile;
            ResDef = resDef;
            LocalValue = 0;
            TotalValue = 0;
            BonusList.Clear();
            LocalList.Clear();
        }

        public void Clear()
        {
            Tile = Data.MapTileRef.INVALID;
            ResDef = Def.ResRef.INVALID;
            LocalValue = 0;
            TotalValue = 0;
            BonusList.Clear();
            LocalList.Clear();
        }
    }
}
