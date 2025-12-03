using System.Collections.Generic;

namespace UI
{
    public static class Resources
    {
        private static List<GodotUI.UIResources.ItemData> _ItemsData = new List<GodotUI.UIResources.ItemData>();

        // ---------------------------------------------------------------------------------------------------
        public static void Show()
        {
            GodotUI.UIMain.X.Resoruces.ShowAnim();
        }

        public static void Hide()
        {
            GodotUI.UIMain.X.Resoruces.HideAnim();
        }

        public static void Refresh(List<Hex.Data.Res> stockpile, List<Hex.Data.Res> income)
        {
            // refresh items data
            _ItemsData.Clear();
            foreach (Hex.Def.Res resDef in Hex.Def.Lib.Res)
            {
                int resValue = 0;
                int resIncome = 0;
                foreach (Hex.Data.Res res in stockpile)
                {
                    if (res.Def == resDef)
                    {
                        resValue = res.Value;
                        break;
                    }
                }
                foreach (Hex.Data.Res res in income)
                {
                    if (res.Def == resDef)
                    {
                        resIncome = res.Value;
                        break;
                    }
                }
                if (resValue != 0 || resIncome != 0)
                {
                    _ItemsData.Add(new GodotUI.UIResources.ItemData { ResDef = resDef, Value = resValue, Income = resIncome });
                }
            }

            GodotUI.UIMain.X.Resoruces.Refresh(_ItemsData);
        }
    }
}
