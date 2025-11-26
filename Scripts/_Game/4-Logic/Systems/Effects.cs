using System.Collections.Generic;

namespace Logic
{
    public static partial class Effects
    {
        private static Dictionary<Data.Tile, List<Node>> _effectNodes = new Dictionary<Data.Tile, List<Node>>();
        private static Dictionary<Data.Tile, List<Node>> _effectOverwriteNodes = new Dictionary<Data.Tile, List<Node>>();

        // -----------------------------------------------------------------------------------------
        private static Data.Tile _overwriteTile = null;
        private static Data.HexPos _overwriteCoords = Data.HexPos.Invalid;
        public static void ReapplyAllEffectsWithOverwriteTile(Data.Tile overwriteTile, Data.HexPos overwriteCoords)
        {
            _overwriteTile = overwriteTile;
            _overwriteCoords = overwriteCoords;
            ReapplyAllEffects();
            _overwriteTile = null;
            _overwriteCoords = Data.HexPos.Invalid;
        }

        //public static void AddTile(Data.Tile tile)
        //{
        //    if (_effectNodes.ContainsKey(tile) == true) Debug.LogError("Tile effects already added");
        //
        //    List<Node> tileEffectNodes = new List<Node>();
        //    List<Node> tileEffectOverwriteNodes = new List<Node>();
        //    foreach (Def.Var effectVar in tile.Def.Effects)
        //    {
        //        tileEffectNodes.Add(new Node(tile, effectVar));
        //        tileEffectOverwriteNodes.Add(new Node(tile, effectVar));
        //    }
        //    _effectNodes[tile] = tileEffectNodes;
        //    _effectOverwriteNodes[tile] = tileEffectOverwriteNodes;
        //}

        public static void ReapplyAllEffects()
        {
            Dictionary<Data.Tile, List<Node>> effects = _effectNodes;
            if (_overwriteTile != null)
            {
                effects = _effectOverwriteNodes;
            }

            // fitst clear all
            effects.Clear();
            for (int tileIdx = 0; tileIdx < Data.Map.TilesInPlay.Count; tileIdx++)
            {
                Data.Tile tile = Data.Map.TilesInPlay[tileIdx];
                List<Node> tileEffectNodes = new List<Node>();
                foreach (Def.Var effectVar in tile.Def.Effects)
                {
                    tileEffectNodes.Add(new Node(tile, effectVar));
                }
                effects[tile] = tileEffectNodes;
            }

            // then reapply
            for (int tileIdx = 0; tileIdx < Data.Map.TilesInPlay.Count; tileIdx++)
            {
                Data.Tile tile = Data.Map.TilesInPlay[tileIdx];
                Data.HexPos coords = Data.Map.GetCoords(tile);
                if (_overwriteTile != null && coords == _overwriteCoords)
                {
                    tile = _overwriteTile;
                }

                ReapplyAllEffectsForTile(effects, coords, tile);
            }
        }

