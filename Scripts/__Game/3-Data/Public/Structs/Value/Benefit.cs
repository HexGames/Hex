namespace Hex.Data
{
    public struct Benefit
    {
        public Res Res;
        public HexPos HexPos;
        public Def.Timing BenefitTiming = Def.Timing.PerTurn;

        public Benefit(Def.ResRef def, int value, HexPos hexPos, Def.Timing benefitTiming)
        {

            Res = new Res(def, value);
            HexPos = hexPos;
            BenefitTiming = benefitTiming;
        }
    }
}
