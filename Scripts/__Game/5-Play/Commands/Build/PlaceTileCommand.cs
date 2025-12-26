using CommandSystem;
using System;
using System.Collections.Generic;

namespace Hex.Play
{
    public class PlayTileCommandResult : ICommandResult
    {
        public Data.MapTileRef MapTile;
        public readonly Logic.Production[] OnPlaceProduction;
        public readonly bool Success = false;

        public PlayTileCommandResult()
        {
            Success = false;
        }
        
        public PlayTileCommandResult(Data.MapTileRef mapTile, ReadOnlySpan<Logic.Production> onPlaceProduction)
        {
            Success = true;
            MapTile = mapTile;
            OnPlaceProduction = onPlaceProduction.ToArray();
        }
    }
}

namespace Hex.PlayInternal
{
    public class PlayTileCommandInfo : ICommandInfo
    {
        public Data.DeckTileRef DeckTile;
        public Data.HexPos AtHexPos;

        public PlayTileCommandInfo(Data.DeckTileRef deckTile, Data.HexPos atHexPos)
        {
            DeckTile = deckTile;
            AtHexPos = atHexPos;
        }
    }

    public class PlayTileCommand
    {
        public static ICommandResult HandleCommand(ICommandInfo commandInfo)
        {
            var info = Commands.GetInfo<PlayTileCommandInfo>(commandInfo);
            if (info == null) return null;

            bool success = Logic.Actions.PlayTile(info.DeckTile, info.AtHexPos, out Data.MapTileRef mapTile, out ReadOnlySpan<Logic.Production> production);
            if (success == false) return new Play.PlayTileCommandResult();

            Play.PlayTileCommandResult result = new Play.PlayTileCommandResult(mapTile, production);

            return result;
        }
    }
}
