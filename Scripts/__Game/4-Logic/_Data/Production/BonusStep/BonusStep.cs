
namespace Hex.Logic
{
    public struct BonusStep
    {
        public Data.MapTileRef Tile;
        public Data.MapTileRef SourceTile;
        public int Depth;
        public Def.Bonus BonusType;
        public int Value;

        public BonusStep(Data.MapTileRef tile, Def.Bonus bonusType, int value)
        {
            Tile = tile;
            SourceTile = tile;
            Depth = 0;
            BonusType = Def.Bonus.Add;
            Value = value;
        }

        public BonusStep(Data.MapTileRef tile, Data.MapTileRef sourceTile, int depth, Def.Bonus bonusType, int value)
        {
            Tile = tile;
            SourceTile = sourceTile;
            Depth = depth;
            BonusType = bonusType;
            Value = value;
        }
    }
}
