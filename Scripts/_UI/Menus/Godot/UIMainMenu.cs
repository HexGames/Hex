using Godot;
using Godot.Collections;
using System.ComponentModel;

namespace GodotUI
{
    public partial class UIMainMenu : AnimControl
    {
        public void OnStartGame()
        {
            UI.MainMenu.OnStartGame.Invoke();
        }
    }
}