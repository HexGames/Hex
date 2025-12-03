using System.Collections.Generic;

namespace Logic
{
    public static partial class Effects
    {
        private static Dictionary<Hex.Data.Tile, List<Node>> _effectNodes = new Dictionary<Hex.Data.Tile, List<Node>>();
        private static Dictionary<Hex.Data.Tile, List<Node>> _effectOverwriteNodes = new Dictionary<Hex.Data.Tile, List<Node>>();

        // -----------------------------------------------------------------------------------------
        private static Hex.Data.Tile _overwriteTile = null;
        private static Hex.Data.HexPos _overwriteCoords = Hex.Data.HexPos.Invalid;
        public static void ReapplyAllEffectsWithOverwriteTile(Hex.Data.Tile overwriteTile, Hex.Data.HexPos overwriteCoords)
        {
            _overwriteTile = overwriteTile;
            _overwriteCoords = overwriteCoords;
            ReapplyAllEffects();
            _overwriteTile = null;
            _overwriteCoords = Hex.Data.HexPos.Invalid;
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
            Dictionary<Hex.Data.Tile, List<Node>> effects = _effectNodes;
            if (_overwriteTile != null)
            {
                effects = _effectOverwriteNodes;
            }

            // fitst clear all
            effects.Clear();
            for (int tileIdx = 0; tileIdx < Hex.Data.Map.TilesInPlay.Count; tileIdx++)
            {
                Hex.Data.Tile tile = Hex.Data.Map.TilesInPlay[tileIdx];
                List<Node> tileEffectNodes = new List<Node>();
                foreach (Hex.Def.Var effectVar in tile.Def.Effects)
                {
                    tileEffectNodes.Add(new Node(tile, effectVar));
                }
                effects[tile] = tileEffectNodes;
            }

            // then reapply
            for (int tileIdx = 0; tileIdx < Hex.Data.Map.TilesInPlay.Count; tileIdx++)
            {
                Hex.Data.Tile tile = Hex.Data.Map.TilesInPlay[tileIdx];
                Hex.Data.HexPos coords = Hex.Data.Map.GetCoords(tile);
                if (_overwriteTile != null && coords == _overwriteCoords)
                {
                    tile = _overwriteTile;
                }

                ReapplyAllEffectsForTile(effects, coords, tile);
            }
        }

