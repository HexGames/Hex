using System.Collections.Generic;

namespace Hex.UI
{
    public static class Benefit3D
    {
        private static List<GodotUI.Benefit3DControl> _benefit3DPool = new List<GodotUI.Benefit3DControl>();

        // ---------------------------------------------------------------------------------------------------
        public static void Create(Data.HexPos forTile, Def.Timing timing, int tileIdx, int tileCount, out int benefitID)
        {
            benefitID = GetNewBenefit(forTile, timing, tileIdx);

            _benefit3DPool[benefitID].Visible = true;
            int offsetStart = (tileCount + 1) / 2;
            _benefit3DPool[benefitID].MoveToOffset(offsetStart + tileIdx);
        }

        private static int GetNewBenefit(Data.HexPos forTile, Def.Timing timing, int tileIdx)
        {
            int benefitPopIdx = GetNewBenefitFromPool();
            GodotUI.Benefit3DControl tileInfo = _benefit3DPool[benefitPopIdx];
            tileInfo.SetData(forTile, timing, tileIdx);
            tileInfo.Name = $"TileInfo_{forTile}_{tileIdx}";
            return benefitPopIdx;
        }

        public static void Show(in int benefitID, string prefix, string valueText, string suffix)
        {
            _benefit3DPool[benefitID].RefreshTexts(prefix, valueText, suffix);
            _benefit3DPool[benefitID].ShowBenefit();
        }

        public static void ChangeValue(in int benefitID, string valueText)
        {
            _benefit3DPool[benefitID].ChangeValue(valueText);
        }

        public static void FadeOut(in int benefitID)
        {
            _benefit3DPool[benefitID].FadeOut();
        }

        public static void Pop(in int benefitID)
        {
            _benefit3DPool[benefitID].Pop();
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
