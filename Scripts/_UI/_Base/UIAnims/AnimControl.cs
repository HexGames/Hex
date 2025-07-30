using Godot;
using Godot.Collections;
using System.Collections.Generic;

namespace GodotUI
{
    public partial class AnimControl : Control
    {
        private SceneTree Tree = null;
        private List<AnimBase> AnimChildren = null;
        private bool AfterHideCanceled = false;

        public override void _Ready()
        {
            Tree = GetTree();

            AnimChildren = new List<AnimBase>(UIHelper.GDGetAllNodes<AnimBase>(this, true));
            Visible = false;
        }

        public void ShowAnim()
        {
            AfterHideCanceled = true;
            Visible = true;
            float delay = 0.0f;
            for (int idx = 0; idx < AnimChildren.Count; idx++)
            {
                AnimChildren[idx].ShowAnim(delay);
                delay += AnimChildren[idx].GetShowDelay();
            }
        }

        public void HideAnim()
        {
            AfterHideCanceled = false;
            float delay = 0.0f;
            for (int idx = AnimChildren.Count - 1; idx >= 0; idx--)
            {
                AnimChildren[idx].HideAnim(delay);
                delay += AnimChildren[idx].GetHideDelay();
            }

            SceneTreeTimer timer = Tree.CreateTimer(delay);
            timer.Timeout += AfterHide;
        }

        private void AfterHide()
        {
            if (AfterHideCanceled == true) return;

            Visible = false;
        }
    }
}