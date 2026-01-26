namespace Hex.Logic
{
    public struct BonusLink
    {
        public BonusGiverListRef Links;

        public BonusLink()
        {
            Links = new BonusGiverListRef();
        }

        public void Clear()
        {
            Links.Clear();
        }
    }
}
