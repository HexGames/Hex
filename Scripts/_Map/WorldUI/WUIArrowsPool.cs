using Godot;
using System.Collections.Generic;

namespace Godot3D
{
    public partial class WUIArrowsPool : Node
    {
        [Export]
        private WUIArrow _arrowPrototype;

        private List<WUIArrow> _arrowPool = new List<WUIArrow>();
        private Dictionary<(Data.HexCoord fromTile, Data.HexCoord toTile), int> _arrowsIdx = new Dictionary<(Data.HexCoord fromTile, Data.HexCoord toTile), int>();

        public override void _Ready()
        {
            _arrowPrototype.Visible = false;
            _arrowPool.Add(_arrowPrototype);
        }

        public void AddArrow(Data.HexCoord fromTile, Data.HexCoord toTile)
        {
            if (_arrowsIdx.TryGetValue((fromTile, toTile), out int poolIdx) == true)
            {
                return;
            }

            int arrowIdx = GetNewArrowIdx();
            WUIArrow arrow = _arrowPool[arrowIdx];
            arrow.Refresh(fromTile, toTile);
            arrow.Name = $"Arrow_{fromTile}_{toTile}";
            arrow.Visible = true;

            _arrowsIdx.Add((fromTile, toTile), arrowIdx);
        }

        public void RemoveArrow(Data.HexCoord fromTile, Data.HexCoord toTile)
        {
            if (_arrowsIdx.TryGetValue((fromTile, toTile), out int poolIdx) == true)
            {
                _arrowPool[poolIdx].Name = "Arrow_unused";
                _arrowPool[poolIdx].Visible = false;

                _arrowsIdx.Remove((fromTile, toTile));
            }
        }

        public void ClearArrows()
        {
            foreach (var arrow in _arrowPool)
            {
                arrow.Name = "Arrow_unused";
                arrow.Visible = false;
            }
            _arrowsIdx.Clear();
        }

        private int GetNewArrowIdx()
        {
            for (int idx = 0; idx < _arrowPool.Count; idx++)
            {
                if (_arrowPool[idx].Visible == false)
                {
                    return idx;
                }
            }

            WUIArrow newArrow = _arrowPrototype.Duplicate(7) as WUIArrow;
            _arrowPrototype.GetParent().AddChild(newArrow);
            _arrowPool.Add(newArrow);
            return _arrowPool.Count - 1;
        }
    }
}
