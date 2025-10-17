using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Data
{
    public class EffectNode
    {
        // ------------------------------------------------------------------------------------------ types
        public enum BonusType
        {
            Add,
            Multiply,
            Reactivate
        }

        private struct TileBonuus
        {
            public Tile Tile;
            public int Value;
            public BonusType Type;

            public TileBonuus(Tile tile, int value, BonusType type)
            {
                Tile = tile;
                Value = value;
                Type = type;
            }
        }

        private struct NodeBonuus
        {
            public EffectNode OtherNode;
            public BonusType Type;

            public NodeBonuus(EffectNode otherNode, BonusType type)
            {
                OtherNode = otherNode;
                Type = type;
            }
        }

        public struct Values 
        {
            public int value;
            public int multiply;
            public int reactivate;
        }

        // ------------------------------------------------------------------------------------------ variables
        public Tile ForTile;
        public Def.Var EffectDef;
        public Def.Timing Timing;
        public Def.Res ResDef = null;

        //public int _baseValue = 0;
        public int _baseValue
        {
            get => xbaseValue;
            set => xbaseValue = value;
        }
        private int xbaseValue = 0;
        private readonly List<TileBonuus> _tileBonuses = new List<TileBonuus>();
        private readonly List<NodeBonuus> _nodeBonuses = new List<NodeBonuus>();


        // ------------------------------------------------------------------------------------------ constructor
        public EffectNode(Tile forTile, Def.Var forEffect)
        {
            ForTile = forTile;
            EffectDef = forEffect;
            Timing = EffectDef.GetTiming(0);
            if (EffectDef.IsRes(1)) ResDef = EffectDef.GetRes(1);
        }

        // ------------------------------------------------------------------------------------------ methods
        public void Clear()
        {
            _baseValue = 0;
            _tileBonuses.Clear();
            _nodeBonuses.Clear();
        }

        public void SetBaseValue(int value)
        {
            _baseValue = value;
        }

        public void AddTileBonus(Tile fromTile, int value, BonusType type)
        {
            _tileBonuses.Add(new TileBonuus(ForTile, value, type));
        }

        public void AddNodeBonus(EffectNode otherNode, BonusType type)
        {
            _nodeBonuses.Add(new NodeBonuus(otherNode, type));
        }

        public Values GetValues()
        {
            List<EffectNode> processedNodes = new List<EffectNode>();
            return GetValues(processedNodes);
        }

        private Values GetValues(List<EffectNode> processedNodes)
        {
            int value = _baseValue;
            int multiply = 1;
            int reactivate = 0;

            foreach (var tileBonus in _tileBonuses)
            {
                if (tileBonus.Type == BonusType.Add)
                {
                    value += tileBonus.Value;
                }
                else if (tileBonus.Type == BonusType.Multiply)
                {
                    multiply += tileBonus.Value;
                }
                else if (tileBonus.Type == BonusType.Reactivate)
                {
                    reactivate += tileBonus.Value;
                }
            }
            foreach (var nodeBonus in _nodeBonuses)
            {
                if (processedNodes.Contains(nodeBonus.OtherNode) == true)
                    continue;

                if (nodeBonus.Type == BonusType.Add)
                {
                    Values values = nodeBonus.OtherNode.GetValues();
                    value += values.value * values.multiply * (1 * values.multiply);
                }
                else if (nodeBonus.Type == BonusType.Multiply)
                {
                    Values values = nodeBonus.OtherNode.GetValues();
                    multiply += values.value * values.multiply * (1 * values.multiply);
                }
                else if (nodeBonus.Type == BonusType.Reactivate)
                {
                    Values values = nodeBonus.OtherNode.GetValues();
                    reactivate += values.value * values.multiply * (1 * values.multiply);
                }
            }
            return new Values { value = value, multiply = multiply, reactivate = reactivate };
        }

        // ------------------------------------------------------------------------------------------ statics
        public static Def.Timing GetTiming(Def.Var effectDef)
        {
            return effectDef.GetTiming(0);
        }
    }
}
