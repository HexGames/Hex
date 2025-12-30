using System;
using System.Runtime.InteropServices;

namespace Hex.Logic
{
    public static partial class Actions
    {
        public static void StartTurn(out int turnNumber)
        {
            Data.Game.NewTurn();
            turnNumber = Data.Game.GetTurnCount();
        }
    }
}
