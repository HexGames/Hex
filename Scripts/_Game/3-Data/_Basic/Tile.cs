using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Data
{
    public class Tile
    {
        public enum State
        {
            IN_NEXT,
            IN_PLAY
        };

        public Def.Tile Def;
        public State Status;

        private readonly List<EffectNode> Effects = new List<EffectNode>();
        private readonly List<EffectNode> OverwriteEffects = new List<EffectNode>();

        public Tile(Def.Tile def, State state)
        {
            Def = def;
            Status = state;

            foreach (Def.Var effectVar in Def.Effects)
            {
                Effects.Add(new EffectNode(this, effectVar));
                OverwriteEffects.Add(new EffectNode(this, effectVar));
            }
        }

        public List<EffectNode> GetEffectsList(bool overwrite)
        {
            if (overwrite) return OverwriteEffects;
            return Effects;
        }
    }
}

