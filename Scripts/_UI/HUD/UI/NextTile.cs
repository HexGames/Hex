using System.Collections.Generic;

namespace Hex.UI
{
    public static class NextTile
    {
        public static void Show()
        {
            GodotUI.UIMain.X.NextTiles.ShowAnim();
        }

        public static void Hide()
        {
            GodotUI.UIMain.X.NextTiles.HideAnim();
        }
    }
}
