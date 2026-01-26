using Hex.Logic.DataBuffer;

namespace Hex.Logic
{
    public readonly struct BonusGiverListRef
    {
        internal readonly int Id = -1;

        public BonusGiverListRef()
        {
            BonusGiverBuffer.CreateList(out int id);
            Id = id;
        }

        public ref BonusGiver this[int index]
        {
            get => ref BonusGiverBuffer.Get(this, index);
        }

        public void Add(in BonusGiver bonusGiver) => BonusGiverBuffer.Add(this, in bonusGiver);

        public int Count => BonusGiverBuffer.Count(this);

        public void Clear() => BonusGiverBuffer.Clear(this);

        public bool IsValid() => Id >= 0;
    }
}

