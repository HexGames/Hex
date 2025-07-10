namespace Data
{
    public class Effect
    {
        public Def.Res Res = null;
        public int Value = 0;
        public Def.Timing EffectTiming = Def.Timing.PerTurn;
        public string EffectID = "";
        public string EffectParam = "";

        public Effect(Def.VarEffect defVar) 
        {
            Res = defVar.Res;
            Value = defVar.Value;
            EffectID = defVar.EffectID;
            EffectParam = defVar.EffectParam;
            switch (defVar.EffectTiming)
            {
                case "PerTurn":
                    EffectTiming = Def.Timing.PerTurn; break;
                case "OnDestroySelf":
                    EffectTiming = Def.Timing.OnDestroySelf; break;
                case "OnDestroyOther":
                    EffectTiming = Def.Timing.OnDestroyOther; break;
                case "OnPlaceOther":
                    EffectTiming = Def.Timing.OnPlaceOther; break;
                case "OnPlaceSelf":
                default:
                    EffectTiming = Def.Timing.OnPlaceSelf; break;
            }
        }
    }
}
