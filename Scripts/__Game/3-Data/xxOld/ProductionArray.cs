namespace Hex.Logic
{
      /*
    
    public struct ProductionList
    {
        private static Production[] _buffer = null;
        private static int _occupiedChunks = 0;
        private const int CHUNK_SIZE = 8;
        private const int ALLOCATE_CHUNCKS = 4;
        private const int INITIAL_ALLOCATE_CHUNCKS = 4;

        public int StartIndex;
        public int Size;
        public int AllocatedSize;

        public ProductionList()
        {
            if (_buffer == null)
            {
                _buffer = new Production[INITIAL_ALLOCATE_CHUNCKS * ALLOCATE_CHUNCKS * CHUNK_SIZE];
            }
            while (_occupiedChunks >= _buffer.Length)
            {
                Production[] newBuffer = new Production[_buffer.Length + ALLOCATE_CHUNCKS * CHUNK_SIZE];
                for (int i = 0; i < _buffer.Length; i++)
                {
                    newBuffer[i] = _buffer[i];
                }
                _buffer = newBuffer;
            }

            StartIndex = _occupiedChunks * CHUNK_SIZE;
            Size = 0;
            AllocatedSize = CHUNK_SIZE;
        }

        public ref Production this[int index]
        {
            get
            {
                return ref _buffer[StartIndex + index];
            }
        }

        public void Add(Production production)
        {
        }
        public void RemoveAt(int idx)
        {
        }
    }*/
}
