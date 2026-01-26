namespace Hex.Logic
{
    public struct BonusGiver
    {
        public Data.MapTileRef MapTile;
        public int EffectIdx;

        public BonusGiver(in Data.MapTileRef mapTile, int effectIndex)
        {
            MapTile = mapTile;
            EffectIdx = effectIndex;
        }
    }
}
