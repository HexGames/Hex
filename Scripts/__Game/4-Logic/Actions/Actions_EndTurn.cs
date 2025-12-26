using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Hex.Logic
{
    public static partial class Actions
    {
        public static void EndTurn(out ReadOnlySpan<Production> production)
        {
            foreach (Production income in Production.OnEndTurnProduction) income.Clear();
            Production.OnEndTurnProductionCount = 0;

            // calculate onPlaceBonusTree and production
            if (Production.OnEndTurnProduction.Count <= 1) Production.OnEndTurnProduction.Add(new Production()); // hack
            Production.OnEndTurnProduction[0].Total = new Data.Res(Def.Lib.GetResRef("Population"), 3); // hack
            Production.OnEndTurnProductionCount++; // hack

            production = CollectionsMarshal.AsSpan(Production.OnEndTurnProduction).Slice(0, Production.OnEndTurnProductionCount);

            // TO DO - implement end turn logic

            // .Game.NewTurn(); // to do - not here!!
        }
    }
}
