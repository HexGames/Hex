using System.Collections.Generic;

namespace Hex.Data
{
    public enum TileState
    {
        InMeta,
        InDrawDeck,
        InNextQueue,
        OnMap
    }

    public struct Tile
    {
        internal const int MAX_DECK = 91; // so that MAX_DECK + MAX_MAP = 128
        internal const int MAX_MAP = 37; // map size is 1 + 6 * (1 + 2 + 3) = 37 tiles

        private readonly int _defID = -1;
        public readonly Def.Tile Def { get => Hex.Def.Lib.GetTile(_defID); }
        public readonly Def.TileData DefData;
        public bool IsDeckTile = false; // the big player deck with all the tiles, not just the draw deck
        public TileState State = TileState.InMeta;

        //private readonly List<EffectNode> Effects = new List<EffectNode>();
        //private readonly List<EffectNode> OverwriteEffects = new List<EffectNode>();

        public Tile(Def.Tile def)
        {
            _defID = def.ID;
            DefData = Hex.Def.Lib.GetTileData(_defID);

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

