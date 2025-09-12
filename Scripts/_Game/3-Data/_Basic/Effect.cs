namespace Data
{
    public class Effect
    {
        public Def.Timing EffectTiming = Def.Timing.PerTurn;

        public Effect(Def.Var defEffect)
        {
            string timing = defEffect.GetString(0);
            switch (timing)
            {
                case "Always":
                    EffectTiming = Def.Timing.Always; break;
                case "PerTurn":
                    EffectTiming = Def.Timing.PerTurn; break;
                case "OnLevelUp":
                    EffectTiming = Def.Timing.OnLevelUp; break;
                //case "OnDestroySelf":
                //    EffectTiming = Def.Timing.OnDestroySelf; break;
                //case "OnDestroyOther":
                //    EffectTiming = Def.Timing.OnDestroyOther; break;
                //case "OnPlaceOther":
                //    EffectTiming = Def.Timing.OnPlaceOther; break;
                case "OnPlace":
                default:
                    EffectTiming = Def.Timing.OnPlace; break;
            }
        }
    }
}
