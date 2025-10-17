using GodotUI;
using System.Collections.Generic;

namespace UI
{
    public static class BenefitPop3D
    {
        private static List<UIBenefitPop3D> _benefitPopPool = new List<UIBenefitPop3D>();
        private static Dictionary<Data.HexCoords, int> _benefitPopIdx = new Dictionary<Data.HexCoords, int>();

        // ---------------------------------------------------------------------------------------------------
        public static void Add(Data.HexCoords forTile, string text)
        {
            if (_benefitPopIdx.TryGetValue(forTile, out int poolIdx) == true)
            {
                return;
            }

            int benefitPopIdx = GetNewTileInfoIdx();
            UIBenefitPop3D tileInfo = _benefitPopPool[benefitPopIdx];
            tileInfo.Show(forTile, text);
            tileInfo.Name = $"TileInfo_{forTile}";
            tileInfo.Visible = true;

            _benefitPopIdx.Add(forTile, benefitPopIdx);
        }

        public static void Remove(Data.HexCoords forTile)
        {
            if (_benefitPopIdx.TryGetValue(forTile, out int poolIdx) == true)
            {
                _benefitPopPool[poolIdx].Name = "BenefitPop_unused";
                _benefitPopPool[poolIdx].Visible = false;

                _benefitPopIdx.Remove(forTile);
            }
        }

        public static void ClearAll()
        {
            foreach (var tielInfo in _benefitPopPool)
            {
                tielInfo.Name = "BenefitPop_unused";
                tielInfo.Visible = false;
            }
            _benefitPopIdx.Clear();
        }

        // ---------------------------------------------------------------------------------------------------
        private static int GetNewTileInfoIdx()
        {
            if (_benefitPopPool.Count == 0)
            {
                UIBenefitPop3D prototype = UIMain.X.BenefitPopPrototype;
                prototype.Visible = false;
                _benefitPopPool.Add(prototype);
                return 0;
            }

            for (int idx = 0; idx < _benefitPopPool.Count; idx++)
            {
                if (_benefitPopPool[idx].Visible == false)
                {
                    return idx;
                }
            }

            UIBenefitPop3D newBenefitPop = _benefitPopPool[0].Duplicate(7) as UIBenefitPop3D;
            _benefitPopPool[0].GetParent().AddChild(newBenefitPop);
            _benefitPopPool.Add(newBenefitPop);
            return _benefitPopPool.Count - 1;
        }
    }
}
