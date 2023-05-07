using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Views
{
    public class CurrentTilePositionDetector : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private LayerMask layerMask;

        private readonly Subject<Vector3> _subjectOnDetected = new();
        public IObservable<Vector3> OnDetected => _subjectOnDetected;

        private void Awake()
        {
            this.UpdateAsObservable()
                .Subscribe(_ =>
                {
                    if (Mouse.current == null)
                    {
                        return;
                    }

                    var ray = mainCamera.ScreenPointToRay(Mouse.current.position.value);

                    if (Physics.Raycast(ray, out var hit, float.PositiveInfinity, layerMask))
                    {
                        _subjectOnDetected.OnNext(hit.point);
                    }
                }).AddTo(gameObject);
        }
    }
}
