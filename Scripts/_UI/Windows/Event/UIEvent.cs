using Godot;
using Godot.Collections;
using System.Collections.Generic;

namespace GodotUI
{
    public partial class UIEvent : AnimControl // unused - todo use or remove
    {
        [Export]
        private UIText Title;
        [Export]
        private UIText Description;

        //public void Refresh(bool show)
        //{
        //    if (show)
        //    {
        //        //Visible = true;
        //        ShowWithAnim();
        //    }
        //    else
        //    {
        //        HideWithAnim();
        //        //Visible = false;
        //    }
        //}

        public void OnChoice_0()
        {
            //Data.Timeline timeline = Logic.Game.Timeline;
            //timeline.Event = false;
            //
            //Actions.OnEventWindow(false);
        }
        public void OnClose()
        {
            //Actions.OnEventWindow(false);
        }
    }
}