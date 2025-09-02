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

        public readonly List<Effect> Effects;
        public readonly List<Benefit> Benefits;

        public Tile(Def.Tile def, State state)
        {
            Def = def;
            Status = state;
            Effects = new List<Effect>();
            Benefits = new List<Benefit>();

            for (int idx = 0; idx < Def.Data_Effects.Count; idx++)
            {
                Effects.Add(new Effect(Def.Data_Effects[idx]));
            }
        }
    }
}

