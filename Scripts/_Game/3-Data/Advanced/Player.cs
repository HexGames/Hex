using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Data
{
    public class Player
    {
        public readonly List<Res> Stockpile = new List<Res>();
        public readonly List<Res> Income = new List<Res>();
        public readonly List<Tile> DraftTiles = new List<Tile>();
        public Tile DraftedTile = null;
        public HexCoord DraftedTileHoverCoords = HexCoord.Invalid;
        public readonly List<Effect> DraftedTileHoverEffects = new List<Effect>();
        public int DraftTilesCount = 0;

        public Player()
        {
            DraftTilesCount = Def.Base.DefaultDraftTiles;
        }

        // --------------------------------------------------------------------------------------------------------------
        //public Res GetStockpile(string id)
        //{
        //    foreach (Res res in Stockpile)
        //    {
        //        if (res.Def.ID == id)
        //        {
        //            return res;
        //        }
        //    }
        //    return Res.Null;
        //}
        //
        //public Res GetStockpile(Def.Res def)
        //{
        //    foreach (Res res in Stockpile)
        //    {
        //        if (res.Def == def)
        //        {
        //            return res;
        //        }
        //    }
        //    return Res.Null;
        //}
        //
        //public bool ResourceCheck(Def.Res def, int value)
        //{
        //    foreach (Res res in Stockpile)
        //    {
        //        if (res.Def == def)
        //        {
        //            return res.Value >= value;
        //        }
        //    }
        //    return false;
        //}

        //public void AddToStockpile(Def.Res resDef, int value)
        //{
        //    for (int resIdx = 0; resIdx < Stockpile.Count; resIdx++)
        //    {
        //        if (Stockpile[resIdx].Def == resDef)
        //        {
        //            Res res = Stockpile[resIdx];
        //            res.Value += value;
        //            Stockpile[resIdx] = res;
        //            return;
        //        }
        //    }
        //    // If not found, add new Res to stockpile
        //    var newRes = new Res(resDef, value, 0);
        //    Stockpile.Add(newRes);
        //}

        //public void AddToStockpileIncome(Def.Res resDef, int perTurn)
        //{
        //    for (int resIdx = 0; resIdx < Stockpile.Count; resIdx++)
        //    {
        //        if (Stockpile[resIdx].Def == resDef)
        //        {
        //            Res res = Stockpile[resIdx];
        //            res.PerTurn += perTurn;
        //            Stockpile[resIdx] = res;
        //            return;
        //        }
        //    }
        //    // If not found, add new Res to stockpile
        //    var newRes = new Res(resDef, 0, perTurn);
        //    Stockpile.Add(newRes);
        //}

        //public void AddToStockpile(Data.Res res)
        //{
        //    for (int resIdx = 0; resIdx < Stockpile.Count; resIdx++)
        //    {
        //        if (Stockpile[resIdx].Def == res.Def)
        //        {
        //            Res valueRes = Stockpile[resIdx];
        //            valueRes.Value += res.Value;
        //            valueRes.PerTurn += res.PerTurn;
        //            Stockpile[resIdx] = valueRes;
        //            return;
        //        }
        //    }
        //    // If not found, add new Res to stockpile
        //    Stockpile.Add(res);
        //}

        //public void SubstractFromStockpile(Res res)
        //{
        //    AddToStockpile(new Data.Res(res.Def, -res.Value, -res.PerTurn));
        //}
    }
}
