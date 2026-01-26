using Hex.Logic.DataBuffer;

namespace Hex.Logic
{
    public readonly struct BonusStepListRef
    {
        internal readonly int Id = -1;

        public BonusStepListRef()
        {
            BonusStepBuffer.CreateList(out int id);
            Id = id;
        }

        public ref BonusStep this[int index]
        {
            get => ref BonusStepBuffer.Get(this, index);
        }

        public void Add(in BonusStep step) => BonusStepBuffer.Add(this, in step);

        public int Count() => BonusStepBuffer.Count(this);

        public void Clear() => BonusStepBuffer.Clear(this);
    }
}
