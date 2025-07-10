using Godot;
using Godot.Collections;
using System.ComponentModel;

namespace GodotUI
{
    public partial class UIMenu : AnimControl
    {
        public void OnStartGame()
        {
            Actions.OnStartRun();
        }
    }
}