        private static void ReapplyAllEffectsForTile(Dictionary<Data.Tile, List<Node>> effects, Data.HexPos coords, Data.Tile tile)
        {
            if (effects.TryGetValue(tile, out List<Node> tileEffectNodes) == true)
            {
                foreach (Node effectNode in tileEffectNodes)
                {
                    Def.Var effectDef = effectNode.EffectDef;
                    int baseValue = 0;

                    if (effectNode.ResDef == null)
                    {
                        // verify condition
                        string keyword = effectDef.GetString(1);
                        if (keyword == "AddAdjacent")
                        {
                            ApplyAdjacentEffectNode(effects, effectNode, coords, effectDef, Node.BonusType.Add);
                        }
                        else if (keyword == "MultiplyAdjacent")
                        {
                            ApplyAdjacentEffectNode(effects, effectNode, coords, effectDef, Node.BonusType.Multiply);
                        }
                        else if (keyword == "ReactivateAdjacent")
                        {
                            ApplyAdjacentEffectNode(effects, effectNode, coords, effectDef, Node.BonusType.Reactivate);
                        }
                    }

                    for (int valueGroupIdx = 2; valueGroupIdx < effectDef.GetCount(); valueGroupIdx++)
                    {
                        if (effectDef.GetSubCount(valueGroupIdx) == 1)
                        {
                            baseValue += effectDef.GetInt(valueGroupIdx);
                        }
                    }
                    effectNode.SetBaseValue(baseValue);
                    for (int valueGroupIdx = 2; valueGroupIdx < effectDef.GetCount(); valueGroupIdx++)
                    {
                        if (effectDef.GetSubCount(valueGroupIdx) == 3)
                        {
                            // verify condition
                            string codition = effectDef.GetString(valueGroupIdx, 1);
                            if (codition == "IfAdjacent")
                            {
                                ApplyAdjacentEffectTile(effectNode, coords, effectDef.GetString(valueGroupIdx, 2), effectDef.GetInt(valueGroupIdx, 0), true, false, Node.BonusType.Add);
                            }
                            else if (effectDef.GetString(valueGroupIdx, 1) == "PerAdjacent")
                            {
                                ApplyAdjacentEffectTile(effectNode, coords, effectDef.GetString(valueGroupIdx, 2), effectDef.GetInt(valueGroupIdx, 0), false, false, Node.BonusType.Add);
                            }
                            else if (effectDef.GetString(valueGroupIdx, 1) == "PerAdjacentLevel")
                            {
                                ApplyAdjacentEffectTile(effectNode, coords, effectDef.GetString(valueGroupIdx, 2), effectDef.GetInt(valueGroupIdx, 0), false, true, Node.BonusType.Add);
                            }
                        }
                    }
                }
            }
        }

        private static void ApplyAdjacentEffectTile(Node effectNode, Data.HexPos coords, string targetTag, int value, bool justOnce, bool perLevel, Node.BonusType bonusType)
        {
            foreach (Data.HexPos direction in Data.HexPos.Directions)
            {
                Data.HexPos adjCoords = coords + direction;
                Data.Tile adjTile = Data.Map.GetTile(coords + direction);
                if (_overwriteTile != null && adjCoords == _overwriteCoords)
                {
                    adjTile = _overwriteTile;
                }
                if (adjTile != null && adjTile.Def.Tags.Contains(targetTag))
                {
                    int multiplier = 1;
                    if (perLevel) multiplier = adjTile.Def.Level;
                    effectNode.AddTileBonus(adjTile, multiplier * value, bonusType);
                    if (justOnce == true)
                        return;
                }
            }
        }

        private static void ApplyAdjacentEffectNode(Dictionary<Data.Tile, List<Node>> effects, Node effectNode, Data.HexPos coords, Def.Var effectDef, Node.BonusType bonusType)
        {
            foreach (Data.HexPos direction in Data.HexPos.Directions)
            {
                Data.HexPos adjCoords = coords + direction;
                Data.Tile adjTile = Data.Map.GetTile(coords + direction);
                if (_overwriteTile != null && adjCoords == _overwriteCoords)
                {
                    adjTile = _overwriteTile;
                }
                string targetTag = effectDef.GetString(1, 1);
                if (adjTile != null && adjTile.Def.Tags.Contains(targetTag))
                {
                    if (effects.TryGetValue(adjTile, out List<Node> tileEffectNodes) == true)
                    {
                        foreach (Node adjEffectNode in tileEffectNodes)
                        {
                            {
                                adjEffectNode.AddNodeBonus(effectNode, bonusType);
                            }
                        }
                    }
                }
            }
        }

        // -----------------------------------------------------------------------------------------
        private static List<Data.Benefit> _benefitsTotal = new List<Data.Benefit>();
        private static List<Data.Benefit> _benefitsAtCoords = new List<Data.Benefit>();
        private static List<Data.Benefit> _benefitsTotalWithOverwrite = new List<Data.Benefit>();
        private static List<Data.Benefit> _benefitsAtCoordsWithOverwrite = new List<Data.Benefit>();
        //private static List<Data.Benefit> _benefitsSteps = new List<Data.Benefit>(); // TO DO ?

        public static void CalculateAllBenefitsWithOverwriteTile(Data.Tile overwriteTile, Data.HexPos overwriteCoords, out List<Data.Benefit> benefitsTotal, out List<Data.Benefit> benefitsAtCoords)
        {
            _overwriteTile = overwriteTile;
            _overwriteCoords = overwriteCoords;
            benefitsTotal = _benefitsTotalWithOverwrite;
            benefitsAtCoords = _benefitsAtCoordsWithOverwrite;
            AddAllBenefits(benefitsTotal, benefitsAtCoords);
            _overwriteTile = null;
            _overwriteCoords = Data.HexPos.Invalid;
        }

