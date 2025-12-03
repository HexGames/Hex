using System.Collections.Generic;

namespace Logic
{
    public static class Stockpile
    {
        // -----------------------------------------------------------------------------------------
        public static int GetValue(List<Hex.Data.Res> stockpilem, Hex.Def.Res res)
        {
            foreach (Hex.Data.Res stockRes in stockpilem)
            {
                if (stockRes.Def == res)
                {
                    return stockRes.Value;
                }
            }
            return 0;
        }

        // -----------------------------------------------------------------------------------------
        public static void AddResToStockpile(List<Hex.Data.Res> stockpile, Hex.Data.Res res)
        {
            for (int resIdx = 0; resIdx < stockpile.Count; resIdx++)
            {
                if (stockpile[resIdx].Def == res.Def)
                {
                    Hex.Data.Res valueRes = stockpile[resIdx];
                    valueRes.Value += res.Value;
                    stockpile[resIdx] = valueRes;
                    return;
                }
            }
            // If not found, add new Res to stockpile
            stockpile.Add(res);
        }

        // -----------------------------------------------------------------------------------------
        public static void AddBenefitsToStockpile(List<Hex.Data.Res> stockpile, List<Hex.Data.Benefit> benefits)
        {
            foreach (Hex.Data.Benefit benefit in benefits)
            {
                AddResToStockpile(stockpile, benefit.Res);
            }
        }
    }
}
