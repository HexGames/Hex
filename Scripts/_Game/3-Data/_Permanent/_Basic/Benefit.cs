namespace Hex.Data
{
    public struct Benefit
    {
        public Res Res;
        public HexPos HexPos;
        public Hex.Def.Timing BenefitTiming = Hex.Def.Timing.PerTurn;

        public Benefit(Hex.Def.Res def, int value, HexPos hexPos, Hex.Def.Timing benefitTiming)
        {

            Res = new Res(def, value);
            HexPos = hexPos;
            BenefitTiming = benefitTiming;
        }
    }
}
