namespace Hex.Data
{
    public struct Bonus
    {
        public enum Type
        {
            None,
            Conditional,
            Add,
            Multiply,
            Reactivate
        }

        public readonly int ReceivingTileID;
        public readonly int BonusTileID;

        public readonly Type BonusType;

        public int OriginalValue;
        public int BonusValue;
        public int ResultingValue;

        public Bonus(int receivingTileID, int bonusTileID, Type bonusType, int originalValue, int bonusValue, int resultingValue)
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
