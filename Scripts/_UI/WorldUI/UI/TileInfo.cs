using GodotUI;
using System.Collections.Generic;

namespace UI
{
    public static class TileInfo3D
    {
        private static List<UITileInfo3D> _tileInfoPool = new List<UITileInfo3D>();
        private static Dictionary<Hex.Data.HexPos, int> _tileInfoIdx = new Dictionary<Hex.Data.HexPos, int>();

        // ---------------------------------------------------------------------------------------------------
        public static void Add(Hex.Data.HexPos forTile, string text)
        {
            if (_tileInfoIdx.TryGetValue(forTile, out int poolIdx) == true)
            {
                return;
            }

            int tileInfoIdx = GetNewTileInfoIdx();
            UITileInfo3D tileInfo = _tileInfoPool[tileInfoIdx];
            tileInfo.Show(forTile, text);
            tileInfo.Name = $"TileInfo_{forTile}";
            tileInfo.Visible = true;

            _tileInfoIdx.Add(forTile, tileInfoIdx);
        }

        public static void Refresh(Hex.Data.HexPos forTile, string text)
        {
            if (_tileInfoIdx.TryGetValue(forTile, out int poolIdx) == true)
            {
                _tileInfoPool[poolIdx].Refresh(text);
            }
        }

        public static void Remove(Hex.Data.HexPos forTile)
        {
            if (_tileInfoIdx.TryGetValue(forTile, out int poolIdx) == true)
            {
                _tileInfoPool[poolIdx].Hide();

                _tileInfoPool[poolIdx].Name = "TileInfo_unused";
                _tileInfoPool[poolIdx].Visible = false;

                _tileInfoIdx.Remove(forTile);
            }
        }

        public static void ClearAll()
        {
            foreach (var tielInfo in _tileInfoPool)
            {
                tielInfo.Name = "TileInfo_unused";
                tielInfo.Visible = false;
            }
            _tileInfoIdx.Clear();
        }

        // ---------------------------------------------------------------------------------------------------
        private static int GetNewTileInfoIdx()
        {
            if (_tileInfoPool.Count == 0)
            {
                UITileInfo3D prototype = GodotUI.UIMain.X.TileInfoPrototype;
                prototype.Visible = false;
                _tileInfoPool.Add(prototype);
                return 0;
            }

            for (int idx = 0; idx < _tileInfoPool.Count; idx++)
            {
                if (_tileInfoPool[idx].Visible == false)
                {
                    return idx;
                }
            }

            UITileInfo3D newTileInfo = _tileInfoPool[0].Duplicate(7) as UITileInfo3D;
            _tileInfoPool[0].GetParent().AddChild(newTileInfo);
            _tileInfoPool.Add(newTileInfo);
            return _tileInfoPool.Count - 1;
        }
    }
}
