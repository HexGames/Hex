using System.Collections.Generic;

namespace Logic
{
    public class GameMain
    {
        public static GameMain X = null;

        public Hex.Data.TilePool Decks = null;
        public Hex.Data.Player Player = null;
        public Hex.Data.Turn Turn = null;

        public int TurnNo = 0;
        public bool Finished = false;

        // ------------------------------------------------------------------------------------------
        public GameMain()
        {
            X = this;

            Decks = new Hex.Data.TilePool();
            Player = new Hex.Data.Player();
            Hex.Data.Map.Init(Start.GenerateMap());
            Turn = new Hex.Data.Turn();
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
