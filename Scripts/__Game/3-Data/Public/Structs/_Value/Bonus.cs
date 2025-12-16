namespace Hex.Data
{
    public struct TileToTile
    {
        public enum Bonus
        {
            None,
            Conditional,
            Add,
            Multiply,
            Reactivate
        }

        public readonly int ReceivingTileID;
        public readonly int BonusTileID;

        public readonly Bonus BonusType;

        public int OriginalValue;
        public int BonusValue;
        public int ResultingValue;

        public TileToTile(int receivingTileID, int bonusTileID, Bonus bonusType, int originalValue, int bonusValue, int resultingValue)
        {
            ReceivingTileID = receivingTileID;
            BonusTileID = bonusTileID;

            BonusType = bonusType;
            OriginalValue = originalValue;
            BonusValue = bonusValue;
            ResultingValue = resultingValue;
        }
    }
}
