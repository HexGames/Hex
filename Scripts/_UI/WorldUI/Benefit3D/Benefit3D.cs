using System.Collections.Generic;

namespace Hex.UI
{
    public static class Benefit3D
    {
        private static List<GodotUI.Benefit3DControl> _benefit3DPool = new List<GodotUI.Benefit3DControl>();
        private static Dictionary<Hex.Data.HexPos, List<int>> _benefit3DIdx = new Dictionary<Hex.Data.HexPos, List<int>>();

        // ---------------------------------------------------------------------------------------------------
        public static void Add(Hex.Data.HexPos forTile, Hex.Def.Timing timing, string text)
        {
            if (_benefit3DIdx.TryGetValue(forTile, out List<int> poolIdxes) == true)
            {
                int idx = 0;
                while (idx < poolIdxes.Count && _benefit3DPool[idx].Timing < timing)
                {
                    idx++;
                }

                int offsetStart = (poolIdxes.Count + 1) / 2;
                int offset = offsetStart;
                int newBenefitOffset = 0;
                for (int otherIdx = 0; otherIdx < poolIdxes.Count; otherIdx++)
                {
                    if (otherIdx == idx)
                    {
                        newBenefitOffset = offsetStart + offset;
                        offset += 2;
                    }
                    _benefit3DPool[poolIdxes[otherIdx]].MoveToOffset(offsetStart + offset);
                    offset += 2;
                }

                int benefitPopIdx = GetNewBenefit(forTile, timing, text, newBenefitOffset);

                poolIdxes.Insert(idx, benefitPopIdx);
            }
            else
            {
                int benefitPopIdx = GetNewBenefit(forTile, timing, text, 0);

                poolIdxes = new List<int>();
                poolIdxes.Add(benefitPopIdx);

                _benefit3DIdx.Add(forTile, poolIdxes);
            }
        }

        private static int GetNewBenefit(Hex.Data.HexPos forTile, Hex.Def.Timing timing, string text, int onTileIdx)
        {
            int benefitPopIdx = GetNewBenefitFromPool();
            GodotUI.Benefit3DControl tileInfo = _benefit3DPool[benefitPopIdx];
            tileInfo.SetData(forTile, timing, text, onTileIdx);
            tileInfo.Show(forTile, text);
            tileInfo.Name = $"TileInfo_{forTile}_{onTileIdx}";
            tileInfo.Visible = true;
            return benefitPopIdx;
        }

        //public static void Refresh(Data.HexPos forTile, Def.Timing timing, string text)
        //{
        //    if (_benefitPopIdx.TryGetValue(forTile, out List<int> poolIdxes) == true)
        //    { 
        //        _benefitPopPool[poolIdx].Refresh(text);
        //    }
        //}

        public static void Pop(Hex.Data.HexPos forTile, Hex.Def.Timing timing)
        {
            if (_benefit3DIdx.TryGetValue(forTile, out List<int> poolIdxes) == true)
            {
                for (int idx = 0; idx < poolIdxes.Count; idx++)
                {
                    GodotUI.Benefit3DControl benefit3D = _benefit3DPool[poolIdxes[idx]];
                    if (benefit3D.Timing == timing)
                    {
                        benefit3D.Pop();
                        return;
                    }
                }
            }

            Debug.LogError($"[Benefit3D] Benefit at {forTile} with timing {timing} not found.");
        }

        //public static void Remove(Data.HexPos forTile, Def.Timing timing)
        //{
        //    if (_benefit3DIdx.TryGetValue(forTile, out List<int> poolIdxes) == true)
        //    {
        //        _benefit3DPool[poolIdx].Name = "BenefitPop_unused";
        //        _benefit3DPool[poolIdx].Visible = false;
        //
        //        _benefit3DIdx.Remove(forTile);
        //    }
        //}

        //public static void ClearAll()
        //{
        //    foreach (var tielInfo in _benefit3DPool)
        //    {
        //        tielInfo.Name = "BenefitPop_unused";
        //        tielInfo.Visible = false;
        //    }
        //    _benefit3DIdx.Clear();
        //}

        // ---------------------------------------------------------------------------------------------------
        private static int GetNewBenefitFromPool()
        {
            if (_benefit3DPool.Count == 0)
            {
                GodotUI.Benefit3DControl prototype = GodotUI.UIMain.X.Benefit3DPrototype;
                prototype.Visible = false;
                _benefit3DPool.Add(prototype);
                return 0;
            }

            for (int idx = 0; idx < _benefit3DPool.Count; idx++)
            {
                if (_benefit3DPool[idx].Visible == false)
                {
                    return idx;
                }
            }

            GodotUI.Benefit3DControl newBenefitPop = _benefit3DPool[0].Duplicate(7) as GodotUI.Benefit3DControl;
            _benefit3DPool[0].GetParent().AddChild(newBenefitPop);
            _benefit3DPool.Add(newBenefitPop);
            return _benefit3DPool.Count - 1;
        }
    }
}
