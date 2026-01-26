namespace Hex.Logic
{
    public static class BonusLinkPool
    {
        public static BonusLink[] BonusLinks = null;

        public static void Init()
        {
            BonusLinks = new BonusLink[Data.MapTile.MAP_SIZE];
            for (int idx = 0; idx < BonusLinks.Length; idx++)
            {
                BonusLinks[idx] = new BonusLink();
            }
        }
    }
}
