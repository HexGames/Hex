namespace Data
{
    public struct Benefit
    {
        public Res Res;
        public HexCoord HexCoord;
        public Def.Timing BenefitTiming = Def.Timing.PerTurn;

        public Benefit(Def.Res def, int value, HexCoord hexCoord, Def.Timing benefitTiming)
        {

            Res = new Res(def, value);
            HexCoord = hexCoord;
            BenefitTiming = benefitTiming;
        }
    }
}
