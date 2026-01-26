namespace Hex.Logic
{
    public static class BonusLinkPool
    {
        public static BonusLink[] BonusLinks = new BonusLink[Data.MapTile.MAP_SIZE];

        public static void Init()
        {
            for (int idx = 0; idx < BonusLinks.Length; idx++)
            {
                BonusLinks[idx] = new BonusLink();
            }
        }
    }
}
