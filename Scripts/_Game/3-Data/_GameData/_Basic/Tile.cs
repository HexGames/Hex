using System.Collections.Generic;

namespace Hex.Data
{
    public enum TileState
    {
        InMeta,
        InDrawDeck,
        InNextQueue,
        OnMap,
        Destroyed
    }

    public struct Tile
    {
        internal const int MAX_DECK = 91; // so that MAX_DECK + MAX_MAP = 128
        internal const int MAX_MAP = 37; // map size is 1 + 6 * (1 + 2 + 3) = 37 tiles

        private readonly int _defID = -1;

        public Def.Tile Def { get => Hex.Def.Lib.GetTile(_defID); }
        public Def.TileData _defData;
        public int DeckTileID = -1; // the intex from TileArray
        public int MapTileID = -1; // the intex from TileArray
        public TileState State = TileState.InMeta;

        //private readonly List<EffectNode> Effects = new List<EffectNode>();
        //private readonly List<EffectNode> OverwriteEffects = new List<EffectNode>();

        public Tile(Def.Tile def, int id)
        {
            _defID = def.ID;
            _defData = Hex.Def.Lib.GetTileData(_defID);
            if (id < MAX_DECK) DeckTileID = id;
            else MapTileID = id;

            //foreach (Def.Var effectVar in Def.Effects)
            //{
            //    Effects.Add(new EffectNode(this, effectVar));
            //    OverwriteEffects.Add(new EffectNode(this, effectVar));
            //}
        }

        public bool IsValid()
        {
            return _defID != -1;
        }

        //public List<EffectNode> GetEffectsList(bool overwrite)
        //{
        //    if (overwrite) return OverwriteEffects;
        //    return Effects;
        //}
    }
}

