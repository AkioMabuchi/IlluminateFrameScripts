using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.InputSystem;
using Views.Instances;

namespace Views
{
    public class PanelCellDetector : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private LayerMask layerMask;

        private readonly Subject<Vector2Int> _subjectOnDetectPanelCell = new();
        public IObservable<Vector2Int> OnDetectPanelCell => _subjectOnDetectPanelCell;
        private readonly Subject<Unit> _subjectOnDetectNone = new();
        public IObservable<Unit> OnDetectNone => _subjectOnDetectNone;
        private void Awake()
        {
            this.UpdateAsObservable()
                .Subscribe(_ =>
                {
                    if (Mouse.current != null)
                    {
                        var ray = mainCamera.ScreenPointToRay(Mouse.current.position.value);
                        
                        if (Physics.Raycast(ray, out var hit, float.PositiveInfinity, layerMask))
                        {
                            if (hit.collider.TryGetComponent(out PanelCellDetectTarget target))
                            {
                                _subjectOnDetectPanelCell.OnNext(target.CellPosition);
                                return;
                            }
                        }
                    }
                    
                    _subjectOnDetectNone.OnNext(Unit.Default);
                }).AddTo(gameObject);
        }
    }
}
