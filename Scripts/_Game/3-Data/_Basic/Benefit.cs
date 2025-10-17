namespace Data
{
    public struct Benefit
    {
        public Res Res;
        public HexCoords HexCoords;
        public Def.Timing BenefitTiming = Def.Timing.PerTurn;

        public Benefit(Def.Res def, int value, HexCoords hexCoords, Def.Timing benefitTiming)
        {

            Res = new Res(def, value);
            HexCoords = hexCoords;
            BenefitTiming = benefitTiming;
        }
    }
}
