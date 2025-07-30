using Godot;
using Godot.Collections;
using System.Collections.Generic;

namespace GodotUI
{
    public partial class UIResources : AnimControl
    {
        private struct ItemData
        {
            public Def.Res ResDef;
            public int Value;
            public int Income;
        }

        [Export]
        private UIResourcesItem ResPrototype;
        private List<UIResourcesItem> Res = new List<UIResourcesItem>();


        private List<ItemData> _ItemsData = new List<ItemData>();

        public override void _Ready()
        {
            base._Ready();
            Res.Add(ResPrototype);
        }

        public void Refresh(List<Data.Res> stockpile, List<Data.Res> income)
        {
            // refresh items data
            _ItemsData.Clear();
            foreach (Def.Res resDef in Def.Lib.Res)
            {
                int resValue = 0;
                int resIncome = 0;
                foreach (Data.Res res in stockpile)
                {
                    if (res.Def == resDef)
                    {
                        resValue = res.Value;
                        break;
                    }
                }
                foreach (Data.Res res in income)
                {
                    if (res.Def == resDef)
                    {
                        resIncome = res.Value;
                        break;
                    }
                }
                if (resValue != 0 || resIncome != 0)
                {
                    _ItemsData.Add(new ItemData { ResDef = resDef, Value = resValue, Income = resIncome });
                }
            }

            // grow
            while (Res.Count < _ItemsData.Count)
            {
                UIResourcesItem newItem = Res[0].Duplicate(7) as UIResourcesItem;
                Res[0].GetParent().AddChild(newItem);
                Res.Add(newItem);
            }

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

            Refresh(true);
        }

        public void Refresh(bool show)
        {
            if (show)
            {
                ShowAnim();
            }
            else
            {
                HideAnim();
            }
        }
    }
}