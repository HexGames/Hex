using System;
using System.Collections.Generic;

namespace Logic
{
    public static class Start
    {
        // -----------------------------------------------------------------------------------------
        

        // -----------------------------------------------------------------------------------------
        public static void AddStartingRes()
        {
            Hex.Data.Player player = GameMain.X.Player;

            foreach (Hex.Def.Res resDef in Hex.Def.Lib.Res)
            {
                Stockpile.AddResToStockpile(player.Stockpile, new Hex.Data.Res(resDef, resDef.Default));
            }
        }

        // -----------------------------------------------------------------------------------------
        
    }
}

