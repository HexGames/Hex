using System.Runtime.CompilerServices;
namespace Hex.Data
{
    internal partial struct GameData
    {
        internal static GameData Data;

        internal TurnArray Turns;
        internal int CurrentTurn;
        internal bool Finished;
    }
}
