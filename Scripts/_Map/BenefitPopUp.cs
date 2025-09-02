using Godot;
using System.Collections.Generic;

namespace Godot3D
{
    public partial class BenefitPopUp : Control
    {
        public float PopInDuration = 0.3f;
        public float UpDuration = 1.5f;
        public float UpDistance  = 96.0f;
        public float FadeOutDuration = 0.5f;

        private GodotUI.UIText _text;
        //private float _Timer = 0f;
        //private Vector2 _StartPosition;
        private bool _animating = false;
        private Tween _tween;

        public override void _Ready()
        {
            _text = GetNode<GodotUI.UIText>("Text");
            _text.Modulate = new Color(1, 1, 1, 0);
            //_StartPosition = Position;
        }

        //public void ShowPopUp(Vector2 position, string text)
        //{
        //    _Text.SetText("$", text);
        //    _Timer = 0f;
        //    _Animating = true;
        //    _Text.Scale = Vector2.One * 2.0f;
        //    _Text.Modulate = new Color(1, 1, 1, 0);
        //    Position = position;
        //    _StartPosition = position;
        //}

        public void ShowPopUp(Vector2 position, string text)
        {
            Position = position;
            _text.SetText("$", text);
            _animating = true;

            // Reset state
            _text.Scale = Vector2.One * 2.0f;
            _text.Modulate = new Color(1, 1, 1, 0);

            // Kill previous tween if any
            _tween?.Kill();
            _tween = CreateTween();
            _tween.SetTrans(Tween.TransitionType.Linear);
            //_tween.SetEase(Tween.EaseType.Out);

            // --- Pop in (scale + fade in) ---
            _tween.Parallel().TweenProperty(_text, "modulate:a", 1.0f, 0.8 * PopInDuration)
                  .From(0.0f);

            _tween.Parallel().TweenProperty(_text, "scale", Vector2.One * 2.5f, 0.8 * PopInDuration)
                  .From(Vector2.One); // overshoot bounce

            _tween.Parallel().TweenProperty(_text, "scale", Vector2.One * 2.0f, 0.2 * PopInDuration)
                .SetDelay(0.8 * PopInDuration); // overshoot bounce

            // --- Move up --- 
            _tween.Parallel().TweenProperty(this, "position:y", position.Y - UpDistance, UpDuration);

            // --- Fade out ---
            _tween.Parallel().TweenProperty(_text, "modulate:a", 0.0f, FadeOutDuration)
                  .SetDelay(UpDuration - FadeOutDuration);

            // Hide when done
            _tween.Finished += () =>
            {
                _animating = false;
                _text.Modulate = new Color(1, 1, 1, 0);
            };
        }

        private void AnimationEnding()
        {
            _animating = false;
        }

        public bool IsAnimating()
        {
            return _animating;
        }

        //public override void _Process(double delta)
        //{
        //    if (_Animating == false)
        //        return;
        //
        //    _Timer += (float)delta;
        //
        //    if (_Timer < PopInDuration)
        //    {
        //        // Pop in with bounce
        //        float t = _Timer / PopInDuration;
        //        float scale = 1.2f - 0.2f * Mathf.Cos(Mathf.Pi * t); // bounce effect
        //        _Text.Scale = new Vector2(scale, scale) * 2.0f;
        //        _Text.Modulate = new Color(1, 1, 1, t);
        //    }
        //    else if (_Timer < PopInDuration + UpDuration)
        //    {
        //        // Move up and stay visible
        //        float t = (_Timer - PopInDuration) / UpDuration;
        //        Position = _StartPosition + new Vector2(0, -UpDistance * t);
        //        _Text.Scale = Vector2.One * 2.0f;
        //        _Text.Modulate = new Color(1, 1, 1, 1);
        //    }
        //    else if (_Timer < PopInDuration + UpDuration + FadeOutDuration)
        //    {
        //        // Fade out while moving up
        //        float t = (_Timer - PopInDuration - UpDuration) / FadeOutDuration;
        //        Position = _StartPosition + new Vector2(0, -UpDistance);
        //        _Text.Modulate = new Color(1, 1, 1, 1 - t);
        //    }
        //    else
        //    {
        //        // Animation done
        //        _Text.Modulate = new Color(1, 1, 1, 0);
        //        _Animating = false;
        //    }
        //}
    }
}
