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

        public readonly List<Def.Var> Effects;
        public readonly List<Benefit> Benefits;

        public Tile(Def.Tile def, State state)
        {
            Def = def;
            Status = state;
            Effects = new List<Def.Var>();
            Benefits = new List<Benefit>();

            for (int idx = 0; idx < Def.Data_Effects.Count; idx++)
            {
                Effects.Add(Def.Data_Effects[idx].Clone());
            }
        }
    }
}

