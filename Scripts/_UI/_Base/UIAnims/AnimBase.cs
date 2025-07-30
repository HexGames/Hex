using Godot;
using Godot.Collections;
using System.Collections.Generic;

namespace GodotUI
{
    public partial class AnimBase : Control
    {
        enum EntryAnimationType
        {
            Y_SCALE_SMALL,
            Y_SCALE_BIG,
            Y_SCALE_VERY_BIG,
            ALPHA_SLOW,
        }

        protected bool Active = false;
        protected bool Animating = false;

        private bool MustHide = false;
        private bool MustShow = false;
        private float MustDelay = 0.0f;

        [Export] private EntryAnimationType EntryAnimation = AnimBase.EntryAnimationType.Y_SCALE_SMALL;
        [Export] private bool AutoShowDelay = true;
        [Export] private float CustomShowDelay = 0.0f;
        [Export] private bool AutoHideDelay = true;
        [Export] private float CustomHideDelay = 0.0f;

        protected Vector2 DefaultScale;
        protected SceneTree Tree;
        protected readonly Color Transparent = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        protected readonly Color White = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        protected readonly float FastTime = 0.02f;
        protected readonly float MediumTime = 0.04f;
        protected readonly float SlowTime = 0.1f;
        protected readonly float VerySlowTime = 0.2f;

        private readonly Vector2 HiddenScale = new Vector2(1.0f, 0.0f);
        private bool KeepHiddenScale = false;

        public override void _Ready()
        {
            Tree = GetTree();

            CallDeferred("InitBase");
        }

        private void InitBase()
        {
            DefaultScale = Scale;
            PivotOffset = Size / 2;

            if (EntryAnimation == EntryAnimationType.Y_SCALE_BIG) Scale = HiddenScale;
            else if (EntryAnimation == EntryAnimationType.Y_SCALE_VERY_BIG) Scale = HiddenScale;
            else if (EntryAnimation == EntryAnimationType.ALPHA_SLOW) Modulate = Transparent;
            else Scale = HiddenScale;

        }

        public void ShowAnim(float delay = 0.0f)
        {
            //GD.PrintRich("[b]Show Anim " + Name + "[/b]");
            MustHide = false;
            if (Active == true && Animating == false)
            {
                MustShow = false;
                return;
            }
            else if (Animating == true)
            {
                MustDelay = delay;
                MustShow = true;
                return;
            }

            Animating = true;
            if (EntryAnimation != EntryAnimationType.ALPHA_SLOW) KeepHiddenScale = true;
            SceneTreeTimer timer = Tree.CreateTimer(delay);
            timer.Timeout += StartShowAnimation;
        }

        private void StartShowAnimation()
        {
            KeepHiddenScale = false;
            //GD.PrintRich("[b]Show Anim " + Name + " " + Time.GetTicksMsec() + "[/b]");
            Tween tween = Tree.CreateTween();
            if (EntryAnimation == EntryAnimationType.Y_SCALE_BIG) tween.TweenProperty(this, "scale", DefaultScale, SlowTime).SetTrans(Tween.TransitionType.Linear);
            else if (EntryAnimation == EntryAnimationType.Y_SCALE_VERY_BIG) tween.TweenProperty(this, "scale", DefaultScale, VerySlowTime).SetTrans(Tween.TransitionType.Linear);
            else if (EntryAnimation == EntryAnimationType.ALPHA_SLOW) tween.TweenProperty(this, "modulate", White, SlowTime).SetTrans(Tween.TransitionType.Linear);
            else tween.TweenProperty(this, "scale", DefaultScale, MediumTime).SetTrans(Tween.TransitionType.Linear); // EntryAnimationType.Y_SCALE_SMALL)
            tween.Finished += SetActive;
        }

        private void SetActive()
        {
            Animating = false;
            Active = true;
            OnSetActive();
        }

        protected virtual void OnSetActive() { }

        public float GetShowDelay()
        {
            if (AutoShowDelay)
            {
                if (EntryAnimation == EntryAnimationType.Y_SCALE_BIG) return 0.15f;
                else if (EntryAnimation == EntryAnimationType.Y_SCALE_VERY_BIG) return 0.35f;
                else if (EntryAnimation == EntryAnimationType.ALPHA_SLOW) return 0.35f;
                else return 0.05f;
            }

            return CustomShowDelay;
        }

        public void HideAnim(float delay = 0.0f)
        {
            //GD.PrintRich("[b]Hide Anim " + Name + "[/b]");
            MustShow = false;
            if (Active == false && Animating == false)
            {
                MustHide = false;
                return;
            }
            else if (Animating == true)
            {
                MustDelay = delay;
                MustHide = true;
                return;
            }

            Active = false;
            Animating = true;
            SceneTreeTimer timer = Tree.CreateTimer(delay);
            timer.Timeout += StartHideAnimation;
        }

        private void StartHideAnimation()
        {
            Tween tween = Tree.CreateTween();
            if (EntryAnimation == EntryAnimationType.Y_SCALE_BIG) tween.TweenProperty(this, "scale", HiddenScale, SlowTime).SetTrans(Tween.TransitionType.Linear);
            else if (EntryAnimation == EntryAnimationType.Y_SCALE_VERY_BIG) tween.TweenProperty(this, "scale", HiddenScale, VerySlowTime).SetTrans(Tween.TransitionType.Linear);
            else if (EntryAnimation == EntryAnimationType.ALPHA_SLOW) tween.TweenProperty(this, "modulate", Transparent, SlowTime).SetTrans(Tween.TransitionType.Linear);
            else tween.TweenProperty(this, "scale", HiddenScale, MediumTime).SetTrans(Tween.TransitionType.Linear); // EntryAnimationType.Y_SCALE_SMALL)
            tween.Finished += SetInvisible;
        }

        private void SetInvisible()
        {
            Animating = false;
        }

        public float GetHideDelay()
        {
            if (AutoHideDelay)
            {
                if (EntryAnimation == EntryAnimationType.Y_SCALE_BIG) return 0.15f;
                else if (EntryAnimation == EntryAnimationType.Y_SCALE_VERY_BIG) return 0.35f;
                else if (EntryAnimation == EntryAnimationType.ALPHA_SLOW) return 0.35f;
                else return 0.05f;
            }

            return CustomHideDelay;
        }

        public override void _Process(double delta)
        {
            if (KeepHiddenScale == true)
            {
                Scale = HiddenScale;
            }

            MustDelay = Mathf.Max(0.0f, MustDelay - (float)delta);

            if (MustShow == true && Animating == false && Active == false)
            {
                ShowAnim(MustDelay);
            }
            else if (MustHide == true && Animating == false && Active == true)
            {
                HideAnim(MustDelay);
            }
        }
    }
}