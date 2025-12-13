using System.Runtime.CompilerServices;

namespace Hex.Data
{
    internal struct TurnArray
    {
        [InlineArray(Turn.MAX_TURNS)] public struct TurnArrayInlineArray { private Turn _element0; }
        internal TurnArrayInlineArray Array;

        public TurnArray()
        {
            //_array = default; // is this necessary?
        }
    }
}
