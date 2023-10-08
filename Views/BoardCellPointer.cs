using Enums;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Views
{
    [RequireComponent(typeof(MeshRenderer))]
    public class BoardCellPointer : MonoBehaviour
    {
        [SerializeField] private MeshRenderer meshRenderer;

        [SerializeField] private float tileSize;
        [SerializeField] private Vector3 basePositionSmall;
        [SerializeField] private Vector3 basePositionMedium;
        [SerializeField] private Vector3 basePositionLarge;
        
        private FrameSize _frameSize;
        private Vector2Int? _nullableCellPosition;
        
        private void Reset()
        {
            meshRenderer = GetComponent<MeshRenderer>();
        }

        private void Awake()
        {
            this.UpdateAsObservable()
                .Subscribe(_ =>
                {
                    if (_nullableCellPosition.HasValue)
                    {
                        var cellPosition = _nullableCellPosition.Value;
                        transform.position =
                            new Vector3(cellPosition.x * tileSize, 0.0f, cellPosition.y * tileSize) +
                            _frameSize switch
                            {
                                FrameSize.Small => basePositionSmall,
                                FrameSize.Medium => basePositionMedium,
                                FrameSize.Large => basePositionLarge,
                                _ => Vector3.zero
                            };
                        meshRenderer.enabled = true;
                    }
                    else
                    {
                        meshRenderer.enabled = false;
                    }
                }).AddTo(gameObject);
        }

        public void SetFrameSize(FrameSize frameSize)
        {
            _frameSize = frameSize;
        }

        public void SetNullableCellPosition(Vector2Int? nullableCellPosition)
        {
            _nullableCellPosition = nullableCellPosition;
        }
    }
}