        public static void CalculateAllBenefits(out List<Data.Benefit> benefitsTotal, out List<Data.Benefit> benefitsAtCoords)
        {
            benefitsTotal = _benefitsTotal;
            benefitsAtCoords = _benefitsAtCoords;
            AddAllBenefits(benefitsTotal, benefitsAtCoords);
        }

        private static void AddAllBenefits(List<Data.Benefit> benefitsTotal, List<Data.Benefit> benefitsAtCoords)
        {
            Dictionary<Data.Tile, List<Node>> effects = _effectNodes;
            if (_overwriteTile != null)
            {
                effects = _effectOverwriteNodes;
            }

            benefitsTotal.Clear();
            benefitsAtCoords.Clear();

            for (int tileIdx = 0; tileIdx < Data.Map.TilesInPlay.Count; tileIdx++)
            {
                Data.Tile tile = Data.Map.TilesInPlay[tileIdx];
                Data.HexPos coords = Data.Map.GetCoords(tile);
                if (_overwriteTile != null && coords == _overwriteCoords)
                {
                    tile = _overwriteTile;
                }

                if (effects.TryGetValue(tile, out List<Node> tileEffectNodes) == true)
                {
                    foreach (Node effectNode in tileEffectNodes)
                    {
                        Def.Timing benefitTiming = effectNode.Timing;
                        if (benefitTiming != Def.Timing.OnPlace)
                            continue;
                        Def.Res def = effectNode.ResDef;
                        Data.HexPos hexPos = coords;
                        Node.Values values = effectNode.GetValues();
                        int totalValue = values.value * values.multiply * ( 1 + values.reactivate);

                        AddBenefitToList(benefitsAtCoords, def, totalValue, hexPos, benefitTiming);
                        AddBenefitToList(benefitsTotal, def, totalValue, Data.HexPos.Invalid, benefitTiming);
                    }
                }
            }
        }

        public static void AddBenefitToList(List<Data.Benefit> benefits, Def.Res resDef, int value, Data.HexPos coords, Def.Timing timing)
        {
            for (int idx = 0; idx < benefits.Count; idx++)
            {
                Data.Benefit benefit = benefits[idx];
                if (benefit.Res.Def == resDef && benefit.BenefitTiming == timing && benefit.HexPos == coords)
                {
                    benefit.Res.Value += value;
                    benefits[idx] = benefit;
                    return;
                }
            }
            Data.Benefit newBenefit = new Data.Benefit(resDef, value, coords, timing);
            benefits.Add(newBenefit);
        }

        // -----------------------------------------------------------------------------------------
        public static void RemoveBenefitsRange(List<Data.Benefit> originalBenefits, List<Data.Benefit> toRemoveBenefits)
        {
            foreach (Data.Benefit toRemove in toRemoveBenefits)
            {
                for (int idx = 0; idx < originalBenefits.Count; idx++)
                {
                    Data.Benefit original = originalBenefits[idx];
                    if (original.Res.Def == toRemove.Res.Def && original.BenefitTiming == toRemove.BenefitTiming && original.HexPos == toRemove.HexPos)
                    {
                        original.Res.Value -= toRemove.Res.Value;
                        if (original.Res.Value == 0)
                        {
                            originalBenefits.RemoveAt(idx);
                        }
                        else
                        {
                            originalBenefits[idx] = original;
                        }
                        break;
                    }
                }
            }
        }

        // ----------------------------------------------------------------------------------------- for UI
        public static string GetTileEddectsDescription(Data.Tile tile, Def.Timing timingFilter)
        {
            string description = "";
            if (_effectNodes.TryGetValue(tile, out List<Node> tileEffectNodes) == true)
            {
                foreach (Node effectNode in tileEffectNodes)
                {
                    if (effectNode.Timing == timingFilter)
                    {
                        description += GodotUI.UIHelper.EffectVarToString(effectNode.EffectDef) + "\n";
                    }
                }
            }
            return description;
        }
    }
}
