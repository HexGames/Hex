
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
            int newSize = OnPlaceProduction.Length + 4;
            Production[] newArray = new Production[newSize];
            for (int i = 0; i < OnPlaceProduction.Length; i++)
            {
                newArray[i] = OnPlaceProduction[i];
            }
            OnPlaceProduction = newArray;
        }

        internal static void GrowOnEndTurnProduction()
        {
            int newSize = OnEndTurnProduction.Length + 8;
            Production[] newArray = new Production[newSize];
            for (int i = 0; i < OnEndTurnProduction.Length; i++)
            {
                newArray[i] = OnEndTurnProduction[i];
            }
            OnEndTurnProduction = newArray;
        }

        internal static void ClearOnPlaceProduction()
        {
            for (int idx = 0; idx < OnPlaceProductionCount; idx++)
            {
                ref Production production = ref OnPlaceProduction[idx];
                production.BonusList.Clear();
                production.Total = Data.Res.INVALID;
            }
            OnPlaceProductionCount = 0;
        }

        internal static void ClearOnEndTurnProduction()
        {
            for (int idx = 0; idx < OnEndTurnProductionCount; idx++)
            {
                ref Production production = ref OnEndTurnProduction[idx];
                production.BonusList.Clear();
                production.Total = Data.Res.INVALID;
            }
            OnEndTurnProductionCount = 0;
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
