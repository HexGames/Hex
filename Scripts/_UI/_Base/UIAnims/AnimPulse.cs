using Godot;
using Godot.Collections;
using System.Collections.Generic;

namespace GodotUI
{
    public partial class AnimPulse : Control
    {
        [Export] private float PulseScale = 1.2f;
        [Export] private float PulseTime = 0.2f;

        private Vector2 DefaultScale;
        private SceneTree Tree;

        private int _CurrentNrOfPulses = 0;
        private Queue<int> _pulseCalls = new Queue<int>();

        public override void _Ready()
        {
            Tree = GetTree();

            CallDeferred("InitBase");
        }

        private void InitBase()
        {
            DefaultScale = Scale;
            PivotOffset = Size / 2;
        }

        public void PulseAnim(int numberOfPulses)
        {
            if (_CurrentNrOfPulses > 0)
            {
                _pulseCalls.Enqueue(numberOfPulses);
                return;
            }

            _CurrentNrOfPulses = numberOfPulses;
            PulseTime = 0.01f * Mathf.Pow(0.9f, 0.02f * numberOfPulses) + 0.05f * Mathf.Pow(0.9f, 0.5f * numberOfPulses) + Mathf.Pow(0.2f, numberOfPulses); // Decrease time with more pulses

            PulseAnim();
        }

        private void PulseAnim()
        {
            Tween tween = Tree.CreateTween();
            tween.TweenProperty(this, "scale", DefaultScale * PulseScale, PulseTime).SetTrans(Tween.TransitionType.Linear);
            tween.Finished += PulseReturnAnim;
        }

        private void PulseReturnAnim()
        {
            Tween tween = Tree.CreateTween();
            tween.TweenProperty(this, "scale", DefaultScale, PulseTime).SetTrans(Tween.TransitionType.Linear); 
            tween.Finished += ResetAnim;
        }

        private void ResetAnim()
        {
            _CurrentNrOfPulses--;

            if (_CurrentNrOfPulses > 0)
            {
                PulseAnim();
                return;
            }

            if (_pulseCalls.Count > 0)
            {
                PulseAnim(_pulseCalls.Dequeue());
                return;
            }
        }
    }
}