using System;

namespace Hex.Logic.DataBuffer
{
    internal static class BonusGiverBuffer
    {
        internal struct BonusGiverList
        {
            public int Start;
            public int Count;
            public int Capacity;

            public BonusGiverList(int start, int count, int capacity)
            {
                Start = start;
                Count = count;
                Capacity = capacity;
            }
        }

        private static BonusGiver[] _buffer;
        private static BonusGiverList[] _lists;

        private static int _usedCapacity;
        private static int _listsCount;

        private const int CHUNK = 8;
        private const int GROW_CHUNKS = 4 * CHUNK;
        private const int INITIAL_BUFFER = 32 * CHUNK;
        private const int INITIAL_ENTRIES = 32;

        internal static void CreateList(out int listID, int initialCapacity = CHUNK)
        {
            EnsureEntryCapacity(_listsCount + 1);

            int capacity = ((initialCapacity + CHUNK - 1) / CHUNK) * CHUNK; // smallest multiple of CHUNK >= initialCapacity
            EnsureBufferCapacity(_usedCapacity + capacity);

            _lists[_listsCount] = new BonusGiverList(_usedCapacity, 0, capacity);

            listID = _listsCount;
            _usedCapacity += capacity;
            _listsCount++;
        }

        internal static void Add(BonusGiverListRef handle, in BonusGiver BonusGiver)
        {
            ref BonusGiverList list = ref _lists[handle.Id];

            if (list.Count == list.Capacity) Grow(handle);

            _buffer[list.Start + list.Count] = BonusGiver;
            list.Count++;
        }

        internal static ref BonusGiver Get(BonusGiverListRef listRef, int index)
        {
            ref BonusGiverList list = ref _lists[listRef.Id];

            if (index >= list.Count)
                throw new IndexOutOfRangeException();

            return ref _buffer[list.Start + index];
        }

        internal static int Count(BonusGiverListRef handle) => _lists[handle.Id].Count;

        private static void Grow(BonusGiverListRef handle)
        {
            ref BonusGiverList list = ref _lists[handle.Id];

            EnsureBufferCapacity(_usedCapacity + CHUNK);

            int nextListStart = list.Start + list.Capacity;
            Array.Copy(_buffer, nextListStart, _buffer, nextListStart + CHUNK, _usedCapacity - nextListStart);
            _usedCapacity += CHUNK;
            list.Capacity += CHUNK;

            for (int idx = 0; idx < _listsCount; idx++)
            {
                if (_lists[idx].Start >= nextListStart)
                {
                    _lists[idx].Start += CHUNK;
                }
            }
        }

        private static void EnsureBufferCapacity(int requiredCapacity)
        {
            if (_buffer == null)
            {
                _buffer = new BonusGiver[Math.Max(INITIAL_BUFFER, requiredCapacity)];
            }

            if (requiredCapacity <= _buffer.Length) return;

            int capacity = ((requiredCapacity + GROW_CHUNKS - 1) / GROW_CHUNKS) * GROW_CHUNKS; // smallest multiple of GROW_CHUNKS >= requiredCapacity
            var newBuffer = new BonusGiver[capacity];
            Array.Copy(_buffer, newBuffer, _buffer.Length);
            _buffer = newBuffer;
        }

        private static void EnsureEntryCapacity(int required)
        {
            if (_lists == null)
                _lists = new BonusGiverList[INITIAL_ENTRIES];

            if (required <= _lists.Length)
                return;

            Array.Resize(ref _lists, _lists.Length * 2);
        }

        internal static void Clear(BonusGiverListRef handle)
        {
            ref BonusGiverList list = ref _lists[handle.Id];

            list.Count = 0;
        }
    }
}
