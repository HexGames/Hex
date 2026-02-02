using Godot;
using Hex;

namespace Hex.GodotUI
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
        [Export]
        private float _popOutScalePeak = 1.5f;
        [Export]
        private float _popOutScaleOut = 1.25f;

        [Export]
        private float _changeScaleUpDuration = 0.15f;
        [Export]
        private float _changeScaleDownDuration = 0.15f;
        [Export]
        private float _changeScalePeak = 1.3f;

        private UIText _text;

        public Data.HexPos HexPos;
        public Def.Timing Timing;

        private int _offset;
        private Vector3 _tileWorldPos;
        private Tween _tween;
        private Tween _tweenForOffset;
        private Tween _tweenForChange;
        private bool _isDone;

        public override void _Ready()
        {
            _text = GetNode<UIText>("Bg/Text");
            Modulate = new Color(1, 1, 1, 0);
        }

        /// <param name="offset">Possible offset patterns are: {0} {-1, 1} {-2, 0, 2} {-3, -1, 1, 3} ...</param>
        public void SetData(Data.HexPos hexPos, Hex.Def.Timing timing, int offset)
        {
            HexPos = hexPos;
            Timing = timing;

            _offset = offset;
            _tileWorldPos = GodotMap.Convert.HexPosToWorld(HexPos) + 0.3f * Vector3.Up;
            Position = GodotMap.Convert.WorldToScreen(_tileWorldPos) + GetOffset2D(_offset);
        }

        public void Show(string text)
        {
            Vector2 screenPos = GodotMap.Convert.WorldToScreen(_tileWorldPos) + GetOffset2D(_offset);
            Position = screenPos;
            _text.SetText("$", text);
            _text.Modulate = ColorLib.GetColor_Text(Timing);

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

            Vector2 newPosition = GodotMap.Convert.WorldToScreen(_tileWorldPos) + GetOffset2D(_offset);

            _tweenForOffset.TweenProperty(this, "position:y", newPosition.Y, _offsetDuration);
        }

        public void ChangeValue(string newText)
        {
            _text.SetText("$", newText);

            // Kill previous Change tween if any
            _tweenForChange?.Kill();
            _tweenForChange = CreateTween();
            _tweenForChange.SetTrans(Tween.TransitionType.Quad);

            // --- Scale up ---
            _tweenForChange.TweenProperty(this, "scale", Vector2.One * _changeScalePeak, _changeScaleUpDuration)
                .SetEase(Tween.EaseType.Out);

            // --- Scale down ---
            _tweenForChange.TweenProperty(this, "scale", Vector2.One, _changeScaleDownDuration)
                .SetEase(Tween.EaseType.In);
        }

        public void FadeOut()
        {
            if (_tween?.IsRunning() == true)
            {
                _tween.Finished += FadeOutAnimation;
            }
            else
            {
                FadeOutAnimation();
            }
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

        private void FadeOutAnimation()
        {
            // Kill previous tween if any
            _tween?.Kill();
            _tween = CreateTween();
            _tween.SetTrans(Tween.TransitionType.Linear);

            // --- Fade out ---
            _tween.Parallel().TweenProperty(this, "modulate:a", 0.0f, 0.5f * _fadeOutDuration); // faster fade out duration

            // Hide when done
            _tween.Finished += () =>
            {
                Modulate = new Color(1, 1, 1, 0);
                _isDone = true;
            };
        }

        private void PopAnimation()
        {
            // Kill previous tween if any
            _tween?.Kill();
            _tween = CreateTween();
            _tween.SetTrans(Tween.TransitionType.Linear);

            // --- Scale up ---
            _tween.Parallel().TweenProperty(this, "scale", Vector2.One * _popOutScalePeak, _changeScaleUpDuration)
                .SetEase(Tween.EaseType.Out);

            // --- Scale down ---
            _tween.Parallel().TweenProperty(this, "scale", Vector2.One * _popOutScaleOut, _upDuration - _fadeOutDuration - _changeScaleUpDuration)
                .SetEase(Tween.EaseType.Out)
                .SetDelay(_changeScaleUpDuration);

            // --- Move up --- 
            _tween.Parallel().TweenProperty(this, "position:y", Position.Y - _upDistance, _upDuration)
                .SetDelay(0.0f);

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
