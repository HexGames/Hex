using System.Collections.Generic;

namespace Hex.UI
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

        public static void Refresh(List<Data.Res> stockpile, List<Hex.Data.Res> income)
        {
            // refresh items data
            _ItemsData.Clear();
            foreach (Def.Res res in Def.Lib.Res)
            {
                Def.ResRef resDef = Def.ResRef.FromID(res.ID);
                int resValue = 0;
                int resIncome = 0;
                foreach (Data.Res sRes in stockpile)
                {
                    if (sRes.ResDef == resDef)
                    {
                        resValue = res.Value;
                        break;
                    }
                }
                foreach (Data.Res iRes in income)
                {
                    if (iRes.ResDef == resDef)
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
