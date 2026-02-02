
namespace Hex.Logic
{
    public static class ProductionPools
    {
        internal static Production[] OnPlaceProduction = null;
        internal static int OnPlaceProductionCount;
        internal static Production[] OnEndTurnProduction = null;
        internal static int OnEndTurnProductionCount;
        internal static Data.MapTileRef[] BonusQueue = null;
        internal static int BonusQueueStart;
        internal static int BonusQueueEnd;
        internal static LocalStepListRef BonusLocalSteps;

        public static void Init()
        {
            OnPlaceProduction = new Production[4];
            for (int idx = 0; idx < OnPlaceProduction.Length; idx++)
            {
                OnPlaceProduction[idx] = new Production();
            }
            OnEndTurnProduction = new Production[16];
            for (int idx = 0; idx < OnEndTurnProduction.Length; idx++)
            {
                OnEndTurnProduction[idx] = new Production();
            }
            BonusQueue = new Data.MapTileRef[128];
            BonusLocalSteps = new LocalStepListRef();
        }

        internal static void GrowOnPlaceProduction()
        {
            int oldSize = OnPlaceProduction.Length;
            int newSize = oldSize + 4;
            Production[] newArray = new Production[newSize];
            for (int i = 0; i < oldSize; i++)
            {
                newArray[i] = OnPlaceProduction[i];
            }
            for (int i = oldSize; i < newSize; i++)
            {
                newArray[i] = new Production();
            }
            OnPlaceProduction = newArray;
        }

        internal static void GrowOnEndTurnProduction()
        {
            int oldSize = OnEndTurnProduction.Length;
            int newSize = oldSize + 8;
            Production[] newArray = new Production[newSize];
            for (int i = 0; i < oldSize; i++)
            {
                newArray[i] = OnEndTurnProduction[i];
            }
            for (int i = oldSize; i < newSize; i++)
            {
                newArray[i] = new Production();
            }
            OnEndTurnProduction = newArray;
        }

        internal static void ClearOnPlaceProduction()
        {
            for (int idx = 0; idx < OnPlaceProductionCount; idx++)
            {
                ref Production production = ref OnPlaceProduction[idx];
                ClearBonusStepLocalLists(ref production);
                production.BonusList.Clear();
                production.LocalList.Clear();
                production.LocalValue = 0;
                production.TotalValue = 0;
                production.ResDef = Def.ResRef.INVALID;
            }
            OnPlaceProductionCount = 0;
        }

        internal static void ClearOnEndTurnProduction()
        {
            for (int idx = 0; idx < OnEndTurnProductionCount; idx++)
            {
                ref Production production = ref OnEndTurnProduction[idx];
                ClearBonusStepLocalLists(ref production);
                production.BonusList.Clear();
                production.LocalList.Clear();
                production.LocalValue = 0;
                production.TotalValue = 0;
                production.ResDef = Def.ResRef.INVALID;
            }
            OnEndTurnProductionCount = 0;
        }

        private static void ClearBonusStepLocalLists(ref Production production)
        {
            for (int bonusIdx = 0; bonusIdx < production.BonusList.Count; bonusIdx++)
            {
                production.BonusList[bonusIdx].LocalList.Clear();
            }
        }

        internal static void ClearBonusQueue()
        {
            for (int idx = BonusQueueStart; idx < BonusQueueEnd; idx++)
            {
                BonusQueue[idx] = Data.MapTileRef.INVALID;
            }
            BonusQueueStart = 0;
            BonusQueueEnd = 0;
        }

        internal static void BonusEnqueue(Data.MapTileRef mapTileRef)
        {
            if (BonusQueueEnd >= BonusQueue.Length)
            {
                int newSize = BonusQueue.Length + 128;
                Data.MapTileRef[] newArray = new Data.MapTileRef[newSize];
                for (int i = 0; i < BonusQueue.Length; i++)
                {
                    newArray[i] = BonusQueue[i];
                }
                BonusQueue = newArray;
            }

            BonusQueue[BonusQueueEnd] = mapTileRef;
            BonusQueueEnd++;
        }

        internal static ref Data.MapTileRef BonusDequeue()
        {
            int dequeueIndex = BonusQueueStart;
            BonusQueueStart++;
            return ref BonusQueue[dequeueIndex];
        }
    }
}
