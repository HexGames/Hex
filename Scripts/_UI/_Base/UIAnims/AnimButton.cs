using Godot;
using Godot.Collections;
using System.Collections.Generic;

namespace GodotUI
{
    public partial class AnimButton : AnimBase
    {
        [Signal]
        public delegate void LeftClickEventHandler();

        [Export] public Control[] HoverOverlays;
        [Export] public Control[] PressedOverlays;
        [Export] public Control[] ActionOverlays;
        [Export] public Control Offset;

        private Vector2 DefaultOffest;

        private readonly Vector2 HoverScale = new Vector2(1.1f, 1.1f);
        private readonly Vector2 ClickScale = new Vector2(0.9f, 0.9f);

        private bool Hovering = false;
        private bool Clicking = false;

        public override void _Ready()
        {
            base._Ready();

            //Offset = GetChild<Control>(1);
            //PressedOverlays.Add(Offset.GetChild(0).GetChild<Control>(1));
            //ActionOverlays.Add(Offset.GetChild(0).GetChild<Control>(2));
            //HoverOverlays.Add(Offset.GetChild(1).GetChild<Control>(1));
            //HoverOverlays.Add(Offset.GetChild(2).GetChild<Control>(1));
            //PressedOverlays.Add(Offset.GetChild(2).GetChild<Control>(2));

            Tree = GetTree();

            MouseEntered += OnHover;
            MouseExited += OnDehover;
            GuiInput += OnButtonInput;

            CallDeferred("Init");
        }

        private void Init()
        {
            DefaultOffest = Offset.Position;
            PivotOffset = Size / 2;
        }
        protected override void OnSetActive()
        {
            if (GetRect().HasPoint(GetLocalMousePosition())) OnHover();
        }

        private void OnHover()
        {
            if (base.Active == false) return;

            Hovering = true;

            // scale
            {
                Tween tween = Tree.CreateTween();
                tween.TweenProperty(this, "scale", HoverScale, FastTime).SetTrans(Tween.TransitionType.Sine).SetEase(Tween.EaseType.Out);
            }

            foreach (var overlay in HoverOverlays)
            {
                Tween tween = Tree.CreateTween();
                tween.TweenProperty(overlay, "anchor_right", 1.0f, SlowTime).SetTrans(Tween.TransitionType.Linear);
            }
        }

        private void OnDehover()
        {
            if (base.Active == false) return;

            Hovering = false;

            if (Clicking)
            {
                OnButtonUp();
                Clicking = false;
            }

            // scale
            {
                Tween tween = Tree.CreateTween();
                tween.TweenProperty(this, "scale", DefaultScale, FastTime).SetTrans(Tween.TransitionType.Sine).SetEase(Tween.EaseType.Out);
            }

            foreach (var overlay in HoverOverlays)
            {
                Tween tween = Tree.CreateTween();
                tween.TweenProperty(overlay, "anchor_right", 0.0f, SlowTime).SetTrans(Tween.TransitionType.Linear);
            }
        }

        private void OnButtonInput(InputEvent inputEvent)
        {
            if (base.Active == false) return;

            if (inputEvent is InputEventMouseButton mouseButtonEvent)
            {
                if (mouseButtonEvent.IsPressed())
                {
                    if (mouseButtonEvent.ButtonIndex == MouseButton.Left && Hovering)
                    {
                        if (Hovering == true)
                        {
                            OnButtonDown();
                        }
                    }
                }
                else if (mouseButtonEvent.IsReleased())
                {
                    if (mouseButtonEvent.ButtonIndex == MouseButton.Left && Clicking == true)
                    {
                        if (Hovering == true)
                        {
                            OnActionStart();
                        }
                        OnButtonUp();
                        Clicking = false;
                    }
                }
            }
        }

        private void OnButtonDown()
        {
            Clicking = true;

            // position
            {
                Tween tween = Tree.CreateTween();
                tween.TweenProperty(Offset, "position", Vector2.Zero, FastTime).SetTrans(Tween.TransitionType.Sine).SetEase(Tween.EaseType.Out);
            }

            foreach (var overlay in PressedOverlays)
            {
                Tween tween = Tree.CreateTween();
                tween.TweenProperty(overlay, "anchor_bottom", 1.0f, MediumTime).SetTrans(Tween.TransitionType.Linear);
            }
        }

        private void OnButtonUp()
        {
            Clicking = true;

            // position
            {
                Tween tween = Tree.CreateTween();
                tween.TweenProperty(Offset, "position", DefaultOffest, FastTime).SetTrans(Tween.TransitionType.Sine).SetEase(Tween.EaseType.Out);
            }

            foreach (var overlay in PressedOverlays)
            {
                Tween tween = Tree.CreateTween();
                tween.TweenProperty(overlay, "anchor_bottom", 0.0f, MediumTime).SetTrans(Tween.TransitionType.Linear);
            }
        }

        private void OnActionStart()
        {
            SceneTreeTimer timer = Tree.CreateTimer(MediumTime);
            timer.Timeout += OnAction;
            foreach (var overlay in ActionOverlays)
            {
                Tween tween = Tree.CreateTween();
                tween.TweenProperty(overlay, "color", White, FastTime).SetTrans(Tween.TransitionType.Linear);
            }
        }

        private void OnAction()
        {
            EmitSignal("LeftClick");

            foreach (var overlay in ActionOverlays)
            {
                Tween tween = Tree.CreateTween();
                tween.TweenProperty(overlay, "color", Transparent, SlowTime).SetTrans(Tween.TransitionType.Linear);
            }
        }
    }
}