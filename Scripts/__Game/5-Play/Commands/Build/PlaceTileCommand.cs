using CommandSystem;
using System.Collections.Generic;

namespace Hex.Play
{
    public class PlayTileCommandResult : ICommandResult
    {
        public Data.MapTileRef MapTile;
        public readonly List<Data.TileToTile> OnPlaceBonusTree = new List<Data.TileToTile>();
        public readonly Data.Res Production;
        public readonly bool Success = false;

        public PlayTileCommandResult()
        {
            Success = false;
        }
        
        public PlayTileCommandResult(Data.MapTileRef mapTile, List<Data.TileToTile> onPlaceBonusTree, Data.Res production)
        {
            Success = true;
            MapTile = mapTile;
            OnPlaceBonusTree = onPlaceBonusTree;
            Production = production;
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

            bool success = Logic.Actions.PlayTile(info.DeckTile, info.AtHexPos, out Data.MapTileRef mapTile, out List<Data.TileToTile> onPlaceBonusTree, out Data.Res production);
            if (success == false) return new Play.PlayTileCommandResult();

            Play.PlayTileCommandResult result = new Play.PlayTileCommandResult(mapTile, onPlaceBonusTree, production);

            return result;
        }
    }
}
