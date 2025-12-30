using System;
using System.Collections.Generic;

namespace Hex.Data
{
    public static partial class Game
    {
        public static void NewTurn()
        {
            if (GameData.Data.CurrentTurn + 1 >= Turn.MAX_TURNS)
            {
                Debug.LogWarning("[Game NewTurn] Cannot create new turn. Already at max game lenght.");
                return;
            }

            GameData.Data.CurrentTurn++;
            GameData.Data.Turns.Array[GameData.Data.CurrentTurn] = GameData.Data.Turns.Array[GameData.Data.CurrentTurn - 1]; // copy data
        }

        public static void UndoTurn()
        {
            if (GameData.Data.CurrentTurn == 0)
            {
                Debug.LogWarning("[Game UndoTurn] Cannot undo turn. Already at turn 0.");
                return;
            }

            GameData.Data.Turns.Array[GameData.Data.CurrentTurn] = GameData.Data.Turns.Array[GameData.Data.CurrentTurn - 1];
        }

        public static int GetTurnCount()
        {
            return GameData.Data.CurrentTurn;
        }
    }
}
