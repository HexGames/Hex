using CommandSystem;
using System;
using System.Collections.Generic;

namespace Hex.Play
{
    public class EndTurnCommandResult : ICommandResult
    {
        public readonly Logic.Production[] EndTurnProduction;

        public EndTurnCommandResult(ReadOnlySpan<Logic.Production> endTurnProduction)
        {
            EndTurnProduction = endTurnProduction.ToArray();
        }
    }
}

namespace Hex.PlayInternal
{
    public class EndTurnCommandInfo : ICommandInfo
    {
        public EndTurnCommandInfo()
        {
        }
    }

    public class EndTurnCommand
    {
        public static ICommandResult HandleCommand(ICommandInfo commandInfo)
        {
            var info = Commands.GetInfo<EndTurnCommandInfo>(commandInfo);
            if (info == null) return null;

            Logic.Actions.EndTurn(out ReadOnlySpan<Logic.Production> productions);
            Play.EndTurnCommandResult result = new Play.EndTurnCommandResult(productions);

            return result;
        }
    }
}
