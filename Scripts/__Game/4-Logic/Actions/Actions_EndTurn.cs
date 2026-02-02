using System;
using System.Runtime.InteropServices;

namespace Hex.Logic
{
    public static partial class Actions
    {
        public static void EndTurn(out ReadOnlySpan<Production> production)
        {
            ProductionPools.ClearOnEndTurnProduction();

            // calculate bonusLink for all tiles
            ProductionCalculator.CalculateAllBonusLinks();

            // iterate through all map tiles and calculate PerTurn production
            for (int mapTileIdx = 0; mapTileIdx < Data.MapTile.MAP_SIZE; mapTileIdx++)
            {
                Data.MapTileRef mapTile = Data.MapTileRef.FromID(mapTileIdx);
                if (mapTile.Value.IsValid() == false)
                    continue;

                // check if this tile has any PerTurn production effects
                bool hasPerTurnProduction = false;
                for (int effectIdx = 0; effectIdx < mapTile.Value.DefData.Effects.Count; effectIdx++)
                {
                    ref readonly Def.Effect effect = ref mapTile.Value.DefData.Effects[effectIdx];
                    if (effect.GetTiming() == Def.Timing.PerTurn && effect.GetEffectType() == Def.EffectType.Production)
                    {
                        hasPerTurnProduction = true;
                        break;
                    }
                }

                if (hasPerTurnProduction)
                {
                    int startIdx = ProductionPools.OnEndTurnProductionCount;

                    // calculate PerTurn production for this tile
                    ProductionCalculator.CalculateTileProduction(
                        mapTile,
                        Def.Timing.PerTurn,
                        ProductionPools.OnEndTurnProduction,
                        ref ProductionPools.OnEndTurnProductionCount,
                        ProductionPools.GrowOnEndTurnProduction);

                    // calculate bonuses for the newly added production items
                    ProductionCalculator.CalculateBonuses(
                        mapTile,
                        ProductionPools.OnEndTurnProduction,
                        ProductionPools.OnEndTurnProductionCount,
                        startIdx);
                }
            }

            // calculate final total and target values for all production items
            ProductionCalculator.CalculateFinalValues(
                ProductionPools.OnEndTurnProduction,
                ProductionPools.OnEndTurnProductionCount);

            production = new ReadOnlySpan<Production>(ProductionPools.OnEndTurnProduction, 0, ProductionPools.OnEndTurnProductionCount);
        }
    }
}
