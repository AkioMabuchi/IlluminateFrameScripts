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

                    var plane = new Plane(Vector3.up, Vector3.zero);

                    var ray = mainCamera.ScreenPointToRay(Mouse.current.position.value);

                    if (plane.Raycast(ray, out var enter))
                    {
                        _subjectOnDetected.OnNext(ray.GetPoint(enter));
                    }
                }).AddTo(gameObject);
        }
    }
}
