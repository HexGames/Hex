using System.Collections.Generic;

namespace Logic
{
    //using EffectIdx = System.Int32;

    public static class bEffects
    {
        private enum EffectType
        {
            Production,
            BonusAdd,
            BonusMultiply,
            BonusReactivate
        }

        private struct Adjiacency
        {
            public Data.Tile Tile;
            public int Value;
        }

        private class Effect
        {
            public Data.Tile FromTile;
            public EffectType BonusType;
            public Def.Res ProdctionResource;
            public int BaseValue;
            public List<Adjiacency> Adjiacencies;
            public List<Effect> Bonuses;

            public Effect(Effect other)
            {
                FromTile = other.FromTile;
                BonusType = other.BonusType;
                ProdctionResource = other.ProdctionResource;
                BaseValue = other.BaseValue;
                Adjiacencies = new List<Adjiacency>(other.Adjiacencies);
                Bonuses = new List<Effect>(other.Bonuses);
            }
        }

        private static List<Effect> _effectsList = new List<Effect>();
        private static List<Effect> _potentialEffectsList = new List<Effect>();

        public static void AddTileEffectsAsPotential(Data.Tile tile, Data.HexPos hexPos)
        {
            // clone effect list
            _potentialEffectsList.Clear();
            foreach ( Effect effect in _effectsList)
            {
                _potentialEffectsList.Add(new Effect(effect));
            }

            Data.Tile oldTile = Data.Map.GetTile(hexPos);


            // to do
            // --- remove old tile effects
            // - remove tile from other effect's adjiacency lists
            // - if bonus effect, remove it from other effect's bonus lists

            // to do
            // --- add new tile effects
            // - create as new effect
            // ...
        }

        public static void GetOnPlaceProduction(Data.Tile tile, Data.HexPos hexPos)
        {

        }
    }
}
