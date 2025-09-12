using Godot;
using System;
using System.Collections.Generic;

namespace GodotUI
{
    public partial class UIWTileInfosPool : Node, IUIWTileInfos
    {
        [Export]
        private UIWTileInfo _tileInfoPrototype;

        private List<UIWTileInfo> _tileInfoPool = new List<UIWTileInfo>();
        private Dictionary<Data.HexCoord, int> _tileInfoIdx = new Dictionary<Data.HexCoord, int>();

        public override void _Ready()
        {
            _tileInfoPrototype.Visible = false;
            _tileInfoPool.Add(_tileInfoPrototype);
        }

        public void AddTileInfo(Data.HexCoord forTile, string text)
        {
            if (_tileInfoIdx.TryGetValue(forTile, out int poolIdx) == true)
            {
                return;
            }

            int tileInfodx = GetNewTileInfoIdx();
            UIWTileInfo tileInfo = _tileInfoPool[tileInfodx];
            tileInfo.Refresh(forTile, text);
            tileInfo.Name = $"TileInfo_{forTile}";
            tileInfo.Visible = true;

            _tileInfoIdx.Add(forTile, tileInfodx);
        }

        public void RemoveTileInfo(Data.HexCoord forTile)
        {
            if (_tileInfoIdx.TryGetValue(forTile, out int poolIdx) == true)
            {
                _tileInfoPool[poolIdx].Name = "TileInfo_unused";
                _tileInfoPool[poolIdx].Visible = false;

                _tileInfoIdx.Remove(forTile);
            }
        }

        public void ClearTileInfos()
        {
            foreach (var tielInfo in _tileInfoPool)
            {
                tielInfo.Name = "Arrow_unused";
                tielInfo.Visible = false;
            }
            _tileInfoIdx.Clear();
        }

        private int GetNewTileInfoIdx()
        {
            for (int idx = 0; idx < _tileInfoPool.Count; idx++)
            {
                if (_tileInfoPool[idx].Visible == false)
                {
                    return idx;
                }
            }

            UIWTileInfo newTileInfo = _tileInfoPrototype.Duplicate(7) as UIWTileInfo;
            _tileInfoPrototype.GetParent().AddChild(newTileInfo);
            _tileInfoPool.Add(newTileInfo);
            return _tileInfoPool.Count - 1;
        }
    }
}
