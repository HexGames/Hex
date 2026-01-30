
namespace Hex.Logic
{
    public struct BonusStep
    {
        public Data.MapTileRef Tile;
        public Data.MapTileRef SourceTile;
        public int Depth;
        public LocalStepListRef LocalList;
        public Def.EffectType EffectType;
        public int Total;

        public BonusStep(Data.MapTileRef tile, Def.EffectType effectType)
        {
            Tile = tile;
            SourceTile = tile;
            Depth = 0;
            LocalList = new LocalStepListRef();
            EffectType = effectType;
            Total = 0;
        }

        public BonusStep(Data.MapTileRef tile, Data.MapTileRef sourceTile, int depth, Def.EffectType effectType)
        {
            Tile = tile;
            SourceTile = sourceTile;
            Depth = depth;
            LocalList = new LocalStepListRef();
            EffectType = effectType;
            Total = 0;
        }
    }
}
