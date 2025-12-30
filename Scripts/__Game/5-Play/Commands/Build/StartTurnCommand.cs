using CommandSystem;
using System;
using System.Collections.Generic;

namespace Hex.Play
{
    public class StartTurnCommandResult : ICommandResult
    {
        public int TurnNumber;

        public StartTurnCommandResult(int turnNumber)
        {
            TurnNumber = turnNumber;
        }
    }
}

namespace Hex.PlayInternal
{
    public class StartTurnCommandInfo : ICommandInfo
    {
        public StartTurnCommandInfo()
        {
        }
    }

    public class StartTurnCommand
    {
        public static ICommandResult HandleCommand(ICommandInfo commandInfo)
        {
            var info = Commands.GetInfo<StartTurnCommandInfo>(commandInfo);
            if (info == null) return null;

            Logic.Actions.StartTurn(out int turnNumber);
            Play.StartTurnCommandResult result = new Play.StartTurnCommandResult(turnNumber);

            return result;
        }
    }
}