
namespace Hex.Logic
{
    public struct LocalStep
    {
        public Data.MapTileRef SourceTile;
        public int Value;

        public LocalStep(Data.MapTileRef sourceTile, int value)
        {
            SourceTile = sourceTile;
            Value = value;
        }
    }
}
