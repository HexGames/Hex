using System.Runtime.CompilerServices;

namespace Hex.Data
{
    internal partial struct GameData
    {
        internal TurnArray Turns;
        internal int CurrentTurn = 0;
        internal bool Finished = false;

        // ---------------------------------------------------------------------------------- TileArray
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
}
