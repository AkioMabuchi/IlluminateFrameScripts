using DG.Tweening;
using UnityEngine;

namespace Views.Controllers
{
    public class CurrentTilePositioner
    {
        private const float LerpDuration = 1.0f;
        
        private Vector3 _startPosition;
        private Vector3 _endPosition;
        private float _lerpValue;

        private Sequence _sequence;

        public Vector3 CurrentPosition => Vector3.Lerp(_startPosition, _endPosition, _lerpValue);
        
        public void SetStartPosition(Vector3 startPosition)
        {
            _startPosition = startPosition;
        }

        public void SetEndPosition(Vector3 endPosition)
        {
            _endPosition = endPosition;
        }

        public void StartLerp()
        {
            _sequence?.Kill();
            _sequence = DOTween.Sequence()
                .Append(DOTween.To(() => 0.0f, value => _lerpValue = value, 1.0f, LerpDuration)
                    .SetEase(Ease.OutQuint));
        }
    }
}