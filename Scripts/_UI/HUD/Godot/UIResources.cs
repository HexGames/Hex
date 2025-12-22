using Godot;
using Godot.Collections;
using System.Collections.Generic;

namespace Hex.GodotUI
{
    public partial class UIResources : AnimControl
    {
        public struct ItemData
        {
            public Def.ResRef ResDef;
            public int Value;
            public int Income;
        }

        [Export]
        private UIResourcesItem ResPrototype;
        private List<UIResourcesItem> Res = new List<UIResourcesItem>();

        public override void _Ready()
        {
            base._Ready();
            Res.Add(ResPrototype);
        }

        public void Refresh(List<ItemData> _ItemsData)
        {
            // grow
            while (Res.Count < _ItemsData.Count)
            {
                UIResourcesItem newItem = Res[0].Duplicate(7) as UIResourcesItem;
                Res[0].GetParent().AddChild(newItem);
                Res.Add(newItem);
            }

            // refresh items
            for (int idx = 0; idx < Res.Count; idx++)
            {
                if (idx < _ItemsData.Count)
                {
                    Res[idx].SetRes(_ItemsData[idx].ResDef, _ItemsData[idx].Value, _ItemsData[idx].Income);
                    Res[idx].Visible = true;
                }
                else
                {
                    Res[idx].Visible = false;
                }
            }
        }
    }
}