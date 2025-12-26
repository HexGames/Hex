using CommandSystem;
using System;
using System.Collections.Generic;

namespace Hex.Play
{
    public class EndTurnCommandResult : ICommandResult
    {
        public Data.MapTileRef MapTile;
        public readonly List<Data.Bonus> OnPlaceBonusTree = new List<Data.Bonus>();
        public readonly Data.Res Production;

        public EndTurnCommandResult(Data.MapTileRef mapTile, List<Data.Bonus> onPlaceBonusTree, Data.Res production)
        {
            MapTile = mapTile;
            OnPlaceBonusTree = onPlaceBonusTree;
            Production = production;
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
            Play.EndTurnCommandResult result = new Play.EndTurnCommandResult(Data.MapTileRef.FromID(-1), null, new Data.Res());

            return result;
        }
    }
}
