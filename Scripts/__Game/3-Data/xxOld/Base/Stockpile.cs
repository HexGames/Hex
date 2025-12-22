using System.Collections.Generic;
using Hex;

namespace Hex.Logic
{
    public static class Stockpile
    {
        // -----------------------------------------------------------------------------------------
        public static int GetValue(List<Data.Res> stockpilem, Def.ResRef res)
        {
            foreach (Data.Res stockRes in stockpilem)
            {
                if (stockRes.Def == res)
                {
                    return stockRes.Value;
                }
            }
            return 0;
        }

        // -----------------------------------------------------------------------------------------
        public static void AddResToStockpile(List<Data.Res> stockpile, Data.Res res)
        {
            for (int resIdx = 0; resIdx < stockpile.Count; resIdx++)
            {
                if (stockpile[resIdx].Def == res.Def)
                {
                    Data.Res valueRes = stockpile[resIdx];
                    valueRes.Value += res.Value;
                    stockpile[resIdx] = valueRes;
                    return;
                }
            }
            // If not found, add new Res to stockpile
            stockpile.Add(res);
        }

        // -----------------------------------------------------------------------------------------
        public static void AddBenefitsToStockpile(List<Data.Res> stockpile, List<Data.Benefit> benefits)
        {
            foreach (Hex.Data.Benefit benefit in benefits)
            {
                AddResToStockpile(stockpile, benefit.Res);
            }
        }
    }
}
