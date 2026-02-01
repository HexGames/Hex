
namespace Hex.Logic
{
    public struct BonusStep
    {
        public Data.MapTileRef TargetTile;
        public Data.MapTileRef SourceTile;
        public int Depth;
        public LocalStepListRef LocalList;
        public Def.EffectType EffectType;
        public int Total;
        public int TargetValue;

        public BonusStep(Data.MapTileRef tile, Def.EffectType effectType)
        {
            TargetTile = tile;
            SourceTile = tile;
            Depth = 0;
            LocalList = new LocalStepListRef();
            EffectType = effectType;
            Total = 0;
            TargetValue = 0;
        }

        public BonusStep(Data.MapTileRef tile, Data.MapTileRef sourceTile, int depth, Def.EffectType effectType)
        {
            TargetTile = tile;
            SourceTile = sourceTile;
            Depth = depth;
            LocalList = new LocalStepListRef();
            EffectType = effectType;
            Total = 0;
            TargetValue = 0;
        }
    }
}
