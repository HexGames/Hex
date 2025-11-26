using System.Collections.Generic;

namespace Logic
{
    public class GameMain
    {
        public static GameMain X = null;

        public Data.Decks Decks = null;
        public Data.Player Player = null;
        public Data.Turn Turn = null;

        public int TurnNo = 0;
        public bool Finished = false;

        // ------------------------------------------------------------------------------------------
        public GameMain()
        {
            X = this;

            Decks = new Data.Decks();
            Player = new Data.Player();
            Data.Map.Init(Start.GenerateMap());
            Turn = new Data.Turn();
        }

        // ------------------------------------------------------------------------------------------
        //public void SetDraftedTile(Data.Tile tile)
        //{
        //    Player.DraftedTile = tile;
        //}

        // ------------------------------------------------------------------------------------------
        //public void PlayTile(Data.Tile tile, Data.HexPos coord)
        //{
        //    // TO DO
        //    //Play.CalculateOnPlayBenefits(this, tile, coord);
        //    Play.RemoveTile(Map, coord);
        //    Play.SetTileIOnMap(Player, Map, tile, coord);
        //    //Play.GainBenefits(Player, tile.Benefits);
        //    //Play.CalculateAllTilesPerTurnBenefits(this);
        //    //Play.CalculateGoalProgress(Player);
        //}

        // ------------------------------------------------------------------------------------------


        // ------------------------------------------------------------------------------------------
        public void EndGame()
        {
        }
    }
}
