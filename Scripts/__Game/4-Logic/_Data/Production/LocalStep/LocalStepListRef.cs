using Hex.Logic.DataBuffer;

namespace Hex.Logic
{
    public readonly struct LocalStepListRef
    {
        internal readonly int Id = -1;

        public LocalStepListRef()
        {
            LocalStepBuffer.CreateList(out int id);
            Id = id;
        }

        public ref LocalStep this[int index]
        {
            get => ref LocalStepBuffer.Get(this, index);
        }

        public void Add(in LocalStep step) => LocalStepBuffer.Add(this, in step);

        public int Count => LocalStepBuffer.Count(this);

        public void Clear() => LocalStepBuffer.Clear(this);
    }
}
