using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Parameters.Enums;
using UnityEngine;

namespace Views.Instances.Tiles
{
    public abstract class TileBase : MonoBehaviour
    {
        private const float DurationRotation = 0.2f;
        
        [SerializeField] private Transform transformRotatable;
        
        private RotateStatus _rotateStatus;
        private float _currentRotationY;

        private TweenerCore<float, float, FloatOptions> _tweenerRotation;

        public void SetRotateStatus(RotateStatus rotateStatus)
        {
            _rotateStatus = rotateStatus;
        }

        public void RotateTile()
        {
            _tweenerRotation?.Kill();
            
            var targetRotationY = _rotateStatus switch
            {
                RotateStatus.Rotate0 => 0.0f,
                RotateStatus.Rotate90 => 90.0f,
                RotateStatus.Rotate180 => 180.0f,
                RotateStatus.Rotate270 => 270.0f,
                _ => 0.0f
            };

            for (var loopLimit = 0; loopLimit < int.MaxValue && _currentRotationY > targetRotationY; loopLimit++)
            {
                targetRotationY += 360.0f;
            }
            
            _tweenerRotation = DOTween.To(() => _currentRotationY, value =>
                {
                    _currentRotationY = value;
                }, targetRotationY, DurationRotation)
                .SetEase(Ease.OutQuad)
                .OnUpdate(() =>
                {
                    transformRotatable.localEulerAngles = new Vector3(0.0f, _currentRotationY, 0.0f);
                })
                .OnComplete(() =>
                {
                    _currentRotationY = _rotateStatus switch
                    {
                        RotateStatus.Rotate0 => 0.0f,
                        RotateStatus.Rotate90 => 90.0f,
                        RotateStatus.Rotate180 => 180.0f,
                        RotateStatus.Rotate270 => 270.0f,
                        _ => 0.0f
                    };

                    transformRotatable.localEulerAngles = new Vector3(0.0f, _currentRotationY, 0.0f);
                }).SetLink(gameObject);
        }

        public void RotateTileImmediate()
        {
            _tweenerRotation?.Kill();
            
            var rotationY = _rotateStatus switch
            {
                RotateStatus.Rotate0 => 0.0f,
                RotateStatus.Rotate90 => 90.0f,
                RotateStatus.Rotate180 => 180.0f,
                RotateStatus.Rotate270 => 270.0f,
                _ => 0.0f
            };

            transformRotatable.localEulerAngles = new Vector3(0.0f, rotationY, 0.0f);
        }
    }
}
