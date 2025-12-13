using System;
using System.Threading.Tasks;

namespace Hex.UI
{
    public static class MainMenu
    {
        public static Action OnStartGame;

        public static void Show()
        {
            GodotUI.UIMain.X.Menu.ShowAnim();
        }

        public static void Hide()
        {
            GodotUI.UIMain.X.Menu.HideAnim();
        }
    }
}
