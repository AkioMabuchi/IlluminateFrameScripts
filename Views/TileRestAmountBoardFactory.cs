using Enums;
using UnityEngine;
using Views.Instances;

namespace Views
{
    public class TileRestAmountBoardFactory : MonoBehaviour
    {
        [SerializeField] private NumberDisplayBoard prefabTileRestAmountBoard;
        
        [SerializeField] private Vector3 generatePositionSmall;
        [SerializeField] private Vector3 generatePositionMedium;
        [SerializeField] private Vector3 generatePositionLarge;
        
        private NumberDisplayBoard _tileRestAmountBoard;
        public NumberDisplayBoard TileRestAmountBoard => _tileRestAmountBoard;

        public void GenerateTileRestAmountBoard(FrameSize frameSize)
        {
            var position = frameSize switch
            {
                FrameSize.Small => generatePositionSmall,
                FrameSize.Medium => generatePositionMedium,
                FrameSize.Large => generatePositionLarge,
                _ => Vector3.zero
            };

            var rotation = frameSize switch
            {
                _ => Quaternion.identity
            };

            _tileRestAmountBoard = Instantiate(prefabTileRestAmountBoard, position, rotation, transform);
        }
    }
}
