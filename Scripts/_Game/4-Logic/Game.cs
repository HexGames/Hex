using System.Collections.Generic;

namespace Logic
{
    public partial class Game
    {
        public Data.Decks Decks = null;
        public Data.Player Player = null;
        public Data.Map Map = null;

        public int TurnNo = 0;
        public bool Finished = false;

        // ------------------------------------------------------------------------------------------
        public Game()
        {
            Decks = new Data.Decks();
            Player = new Data.Player();
            Map = new Data.Map(Start.GenerateMap());

            TurnNo = 0;
            Finished = false;

            Start.AddStartingDeckTiles(Decks);
            Start.AddStartingRes(Player);

            StartTurn();
        }

        // ------------------------------------------------------------------------------------------
        //public void SetDraftedTile(Data.Tile tile)
        //{
        //    Player.DraftedTile = tile;
        //}

        // ------------------------------------------------------------------------------------------
        public bool CheckPlayable(Data.Tile tile, Data.HexCoord coord)
        {
            return Play.CheckPlayable(Player, Map, tile, coord);
        }

        // ------------------------------------------------------------------------------------------
        public void SimulatePlayTile(Data.Tile tile, Data.HexCoord coord)
        {
            Play.CalculateOnPlayBenefits(this, tile, coord);
        }

        // ------------------------------------------------------------------------------------------
        public void PlayTile(Data.Tile tile, Data.HexCoord coord)
        {
            Play.CalculateOnPlayBenefits(this, tile, coord);
            Play.RemoveTile(Map, coord);
            Play.MoveTileInPlay(Player, Map, tile, coord);
            Play.GainBenefits(Player, tile.Benefits);
            Play.CalculateAllTilesPerTurnBenefits(this);
            //Play.CalculateGoalProgress(Player);
        }

        // ------------------------------------------------------------------------------------------
        public void EndTurn()
        {
            TurnNo++;
        }

        // ------------------------------------------------------------------------------------------
        public void EndGame()
        {
        }

        // ------------------------------------------------------------------------------------------
        public void StartTurn()
        {
            Turn.GainAllBenefits(Player, Map);
            Turn.PutNewTilesIntoNextQueue(Decks, Player);
            Turn.SetNextTileAsCurrentTile(Player);
        }
    }
}
