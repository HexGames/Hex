using System;
using System.Runtime.InteropServices;

namespace Hex.Logic
{
    public static partial class Actions
    {
        public static void EndTurn(out ReadOnlySpan<Production> production)
        {
            ProductionPools.ClearOnEndTurnProduction();

            // calculate onPlaceBonusTree and production
            //if (Production.OnEndTurnProduction.Count <= 1) Production.OnEndTurnProduction.Add(new Production()); // hack
            ProductionPools.OnEndTurnProduction[0].ResDef = Def.Lib.GetResRef("Population");
            ProductionPools.OnEndTurnProduction[0].TotalValue = 3; // hack
            ProductionPools.OnEndTurnProductionCount++; // hack

            production = null;

            // TO DO - implement end turn logic

            // .Game.NewTurn(); // to do - not here!!
        }
    }
}