        private static void ReapplyAllEffectsForTile(Dictionary<Hex.Data.Tile, List<Node>> effects, Hex.Data.HexPos coords, Hex.Data.Tile tile)
        {
            if (effects.TryGetValue(tile, out List<Node> tileEffectNodes) == true)
            {
                foreach (Node effectNode in tileEffectNodes)
                {
                    Hex.Def.Var effectDef = effectNode.EffectDef;
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

        private static void ApplyAdjacentEffectTile(Node effectNode, Hex.Data.HexPos coords, string targetTag, int value, bool justOnce, bool perLevel, Node.BonusType bonusType)
        {
            foreach (Hex.Data.HexPos direction in Hex.Data.HexPos.Directions)
            {
                Hex.Data.HexPos adjCoords = coords + direction;
                Hex.Data.Tile adjTile = Hex.Data.Map.GetTile(coords + direction);
                if (_overwriteTile != null && adjCoords == _overwriteCoords)
                {
                    adjTile = _overwriteTile;
                }
                if (adjTile != null && adjTile.Def.BuildingTags.Contains(targetTag))
                {
                    int multiplier = 1;
                    if (perLevel) multiplier = adjTile.Def.Level;
                    effectNode.AddTileBonus(adjTile, multiplier * value, bonusType);
                    if (justOnce == true)
                        return;
                }
            }
        }

        private static void ApplyAdjacentEffectNode(Dictionary<Hex.Data.Tile, List<Node>> effects, Node effectNode, Hex.Data.HexPos coords, Hex.Def.Var effectDef, Node.BonusType bonusType)
        {
            foreach (Hex.Data.HexPos direction in Hex.Data.HexPos.Directions)
            {
                Hex.Data.HexPos adjCoords = coords + direction;
                Hex.Data.Tile adjTile = Hex.Data.Map.GetTile(coords + direction);
                if (_overwriteTile != null && adjCoords == _overwriteCoords)
                {
                    adjTile = _overwriteTile;
                }
                string targetTag = effectDef.GetString(1, 1);
                if (adjTile != null && adjTile.Def.BuildingTags.Contains(targetTag))
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
        private static List<Hex.Data.Benefit> _benefitsTotal = new List<Hex.Data.Benefit>();
        private static List<Hex.Data.Benefit> _benefitsAtCoords = new List<Hex.Data.Benefit>();
        private static List<Hex.Data.Benefit> _benefitsTotalWithOverwrite = new List<Hex.Data.Benefit>();
        private static List<Hex.Data.Benefit> _benefitsAtCoordsWithOverwrite = new List<Hex.Data.Benefit>();
        //private static List<Data.Benefit> _benefitsSteps = new List<Data.Benefit>(); // TO DO ?

        public static void CalculateAllBenefitsWithOverwriteTile(Hex.Data.Tile overwriteTile, Hex.Data.HexPos overwriteCoords, out List<Hex.Data.Benefit> benefitsTotal, out List<Hex.Data.Benefit> benefitsAtCoords)
        {
            _overwriteTile = overwriteTile;
            _overwriteCoords = overwriteCoords;
            benefitsTotal = _benefitsTotalWithOverwrite;
            benefitsAtCoords = _benefitsAtCoordsWithOverwrite;
            AddAllBenefits(benefitsTotal, benefitsAtCoords);
            _overwriteTile = null;
            _overwriteCoords = Hex.Data.HexPos.Invalid;
        }

        public static void CalculateAllBenefits(out List<Hex.Data.Benefit> benefitsTotal, out List<Hex.Data.Benefit> benefitsAtCoords)
        {
            benefitsTotal = _benefitsTotal;
            benefitsAtCoords = _benefitsAtCoords;
            AddAllBenefits(benefitsTotal, benefitsAtCoords);
        }

        private static void AddAllBenefits(List<Hex.Data.Benefit> benefitsTotal, List<Hex.Data.Benefit> benefitsAtCoords)
        {
            Dictionary<Hex.Data.Tile, List<Node>> effects = _effectNodes;
            if (_overwriteTile != null)
            {
                effects = _effectOverwriteNodes;
            }

            benefitsTotal.Clear();
            benefitsAtCoords.Clear();

            for (int tileIdx = 0; tileIdx < Hex.Data.Map.TilesInPlay.Count; tileIdx++)
            {
                Hex.Data.Tile tile = Hex.Data.Map.TilesInPlay[tileIdx];
                Hex.Data.HexPos coords = Hex.Data.Map.GetCoords(tile);
                if (_overwriteTile != null && coords == _overwriteCoords)
                {
                    tile = _overwriteTile;
                }

                if (effects.TryGetValue(tile, out List<Node> tileEffectNodes) == true)
                {
                    foreach (Node effectNode in tileEffectNodes)
                    {
                        Hex.Def.Timing benefitTiming = effectNode.Timing;
                        if (benefitTiming != Hex.Def.Timing.OnPlace)
                            continue;
                        Hex.Def.Res def = effectNode.ResDef;
                        Hex.Data.HexPos hexPos = coords;
                        Node.Values values = effectNode.GetValues();
                        int totalValue = values.value * values.multiply * ( 1 + values.reactivate);

                        AddBenefitToList(benefitsAtCoords, def, totalValue, hexPos, benefitTiming);
                        AddBenefitToList(benefitsTotal, def, totalValue, Hex.Data.HexPos.Invalid, benefitTiming);
                    }
                }
            }
        }

        public static void AddBenefitToList(List<Hex.Data.Benefit> benefits, Hex.Def.Res resDef, int value, Hex.Data.HexPos coords, Hex.Def.Timing timing)
        {
            for (int idx = 0; idx < benefits.Count; idx++)
            {
                Hex.Data.Benefit benefit = benefits[idx];
                if (benefit.Res.Def == resDef && benefit.BenefitTiming == timing && benefit.HexPos == coords)
                {
                    benefit.Res.Value += value;
                    benefits[idx] = benefit;
                    return;
                }
            }
            Hex.Data.Benefit newBenefit = new Hex.Data.Benefit(resDef, value, coords, timing);
            benefits.Add(newBenefit);
        }

        // -----------------------------------------------------------------------------------------
        public static void RemoveBenefitsRange(List<Hex.Data.Benefit> originalBenefits, List<Hex.Data.Benefit> toRemoveBenefits)
        {
            foreach (Hex.Data.Benefit toRemove in toRemoveBenefits)
            {
                for (int idx = 0; idx < originalBenefits.Count; idx++)
                {
                    Hex.Data.Benefit original = originalBenefits[idx];
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
        public static string GetTileEddectsDescription(Hex.Data.Tile tile, Hex.Def.Timing timingFilter)
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
