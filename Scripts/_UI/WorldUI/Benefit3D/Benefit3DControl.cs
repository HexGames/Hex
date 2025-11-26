using Godot;

namespace GodotUI
{
    public partial class Benefit3DControl : Control
    {
        [Export]
        private float _popInDuration = 0.3f;
        [Export]
        private float _upDuration = 1.5f;
        [Export]
        private float _upDistance  = 96.0f;
        [Export]
        private float _fadeOutDuration = 0.5f;
        [Export]
        private float _offsetDuration = 0.15f;

        private UIText _text;

        private Data.HexPos _hexPos;
        private Def.Timing _timing;

        private int _offset;
        private Vector3 _tileWorldPos;
        private Tween _tween;
        private Tween _tweenForOffset;
        private bool _isDone;

        public Def.Timing Timing => _timing;

        public override void _Ready()
        {
            _text = GetNode<UIText>("Bg/Text");
            Modulate = new Color(1, 1, 1, 0);
        }

        /// <param name="offset">Possible offset patterns are: {0} {-1, 1} {-2, 0, 2} {-3, -1, 1, 3} ...</param>
        public void SetData(Data.HexPos hexPos, Def.Timing timing, string text, int offset)
        {
            _hexPos = hexPos;
            _timing = timing;

            _offset = offset;
            _tileWorldPos = Godot3D.Convert.HexPosToWorld(_hexPos) + 0.3f * Vector3.Up;
            Position = Godot3D.Convert.WorldToScreen(_tileWorldPos) + GetOffset2D(_offset);
            _text.SetText("$", text);
            _text.Modulate = ColorLib.GetColor_Text(_timing);
        }

        public void Show(Data.HexPos atHexPos, string text)
        {
            Vector3 worldPos = Godot3D.Convert.HexPosToWorld(atHexPos) + 0.3f * Vector3.Up;
            Vector2 screenPos = Godot3D.Convert.WorldToScreen(worldPos);
            Position = screenPos;
            _text.SetText("$", text);

            // Reset state
            Scale = Vector2.One * 2.0f;
            Modulate = new Color(1, 1, 1, 0);

            // Kill previous tween if any
            _tween?.Kill();
            _tween = CreateTween();
            _tween.SetTrans(Tween.TransitionType.Linear);
            //_tween.SetEase(Tween.EaseType.Out);

            // --- Pop in (scale + fade in) ---
            _tween.Parallel().TweenProperty(this, "modulate:a", 1.0f, 0.8 * _popInDuration)
                  .From(0.0f);

            _tween.Parallel().TweenProperty(this, "scale", Vector2.One * 1.25f, 0.8 * _popInDuration)
                  .From(Vector2.One); // overshoot bounce

            _tween.Parallel().TweenProperty(this, "scale", Vector2.One * 1.0f, 0.2 * _popInDuration)
                .SetDelay(0.8 * _popInDuration); // overshoot bounce
        }

        public void MoveToOffset(int newOffset)
        {
            _offset = newOffset;

            _tweenForOffset?.Kill();
            _tweenForOffset = CreateTween();
            _tweenForOffset.SetTrans(Tween.TransitionType.Linear);

            Vector2 newPosition = Godot3D.Convert.WorldToScreen(_tileWorldPos) + GetOffset2D(_offset);

            _tweenForOffset.TweenProperty(this, "position:y", newPosition.Y, _offsetDuration);
        }

        public void Refresh(string text)
        {
            _text.SetText("$", text);
        }

        public void Pop()
        {
            if (_tween?.IsRunning() == true)
            {
                _tween.Finished += PopAnimation;
            }
            else
            {
                PopAnimation();
            }
        }

        private void PopAnimation()
        { 
            // Kill previous tween if any
            _tween?.Kill();
            _tween = CreateTween();
            _tween.SetTrans(Tween.TransitionType.Linear);

            // --- Move up --- 
            _tween.Parallel().TweenProperty(this, "position:y", Position.Y - _upDistance, _upDuration);

            // --- Fade out ---
            _tween.Parallel().TweenProperty(this, "modulate:a", 0.0f, _fadeOutDuration)
                  .SetDelay(_upDuration - _fadeOutDuration);

            // Hide when done
            _tween.Finished += () =>
            {
                Modulate = new Color(1, 1, 1, 0);
                _isDone = true;
            };
        }

        public bool IsDone()
        {
            return _isDone;
        }


        // ----------------------------------------------------------------- Helpers
        private Vector2 GetOffset2D(int offset) => offset * Vector2.Up * 10.0f;
    }
}
