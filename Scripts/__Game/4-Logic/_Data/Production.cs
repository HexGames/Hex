using System.Collections.Generic;

namespace Hex.Logic
{
    public class Production
    {
        public List<Data.Bonus> BonusTree;
        public Data.Res Total;

        public Production()
        {
            BonusTree = new List<Data.Bonus>(8);
            Total = Data.Res.INVALID;
        }

        public void Clear()
        {
            BonusTree.Clear();
            Total = Data.Res.INVALID;
        }

        internal static List<Production> OnPlaceProduction = new List<Production>();
        internal static int OnPlaceProductionCount;
        internal static List<Production> OnEndTurnProduction = new List<Production>(); 
        internal static int OnEndTurnProductionCount;
    }
}
