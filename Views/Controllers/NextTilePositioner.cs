using DG.Tweening;
using Enums;
using UnityEngine;

namespace Views.Controllers
{
    public class NextTilePositioner
    {
        private const float LerpDuration = 1.0f;

        private Vector3 _startPosition;
        public Vector3 StartPosition => _startPosition;
        private Vector3 _endPosition;
        private float _lerpValue;

        private Sequence _sequence;

        public Vector3 CurrentPosition => Vector3.Lerp(_startPosition, _endPosition, _lerpValue);

        public void SetPositionsByFrameSize(FrameSize frameSize)
        {
            _startPosition = frameSize switch
            {
                FrameSize.Small => new Vector3(0.9f, 0.5f, -0.26f),
                FrameSize.Medium => new Vector3(1.05f, 0.5f, -0.36f),
                FrameSize.Large => new Vector3(1.2f, 0.5f, -0.4f),
                _ => Vector3.zero
            };

            _endPosition = frameSize switch
            {
                FrameSize.Small => new Vector3(0.7f, 0.0f, -0.26f),
                FrameSize.Medium => new Vector3(0.85f, 0.0f, -0.36f),
                FrameSize.Large => new Vector3(1.0f, 0.0f, -0.4f),
                _ => Vector3.zero
            };
        }
        
        public void StartLerp()
        {
            _sequence?.Kill();
            _sequence = DOTween.Sequence()
                .Append(DOTween.To(() => 0.0f, value => _lerpValue = value, 1.0f, LerpDuration)
                    .SetEase(Ease.OutCubic));
        }
    }
}