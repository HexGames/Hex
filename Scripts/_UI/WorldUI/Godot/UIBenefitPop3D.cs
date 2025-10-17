using Godot;

namespace GodotUI
{
    public partial class UIBenefitPop3D : Control
    {
        [Export]
        private float PopInDuration = 0.3f;
        [Export]
        private float UpDuration = 1.5f;
        [Export]
        private float UpDistance  = 96.0f;
        [Export]
        private float FadeOutDuration = 0.5f;

        private UIText _text;

        private Tween _tween;
        private bool isDone = false;

        public override void _Ready()
        {
            _text = GetNode<UIText>("Bg/Text");
            _text.Modulate = new Color(1, 1, 1, 0);
        }

        public void Show(Data.HexCoords atHexCoord, string text)
        {
            Vector3 worldPos = Godot3D.Convert.HexCoordToWorld(atHexCoord) + 0.3f * Vector3.Up;
            Vector2 screenPos = Godot3D.Convert.WorldToScreen(worldPos);
            Position = screenPos;
            _text.SetText("$", text);

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
        }

        public void Pop()
        {
            // --- Move up --- 
            _tween.Parallel().TweenProperty(this, "position:y", Position.Y - UpDistance, UpDuration);

            // --- Fade out ---
            _tween.Parallel().TweenProperty(_text, "modulate:a", 0.0f, FadeOutDuration)
                  .SetDelay(UpDuration - FadeOutDuration);

            // Hide when done
            _tween.Finished += () =>
            {
                _text.Modulate = new Color(1, 1, 1, 0);
                isDone = true;
            };
        }

        public bool IsDone()
        {
            return isDone;
        }
    }
}
