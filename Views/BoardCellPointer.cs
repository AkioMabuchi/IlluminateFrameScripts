using Classes.Statics;
using Enums;
using UnityEngine;

namespace Views
{
    [RequireComponent(typeof(MeshRenderer))]
    public class BoardCellPointer : MonoBehaviour
    {
        [SerializeField] private MeshRenderer meshRenderer;

        [SerializeField] private Vector3 basePositionSmall;
        [SerializeField] private Vector3 basePositionMedium;
        [SerializeField] private Vector3 basePositionLarge;
        
        private Vector3 _currentBasePosition = Vector3.zero;
        
        private void Reset()
        {
            meshRenderer = GetComponent<MeshRenderer>();
        }

        public void SetBasePosition(FrameSize frameSize)
        {
            _currentBasePosition = frameSize switch
            {
                FrameSize.Small => basePositionSmall,
                FrameSize.Medium => basePositionMedium,
                FrameSize.Large => basePositionLarge,
                _ => Vector3.zero
            };
        }
        public void EnableMeshRenderer(bool isEnabled)
        {
            meshRenderer.enabled = isEnabled;
        }

        public void MoveToCellPosition(Vector2 cellPosition)
        {
            transform.position =
                new Vector3(cellPosition.x * Const.TileSize, 0.0f, cellPosition.y * Const.TileSize) +
                _currentBasePosition;
        }
    }
}
