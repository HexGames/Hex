using Godot;
using System.Collections.Generic;

namespace Godot3D
{
    public partial class BenefitPopUp : Control
    {
        public float PopInDuration = 0.3f;
        public float UpDuration = 0.7f;
        public float UpDistance  = 64.0f;
        public float FadeOutDuration = 0.5f;

        private GodotUI.UIText _Text;
        private float _Timer = 0f;
        private Vector2 _StartPosition;
        private bool _Animating = false;

        public override void _Ready()
        {
            _Text = GetNode<GodotUI.UIText>("Text");
            _Text.Modulate = new Color(1, 1, 1, 0);
            _StartPosition = Position;
        }

        public void ShowPopUp(Vector2 position, string text)
        {
            _Text.SetText("$", text);
            _Timer = 0f;
            _Animating = true;
            _Text.Scale = Vector2.One * 2.0f;
            _Text.Modulate = new Color(1, 1, 1, 0);
            Position = position;
            _StartPosition = position;
        }

        public bool IsAnimating()
        {
            return _Animating;
        }

        public override void _Process(double delta)
        {
            if (_Animating == false)
                return;

            _Timer += (float)delta;

            if (_Timer < PopInDuration)
            {
                // Pop in with bounce
                float t = _Timer / PopInDuration;
                float scale = 1.2f - 0.2f * Mathf.Cos(Mathf.Pi * t); // bounce effect
                _Text.Scale = new Vector2(scale, scale) * 2.0f;
                _Text.Modulate = new Color(1, 1, 1, t);
            }
            else if (_Timer < PopInDuration + UpDuration)
            {
                // Move up and stay visible
                float t = (_Timer - PopInDuration) / UpDuration;
                Position = _StartPosition + new Vector2(0, -UpDistance * t);
                _Text.Scale = Vector2.One * 2.0f;
                _Text.Modulate = new Color(1, 1, 1, 1);
            }
            else if (_Timer < PopInDuration + UpDuration + FadeOutDuration)
            {
                // Fade out while moving up
                float t = (_Timer - PopInDuration - UpDuration) / FadeOutDuration;
                Position = _StartPosition + new Vector2(0, -UpDistance);
                _Text.Modulate = new Color(1, 1, 1, 1 - t);
            }
            else
            {
                // Animation done
                _Text.Modulate = new Color(1, 1, 1, 0);
                _Animating = false;
            }
        }
    }
}
